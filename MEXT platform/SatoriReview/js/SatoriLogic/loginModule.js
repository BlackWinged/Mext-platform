(function () {
    var app = angular.module("LoginModule", [])

    //app.controller("MainController", function () {
    //    this.productGroup = {};
    //});

    app.directive("loginContainer", function () {
        return {
            restrict: "E",
            templateUrl: "HTML/LoginContainer.html",
            controllerAs: "LoginContainer",
            controller: ['$http', '$timeout', '$scope', '$rootScope', function ($http, $timeout, $scope, $rootScope) {
                var me = this;
                me.validatedSignIn = false;
                me.username = "";
                me.password = "";
                me.serverMessage = "";

                $scope.$on('needs_signin', function (onEvent, keypressEvent) {
                    me.validatedSignIn = true;
                });

                me.submitLogin = function () {
                    $http.post("AJAX/ajaxMethods.aspx/signIn", JSON.stringify({ data: {username:me.username, password:me.password} })).success(function (data) {
                        me.serverMessage = data;
                        if (data.d == "success") {
                            $rootScope.$broadcast('logged_in');
                        } else {
                            me.serverMessage = "You boned the password, try again.";
                        }
                    });
                }
            }],
        }
    });



})();

