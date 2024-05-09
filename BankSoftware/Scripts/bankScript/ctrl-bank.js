app.controller('bankCtrl', ['$scope', '$http', '$timeout', 'uiGridConstants', '$compile', '$window', function ($scope, $http, $timeout, uiGridConstants, $compile, $window) {

    $scope.loadSignIn = function () {
        $window.location.assign("/User/Logout");
    }

    $scope.loadDashboard = function () {
        $window.location.assign("/Home/Index");
    }
    $scope.loadAccountType = function () {
        $window.location.assign("/Admin/AccountTypeIndex");
    }
    $scope.loadAccount = function () {
        $window.location.assign("/Account/Index");
    }
    $scope.loadTransaction = function () {
        $window.location.assign("/Transaction/Index");
    }
    $scope.debitcreditCardLoad = function () {
        $window.location.assign("/Resource/CardIndex");
    }
    $scope.chequebookLoad = function () {
        $window.location.assign("/Resource/Chequebook");
    }
    $scope.loadRecurringPayment = function () {
        $window.location.assign("/RecurringPayment/Index");
    }


}]);




