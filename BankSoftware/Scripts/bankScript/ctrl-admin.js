app.controller('adminCtrl', ['$scope', '$http', '$timeout', 'uiGridConstants', 'uiGridGroupingConstants', '$compile', '$window', function ($scope, $http, $timeout, uiGridConstants, uiGridGroupingConstants, $compile, $window) {
    $scope.validationErrors = false;
    $scope.Error = false;
    $scope.Success = false;

    $scope.loginValidationErrors = false;
    $scope.loginError = false;
    $scope.loginSuccess = false;
    $scope.genderList = [{ "Id": "Male", "Name": "Male" }, { "Id": "Female", "Name": "Female" }];
    $scope.clearAccountTypeData = function () {
        $scope.Name = null;
        $scope.Description = null;
    }

   
    $scope.saveAccountType = function () {
        if ($scope.accountTypeForm.$invalid) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            UserPk: $scope.UserPk,
            Description: $scope.Description

        };
        $http.post('/Admin/SaveUpdateAccountType', data)
            .then(function (data) {
                debugger;
                if (data == 2) {
                    $scope.accountTypeForm.Success = true;
                    
                }
                else {
                    $scope.accountTypeForm.Success = true;
                   

                }

                $scope.clearAccountTypeData();
                $window.location.assign("/Admin/AccountType");

            }),function () {
                $scope.accountTypeForm.Error = true;
               
            };


    };

    var columnDefinitions = [
        { field: "Name", name: "AccountName", enableColumnMenu: false, width: "16%", cellTooltip: true },
        { field: "Description", name: "Account Descriptoin", enableColumnMenu: false, cellTooltip: true },
    ];
    $scope.availableAccountTypeGrid = {
        enableFiltering: false,
        enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
        //enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        multiSelect: false,
        columnDefs: columnDefinitions
    };
    $scope.availableAccountTypeGrid.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.availableAccountTypeGridApi = gridApi;
        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            window.location.assign("/Home/Index");
            //window.location.assign(appHostName + '/Property/Details/' + row.entity.PropertyCode + '?unitCode=' + row.entity.Code + '#!tab-lease');
        });
    };

   
    $scope.getAccountTypeAll = function () {
        var url = '/Admin/GetAccountTypeAll';
        $http.get(url)
            .then(function (data) {
                debugger;
                $scope.accountTypeList = data.data;
                $scope.availableAccountTypeGrid.data = $scope.accountTypeList;
               // $scope.$apply();

            }), function () {
                $scope.accountTypeForm.Error = true;

            };
            
    }

    $scope.getAccountTypeAll();

}]);




