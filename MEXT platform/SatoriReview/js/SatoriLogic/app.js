(function () {
    var app = angular.module("SatoriReader", [])

    app.controller("MainController", function () {
        this.productGroup = {};
    });

    app.directive("reviewContainer", function () {
        return {
            restrict: "E",
            templateUrl: "HTML/ReviewContainer.html",
            controllerAs: "ReviewContainer",
            controller: ['$http', '$timeout', function ($http, $timeout) {
                var reviewContainer = this;
                reviewContainer.cardCount = 0;
                reviewContainer.cards = {};
                reviewContainer.currentCard = {}
                $http.post("AJAX/ajaxMethods.aspx/getCards", { data: {} }).success(function (data) {
                    reviewContainer.cards = JSON.parse(data.d);
                    reviewContainer.currentCard = reviewContainer.cards[reviewContainer.cardCount];
                });

                this.nextCard = function () {
                    reviewContainer.cardCount++;
                    reviewContainer.currentCard = reviewContainer.cards[reviewContainer.cardCount];
                }
            }],
        }
    });

   


    app.controller("GroupController", ['$http', function ($http) {
        this.groupIdSet = {};
        this.groups = {};
    }]);
    app.controller("AttributeController", ['$http', function ($http) {
        this.attributes = {};
    }]);

    
})();
