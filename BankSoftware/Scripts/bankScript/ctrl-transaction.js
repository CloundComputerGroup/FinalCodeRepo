app.controller('transactionCtrl', ['$scope', '$http', '$timeout', 'uiGridConstants', 'uiGridGroupingConstants', '$compile', '$window', function ($scope, $http, $timeout, uiGridConstants, uiGridGroupingConstants, $compile, $window) {
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
    $scope.openAccountRequest = function () {
       // $scope.getAccountTypeddl();
        $window.location.assign("/Account/AddEdit");
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
       
        { field: "AccountName", name: "Title", enableColumnMenu: false, width: "14%", cellTooltip: true },
        { field: "TransactionTypeName", name: "Trans Type", enableColumnMenu: false, width: "14%", cellTooltip: true },
        { field: "AccountNumber", name: "Account #", enableColumnMenu: false, width: "14%", cellTooltip: true },
        { field: "TransAmount", name: "Amount", enableColumnMenu: false, width: "12%", cellTooltip: true },
        { field: "TransDateFormated", name: "TransDate", enableColumnMenu: false, width: "17%", cellTooltip: true },
        { field: "TransNature", name: "Transaction", enableColumnMenu: false, width: "14%", cellTooltip: true },
        { field: "", name: "Actions", enableColumnMenu: false, width: "15%", cellTemplate: '<div class="btn-group pull-left" data-toggle="buttons-radio"><button style="margin-left:6px" type ="button" ng-click="grid.appScope.performAction(row.entity.TransactionPk)" class= "btn-success" >Reciept</button></div >' }
        ];
    $scope.transactionGrid = {
        enableFiltering: true,
        enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
        //enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        multiSelect: false,
        columnDefs: columnDefinitions
    };
    $scope.transactionGrid.onRegisterApi = function (gridApi) {
       
        //set gridApi on scope
        $scope.transactionGridApi = gridApi;
        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            window.location.assign("/Account/AddEdit?accountId=" + row.entity.AccountPk);
            //window.location.assign(appHostName + '/Property/Details/' + row.entity.PropertyCode + '?unitCode=' + row.entity.Code + '#!tab-lease');
        });
    };
    $scope.generateReciept = function (transaction) {

    }

   
    $scope.getAccountTransactionAll = function () {
        debugger;
        var url = '/Transaction/GetAllTransaction?accountId=' + $scope.SelectedAccount.AccountPk;
        $http.get(url)
            .then(function (data) {
                $scope.transactionList = data.data;
                $scope.transactionGrid.data = $scope.transactionList;
                //$scope.accountTypeList = $scope.accountList;
               // $scope.$apply();

            }), function () {
                $scope.accountTypeForm.Error = true;

            };
            
    }


    $scope.clearAccountData = function () {
        $scope.SelectedAccountType = 0;
        $scope.Balance = 0;
        $scope.SecurityPIN = 0;
        $scope.AccountPk = '00000000-0000-0000-0000-000000000000';
    }
    $scope.accountRequest = function () {
        debugger;
        if ($scope.addAccountForm.$invalid) {
            $scope.validationErrors = true;
            return;
        }
        var data = {
            AccountName: $scope.AccountName,
            AccountPk: $scope.AccountPk,
            Balance: $scope.Balance,
            SecurityPIN: $scope.SecurityPIN,
            AccountTypeFk: $scope.SelectedAccountType != null ? $scope.SelectedAccountType.AccountTypePk : 0,

        };
        $http.post('/Account/SaveUpdateAccount', data)
            .then(function (data) {
                if (data == 2) {
                    $scope.addAccountForm.Success = true;

                }
                else {
                    $scope.addAccountForm.Success = true;


                }

                $scope.clearAccountData();
                $window.location.assign("/Account/Index");

            }), function () {
                $scope.addAccountForm.Error = true;

            };


    };
    //GetAllUserAccount
    $scope.getTransactionTypeddl = function () {
        var url = '/Account/GetAllTransactionTypes';
        $http.get(url)
            .then(function (data) {
                $scope.transactionTypeList = data.data;

            }), function () {
                $scope.accountTypeForm.Error = true;

            };

    }

    $scope.getAccountddl = function () {
        var url = '/Account/GetAllUserAccount';
        $http.get(url)
            .then(function (data) {
                $scope.userAccountList = data.data;

            }), function () {
                $scope.accountTypeForm.Error = true;

            };

    }

    $scope.GetAllBeneficiaryAccountddl = function () {
        var url = '/Account/GetAllBeneficiaryAccount';
        $http.get(url)
            .then(function (data) {
                $scope.beneficiaryAccountList = data.data;
                $scope.beneficiaryTransferAccountList = data.data;

            }), function () {
                $scope.accountTypeForm.Error = true;

            };

    }

    

    $scope.GetAllTransferBeneficiaryAccount = function () {
        var url = '/Account/GetAllTransferBeneficiaryAccount?selectedAccountId=' + ($scope.SelectedTransferAccount != null ? $scope.SelectedTransferAccount.AccountPk : '00000000-0000-0000-0000-000000000000') ;
        $http.get(url)
            .then(function (data) {
                $scope.beneficiaryTransferAccountList = data.data;

            }), function () {
                $scope.accountTypeForm.Error = true;

            };

    }
    $scope.accountChange = function () {
        $scope.GetAllTransferBeneficiaryAccount();

    }
    $scope.getAccountDetailById = function (accountId) {
      //  var accountId = $("#AccountId").val();
        
        //debugger;
        //IsDisabled
        if (accountId != null && accountId != '00000000-0000-0000-0000-000000000000') {
            var url = '/Account/GetAccountById?accountId=' + accountId;
            $http.get(url)
                .then(function (data) {
                    $scope.accountDetail = data.data;
                    $scope.Balance = $scope.accountDetail.Balance;
                    $scope.AccountPk = $scope.accountDetail.AccountPk;
                    $scope.SecurityPIN = $scope.accountDetail.SecurityPIN;
                    debugger;
                    $scope.SelectedAccountType = $scope.accountTypeList.filter(function (item) {
                        return item.AccountTypePk == $scope.accountDetail.AccountTypeFk
                    })[0];
                  // $scope.$apply();
                    $('#accountAddEditModal').modal('show');
                   // modal.show();
                }), function () {
                    $scope.accountTypeForm.Error = true;

                };
        }
       

    }
    $scope.getTransactionTypeddl();
    $scope.getAccountddl();
    $scope.GetAllBeneficiaryAccountddl();
    //$scope.getAccountTransactionAll();
   
    

}]);




