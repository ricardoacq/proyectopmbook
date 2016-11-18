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
    //***********************************************************************************************************************
    $scope.MENUS = {
        ACTIVIDADES: 1,
        INCIDENTES: 2
    };
    $scope.MENU = $scope.MENUS.ACTIVIDADES;
    //***********************************************************************************************************************
  
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
        filtro: '',
    };
    $scope.FechaReporte = $scope.FILTROACTIVIDADES.dFechaFin;
    //Actividades
    $scope.lstActividades = [];
    $scope.LimpiarFiltroActividades = function () {
        $scope.FILTROACTIVIDADES = {
            dFechaInicio: OLDMONTH,
            dFechaFin: HOY,
            filtro: '',
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

    //Declaracion de parametros de grid
    $scope.filterOptions =
    {
        filterText: '',
        useExternalFilter: false
    };
    $scope.btnReporte = '<button id="editBtn" type="button" class="btn btn-primary" ng-click="ConsultarReporte(row)"style="border:0; background-color:transparent;" title="Reportar Avance de Actividad"><i class="fa fa-file-text-o" style="color:#616f71"></i></button>';
    $scope.btnFav     = '<button id="Favorito" type="button" class="btn btn-primary" style="border:0; background-color:transparent;" title="Agregar a Favoritos"><i class="fa fa-star" style="color:#616f71"></i></button>';
    $scope.btnTer = '<button id="Terminado" type="button" class="btn btn-primary" style="border:0; background-color:transparent;" title="Marcar como terminada la actividad"><i class="fa fa-unlock-alt" style="color:#616f71"></i></button>';
    //***********************************************************************************************************************
    //opciones de Grid
    $scope.gridActividades = {
        data: 'lstActividades',
        showGroupPanel: true,
        jqueryUIDraggable: true,
        showFooter: true,
        enableFiltering: false,
        columnDefs: [{ field: 'ID', displayName: 'id' }, { field: 'Proyecto', displayName: 'Proyecto',width: 100 }, { field: 'Modulo', displayName: 'Modulo' },
            {field:'Componente',displayName:'Componente'},{field:'Actividad',displayName:'Actividad'},{field:'Tiempoautorizado',displayName:'Tiempo Autorizado'},
            {field:'TrabajoRealizado',displayName:'Trabajo Realizado'},{field:'TrabajoRestante',displayName:'Trabajo Restante'},{field:'Avance',displayName:'Avance'},
            {field:'Vuelta',displayName:'Vuelta'},{field:'Estatus',displayName:'Estatus'},{field:'Consultor',displayName:'Consultor'},
            { field: 'FechaRegistro', displayName: 'Fecha Registro' },{ displayName: 'Editar', cellTemplate: $scope.btnReporte+$scope.btnFav+$scope.btnTer }],
        filterOptions: $scope.filterOptions,
        selectedItems: $scope.mySelections,
        multiSelect: false,
        aggregateTemplate: '<div ng-click="row.toggleExpand()" " ng-style="rowStyle(row)" class="ngAggregate"> <span class="ngAggregateText">{{row.label CUSTOM_FILTERS}} (Total Consultas: {{ActividadesSum(row)}})</span> <div class="{{row.aggClass()}}"></div> </div>',
    };
    //***********************************************************************************************************************
    //divide las horas y minutos
    $scope.ConsultarReporte = function (row) {
        $scope.Act = row.entity;
        $scope.Comentario = '';
        $scope.dFecha = $scope.FILTROACTIVIDADES.dFechaFin;
        $scope.Accion = '';
        $scope.cadena = $scope.Act.TrabajoRealizado.split(" ", 4);
        $scope.Horas = parseInt($scope.cadena[0]);
        $scope.Minutos = parseInt($scope.cadena[2]);
        $scope.cadena2 = $scope.Act.TrabajoRestante.split(" ", 4);
        $scope.HorasRes = parseInt($scope.cadena2[0]);
        $scope.MinutosRes = parseInt($scope.cadena2[2]);
        $scope.LoadAcciones();
        $("#ReporteActividadModal").modal('show');
    };

    ///INCREMENTAR HORAS Y MINUTOS
    $scope.IncrementaHoras = function () {
        $scope.Horas = $scope.Horas + 1;
    };
    $scope.IncrementaMinutos = function () {
        if ($scope.Minutos === 55) {
            $scope.IncrementaHoras();
            $scope.Minutos = 0;
        } else {
            $scope.Minutos = $scope.Minutos+5;
        }
    };
    $scope.IncrementaHorasRes = function () {
        $scope.HorasRes = $scope.HorasRes + 1;
    };
    $scope.IncrementaMinutosRes = function () {
        if ($scope.MinutosRes === 55) {
            $scope.IncrementaHorasRes();
            $scope.MinutosRes = 0;
        } else {
            $scope.MinutosRes = $scope.MinutosRes + 5;
        }
    };
    //***********************************************************************************************************************
    ///DECREMENTAR HORAS Y MINUTOS
    $scope.DecrementaHoras = function () {
        $scope.Horas = $scope.Horas - 1;
    };
    $scope.DecrementaMinutos = function () {
        if ($scope.Minutos === 0) {
            $scope.DecrementaHoras();
            $scope.Minutos = 55;
        } else {
            $scope.Minutos = $scope.Minutos - 5;
        }
    };
    $scope.DecrementaHorasRes = function () {
        $scope.HorasRes = $scope.HorasRes - 1;
    };
    $scope.DecrementaMinutosRes = function () {
        if ($scope.MinutosRes === 0) {
            $scope.DecrementaHorasRes();
            $scope.MinutosRes = 55;
        } else {
            $scope.MinutosRes = $scope.MinutosRes - 5;
        }
    };
    //***********************************************************************************************************************
    $scope.LimpiarAct = function () {
        $scope.Act = {
            ID:'',
            Proyecto:'',
            Modulo:'',
            Componente:'',
            Actividad:'',
            Tiempoautorizado:'',
            TrabajoRealizado:'',
            TrabajoRestante:'',
            Avance:'',
            Vuelta:'',
            Estatus:'',
            Consultor:'',
            FechaRegistro: ''
        };
    };
    $scope.LimpiarAct();
    //***********************************************************************************************************************
    //Cargar las actividades en un arreglo
    $scope.LoadActividades = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerActividades_Grid',
            data: {
                nFechaInicial: $scope.FILTROACTIVIDADES.dFechaInicio,
                nFechaFinal: $scope.FILTROACTIVIDADES.dFechaFin,
                idProyecto: ($scope.Select.Proyecto != '') ? $scope.Select.Proyecto : 0,
                idModulo: ($scope.Select.ProyectoModulo != '') ?$scope.Select.ProyectoModulo:0,
                idComponente:($scope.Select.ProyectoComponente != '')? $scope.Select.ProyectoComponente:0,
                idConsultor:($scope.Select.Consultor != '')? $scope.Select.Consultor:0
            }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstActividades = response.Actividades_Grid;
                $scope.nUsuarioLogin = response.nUsuarioLogin;
                $('.TablaDivResize').trigger('resize');
            }
        }).error(function (response, status, header, config) {
        });
    };

    $scope.LoadActividadesInActivas = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerActividadesInActivas_Grid',
            data: {
                nFechaInicial: $scope.FILTROACTIVIDADES.dFechaInicio,
                nFechaFinal: $scope.FILTROACTIVIDADES.dFechaFin,
                idProyecto: ($scope.Select.Proyecto != '') ? $scope.Select.Proyecto : 0,
                idModulo: ($scope.Select.ProyectoModulo != '') ? $scope.Select.ProyectoModulo : 0,
                idComponente: ($scope.Select.ProyectoComponente != '') ? $scope.Select.ProyectoComponente : 0,
                idConsultor: ($scope.Select.Consultor != '') ? $scope.Select.Consultor : 0
            }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstActividades = response.Actividades_Grid;
                $scope.nUsuarioLogin = response.nUsuarioLogin;
                $('.TablaDivResize').trigger('resize');
            }
        }).error(function (response, status, header, config) {
        });
    };
    //***********************************************************************************************************************
    //////////////////|Productos|///
    
    $scope.TipoAyuda = [
        {
            idTipoayuda: 1,
            Tipoayuda:'Consultor'
        },
        {
            idTipoayuda: 2,
            Tipoayuda: 'Cliente'
        }
    ];
    ///Cargar proyectos
    $scope.lstProyectosIn = [];
    $scope.LoadProyectosIn = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerProyectosInactivos',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstProyectosIn = response.Proyecto;
            }
        }).error(function (response, status, header, config) {
        });
    };
    ///Cargar proyectos
    $scope.lstProyectos = [];
    $scope.LoadProyectos = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerProyectos',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstProyectos = response.Proyecto;
            }
        }).error(function (response, status, header, config) {
        });
    };
    //modulos
    $scope.lstProyectoModulos = [];
    $scope.LoadProyectoModulos = function (idProyecto) {

        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerModulos',
            data: { idProyecto: idProyecto }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstProyectoModulos = response.ProyectoModulos;
                
            }
        }).error(function (response, status, header, config) {
        });

    };
    //componentes
    $scope.lstProyectoComponentes = [];
    $scope.LoadProyectoComponentes = function (idModulo) {

        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerComponentes',
            data: { idModulo: idModulo }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstProyectoComponentes = response.ProyectoComponentes;
                
            }
        }).error(function (response, status, header, config) {
        });

    };
    //
    $scope.lstImpacto = [];
    $scope.LoadImpacto = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerImpactoAyuda',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstImpacto = response.Impacto;
                
            }
        }).error(function (response, status, header, config) {
        });
    };
    //
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
               
            }
        }).error(function (response, status, header, config) {
        });
    };
    //tipo incidente
    $scope.lstTipoIncidente = [];
    $scope.LoadTipoIncidente = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerTipoIncidente',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstTipoIncidente = response.TipoIncidente;
                
            }
        }).error(function (response, status, header, config) {
        });
    };
    //versiones
    $scope.lstVersiones = [];
    $scope.LoadVersiones = function (idProducto) {
        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerVersiones',
            data: { idProducto: idProducto }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstVersiones = response.Versiones;
                
            }
        }).error(function (response, status, header, config) {
        });

    };
    //modulos
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
                    
            }
        }).error(function (response, status, header, config) {
        });
        
    };
    //componentes
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
                
            }
        }).error(function (response, status, header, config) {
        });

    };
    //clientes
    $scope.lstClientes = [];
    $scope.LoadClientes = function () {

        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerClientes',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstClientes = response.Clientes;
                
            }
        }).error(function (response, status, header, config) {
        });

    };
    //Lideres
    $scope.lstLideres = [];
    $scope.LoadLideres = function () {

        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerLideres',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstLideres = response.Lideres;
            }
        }).error(function (response, status, header, config) {
        });

    };
    //Testers
    $scope.lstTesters = [];
    $scope.LoadTesters = function () {

        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerTesters',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstTesters = response.Testers;
            }
        }).error(function (response, status, header, config) {
        });

    };
    //Consultores
    $scope.lstConsultores = [];
    $scope.LoadConsultores = function () {

        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerConsultores',
            data: {}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstConsultores = response.Consultores;
            }
        }).error(function (response, status, header, config) {
        });

    };
    //Acciones
    $scope.lstAcciones = [];
    $scope.LoadAcciones = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerAcciones',
            data: {idActividad:$scope.Act.ID}
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstAcciones = response.Accion;
            }
        }).error(function (response, status, header, config) {
        });
    };

    $scope.lstHistorial = [];
    $scope.LoadHistorial = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/ObtenerHistorial',
            data: {
                idActividad: $scope.Act.ID,
                idConsultor: $scope.Act.nUsuario
            }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstHistorial = response.Historial;
            }
        }).error(function (response, status, header, config) {
        });
    };
    $scope.gridHistorial = {
        data: 'lstHistorial',
        showGroupPanel: true,
        jqueryUIDraggable: true,
        columnDefs: [{ field: 'Consultor', displayName: 'Consultor' }, { field: 'TrabajoRealizado', displayName: 'Trabajo Realizado' }, { field: 'TrabajoRestante', displayName: 'Trabajo Restante' },
        { field: 'Avance', displayName: 'Avance' }, { field: 'FechaRegistro', displayName: 'Fecha Registro' }, { field: 'Comentario', displayName: 'Comentario', width: "25%" }],
        showFooter: false,
        multiSelect: false,
        enableFiltering: false,
        aggregateTemplate: '<div ng-click="row.toggleExpand()" " ng-style="rowStyle(row)" class="ngAggregate"> <span class="ngAggregateText">{{row.label CUSTOM_FILTERS}} (Total Consultas: {{ActividadesSum(row)}})</span> <div class="{{row.aggClass()}}"></div> </div>',
    };

    $scope.limpiarForm = function () {
        $scope.Select = {
            Cliente: '',
            Cliente2: '',
            Producto: '',
            Modulo: '',
            Componente: '',
            Versiones: '',
            Lider: '',
            Tester: '',
            Consultor: '',
            Consultor2: '',
            TipoIncidente: '',
            cDescripcion: '',
            tDescripcion: '',
            Files: '',
            Impacto: '',
            ProyectoIn: '',
            Proyecto: '',
            ProyectoModulo: '',
            ProyectoComponente: '',
            Cobrable: false,
            ControlCalidad: false
        };
    };
    $scope.limpiarForm();

    $scope.LimpiarInc = function () {
        $scope.Inc = {
            Cliente: '',
            Producto: '',
            Modulo: '',
            Componente: '',
            Versiones: '',
            TipoIncidente: '',
            nIncidente:0,
            cDescripcion: '',
            tDescripcion: '',
            Cobrable: false,
            ControlQC: true,
            THoras: '',
            TMinutos: ''
        };
    };
    $scope.LimpiarInc();

    $scope.lipiarreporte = function () {
        $scope.Select.Cliente2 = '';
        $scope.Select.Consultor2 = '';
    };

    //Incidentes *************************************************************************
    $scope.lstIncidentes = [];
    $scope.gridIncidentes = {
        data: 'lstIncidentes',
        showGroupPanel: true,
        jqueryUIDraggable: true,
        columnDefs: [{ field: 'nIncidente', displayName: 'id' }, { field: 'Incidente', displayName: 'Incidente' }, { field: 'Cliente', displayName: 'Cliente' },
        { field: 'Producto', displayName: 'Producto' }, { field: 'Modulo', displayName: 'Modulo' }, { field: 'Componente', displayName: 'Componente' },
        { field: 'T_Solicitado_UE', displayName: 'T. Solicitado UE' }, { field: 'T_Autorizado', displayName: 'T. Autorizado' }, { field: 'Trab_Realizado', displayName: 'Trab. Realizado' },
        { field: 'Trab_Restante', displayName: 'Trab Restante' }, { field: 'Avance', displayName: 'Avance' }, { field: 'Estatus', displayName: 'Estatus' },
        { field: 'FechaRegistro', displayName: 'Fecha Registro' }],
        showFooter: true,
        enableFiltering: false,
        aggregateTemplate: '<div ng-click="row.toggleExpand()" " ng-style="rowStyle(row)" class="ngAggregate"> <span class="ngAggregateText">{{row.label CUSTOM_FILTERS}} (Total Consultas: {{ActividadesSum(row)}})</span> <div class="{{row.aggClass()}}"></div> </div>',
    };

    $scope.LoadIncidentes = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerIncidentes',
            data: {
                idCliente: ($scope.Select.Cliente != '') ? $scope.Select.Cliente : 0,
                idProducto:($scope.Select.Producto != '') ? $scope.Select.Producto : 0,
                idModulo:($scope.Select.Modulo != '') ? $scope.Select.Modulo : 0,
                idComponente:($scope.Select.Componente != '') ? $scope.Select.Componente : 0,
                idComponente:($scope.Select.Componente != '') ? $scope.Select.Componente : 0,
                idConsultor: ($scope.Select.Consultor != '') ? $scope.Select.Consultor : 0,
            }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstIncidentes = response.Incidentes;
                $('.TablaDivResize').trigger('resize');
            }
        }).error(function (response, status, header, config) {
        });

    };
    $scope.LoadIncidentesInactivos = function () {
        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/ObtenerIncidentesInactivos',
            data: {
                idCliente: ($scope.Select.Cliente != '') ? $scope.Select.Cliente : 0,
                idProducto: ($scope.Select.Producto != '') ? $scope.Select.Producto : 0,
                idModulo: ($scope.Select.Modulo != '') ? $scope.Select.Modulo : 0,
                idComponente: ($scope.Select.Componente != '') ? $scope.Select.Componente : 0,
                idComponente: ($scope.Select.Componente != '') ? $scope.Select.Componente : 0,
                idConsultor: ($scope.Select.Consultor != '') ? $scope.Select.Consultor : 0,
            }
        }).success(function (response, status, header, config) {
            if (response.bError) {
                swal('Error', response.msgErr, 'error');
            }
            else {
                $scope.lstIncidentes = response.Incidentes;
                $('.TablaDivResize').trigger('resize');
            }
        }).error(function (response, status, header, config) {
        });

    };

    $scope.GuardarIncidente = function () {
        debugger;
        $scope.OnServer = true;
        $http({
            method: 'POST',
            url: '/Ejecucion/Incidentes/GuardarIncidente',
            data: {
                idCliente:$scope.Inc.Cliente, 
                idProducto:$scope.Inc.Producto,
                idModulo:$scope.Inc.Modulo, 
                ProductoVersion:$scope.Inc.Versiones,
                idComponente:$scope.Inc.Componente, 
                idConsultor:$scope.Inc.Consultor,  
                TipoIncidente:$scope.Inc.TipoIncidente, 
                nIncidente: ($scope.Inc.nIncidente != '') ? $scope.Inc.nIncidente : 0,
                cIncidente:$scope.Inc.cDescripcion, 
                bCobrable: ($scope.Inc.Cobrable != '') ? $scope.Inc.Cobrable : false,
                tDescripcion:$scope.Inc.tDescripcion,                 
                bRequiereQC: ($scope.Inc.ControlQC != '') ? $scope.Inc.ControlQC : false,
                THoras: $scope.Inc.THoras,
                TMinutos:$scope.Inc.TMinutos
            }
        }).success(function (data, status, header, config) {
            $scope.OnServer = false;
            if (data.bError) {
                $("#Incidente_msgerr").html($scope.MsgErrorHTML('Error...', data.msgErr, '"Incidente_msgerr"'));
            }
            else {
                $scope.LimpiarInc();
                $scope.LoadIncidentes();
                $("#RegistroIncidenteModal").modal('hide');
                swal('Listo!', 'Incidente registrado');
            }
        }).error(function (data, status, header, config) {
        });
    };

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
    //*******************************************************************************************************************
    $scope.GuardarReporteAvanceActividad = function () {
        $scope.OnServer = true;
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/GuardarReporteAvanceActividad',
            data: {
                RegistroTrabajo: $scope.Act,
                dFecha:$scope.dFecha,
                Comentario:$scope.Comentario, 
                Horas:$scope.Horas, 
                Minutos:$scope.Minutos, 
                HorasRes:$scope.HorasRes, 
                MinutosRes:$scope.MinutosRes }
        }).success(function (data, status, header, config) {
            $scope.OnServer = false;
            if (data.bError) {
                $("#Actividad_msgerr").html($scope.MsgErrorHTML('Error...', data.msgErr, '"Actividad_msgerr"'));
            }
            else {
                $scope.LoadActividades();
                $("#ReporteActividadModal").modal('hide');
                swal('Listo!', 'Reporte registrado');
            }
        }).error(function (data, status, header, config) {
        });
    };
    $scope.GuardarAccion = function () {
        debugger;
        $scope.OnServer = true;
        $http({
            method: 'POST',
            url: '/Ejecucion/Actividades/GuardarAccion',
            data: {
                idActividad: $scope.Act.ID,
                Accion: $scope.Accion               
            }
        }).success(function (data, status, header, config) {
            $scope.OnServer = false;
            if (data.bError) {
                $("#Actividad_msgerr").html($scope.MsgErrorHTML('Error...', data.msgErr, '"Actividad_msgerr"'));
            }
            else {
                $scope.Accion='';
                $scope.LoadAcciones();
                swal('Listo!', 'Incidente registrado');
            }
        }).error(function (data, status, header, config) {
        });
    };

    $scope.CerrarActividadesAyudaModal = function () {
        $("#ActividadesAyudaModal").modal('hide');
    };
    $scope.AbrirActividadesAyudaModal = function () {
        $("#ActividadesAyudaModal").modal('show');
    };
    $scope.CerrarReporteActividad = function () {
        $("#ReporteActividadModal").modal('hide');
    };
    $scope.CerrarReporteAvanceActividad = function () {
        $("#ReporteAvanceActividadModal").modal('hide');
    };
    $scope.MostrarHistorial = function () {
        $scope.LoadHistorial();
        $("#ReporteAvanceActividadModal").modal('show');
    };
    $scope.MostrarReporte = function () {        
        $("#ReporteActividadModal").modal('show');
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