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
            controller: ['$http', '$timeout', '$scope', function ($http, $timeout, $scope) {
                var reviewContainer = this;
                reviewContainer.cardCount = 0;
                reviewContainer.cards = {};
                reviewContainer.currentCard = {}
                reviewContainer.answer;
                reviewContainer.showAll = false;
                reviewContainer.currentHiraganaExpression = "";
                reviewContainer.currentKanjiExpression = "";

                $http.post("AJAX/ajaxMethods.aspx/getCards", { data: {} }).success(function (data) {
                    reviewContainer.cards = JSON.parse(data.d);
                    reviewContainer.currentCard = reviewContainer.cards[reviewContainer.cardCount];
                    reviewContainer.buildHiraganaExpression();
                    reviewContainer.buildKanjiExpression();
                });

                this.nextCard = function () {
                    reviewContainer.cardCount++;
                    reviewContainer.currentCard = reviewContainer.cards[reviewContainer.cardCount];
                    reviewContainer.buildHiraganaExpression();
                    reviewContainer.buildKanjiExpression();
                    reviewContainer.showAll = false;
                }

                this.checkAnswer = function () {
                    if (reviewContainer.currentCard.cardType == "EJ") {
                        if (reviewContainer.currentHiraganaExpression.toLowerCase().search(reviewContainer.answer) > -1 || reviewContainer.currentKanjiExpression.toLowerCase().search(reviewContainer.answer) > -1) {
                            alert("is bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId+"?q=3" })).success(function (data) {
                                alert(data.d);
                            });
                        } else {
                            alert("no es bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId + "?q=0" })).success(function (data) {
                                alert(data.d);
                            });
                        }
                    } else {
                        if (reviewContainer.currentCard.definition_text.toLowerCase().search(reviewContainer.answer) > -1) {
                            alert("is bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId + "?q=3" })).success(function (data) {
                                alert(data.d);
                            });
                        } else {
                            alert("no es bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId + "?q=0" })).success(function (data) {
                                alert(data.d);
                            });
                        }
                    }
                    reviewContainer.showAll = true;
                    reviewContainer.answer = ""
                }

                this.isJapanese = function () {
                    if (reviewContainer.currentCard.cardType == "EJ") {
                        return false;
                    } else {
                        return true;
                    }
                }
                this.buildHiraganaExpression = function () {
                    result = ""
                    angular.forEach(reviewContainer.currentCard.expression_text, function (value, key) {
                        if (value.hiragana == "") {
                            result += value.kanji;
                        } else {
                            result += value.hiragana;
                        }
                        reviewContainer.currentHiraganaExpression = result;
                    });
                }
                this.buildKanjiExpression = function () {
                    result = ""
                    angular.forEach(reviewContainer.currentCard.expression_text, function (value, key) {
                            result += value.kanji;
                            reviewContainer.currentKanjiExpression = result;
                    });
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
