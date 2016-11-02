var BaseMVC = angular.module('BaseMVC', ['ngMaterial', 'md.data.table', 'ngMessages', 'ngTagsInput', 'ngFileUpload', 'ui.layout', 'ngAnimate', 'ngGrid', 'ngSanitize', 'ui.grid', 'ngCsv']);
BaseMVC.controller('MainCtrl', ['$scope', '$http', 'Upload', function ($scope, $http, $upload) {
}]);
BaseMVC.config(['$mdThemingProvider', function ($mdThemingProvider) {
	var customPrimary = {
		'50': '#33fcff',
		'100': '#1afbff',
		'200': '#00fbff',
		'300': '#00e2e6',
		'400': '#00c9cc',
		'500': '#00B0B3',
		'600': '#009799',
		'700': '#007e80',
		'800': '#006566',
		'900': '#004c4d',
		'A100': '#4dfcff',
		'A200': '#66fcff',
		'A400': '#80fdff',
		'A700': '#003333'
	};
	$mdThemingProvider
        .definePalette('customPrimary',
                        customPrimary);

	var customAccent = {
		'50': '#062b2c',
		'100': '#094143',
		'200': '#0b585a',
		'300': '#0e6e70',
		'400': '#118487',
		'500': '#149a9d',
		'600': '#1ac6cb',
		'700': '#1ddce1',
		'800': '#33e0e5',
		'900': '#49e4e8',
		'A100': '#1ac6cb',
		'A200': '#17B0B4',
		'A400': '#149a9d',
		'A700': '#60e7eb'
	};
	$mdThemingProvider
        .definePalette('customAccent',
                        customAccent);

	var customWarn = {
		'50': '#fbb4af',
		'100': '#f99d97',
		'200': '#f8877f',
		'300': '#f77066',
		'400': '#f55a4e',
		'500': '#F44336',
		'600': '#f32c1e',
		'700': '#ea1c0d',
		'800': '#d2190b',
		'900': '#ba160a',
		'A100': '#fccbc7',
		'A200': '#fde1df',
		'A400': '#fff8f7',
		'A700': '#a21309'
	};
	$mdThemingProvider
        .definePalette('customWarn',
                        customWarn);

	var customBackground = {
		'50': '#737373',
		'100': '#666666',
		'200': '#595959',
		'300': '#4d4d4d',
		'400': '#404040',
		'500': '#333',
		'600': '#262626',
		'700': '#1a1a1a',
		'800': '#0d0d0d',
		'900': '#000000',
		'A100': '#808080',
		'A200': '#8c8c8c',
		'A400': '#999999',
		'A700': '#000000'
	};
	$mdThemingProvider
        .definePalette('customBackground',
                        customBackground);

	$mdThemingProvider.theme('default')
		.primaryPalette('customPrimary')
		.accentPalette('customAccent')
		.warnPalette('customWarn')
		//.backgroundPalette('customBackground')
}]);
BaseMVC.factory("Fun", function () {
    return {
        MensajeGenerico: function ($mdDialog, Encabezado, Mensaje) {
                    $mdDialog.show(
                          $mdDialog.alert()
                            .parent(angular.element(document.querySelector('#Body_Layout')))
                            .clickOutsideToClose(true)
                            .title(Encabezado)
                            .textContent(Mensaje)
                            .ariaLabel('Alert Dialog Demo')
                            .ok('Entendido')
                        );
        },
        Base64: function (encoded) {
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
        },

        alerta: function (url) {
            alert(this.Base64(url));
        },

        http2: function ($http, url, Metodo, Token, data) {
            var Response = {};
            $http({
                headers: {
                    'RequestVerificationToken': Token
                },
                url: this.Base64(url) + Metodo,
                method: "POST",
                params: data,
            }).success(function (response) {
                debugger
                Response = response;
            }).error(function (error) {
                alert("asd");
            });

            return Response;
        }
    };
})

function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
}

function SetPreloader() { //quitar hardcode pendiente
    $('#preloader').show();
}
function HidePreloader() {
    $('#preloader').hide()
}

function OpenClickDatepicker(DateEditInput) {
    $('#' + DateEditInput).focus()
};

function parseJsonDate(jsonDateString) {
    return new Date(parseInt(jsonDateString.replace('/Date(', '')));
}

function QuitarMsgErr(IdDiv) {
    $("#" + IdDiv).html('');
}
