﻿app.controller('accountCtrl', ['$scope', '$http', '$timeout', 'uiGridConstants', 'uiGridGroupingConstants', '$compile', '$window', function ($scope, $http, $timeout, uiGridConstants, uiGridGroupingConstants, $compile, $window) {
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
   

    $scope.openModalAddEdit = function () {
        $('#accountAddEditModal').modal('show');
        $('#depositModal').css("display", "none");
        $('#paymentModal').css("display", "none");
        $('#transferModal').css("display", "none");
        $('#withdrawModal').css("display", "none");
    }

    $scope.makeTransaction = function (transactionTypeId,Name) {
        debugger;
        $scope.selectedTransactionId = transactionTypeId;
        var selectedTransactionName = Name;
        if (Name == "Deposit") {
            $('#accountAddEditModal').css("display", "none");
            $('#paymentModal').css("display", "none");
            $('#transferModal').css("display", "none");
            $('#withdrawModal').css("display", "none");

            $('#depositModal').modal('show');
            
        }
        if (Name == "Payment") {
            $('#accountAddEditModal').css("display", "none");
            $('#depositModal').css("display", "none");
            $('#transferModal').css("display", "none");
            $('#withdrawModal').css("display", "none");

            $('#paymentModal').modal('show');
        }
        if (Name == "Transfer") {
            $('#accountAddEditModal').css("display", "none");
            $('#depositModal').css("display", "none");
            $('#paymentModal').css("display", "none");
            $('#withdrawModal').css("display", "none");

            $('#transferModal').modal('show');
        }
        if (Name == "Withdraw") {
            $('#accountAddEditModal').css("display", "none");
            $('#depositModal').css("display", "none");
            $('#paymentModal').css("display", "none");
            $('#transferModal').css("display", "none");

            $('#withdrawModal').modal('show');
        }

    }

    $scope.deleteAccount = function (p) {
       // window.location.assign("/Account/DeleteAccount?accountId=" + $scope.SelectedAccountPk);
        var data = {

            accountId: $scope.SelectedAccountPk
        };
        $http.post('/Account/DeleteAccount', data)
            .then(function (data) {
               
                $window.location.assign("/Account/Index");

            }), function () {
                $scope.addAccountForm.Error = true;

            };
    }

    $scope.performAction = function(accountId,actionId) {
        if (actionId == 1 && accountId != null && accountId != '00000000-0000-0000-0000-000000000000') {
            $scope.SelectedAccountPk = accountId;
            $scope.IsDisabled = true;
            $scope.getAccountDetailById($scope.SelectedAccountPk);
            //window.location.assign("/Account/ViewDetail?accountId=" + accountId );

        }
        if (actionId == 2 && accountId != null && accountId != '00000000-0000-0000-0000-000000000000') {
            //window.location.assign("/Account/AddEdit?accountId=" + accountId );
            $scope.SelectedAccountPk = accountId;
            $scope.IsDisabled = false;
            $scope.getAccountDetailById($scope.SelectedAccountPk);

        }
        if (actionId == 3 && accountId != null && accountId != '00000000-0000-0000-0000-000000000000') {
            //deleteAccountModal
            $scope.SelectedAccountPk = accountId;
            $('#deleteAccountModal').modal('show');
            $('#accountAddEditModal').css("display", "none");
            $('#depositModal').css("display", "none");
            $('#paymentModal').css("display", "none");
            $('#transferModal').css("display", "none");
            $('#withdrawModal').css("display", "none");
            

        }
    }

    $scope.depositTransaction = function () {
        if ($scope.depositTransactionForm.$invalid || $scope.Amount <= 0) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            TransAmount: $scope.Amount,
            IsCreditTrans: true,
            IsDebitTrans: false,
            AccountFk: $scope.SelectedAccount != null ? $scope.SelectedAccount.AccountPk : 0,
            TransactionTypeFk: $scope.selectedTransactionId,

        };
        $http.post('/Transaction/SaveUpdateTransaction', data)
            .then(function (data) {
                if (data != '00000000-0000-0000-0000-000000000000') {
                    $window.location.assign("/Account/Index");

                }
                else {
                    $scope.accountTypeForm.Success = true;


                }
                $scope.Amount = 0;
                //$scope.clearAccountTypeData();

            }), function () {
                $scope.accountTypeForm.Error = true;

            };


    };

    $scope.paymentTransaction = function () {
        if ($scope.paymentTransactionForm.$invalid || $scope.Amount <= 0) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            TransAmount: $scope.Amount,
            ReferenceAccountFk: $scope.SelectedBeneficiaryAccount != null ? $scope.SelectedBeneficiaryAccount.AccountPk : 0,
            AccountFk: $scope.SelectedAccount != null ? $scope.SelectedAccount.AccountPk : 0,
            Reference: $scope.Reference,
            TransactionTypeFk: $scope.selectedTransactionId,

        };
        $http.post('/Transaction/SaveUpdatePaymentTransaction', data)
            .then(function (data) {
                if (data != '00000000-0000-0000-0000-000000000000') {
                    $window.location.assign("/Account/Index");

                }
                else {
                    $scope.accountTypeForm.Success = true;


                }

                //$scope.clearAccountTypeData();

            }), function () {
                $scope.accountTypeForm.Error = true;

            };


    };

    $scope.transferTransaction = function () {
        if ($scope.transferTransactionForm.$invalid || $scope.TransferAmount <= 0) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            TransAmount: $scope.TransferAmount,
            ReferenceAccountFk: $scope.SelectedTransferBeneficiaryAccount != null ? $scope.SelectedTransferBeneficiaryAccount.AccountPk : 0,
            AccountFk: $scope.SelectedTransferAccount != null ? $scope.SelectedTransferAccount.AccountPk : 0,
            TransactionTypeFk: $scope.selectedTransactionId,

        };
        $http.post('/Transaction/SaveUpdatePaymentTransaction', data)
            .then(function (data) {
                if (data != '00000000-0000-0000-0000-000000000000') {
                    $window.location.assign("/Account/Index");

                }
                else {
                    $scope.accountTypeForm.Success = true;


                }

                //$scope.clearAccountTypeData();

            }), function () {
                $scope.accountTypeForm.Error = true;

            };


    };

    $scope.withdrawTransaction = function () {
        if ($scope.withdrawTransactionForm.$invalid || $scope.WithdrawAmount <= 0 || $scope.SelectedWithdrawAccount == null) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            TransAmount: (-1)* $scope.WithdrawAmount,
            AccountFk: $scope.SelectedWithdrawAccount != null ? $scope.SelectedWithdrawAccount.AccountPk : '00000000-0000-0000-0000-000000000000',
            TransactionTypeFk: $scope.selectedTransactionId,

        };
        $http.post('/Transaction/SaveUpdateTransaction', data)
            .then(function (data) {
                if (data != '00000000-0000-0000-0000-000000000000') {
                    $window.location.assign("/Account/Index");

                }
                else {
                    $scope.accountTypeForm.Success = true;


                }

                //$scope.clearAccountTypeData();

            }), function () {
                $scope.accountTypeForm.Error = true;

            };


    };


   
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
       
        { field: "AccountName", name: "Title", enableColumnMenu: false, width: "16%", cellTooltip: true },
        { field: "AccountTypeName", name: "Type", enableColumnMenu: false, width: "17%", cellTooltip: true },
        { field: "AccountNumber", name: "Account Number", enableColumnMenu: false, width: "17%", cellTooltip: true },
        { field: "Balance", name: "Balance", enableColumnMenu: false, width: "17%", cellTooltip: true },
        { field: "", name: "Actions", enableColumnMenu: false, width: "33%", cellTemplate: '<div class="btn-group pull-left" data-toggle="buttons-radio"><button style="margin-left:6px" type ="button" ng-click="grid.appScope.performAction(row.entity.AccountPk,1)" class= "btn-info" >Detail</button><button style="margin-left:6px" ng-click="grid.appScope.performAction(row.entity.AccountPk,2)" type="button" class="btn-warning">Update</button><button style="margin-left:6px" ng-click="grid.appScope.performAction(row.entity.AccountPk,3)" type="button" class="btn-inverse">Block</button></div >' },
        //{ field: "CreateDate", name: "Creation Date", cellTemplate: '<div class="ui-grid-cell-contents"><b class="label green pull-right">{{row.entity.Balance}}</b></div>',enableColumnMenu: false },
        //{ field: "Status", name: "Void Category", enableColumnMenu: false, width: "10%" },
        //{ field: "DaysVoid", enableColumnMenu: false, width: "10%" },
        //{ field: "RentLostToDate", name: "Income Lost", enableColumnMenu: false, width: "12%" },
        //'<div class="btn-group pull-right" data-toggle="buttons-radio">< button type = "button" class= "btn" >All</button ><button type="button" class="btn">Male</button><button type="button" class="btn">Female</button></div >'
    ];
    $scope.accountGrid = {
        enableFiltering: false,
        enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
        //enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        multiSelect: false,
        columnDefs: columnDefinitions
    };
    $scope.accountGrid.onRegisterApi = function (gridApi) {
       
        //set gridApi on scope
        $scope.accountGridApi = gridApi;
        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            window.location.assign("/Account/AddEdit?accountId=" + row.entity.AccountPk);
            //window.location.assign(appHostName + '/Property/Details/' + row.entity.PropertyCode + '?unitCode=' + row.entity.Code + '#!tab-lease');
        });
    };

   
    $scope.getAccountAll = function () {
        var url = '/Account/GetAllUserAccount';
        $http.get(url)
            .then(function (data) {
                $scope.accountList = data.data;
                $scope.accountGrid.data = $scope.accountList;
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

            AccountPk: $scope.AccountPk,
            AccountName: $scope.AccountName,
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

    $scope.getAccountTypeddl = function () {
        var url = '/Admin/GetAccountTypeAll';
        $http.get(url)
            .then(function (data) {
                $scope.accountList = data.data;
                $scope.accountTypeList = $scope.accountList;
                // $scope.$apply();

            }), function () {
                $scope.accountTypeForm.Error = true;

            };

    }

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
        var url = '/Account/GetAllTransferBeneficiaryAccount?selectedAccountId=' + ($scope.SelectedTransferAccount != null ? $scope.SelectedTransferAccount.AccountPk : '00000000-0000-0000-0000-000000000000');
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
    
    $scope.getAccountTypeddl();
    $scope.getTransactionTypeddl();
    $scope.getAccountAll();


    $scope.getAccountddl();
    $scope.GetAllBeneficiaryAccountddl();
   
    

}]);




