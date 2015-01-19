﻿app.controller('editContentController', ['$scope', '$location', 'toastrService', 'contentService', 'cultureService', 'siteService',
    function ($scope, $location, toastrService, contentService, cultureService, siteService) {

        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $scope.$apply(function () {
                    $scope.showSaveButton = true;
                });
            }
            else {
                $scope.$apply(function () {
                    $scope.showSaveButton = false;
                });
            }
        });
        var noAdmin = $location.search()["noAdmin"];
        if (noAdmin != null) {
            $scope.isAdmin = false;
        }
        $scope.get = function () {
            contentService.getTree().then(function (data) {
                $scope.data = data;
                $scope.updateHintRemaining();
            });
        };
        $scope.save = function () {
            contentService.saveTree($scope.data).then(function (data) {
                toastrService.success("Cambios guardados exitosamente");
            });
        }
        $scope.updateHintRemaining = function () {
            //var _remaining = 0;
            //angular.forEach($scope.data, function (item) {
            //    if (item.Hint == null) {
            //        _remaining++;
            //    }
            //});
            //angular.forEach($scope.dataShowInContent, function (item) {
            //    if (item.Hint == null) {
            //        _remaining++;
            //    }
            //});
            //$scope.hintRemain = _remaining;
        };
        $scope.duplicate = function (item) {
            contentService.duplicate(item).then(function (data) {
                $scope.get();
                toastrService.success("Nuevo " + item.Hint + " creado");
            });
        };
        $scope.delete = function (item) {
            if (confirm("¿Estás seguro que quieres eliminar esta propiedad?")) {
                contentService.remove(item).then(function (data) {
                    $scope.get();
                    toastrService.success("Propiedad eliminada");
                });
            }
        };
        /*INIT*/
        $scope.init = function () {
            $scope.get();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);
