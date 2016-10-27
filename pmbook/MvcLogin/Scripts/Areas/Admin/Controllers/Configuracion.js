BaseMVC.controller('ConfiguracionCtrl', ['$http', '$scope', '$filter', '$q', '$timeout', '$log', '$mdDialog', '$mdMedia', '$rootScope', 'Fun', function ($http, $scope, $filter, $q, $timeout, $log, $mdDialog, $mdMedia, $rootScope, Fun) {

    $scope.cUrlModulo = "ConfiguracionCtrl";
    $scope.CargaInicial = {};
    $scope.Modulo = {};
    $scope.Area = {};

    //Fun.MensajeGenerico($mdDialog, "sdfs", "holis");

    $scope.CargaInicial = function (cCargaInicial, Token) {
        $scope.CargaInicial = cCargaInicial;
        $scope.antiForgeryToken = Token;
        //$scope.CargaInicial.cUrl = Fun.Base64($scope.CargaInicial.cUrl);
        Fun.alerta($scope.CargaInicial.cUrl);
        $scope.toggleItem(1);
    }

    $scope.MostrarArea = function (index)
    {
        $scope.Area = $scope.CargaInicial.lstAreas[index];

    }


    $scope.toggleItem = function (item) {

        if ($scope.shownItem != item) {
            if ($scope.isItemShown(item)) {
                $scope.shownItem = null;
            } else {
                $scope.shownItem = item;
            }
        }
        

        
    };
    $scope.isItemShown = function (item) {
        return $scope.shownItem === item;
    };

    $scope.AgregarPerfilTabla = function (ev)
    {
        var confirm = $mdDialog.prompt()
          .title('Cual es el nombre del perfil?')
          .textContent('Este sera el nombre del perfil')
          .placeholder('Perfil')
          .ariaLabel('Perfil')
          //.initialValue('Buddy')
          .targetEvent(ev)
          .ok('Agregar!')
          .cancel('Cancelar');
        $mdDialog.show(confirm).then(function (result) {
            $scope.InsertPerfil(result);
        }, function () {
        });

    }

    $scope.InsertPerfil = function (cPerfil)
    {
        $http({
            url: '/Admin/Configuracion/InsertPerfil',
            method: "POST",
            params: {
                'cPerfil': cPerfil
            },
        }).success(function (response) {

            if (!response.Perfil.bError) {
                $scope.CargaInicial.lstPerfiles.push(response.Perfil);
                $scope.MensajeGenerico("Exito", "Ya se agrego el perfil:  " + cPerfil);
            }
            else {
                $scope.MensajeGenerico("Error", "No pudimos generar el perfil");
            }


        }).error(function (error) {

        });
    }

    $scope.Area = {};
    $scope.AgregarPerfil = function (ev,index) {
        $mdDialog.show({
            parent: angular.element(document.querySelector('#Body_Layout')),
            targetEvent: ev,
                template: '<md-dialog aria-label="options dialog">' +
                            '  <md-dialog-content layout-padding>' +
                                '<h2 class="md-title">Perfiles:</h2>' +
                                '<md-select ng-model="Perfil" placeholder="Selecciona un perfil...">' +
                                  '<md-option ng-value="item" ng-repeat="item in CargaInicial.lstPerfiles">{{item.cPerfil}}</md-option>' +

                                '</md-select>' +
                            '  </md-dialog-content>' +
                            '  <md-dialog-actions>' +
                                '<span flex></span>' +
                                '<md-button ng-click="CerrarModalPerfil()">Cancelar</md-button>' +
                                '<md-button ng-disabled="Perfil.nIdPerfil == undefined" ng-click="InserPerfil(Perfil,' + index + ')">Ok</md-button>' +
                            '  </md-dialog-actions>' +
                            '</md-dialog>',
                //locals: {
                //    items: $scope.CargaInicial.lstPerfiles,
                //    Area: Area
                //},
                //controller: PerfilCTRL
                bindToController: true,
                scope: $scope,
                hasBackdrop: true,
                preserveScope: true
        });

       
    };

    $scope.CerrarModalPerfil = function ()
    {
        $mdDialog.cancel();

    }

    $scope.InserPerfil = function (Perfil, index) {
        $http({
            url: '/Admin/Configuracion/AgregarPerfilArea',
            method: "POST",
            params: {
                'nIdArea': $scope.CargaInicial.lstAreas[index].nIdArea,
                'nIdPerfil': Perfil.nIdPerfil
            },
        }).success(function (response) {
            $mdDialog.cancel();
            $scope.CargaInicial.lstAreas[index].lstPerfiles.push(response.Perfil);
            $scope.MensajeGenerico("Exito","Ya se agrego el perfil : ("+ Perfil.cPerfil +") al area");
            

        }).error(function (error) {
        });
    }

    $scope.InserPerfilUsuario = function (Perfil, index) {
        $http({
            url: '/Admin/Configuracion/AgregarPerfilUsuario',
            method: "POST",
            params: {
                'cLogin': $scope.CargaInicial.lstUsuarios[index].cLogin,
                'nIdPerfil': Perfil.nIdPerfil
            },
        }).success(function (response) {
            $mdDialog.cancel();
            $scope.CargaInicial.lstUsuarios[index].lstPerfiles.push(response.Perfil);
            $scope.MensajeGenerico("Exito", "Ya se agrego el perfil : (" + Perfil.cPerfil + ") al area");


        }).error(function (error) {
        });
    }


   

    $scope.AgregarPerfilUsuario = function (ev, index) {
        
        $mdDialog.show({
            parent: angular.element(document.querySelector('#Body_Layout')),
            targetEvent: ev,
            template: '<md-dialog aria-label="options dialog">' +
                        '  <md-dialog-content layout-padding>' +
                            '<h2 class="md-title">Perfiles:</h2>' +
                            '<md-select ng-model="Perfil" placeholder="Selecciona un perfil...">' +
                              '<md-option ng-value="item" ng-repeat="item in CargaInicial.lstPerfiles">{{item.cPerfil}}</md-option>' +

                            '</md-select>' +
                        '  </md-dialog-content>' +
                        '  <md-dialog-actions>' +
                            '<span flex></span>' +
                            '<md-button ng-click="CerrarModalPerfil()">Cancelar</md-button>' +
                            '<md-button ng-disabled="Perfil.nIdPerfil == undefined" ng-click="InserPerfilUsuario(Perfil,' + index + ')">Ok</md-button>' +
                        '  </md-dialog-actions>' +
                        '</md-dialog>',
            //locals: {
            //    items: $scope.CargaInicial.lstPerfiles,
            //    Area: Area
            //},
            //controller: PerfilCTRL
            bindToController: true,
            scope: $scope,
            hasBackdrop: true,
            preserveScope: true
        });

    };


   

    $scope.AgregarModulo = function (ev, item,index) {
        var confirm = $mdDialog.prompt()
         .title('Cual es el nombre del modulo?')
         .textContent('Este sera el nombre del modulo en el area:  (' + item.cNombreArea + ")")
         .placeholder('Nombre')
         .ariaLabel('Nombre')
         //.initialValue('Buddy')
         .targetEvent(ev)
         .ok('Agregar!')
         .cancel('Cancelar');
        $mdDialog.show(confirm).then(function (result) {
            $scope.InsertModulo(item.cNombreArea, result,index,item.nIdArea);
        }, function () {
        });


    }

    $scope.InsertModulo = function (Area, Modulo, index,nArea) {
        console.log(index);
        $http({
            url: '/Admin/Configuracion/GenerarModulo',
            method: "POST",
            params: {
                'cNombreArea': Area,
                'cNombreModulo': Modulo,
                'nArea': nArea
            },
        }).success(function (response) {
            
            if (!response.Modulo.bError) {
                $scope.CargaInicial.lstAreas[index].lstModulos.push(response.Modulo);
                $scope.MensajeGenerico("Exito", "Ya se genero el modulo, ahora solo lo tienes que incluir los scripts en los bundles");
            }
            else {
                $scope.MensajeGenerico("Error","No pudimos generar el modulo");
            }


        }).error(function (error) {

        });

    }


    

   

    $scope.Base64Decode = function (encoded) {
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        do {
            enc1 = keyStr.indexOf(encoded.charAt(i++));
            enc2 = keyStr.indexOf(encoded.charAt(i++));
            enc3 = keyStr.indexOf(encoded.charAt(i++));
            enc4 = keyStr.indexOf(encoded.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }
        } while (i < encoded.length);

        return output;
    }

    $scope.BuscarSalidas = function () {
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
                $scope.lstResultados = response.lstSalidas;
                if (response.lstSalidas.length > 0) {
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

    $scope.MensajeGenerico = function (Encabezado, Texto) {
        $mdDialog.show(
              $mdDialog.alert()
                .parent(angular.element(document.querySelector('#Body_Layout')))
                .clickOutsideToClose(true)
                .title(Encabezado)
                .textContent(Texto)
                .ariaLabel('Alert Dialog Demo')
                .ok('Entendido')
            );

    }






    $scope.lstClientes = [
        {
            cNombres: "rube dario zamorano cervantes",
            cTelefono:"1"
        },
        {
            cNombres: "rube dario zamorano cervantes",
            cTelefono: "2"
        },
        {
            cNombres: "rube dario zamorano cervantes",
            cTelefono: "3"
        },
        {
            cNombres: "gerardo zamorano cervantes",
            cTelefono: "4"
        },
        {
            cNombres: "joel arturo beltran silva",
            cTelefono: "5"
        },
         {
             cNombres: "rube dario zamorano cervantes",
             cTelefono: "6"
         },
        {
            cNombres: "rube dario zamorano cervantes",
            cTelefono: "7"
        },
        {
            cNombres: "rube dario zamorano cervantes",
            cTelefono: "8"
        },
        {
            cNombres: "gerardo zamorano cervantes",
            cTelefono: "9"
        },
        {
            cNombres: "joel arturo beltran silva",
            cTelefono: "10"
        },
         {
             cNombres: "rube dario zamorano cervantes",
             cTelefono: "11"
         },
        {
            cNombres: "rube dario zamorano cervantes",
            cTelefono: "12"
        },
        {
            cNombres: "rube dario zamorano cervantes",
            cTelefono: "13"
        },
        {
            cNombres: "gerardo zamorano cervantes",
            cTelefono: "14"
        },
        {
            cNombres: "joel arturo beltran silva",
            cTelefono: "15"
        },
    ];


    'use strict';

    $scope.options = {
        rowSelection: true,
        multiSelect: true,
        autoSelect: true,
        decapitate: false,
        largeEditDialog: false,
        boundaryLinks: false,
        limitSelect: true,
        pageSelect: true
    };

    $scope.selected = [];

    $scope.limitOptionsPerfiles = [5, 10, 15, {
        label: 'All',
        value: function () {
            return $scope.CargaInicial.lstPerfiles ? $scope.CargaInicial.lstPerfiles.length : 0;
        }
    }];

    $scope.limitOptionsAreas = [5, 10, 15, {
        label: 'All',
        value: function () {
            return $scope.CargaInicial.lstAreas ? $scope.CargaInicial.lstAreas.length : 0;
        }
    }];

    $scope.limitOptionsUsuarios = [5, 10, 15, {
        label: 'All',
        value: function () {
            return $scope.CargaInicial.lstUsuarios ? $scope.CargaInicial.lstUsuarios.length : 0;
        }
    }];

    $scope.query = {
        order: 'name',
        limit: 5,
        page: 1
    };

    // for testing ngRepeat
    $scope.columns = [{
        name: 'Dessert',
        orderBy: 'name',
        unit: '100g serving'
    }, {
        descendFirst: true,
        name: 'Type',
        orderBy: 'type'
    }, {
        name: 'Calories',
        numeric: true,
        orderBy: 'calories.value'
    }, {
        name: 'Fat',
        numeric: true,
        orderBy: 'fat.value',
        unit: 'g'
    }, /* {
    name: 'Carbs',
    numeric: true,
    orderBy: 'carbs.value',
    unit: 'g'
  }, */ {
      name: 'Protein',
      numeric: true,
      orderBy: 'protein.value',
      trim: true,
      unit: 'g'
  }, /* {
    name: 'Sodium',
    numeric: true,
    orderBy: 'sodium.value',
    unit: 'mg'
  }, {
    name: 'Calcium',
    numeric: true,
    orderBy: 'calcium.value',
    unit: '%'
  }, */ {
      name: 'Iron',
      numeric: true,
      orderBy: 'iron.value',
      unit: '%'
  }, {
      name: 'Comments',
      orderBy: 'comment'
  }];

    $scope.desserts = [
    {
        "name": "Frozen yogurt",
        "type": "Ice cream",
        "calories": { "value": 159.0 },
        "fat": { "value": 6.0 },
        "carbs": { "value": 24.0 },
        "protein": { "value": 4.0 },
        "sodium": { "value": 87.0 },
        "calcium": { "value": 14.0 },
        "iron": { "value": 1.0 },
        "comment": "Not as good as the real thing."
    }, {
        "name": "Ice cream sandwich",
        "type": "Ice cream",
        "calories": { "value": 237.0 },
        "fat": { "value": 9.0 },
        "carbs": { "value": 37.0 },
        "protein": { "value": 4.3 },
        "sodium": { "value": 129.0 },
        "calcium": { "value": 8.0 },
        "iron": { "value": 1.0 }
    }, {
        "name": "Eclair",
        "type": "Pastry",
        "calories": { "value": 262.0 },
        "fat": { "value": 16.0 },
        "carbs": { "value": 24.0 },
        "protein": { "value": 6.0 },
        "sodium": { "value": 337.0 },
        "calcium": { "value": 6.0 },
        "iron": { "value": 7.0 }
    }, {
        "name": "Cupcake",
        "type": "Pastry",
        "calories": { "value": 305.0 },
        "fat": { "value": 3.7 },
        "carbs": { "value": 67.0 },
        "protein": { "value": 4.3 },
        "sodium": { "value": 413.0 },
        "calcium": { "value": 3.0 },
        "iron": { "value": 8.0 }
    }, {
        "name": "Jelly bean",
        "type": "Candy",
        "calories": { "value": 375.0 },
        "fat": { "value": 0.0 },
        "carbs": { "value": 94.0 },
        "protein": { "value": 0.0 },
        "sodium": { "value": 50.0 },
        "calcium": { "value": 0.0 },
        "iron": { "value": 0.0 }
    }, {
        "name": "Lollipop",
        "type": "Candy",
        "calories": { "value": 392.0 },
        "fat": { "value": 0.2 },
        "carbs": { "value": 98.0 },
        "protein": { "value": 0.0 },
        "sodium": { "value": 38.0 },
        "calcium": { "value": 0.0 },
        "iron": { "value": 2.0 }
    }, {
        "name": "Honeycomb",
        "type": "Other",
        "calories": { "value": 408.0 },
        "fat": { "value": 3.2 },
        "carbs": { "value": 87.0 },
        "protein": { "value": 6.5 },
        "sodium": { "value": 562.0 },
        "calcium": { "value": 0.0 },
        "iron": { "value": 45.0 }
    }, {
        "name": "Donut",
        "type": "Pastry",
        "calories": { "value": 452.0 },
        "fat": { "value": 25.0 },
        "carbs": { "value": 51.0 },
        "protein": { "value": 4.9 },
        "sodium": { "value": 326.0 },
        "calcium": { "value": 2.0 },
        "iron": { "value": 22.0 }
    }, {
        "name": "KitKat",
        "type": "Candy",
        "calories": { "value": 518.0 },
        "fat": { "value": 26.0 },
        "carbs": { "value": 65.0 },
        "protein": { "value": 7.0 },
        "sodium": { "value": 54.0 },
        "calcium": { "value": 12.0 },
        "iron": { "value": 6.0 }
    }
    ];

    //$http.get('desserts.json').then(function (desserts) {
    //    $scope.desserts = desserts.data;

    //    // $scope.selected.push($scope.desserts.data[1]);

    //    // $scope.selected.push({
    //    //   name: 'Ice cream sandwich',
    //    //   type: 'Ice cream',
    //    //   calories: { value: 237.0 },
    //    //   fat: { value: 9.0 },
    //    //   carbs: { value: 37.0 },
    //    //   protein: { value: 4.3 },
    //    //   sodium: { value: 129.0 },
    //    //   calcium: { value: 8.0 },
    //    //   iron: { value: 1.0 }
    //    // });

    //    // $scope.selected.push({
    //    //   name: 'Eclair',
    //    //   type: 'Pastry',
    //    //   calories: { value:  262.0 },
    //    //   fat: { value: 16.0 },
    //    //   carbs: { value: 24.0 },
    //    //   protein: { value:  6.0 },
    //    //   sodium: { value: 337.0 },
    //    //   calcium: { value:  6.0 },
    //    //   iron: { value: 7.0 }
    //    // });

    //    // $scope.promise = $timeout(function () {
    //    //   $scope.desserts = desserts.data;
    //    // }, 1000);
    //});

    $scope.editComment = function (event, dessert) {
        event.stopPropagation();

        var dialog = {
            // messages: {
            //   test: 'I don\'t like tests!'
            // },
            modelValue: dessert.comment,
            placeholder: 'Add a comment',
            save: function (input) {
                dessert.comment = input.$modelValue;
            },
            targetEvent: event,
            title: 'Add a comment',
            validators: {
                'md-maxlength': 30
            }
        };

        var promise = $scope.options.largeEditDialog ? $mdEditDialog.large(dialog) : $mdEditDialog.small(dialog);

        promise.then(function (ctrl) {
            var input = ctrl.getInput();

            input.$viewChangeListeners.push(function () {
                input.$setValidity('test', input.$modelValue !== 'test');
            });
        });
    };

    $scope.toggleLimitOptions = function () {
        $scope.limitOptions = $scope.limitOptions ? undefined : [5, 10, 15];
    };

    $scope.getTypes = function () {
        return ['Candy', 'Ice cream', 'Other', 'Pastry'];
    };

    $scope.onPaginate = function (page, limit) {
        console.log('Scope Page: ' + $scope.query.page + ' Scope Limit: ' + $scope.query.limit);
        console.log('Page: ' + page + ' Limit: ' + limit);

        $scope.promise = $timeout(function () {

        }, 2000);
    };

    $scope.deselect = function (item) {
        console.log(item.name, 'was deselected');
    };

    $scope.log = function (item) {
        console.log(item.name, 'was selected');
    };

    $scope.loadStuff = function () {
        $scope.promise = $timeout(function () {

        }, 2000);
    };

    $scope.onReorder = function (order) {

        console.log('Scope Order: ' + $scope.query.order);
        console.log('Order: ' + order);

        $scope.promise = $timeout(function () {

        }, 2000);
    };

    //$rootScope.$emit('someEvent22', { data: ["1", "12"] });
    $rootScope.$on(['someEvent22', function (event, data) {
        $scope.some();
    }]);

    $scope.some = function()
    {

        alert(data.data[0]);
    }


    $scope.$on('initiateEvent', function (event, b) {
        alert("padre");
        $scope.$broadcast('someEvent', 'bidule');
    });

    



    



}]);

BaseMVC.controller('PerfilCTRL', ['$http', '$scope', '$mdDialog', '$rootScope', 'items', 'Area', function ($http, $scope, $mdDialog, $rootScope, items, Area) {
    console.log(items);
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };
    $scope.lstPerfiles = [];
    $scope.lstPerfiles = items;
    $scope.Perfil = $scope.lstPerfiles[0];

    $scope.AgregarPerfil = function (Perfil) {
        $http({
            url: '/Admin/Configuracion/AgregarPerfilArea',
            method: "POST",
            params: {
                'nIdArea': Area.nIdArea,
                'nIdPerfil': Perfil.nIdPerfil
            },
        }).success(function (response) {

           
            $scope.$on('someEvent', function (event, b) {
                $scope.content = b;
                alert(b);
            });
            $scope.$emit('initiateEvent', null);

            $rootScope.$emit('someEvent22', { data: ["1", "12"] });

            $mdDialog.cancel();

        }).error(function (error) {
        });

    }

    $scope.AgregarPerfilUsuario = function (Perfil) {
        $http({
            url: '/Admin/Configuracion/AgregarPerfilusuario',
            method: "POST",
            params: {
                'nIdPerfil': Perfil.nIdPerfil,
                'cLogin': Area.cLogin
            },
        }).success(function (response) {
            $mdDialog.cancel();

          





        }).error(function (error) {
        });

    }
}]);
function DialogController($scope, $mdDialog, cNombreArea) {
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



//var PerfilCTRL = ['$scope', '$mdDialog', 'items', 'Area', '$http', '$rootScope', function ($scope, $mdDialog, items, Area, $http, $rootScope) {
//    console.log(items);
//    $scope.hide = function () {
//        $mdDialog.hide();
//    };
//    $scope.cancel = function () {
//        $mdDialog.cancel();
//    };
//    $scope.answer = function (answer) {
//        $mdDialog.hide(answer);
//    };
//    $scope.lstPerfiles = [];
//    $scope.lstPerfiles = items;
//    $scope.Perfil = $scope.lstPerfiles[0];

//    $scope.AgregarPerfil = function (Perfil) {
//        $http({
//            url: '/Admin/Configuracion/AgregarPerfilArea',
//            method: "POST",
//            params: {
//                'nIdArea': Area.nIdArea,
//                'nIdPerfil': Perfil.nIdPerfil
//            },
//        }).success(function (response) {
            
//            //$scope.$emit('someEvent22', ["1", "12"]);
//            //console.log($scope.$emit('someEvent22', { data: ["1", "12"] }));

//            $scope.$on('someEvent', function (event, b) {
//                $scope.content = b;
//                alert(b);
//            });
//            $scope.$emit('initiateEvent', null);

//            $rootScope.$emit('someEvent22', { data: ["1", "12"] });

//            $mdDialog.cancel();

//        }).error(function (error) {
//            //swal('¡Error!', 'Algo salió mal', 'error');
//        });
//        //alert(Perfil.cPerfil + "--" + Area.cNombreArea);

//    }

//    $scope.AgregarPerfilUsuario = function (Perfil) {
//        $http({
//            url: '/Admin/Configuracion/AgregarPerfilusuario',
//            method: "POST",
//            params: {
//                'nIdPerfil': Perfil.nIdPerfil,
//                'cLogin': Area.cLogin
//            },
//        }).success(function (response) {
//            $mdDialog.cancel();
            
//            //$scope.$emit('someEvent22', ["1", "12"]);
//            //console.log($scope.$emit('someEvent22', { data: ["1", "12"] }));
//            //$rootScope.$emit('someEvent22', { data: ["1", "12"] });

            



//        }).error(function (error) {
//            //swal('¡Error!', 'Algo salió mal', 'error');
//        });
//        //alert(Perfil.cPerfil + "--" + Area.cNombreArea);

//    }

//}];