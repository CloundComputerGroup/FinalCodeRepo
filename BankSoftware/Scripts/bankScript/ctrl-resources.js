app.controller('resourcesCtrl', ['$scope', '$http', '$timeout', 'uiGridConstants', 'uiGridGroupingConstants', '$compile', '$window', function ($scope, $http, $timeout, uiGridConstants, uiGridGroupingConstants, $compile, $window) {
    $scope.validationErrors = false;
    $scope.Error = false;
    $scope.Success = false;

    $scope.loginValidationErrors = false;
    $scope.loginError = false;
    $scope.loginSuccess = false;
    $scope.chequebookLeavesList = [{ "Id": "20", "Name": "20" }, { "Id": "50", "Name": "50" }, { "Id": "100", "Name": "100" }];
    $scope.clearAccountTypeData = function () {
        $scope.Name = null;
        $scope.Description = null;
    }
    $scope.openAccountRequest = function () {
       // $scope.getAccountTypeddl();
        $window.location.assign("/Account/AddEdit");
    }

    $scope.opendebitcreditModal = function () {
        $('#requestdebitcreditModal').modal('show');
        $('#holdDebitCreditModal').css("display", "none");
        $('#blockCardModal').css("display", "none");
        $('#requestChequeBookModal').css("display", "none");
        $('#blockChequeBookModal').css("display", "none");
    }

    $scope.opendebitcreditHoldModal = function () {
        $('#holdDebitCreditModal').modal('show');
        $('#requestdebitcreditModal').css("display", "none");
        $('#blockCardModal').css("display", "none");
        $('#requestChequeBookModal').css("display", "none");
        $('#blockChequeBookModal').css("display", "none");
    }

    $scope.openCardBlockModal = function (cardResourceId) {
        $('#blockCardModal').modal('show');
        $('#holdDebitCreditModal').css("display", "none");
        $('#requestdebitcreditModal').css("display", "none");
        $('#requestChequeBookModal').css("display", "none");
        $('#blockChequeBookModal').css("display", "none");
        $scope.cardResourceId = cardResourceId;
    }

    $scope.openChequeBookModal = function () {
        $('#requestChequeBookModal').modal('show');
        $('#requestdebitcreditModal').css("display", "none");
        $('#holdDebitCreditModal').css("display", "none");
        $('#blockCardModal').css("display", "none");
        $('#blockChequeBookModal').css("display", "none");
    }

    $scope.openCardBlockModal = function (cardResourceId) {
        $('#blockChequeBookModal').modal('show');
        $('#requestChequeBookModal').css("display", "none");
        $('#requestdebitcreditModal').css("display", "none");
        $('#holdDebitCreditModal').css("display", "none");
        $('#blockCardModal').css("display", "none");
        $scope.CBResourceId = cardResourceId;
    }

    $scope.openCardPINModal = function (cardResourceId) {
        var url = '/Resource/GetResourceById?resourceId=' + cardResourceId;
        $http.get(url)
            .then(function (data) {
                $scope.cardDetail = data.data;

                $('#cardPINChangeModal').modal('show');
                $('#holdDebitCreditModal').css("display", "none");
                $('#requestdebitcreditModal').css("display", "none");
                $('#blockCardModal').css("display", "none");
                $scope.cardPINChangeResourceId = cardResourceId;

            }), function () {
                $scope.cardDetail = null;

            };
        
    }

    $scope.openChequBookModal = function () {
        $('#requestdebitcreditModal').modal('show');
        $('#holdDebitCreditModal').css("display", "none");
    }

   //Chequebook Script
    $scope.requestChequeBook = function () {
        if ($scope.requestChequeBookForm.$invalid) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            AccountFk: $scope.SelectedUserAccount != null ? $scope.SelectedUserAccount.AccountPk : 0,
            CheckbookNumber: $scope.SelectedLeavesNumber != null ? $scope.SelectedLeavesNumber.Id : 0,

        };
        $http.post('/Resource/SaveUpdateResource', data)
            .then(function (data) {
                //if (data == 2) {
                //    $scope.accountTypeForm.Success = true;
                    
                //}
                //else {
                //    $scope.accountTypeForm.Success = true;
                   

                //}

                $window.location.assign("/Resource/Chequebook");

            }),function () {
                $scope.accountTypeForm.Error = true;
               
            };


    };

    $scope.holdChequeBook = function () {
        if ($scope.holdChequeBookForm.$invalid) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            AccountFk: $scope.SelectedHoldUserAccount != null ? $scope.SelectedHoldUserAccount.AccountPk : 0,
            CheckbookNumber: $scope.SelectedHoldLeavesNumber != null ? $scope.SelectedHoldLeavesNumber.Id : 0,
            IsActive: $scope.IsActive,

        };
        $http.post('/Resource/SaveUpdateResource', data)
            .then(function (data) {
                //if (data == 2) {
                //    $scope.accountTypeForm.Success = true;

                //}
                //else {
                //    $scope.accountTypeForm.Success = true;


                //}

               // $scope.clearAccountTypeData();
                $window.location.assign("/Resource/Chequebook");

            }), function () {
                $scope.accountTypeForm.Error = true;

            };


    };
    

    var columnDefinitions = [
       
        { field: "ResourceAccount.AccountName", name: "Chequebook", enableColumnMenu: false, width: "16%", cellTooltip: true },
        { field: "ResourceTypeName", name: "Resource", enableColumnMenu: false, width: "14%", cellTooltip: true },
        { field: "ResourceAccount.AccountNumber", name: "Account #", enableColumnMenu: false, width: "13%", cellTooltip: true },
        { field: "ActiveStatus", name: "Status", enableColumnMenu: false, width: "14%", cellTooltip: true },
        { field: "IssuedDate", name: "IssuedDate", enableColumnMenu: false, width: "19%", cellTooltip: true },
        { field: "", name: "Actions", enableColumnMenu: false, width: "20%", cellTemplate: '<div class="btn-group pull-left" data-toggle="buttons-radio"><button style="margin-left:6px" type ="button" ng-click="grid.appScope.openCardBlockModal(row.entity.ResourcePk)" class= "btn-info" >Block</button></div >' },

    ];
    $scope.chequebookGrid = {
        enableFiltering: false,
        enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
        //enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        multiSelect: false,
        columnDefs: columnDefinitions
    };
    $scope.chequebookGrid.onRegisterApi = function (gridApi) {
       
        //set gridApi on scope
        $scope.chequebookGridApi = gridApi;
        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            window.location.assign("/Account/AddEdit?accountId=" + row.entity.AccountPk);
            //window.location.assign(appHostName + '/Property/Details/' + row.entity.PropertyCode + '?unitCode=' + row.entity.Code + '#!tab-lease');
        });
    };

   
    $scope.getChequebooksAll = function () {
        debugger;
        var url = '/Resource/GetAllChequebookResource';
        $http.get(url)
            .then(function (data) {
                $scope.chequebookGrid.data = data.data;

            }), function () {
                $scope.accountTypeForm.Error = true;

            };
            
    }


    //Cards Script
    $scope.requestdebitcredit = function () {
        if ($scope.requestdebitcreditForm.$invalid) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            AccountFk: $scope.SelectedCardUserAccount != null ? $scope.SelectedCardUserAccount.AccountPk : 0,
            ResourceTypeFk: $scope.SelectedResourceType != null ? $scope.SelectedResourceType.AccountResourceTypePk : '00000000-0000-0000-0000-000000000000',
            CardPIN: $scope.CardPIN,

        };
        $http.post('/Resource/SaveUpdateCards', data)
            .then(function (data) {
                //if (data == 2) {
                //    $scope.accountTypeForm.Success = true;

                //}
                //else {
                //    $scope.accountTypeForm.Success = true;


                //}

                $window.location.assign("/Resource/CardIndex");

            }), function () {
                $scope.accountTypeForm.Error = true;

            };


    };

    $scope.blockResource = function () {
        
        if ($scope.cardResourceId != null && $scope.cardResourceId != '00000000-0000-0000-0000-000000000000') {
            var data = {

                ResourcePk: $scope.cardResourceId,
                IsActive: false,

            };
            $http.post('/Resource/SaveUpdateCards', data)
                .then(function (data) {
                    //if (data == 2) {
                    //    $scope.accountTypeForm.Success = true;

                    //}
                    //else {
                    //    $scope.accountTypeForm.Success = true;


                    //}

                    // $scope.clearAccountTypeData();
                    $window.location.assign("/Resource/CardIndex");

                }), function () {
                    $scope.accountTypeForm.Error = true;

                };
        }
        


    };

    $scope.blockCBResource = function () {

        if ($scope.CBResourceId != null && $scope.CBResourceId != '00000000-0000-0000-0000-000000000000') {
            var data = {

                ResourcePk: $scope.CBResourceId,
                IsActive: false,

            };
            $http.post('/Resource/SaveUpdateCards', data)
                .then(function (data) {
                    //if (data == 2) {
                    //    $scope.accountTypeForm.Success = true;

                    //}
                    //else {
                    //    $scope.accountTypeForm.Success = true;


                    //}

                    // $scope.clearAccountTypeData();
                    $window.location.assign("/Resource/Chequebook");

                }), function () {
                    $scope.accountTypeForm.Error = true;

                };
        }



    };

    $scope.requestChangeCardPIN = function () {

        if ($scope.cardPINChangeResourceId != null && $scope.cardPINChangeResourceId != '00000000-0000-0000-0000-000000000000') {
            var data = {

                ResourcePk: $scope.cardPINChangeResourceId,
                ConfirmOldPIN: $scope.CardOldPIN,
                CardPIN: $scope.CardNewPIN,

            };
            $http.post('/Resource/ChangeCardPIN', data)
                .then(function (data) {
                    //if (data == 2) {
                    //    $scope.accountTypeForm.Success = true;

                    //}
                    //else {
                    //    $scope.accountTypeForm.Success = true;


                    //}

                    // $scope.clearAccountTypeData();
                    $window.location.assign("/Resource/CardIndex");

                }), function () {
                    $scope.accountTypeForm.Error = true;

                };
        }



    };


    var columnDefinitions = [

        { field: "ResourceAccount.AccountName", name: "Title", enableColumnMenu: false, width: "13%", cellTooltip: true },
        { field: "ResourceTypeName", name: "Resource", enableColumnMenu: false, width: "15%", cellTooltip: true },
        { field: "ActiveStatus", name: "Status", enableColumnMenu: false, width: "12%", cellTooltip: true },
        { field: "CardNumber", name: "CardNumber", enableColumnMenu: false, width: "18%", cellTooltip: true },
        { field: "ExpiryDateString", name: "Expiry", enableColumnMenu: false, width: "18%", cellTooltip: true },
        { field: "", name: "Actions", enableColumnMenu: false, width: "20%", cellTemplate: '<div class="btn-group pull-left" data-toggle="buttons-radio"><button style="margin-left:6px" type ="button" ng-click="grid.appScope.openCardPINModal(row.entity.ResourcePk)" class= "btn-success" >PIN</button><button style="margin-left:6px" type ="button" ng-click="grid.appScope.openCardBlockModal(row.entity.ResourcePk)" class= "btn-info" >Block</button></div >' },
        
    ];
    $scope.debitcreditGrid = {
        enableFiltering: false,
        enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
        //enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        multiSelect: false,
        columnDefs: columnDefinitions
    };
    $scope.debitcreditGrid.onRegisterApi = function (gridApi) {

        //set gridApi on scope
        $scope.chequebookGridApi = gridApi;
        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
           
        });
    };


    $scope.getCardAll = function () {
        debugger;
        var url = '/Resource/GetAllUserCards';
        $http.get(url)
            .then(function (data) {
                $scope.debitcreditGrid.data = data.data;

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
    //GetCardsResourcesType

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

    $scope.GetCardsResourcesTypeddl = function () {
        var url = '/Resource/GetCardsResourcesType';
        $http.get(url)
            .then(function (data) {
                $scope.resourceTypeList = data.data;

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
    $scope.getCardAll();
    $scope.getChequebooksAll();
    $scope.getAccountddl();
    $scope.GetCardsResourcesTypeddl();
    
    //$scope.getAccountTransactionAll();
   
    

}]);




