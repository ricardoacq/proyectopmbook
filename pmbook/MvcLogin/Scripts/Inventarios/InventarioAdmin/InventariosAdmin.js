var app = angular.module('app', ['ngMaterial']);

app.config(function ($mdThemingProvider) {
        var PrimarySK = {
            '50': '#f69297',
            '100': '#f47b80',
            '200': '#f3636a',
            '300': '#f14b53',
            '400': '#ef343d',
            '500': '#ED1C26',
            '600': '#de121b',
            '700': '#c61018',
            '800': '#af0e16',
            '900': '#970c13',
            'A100': '#f8aaae',
            'A200': '#fac1c4',
            'A400': '#fcd9db',
            'A700': '#7f0a10',
            'contrastDefaultColor': 'light',
        };
        $mdThemingProvider.definePalette('PrimarySK', PrimarySK);

        var WarnSK = {
            '50': '#f5de96',
            '100': '#f2d77f',
            '200': '#f0d068',
            '300': '#eec850',
            '400': '#ebc139',
            '500': '#E9BA22',
            '600': '#dcad16',
            '700': '#c49b14',
            '800': '#ad8811',
            '900': '#96760f',
            'A100': '#f7e5ad',
            'A200': '#f9edc4',
            'A400': '#fbf4dc',
            'A700': '#7f640d',
            'contrastDefaultColor': 'light',
        };

        $mdThemingProvider.definePalette('WarnSK', WarnSK);

        $mdThemingProvider.theme('default')
            .primaryPalette('PrimarySK')
            .warnPalette('WarnSK')
        //   .foregroundPalette = 'rgba(255,255,255,1)';
        //$mdThemingProvider.theme('default')
        //   .primaryPalette('purple')
        // specify primary color, all
        // other color intentions will be inherited
        // from default
    });

app.controller('InventariosCtrl', ['$http', '$scope', '$filter', '$q', '$timeout', '$log', '$mdDialog', '$mdMedia', function ($http, $scope, $filter, $q, $timeout, $log, $mdDialog, $mdMedia) {

    $scope.Merma = {
        "background-color": "black"
    }
    $scope.Consumo = {
        "color": "white",
        
    }

    $scope.allItems = [];
    $scope.filteredList = [];
    $scope.lstPaginas = [];
    $scope.nPaginas = 10;
    $scope.CargaInicial = {};
    $scope.Total = 0;
    $scope.FiltrosBusqueda = {};
    $scope.CargaInicial = function (cCargaInicial)
    {
        debugger
        $scope.CargaInicial = cCargaInicial;
        $scope.CargaInicial.bExportar = false;
        $scope.CargaInicial.BusquedaRealizada = false;
        $scope.CentroLogisticoAtc.states = $scope.CargaInicial.lstCentrosLogisticos;
        $scope.AutocompleteMateriales.lstMateriales = $scope.CargaInicial.lstMateriales;
        $scope.FiltrosBusqueda.bConsumo = false;
        $scope.FiltrosBusqueda.bMerma = false;
        //alert($scope.CentroLogisticoAtc.states.length);
    }

    $scope.OnChangePaginas = function (nPaginas)
    {
        $scope.nPaginas = nPaginas;
        $scope.ItemsByPage = $scope.paged($scope.filteredList, $scope.nPaginas);
        $scope.pagination();
        $scope.firstPage();
        //$scope.range(nPaginas);
        debugger
        //alert($scope.nPaginas);

    }

    $scope.ShowFilters = function (movie) {
        return movie.bConsumo === $scope.FiltrosBusqueda.bConsumo ||
            movie.bMerma === $scope.FiltrosBusqueda.bMerma
    };


    $scope.OnChangeGrupo = function (item)
    {
        debugger
        if (item == undefined) {
            $scope.AutocompleteMateriales.lstMateriales = $scope.CargaInicial.lstMateriales;
            $scope.AutocompleteMateriales.searchText = "";
        }
        else {
            $scope.AutocompleteMateriales.lstMateriales = $filter('filter')($scope.CargaInicial.lstMateriales, { nIdGrupo: item.nIdGrupo });
        }
    }

    $scope.OnChangeMaterial = function (item)
    {

        if (item != undefined) {
            $scope.FiltrosBusqueda.Grupo = $filter('filter')($scope.CargaInicial.lstGrupos, { nIdGrupo: item.nIdGrupo })[0];
        }

    }
    

    $scope.lstResultados = [];


    $scope.BuscarSalidas = function ()
    {
        debugger
        $http({
            url: '/Inventarios/InventarioAdmin/BuscarSalidas',
            method: "POST",
            params: {
                'cFiltrosBusqueda': JSON.stringify($scope.FiltrosBusqueda)
            },
        }).success(function (response) {
            debugger
            $scope.CargaInicial.BusquedaRealizada = true;
            if (!response.bError) {
                $scope.Total = response.nTotal;
                $scope.lstResultados = response.lstSalidas;
                if (response.lstSalidas.length > 0) {

                    //Metodos de paginacion
                    $scope.allItems = $scope.lstResultados;
                    $scope.filteredList = $scope.allItems;
                    $scope.ItemsByPage = $scope.paged($scope.filteredList, $scope.nPaginas);
                    $scope.range($scope.ItemsByPage.length);

                    if (!$scope.FiltrosBusqueda.bConsumo && !$scope.FiltrosBusqueda.bMerma)
                    {
                        $scope.FiltrosBusqueda.bConsumo = true;
                        $scope.FiltrosBusqueda.bMerma = true;
                    }
                    



                    $scope.CargaInicial.bExportar = true;
                }
                else {
                    $scope.lstResultados = [];
                }

            }

        }).error(function (error) {
            swal('¡Error!', 'Algo salió mal', 'error');
        });
    }

    


    $scope.ValidarFiltros = function ()
    {
        
        var bError = false;
        var cMensaje = "";
        if ($scope.CentroLogisticoAtc.searchText == undefined || $scope.CentroLogisticoAtc.searchText == "")
        {
            bError = true;
            cMensaje = "Falta seleccionar un centro logístico"
        }
        if ($scope.FiltrosBusqueda.dFechaDesde > $scope.FiltrosBusqueda.dFechaHasta) {
            bError = true;
            cMensaje = "La Fecha de inicio debe ser menor a la fecha fin"
        }
        if ($scope.FiltrosBusqueda.dFechaDesde == undefined || $scope.FiltrosBusqueda.dFechaHasta == undefined)
        {
            bError = true;
            cMensaje = "Falta capturar las fechas";
        }
       
    


        if (bError) {
            $scope.MensajeGenerico("Alerta", cMensaje);
            return;
        }
        else {
            $scope.BuscarSalidas();

        }


    return bError;
}

    $scope.MensajeGenerico = function (Encabezado,Texto)
    {
        $mdDialog.show(
              $mdDialog.alert()
                .parent(angular.element(document.querySelector('#Body_Layout')))
                .clickOutsideToClose(true)
                .title(Encabezado)
                .textContent(Texto)
                .ariaLabel('Alert Dialog Demo')
                .ok('Aceptar')
                //.targetEvent(ev)
            );

    }


    $scope.AutocompleteMateriales = {
         simulateQuery : true,
         isDisabled: false,
         //Material: {}
         // list of `state` value/display objects
         //states : loadAll(),
         //querySearch : querySearch,
         //selectedItemChange : selectedItemChange,
         //searchTextChange : searchTextChange,
         //newState : newState,
    };
    

    $scope.GrupoArticulosAtc = {
        simulateQuery: true,
        isDisabled: false,
        //Grupo: {}
        // list of `state` value/display objects
        //states: loadAll(),
        //querySearch: querySearch,
        //selectedItemChange: selectedItemChange,
        //searchTextChange: searchTextChange,
        //newState: newState,
    };

    $scope.CentroLogisticoAtc = {
        simulateQuery: true,
        isDisabled: false,
        // list of `state` value/display objects
        //states: $scope.CargaInicial.lstCentrosLogisticos,
        //querySearch: querySearch1,
        //selectedItemChange: selectedItemChange,
        //searchTextChange: searchTextChange,
        //newState: newState,
    };

    

    

    




    $scope.GuardarRuta = function () {
        $http({
            url: '/MisPlazas/Plazas/GuardarRuta',
            method: "POST",
            params: {
                'cRuta': $scope.Ruta
            },
        }).success(function (response) {
            debugger
        }).error(function (error) {
            swal('¡Error!', 'Algo salió mal', 'error');
        });

    }





    /***************************metodos de paginacion****************************************************************************/
    $scope.searched = function (valLists, toSearch) {

        return _.filter(valLists, function (i) {
            /* Search Text in all 3 fields */
            return $scope.searchUtil(i, toSearch);
        });
    };

    $scope.paged = function (valLists, pageSize) {
        retVal = [];
        for (var i = 0; i < valLists.length; i++) {
            if (i % pageSize === 0) {
                retVal[Math.floor(i / pageSize)] = [valLists[i]];
            } else {
                retVal[Math.floor(i / pageSize)].push(valLists[i]);
            }
        }
        return retVal;
    };


    $scope.pageSize = 10;
    //$scope.allItems = $scope.CargaInicial.lstPulmonias;

    $scope.reverse = false;
    //$scope.filteredList = $scope.allItems;

    $scope.resetAll = function () {
        $scope.filteredList = $scope.allItems;
        $scope.newEmpId = '';
        $scope.newName = '';
        $scope.newEmail = '';
        $scope.searchText = '';
        $scope.currentPage = 0;
        $scope.Header = ['', '', ''];
    }

    $scope.search = function () {
        $scope.filteredList = $scope.searched($scope.allItems, $scope.searchText);

        if ($scope.searchText == '') {
            $scope.filteredList = $scope.allItems;
        }

        $scope.pagination();
        $scope.firstPage();
    }


    // Calculate Total Number of Pages based on Search Result
    $scope.pagination = function () {
        $scope.ItemsByPage = $scope.paged($scope.filteredList, $scope.nPaginas);
    };

    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };

    $scope.firstPage = function () {
        $scope.currentPage = 0;
    };

    $scope.lastPage = function () {
        $scope.currentPage = $scope.ItemsByPage.length - 1;
    };

    $scope.range = function (input) {
        debugger
        var ret = [];
        var total;
        //if (!total) {
        //    total = input;
        //    input = 0;
        //}
        for (var i = 0; i < input; i++) {
            ret.push(i);
            //if (i != 0 && i != total - 1) {
            //    ret.push(i);
            //}
        }
        $scope.lstPaginas = ret;
        //return ret;
    };

    $scope.sort = function (sortBy) {
        debugger
        $scope.resetAll();

        $scope.columnToOrder = sortBy;

        //$Filter - Standard Service
        $scope.filteredList = $filter('orderBy')($scope.filteredList, $scope.columnToOrder, $scope.reverse);

        if ($scope.reverse)
            iconName = 'arrow_upward';
        else
            iconName = 'arrow_downward';


        if (sortBy === 'Fecha') {
            $scope.Header[0] = iconName;
        }
        else if (sortBy === 'Material') {
            $scope.Header[1] = iconName;
        }
        else if (sortBy === 'TextoBreveMaterial') {
            $scope.Header[2] = iconName;
        }
        else if (sortBy === 'GrupoDeeArticulo') {
            $scope.Header[3] = iconName;
        }
        else if (sortBy === 'Cantidad') {
            $scope.Header[4] = iconName;
        }
        else if (sortBy === 'UnidadMedidaBase') {
            $scope.Header[5] = iconName;
        }
        else if (sortBy === 'CostoUnitario') {
            $scope.Header[6] = iconName;
        }
        else if (sortBy === 'Total') {
            $scope.Header[7] = iconName;
        }
        else if (sortBy === 'cTipo') {
            $scope.Header[8] = iconName;
        }
        $scope.reverse = !$scope.reverse;



        $scope.pagination();
    };

    //By Default sort ny Name
    $scope.sort('Fecha');

    $scope.searchUtil = function (item, toSearch) {
        /* Search Text in all 3 fields */
        return (item.GrupoDeeArticulo.toLowerCase().indexOf(toSearch.toLowerCase()) > -1 || item.Material.toLowerCase().indexOf(toSearch.toLowerCase()) > -1 || item.TextoBreveMaterial == toSearch) ? true : false;
    }

    /*******************************************************************************************************/


}]);





app.directive('showErrors', ['$timeout', 'showErrorsConfig', '$interpolate', function ($timeout, showErrorsConfig, $interpolate) {
    var getShowSuccess, getTrigger, linkFn;
    getTrigger = function (options) {
        var trigger;
        trigger = showErrorsConfig.trigger;
        if (options && (options.trigger != null)) {
            trigger = options.trigger;
        }
        return trigger;
    };
    getShowSuccess = function (options) {
        var showSuccess;
        showSuccess = showErrorsConfig.showSuccess;
        if (options && (options.showSuccess != null)) {
            showSuccess = options.showSuccess;
        }
        return showSuccess;
    };
    linkFn = function (scope, el, attrs, formCtrl) {
        var blurred, inputEl, inputName, inputNgEl, options, showSuccess, toggleClasses, trigger;
        blurred = false;
        options = scope.$eval(attrs.showErrors);
        showSuccess = getShowSuccess(options);
        trigger = getTrigger(options);
        inputEl = el[0].querySelector('.form-control[name]');
        inputNgEl = angular.element(inputEl);
        inputName = $interpolate(inputNgEl.attr('name') || '')(scope);
        if (!inputName) {
            throw "show-errors element has no child input elements with a 'name' attribute and a 'form-control' class";
        }
        inputNgEl.bind(trigger, function () {
            blurred = true;
            return toggleClasses(formCtrl[inputName].$invalid);
        });
        scope.$watch(function () {
            return formCtrl[inputName] && formCtrl[inputName].$invalid;
        }, function (invalid) {
            if (!blurred) {
                return;
            }
            return toggleClasses(invalid);
        });
        scope.$on('show-errors-check-validity', function () {
            return toggleClasses(formCtrl[inputName].$invalid);
        });
        scope.$on('show-errors-reset', function () {
            return $timeout(function () {
                el.removeClass('has-error');
                el.removeClass('has-success');
                return blurred = false;
            }, 0, false);
        });
        return toggleClasses = function (invalid) {
            el.toggleClass('has-error', invalid);
            if (showSuccess) {
                return el.toggleClass('has-success', !invalid);
            }
        };
    };
    return {
        restrict: 'A',
        require: '^form',
        compile: function (elem, attrs) {
            if (attrs['showErrors'].indexOf('skipFormGroupCheck') === -1) {
                if (!(elem.hasClass('form-group') || elem.hasClass('input-group'))) {
                    throw "show-errors element does not have the 'form-group' or 'input-group' class";
                }
            }
            return linkFn;
        }
    };
}])

app.provider('showErrorsConfig', function () {
    var _showSuccess, _trigger;
    _showSuccess = false;
    _trigger = 'blur';
    this.showSuccess = function (showSuccess) {
        return _showSuccess = showSuccess;
    };
    this.trigger = function (trigger) {
        return _trigger = trigger;
    };
    this.$get = function () {
        return {
            showSuccess: _showSuccess,
            trigger: _trigger
        };
    };
});

app.directive('alphanumeric', function () {

    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, elem, attr, ngModel) {
            elem.bind('keypress', function ($event) {
                var charCode = ($event.which) ? $event.which : $event.keyCode;

                return (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode > 47 && charCode < 60);
            });

        }
    };
});

app.directive('integer', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, elem, attr, ngModel) {
            if (!ngModel)
                return;

            function isValid(val) {
                if (val === "")
                    return true;

                var asInt = parseInt(val, 10);
                if (asInt === NaN || asInt.toString() !== val) {
                    return false;
                }

                var min = parseInt(attr.min);
                if (min !== NaN && asInt < min) {
                    return false;
                }

                var max = parseInt(attr.max);
                if (max !== NaN && max < asInt) {
                    return false;
                }

                return true;
            }

            var prev = scope.$eval(attr.ngModel);
            ngModel.$parsers.push(function (val) {
                // short-circuit infinite loop
                if (val === prev)
                    return val;

                if (!isValid(val)) {
                    ngModel.$setViewValue(prev);
                    ngModel.$render();
                    return prev;
                }

                prev = val;
                return val;
            });
        }
    };
});

app.directive('letters', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, elem, attr, ngModel) {
            elem.bind('keypress', function ($event) {
                var charCode = ($event.which) ? $event.which : $event.keyCode;
                return (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 241;
            });

        }
    };
});







