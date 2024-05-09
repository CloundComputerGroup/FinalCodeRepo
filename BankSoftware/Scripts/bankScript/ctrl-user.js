app.controller('userCtrl', ['$scope', '$http', '$timeout', 'uiGridConstants', '$compile', '$window', function ($scope, $http, $timeout, uiGridConstants, $compile, $window) {
    $scope.validationErrors = false;
    $scope.Error = false;
    $scope.Success = false;

    $scope.loginValidationErrors = false;
    $scope.activateAuthentication = false;
    $scope.loginError = false;
    $scope.loginSuccess = false;
    $scope.genderList = [{ "Id": "Male", "Name": "Male" }, { "Id": "Female", "Name": "Female" }];
    $scope.clearRegisterData = function () {
        $scope.UserPk = null;
        $scope.Name = null;
        $scope.Email = null;
        $scope.Password = null;
        $scope.MobileNo = null;
        $scope.DOB = null;
        $scope.NIC = null;
        $scope.SelectedGender = 0;
    }

    $scope.activateTwoFactorAuthentication = function () {
        $scope.activateAuthentication = true;
    }

    $scope.loadSignUp = function () {

        $window.location.assign("/User/Register");

    }
    $scope.forgotPassword = function () {

        $window.location.assign("/User/ForgetPassword");

    }
    $scope.loadSignIn = function () {

        $window.location.assign("/User/Login");

    }
    $scope.VerifyOneTimePassword = function () {
        if ($scope.OneTimePassword == null || $scope.OneTimePassword == "" || $scope.OneTimePassword ==undefined) {
            return;
        }
        var data = {
            Password: $scope.OneTimePassword
        }

        $http.post('/User/VerifyOneTimePassword', data)
            .then(function (data) {
                debugger;
                if (data.data == "Authentic") {
                    $window.location.assign("/Home/Index");

                }
                else {
                    $window.location.assign("/User/Login");
                }

              

            }), function () {
                $window.location.assign("/User/Login");

            };
    }
    $scope.saveUser = function () {
        if ($scope.registerForm.$invalid) {
            $scope.validationErrors = true;
            return;
        }
        var data = {

            UserPk: $scope.UserPk,
            Name: $scope.Name,
            Email: $scope.Email,
            Password: $scope.Password,
            MobileNo: $scope.MobileNo,
            DOB: $scope.DOB,
            NIC: $scope.NIC,
            Gender: $scope.SelectedGender != null ? $scope.SelectedGender.Id : 0,

        };
        $http.post('/User/SaveUpdateUser', data)
            .then(function (data) {
                debugger;
                if (data == 2) {
                    $scope.registerForm.Success = true;
                    
                }
                else {
                    $scope.registerForm.Success = true;
                   

                }

                $scope.clearRegisterData();
                $window.location.assign("/User/Login");

            }),function () {
                $scope.registerForm.Error = true;
               
            };


    };

    $scope.forgetPassword = function () {
        if ($scope.forgetPasswordForm.$invalid) {
            $scope.validationErrors = true;
            return;
        }
        var data = {
            Email: $scope.Email,
        };
        $http.post('/User/ForgetPassword', data)
            .then(function (data) {
                debugger;
                if (data == 2) {
                    $scope.forgetPasswordForm.Success = true;

                }
                else {
                    $scope.forgetPasswordForm.Success = true;


                }

                //$scope.clearRegisterData();
                $window.location.assign("/User/Login");

            }), function () {
                //$scope.registerForm.Error = true;

            };


    };

    $scope.loginUser = function () {
        if ($scope.loginForm.$invalid) {
            $scope.loginValidationErrors = true;
            return;
        }
        var data = {

            Email: $scope.Email,
            Password: $scope.Password,

        };
        $http.post('/User/Login', data)
            .then(function (data) {
                
                if (data.data.Code == 0 && data.data.Status == "Success") {
                    $scope.loginForm.loginSuccess = true;
                    $scope.Email = null;
                    $scope.Password = null;
                    $scope.QrcodeImange = data.data.ErrorMessage;
                    $('#qrAuthenticationModal').modal('show');
                }
                else {
                    $scope.loginForm.loginSuccess = false;
                }

                
               // $window.location.assign("/Home/Index");

            }), function () {
                $scope.loginForm.loginError = true;

            };


    };
    // Dashboard Section Methods
    $scope.getDashboardCount2 = function () {
        var url = '/Bank/GetBankById';
        $http.get(url)
            .success(function (data) {
                alert("success");
                $scope.sectionList = data;
                $scope.$apply();
            });
    }

    $scope.getDashboardCount3 = function () {
        debugger;
        var url = '/Home/GetDashboardCount';
        apiGet($http, url, null, function (result) {
            alert("Success");
            var data = result.data;
        }, function (result) {
            alert("error");
            $scope.loadedCorepatchInfo = true;
            $scope.corePatchInfoErrorMessage = "Error occured! Please try again.";
        });
    }

    $scope.getVoidsDashboard = function () {
        var url = '/Dashboard/GetVoidsDashboard?currentUserId=' + $scope.currentUserId + '&isTeamView=' + $scope.isTeamview;
        apiGet($http, url, null, function (result) {
            var data = result.data;
            $scope.voidsDashboard = data;
            calculateVoidsAvailableToLetRating();
            calculateVoidsValueLostRating();
            $scope.showVoidsTiles = true;
            $scope.loadedVoids = true;
        }, function (result) {
            $scope.loadedVoids = true;
            $scope.voidsErrorMessage = "Error occured! Please try again.";
        });
    }

    $scope.getArrearsDashboard = function () {
        var url = '/Dashboard/GetArrearsDashboard?currentUserId=' + $scope.currentUserId + '&isTeamView=' + $scope.isTeamview;
        apiGet($http, url, null, function (result) {
            var data = result.data;
            $scope.arrearsDashboard = data;
            $scope.showArrearsTiles = true;
            calculateArrearsRatings();
            $scope.loadedArrears = true;
        }, function (result) {
            $scope.loadedArrears = true;
            $scope.arrearsErrorMessage = "Error occured! Please try again.";
        });
    }

   

}]);




