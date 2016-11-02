var BaseMVC = angular.module('BaseMVC');
BaseMVC.controller('EjecucionCtrl', ['$http', '$scope', '$timeout', '$mdDialog', 'Upload', function ($http, $scope, $timeout, $mdDialog, $upload) {

    //VARIABLES GLOBALES
    
    $scope.OnServer = false; // Muestra Preloader cuando esta en el servidor
    $scope.promise = $timeout(function () { 
        // loading
    }, 2000);//Barra de la tabla, velocidad con la que carga
    $scope.regexRFC = /(^([a-z]{3,4})-?([0-9]{6})-?([a-z0-9]{3,4})$)*/i;
    $scope.regexTelefono = /^([0-9](-?|\ )){10}$/;
    $scope.regexEmail = /(^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$)*/;
    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
        };
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    }

    $scope.MENUS = {
        ACTIVIDADES: 1,
        INCIDENTES: 2
    };
    $scope.MENU = $scope.MENUS.ACTIVIDADES;

  
    $scope.Obligatorio = function (Atributo) {
        return ($scope.ErrorFormulario && Atributo == '') ? 'alert-danger' : '';
    };
    $scope.RFCValido = function (Atributo) {
        return (!$scope.regexRFC.test(Atributo) && $scope.ErrorFormulario) ? 'alert-danger' : '';
    };
    $scope.TelefonoValido = function (Atributo) {
        return (!$scope.regexTelefono.test(Atributo) && $scope.ErrorFormulario) ? 'alert-danger' : '';
    };
    $scope.EmailValido = function (Atributo) {
        return (!$scope.regexEmail.test(Atributo) && $scope.ErrorFormulario) ? 'alert-danger' : '';
    };
    //*******************************************************************************************************************

    //HERRAMIENTAS GLOBALES
    
    $scope.MsgErrorHTML = function (Cabecero, Mensaje, IdDiv) {
        return "<div class='col-md-10 col-md-offset-1 alert alert-danger'>" +
                    "<button type='button' class='close' onclick='QuitarMsgErr(" + IdDiv + ")' aria-hidden='true'>X</button>" +
                        "<h4>" + Cabecero + "</h4>" +
                        "<p>" + Mensaje + "</p></div>";
    };

    //*********************************************************************************************************************

    //**********************************************************************************************************************************************************************
    //METODOS ACTIVIDADES//Altas, bajas y cambios ACTIVIDADES
    var HOY = new Date(); //Hoy
    var OLDMONTH = new Date(new Date(HOY).setMonth(HOY.getMonth() - 1)); // un mes atras
    $scope.FILTROACTIVIDADES = { //Inicializamos la fecha de las actividades
        dFechaInicio: OLDMONTH,
        dFechaFin: HOY,
        filtro: ''
    };
    $scope.lstActividades = [];
    //Actividades
    $scope.LimpiarFiltroActividades = function () {
        $scope.FILTROACTIVIDADES = {
            dFechaInicio: OLDMONTH,
            dFechaFin: HOY,
            filtro: ''
        };
        $('.datepickerFin').datepicker('update', HOY);
        $('.datepickerInicio').datepicker('update', OLDMONTH);
    }; $scope.LimpiarFiltroActividades();

    $scope.ActividadesSum = function (row) {
        var res = 0;

        var calculateChildren = function (cur) {
            var res = 0;
            angular.forEach(cur.children, function (a) {
                res += 1;
            });
            return res;
        };

        var calculateAggChildren = function (cur) {
            var res = 0;
            res += calculateChildren(cur);
            angular.forEach(cur.aggChildren, function (a) {
                res += calculateAggChildren(a);
            });
            return res;
        };

        return (calculateAggChildren(row));
    };

    //opciones de Grid
    //Declaracion de parametros de grid
    $scope.filterOptions= 
    {filterText: '', 
        useExternalFilter: false
    };
    $scope.gridActividades = {
        data: 'lstActividades',
        showGroupPanel: true,
        jqueryUIDraggable: true,
        showFooter: true,
        enableFiltering: false,
        filterOptions: $scope.filterOptions,
        aggregateTemplate: '<div ng-click="row.toggleExpand()" " ng-style="rowStyle(row)" class="ngAggregate"> <span class="ngAggregateText">{{row.label CUSTOM_FILTERS}} (Total Consultas: {{ActividadesSum(row)}})</span> <div class="{{row.aggClass()}}"></div> </div>',
    };


    $scope.LoadActividades = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerActividades_Grid',
            data: {
                nFechaInicial: FechaJuliana($scope.FILTROACTIVIDADES.dFechaInicio),
                nFechaFinal: FechaJuliana($scope.FILTROACTIVIDADES.dFechaFin)
            }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstActividades = response.Actividades_Grid;
            }
        }).error(function (response, status, header, config) {
        });
    };

    $scope.LoadActividadesInActivas = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerActividadesInActivas_Grid',
            data: {
                nFechaInicial: FechaJuliana($scope.FILTROACTIVIDADES.dFechaInicio),
                nFechaFinal: FechaJuliana($scope.FILTROACTIVIDADES.dFechaFin)
            }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstActividades = response.Actividades_Grid;
            }
        }).error(function (response, status, header, config) {
        });
    };


    //////////////////|Productos|//////////////////////////
    $scope.lstProductos = [];
    $scope.LoadProductos = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerProductos',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstProductos = response.Productos;
                $scope.selProductos = $scope.Productos;
            }
        }).error(function (response, status, header, config) {
        });
    };

    $scope.lstModulos = [];
    $scope.LoadModulos = function (idProducto) {
  
            $http({
                method: 'POST',
                url: '/Ejecucion/Incidentes/ObtenerModulos',
                data: { idProducto: idProducto }
            }).success(function (response, status, header, config) {
                if (response.bError) {
                    swal('Error', response.msgErr, 'error');
                }
                else {
                    $scope.lstModulos = response.Modulos;
                    $scope.selModulos = $scope.lstModulos;
                }
            }).error(function (response, status, header, config) {
            });
        
    };
    /////////////////////////////////////////////
    $scope.lstComponentes = [];
    $scope.LoadComponentes = function (idModulo) {

        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerComponentes',
            data: { idModulo: idModulo }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstComponentes = response.Componentes;
                $scope.selComponentes = $scope.lstComponentes;
            }
        }).error(function (response, status, header, config) {
        });

    };

    $scope. limpiarForm= function() {
        $scope.selModulos = '';
        $scope.selProductos = '';
        $scope.selComponentes = '';
    }

    //*******************************************************************************************************************
    $scope.ObtenerActividad = function (nActividad) {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerActividad',
            data: { nActividad: nActividad }
        }).success(function (data, status, header, config) {
            if (data.bError) {
                swal('Error', data.msgErr, 'error');
            }
            else {
                $scope.ACTIVIDAD = data.Actividad;
            }
        }).error(function (data, status, header, config) {
        });
    };
    $scope.GuardarActividad = function () {
        $scope.OnServer = true;
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/GuardarActividad',
            data: { Actividad: $scope.ACTIVIDAD }
        }).success(function (data, status, header, config) {
            $scope.OnServer = false;
            if (data.bError) {
                $("#Actividad_msgerr").html($scope.MsgErrorHTML('Error...', data.msgErr, '"Actividad_msgerr"'));
            }
            else {
                $scope.LimpiarActividad();
                $scope.ObtenerActividad();
                $("#RegistroDiagnosticoModal").modal('hide');
                swal('Listo!', 'Actividad registrado');
            }
        }).error(function (data, status, header, config) {
        });
    };
    $scope.NuevoIncidenteModal = function () {
        $("#RegistroIncidenteModal").modal('show');
    };
   
    //*******************************************************************************************************************



}]);

var Hidden = false;
function ChangeMenuAside() {
    Hidden = !Hidden;
    if (Hidden) {
        $('#menuHide').hide();
        $('#menuShow').show();
        $('.TablaDivResize').switchClass("col-md-11", "col-md-12");
    }
    else {
        $('#menuHide').show();
        $('#menuShow').hide();
        $('.TablaDivResize').switchClass("col-md-12", "col-md-11");
    }
}

function CambiarVista(vista) {
    var scope = $('#V_Catalogos').scope();
    scope.MENU = vista;
    scope.$apply();
}

function FechaJuliana(fecha) {
    //Mes, dia, año
    var objFecha = fecha;
    if (fecha.getMonth == undefined) {
        var mes = fecha.split('/')[1];
        var dia = fecha.split('/')[0];
        var year = fecha.split('/')[2];
        objFecha = new Date(year, mes, 0);
    }
    return Math.floor((objFecha / 86400000) - (objFecha.getTimezoneOffset() / 1440) + 2440587.5);
};