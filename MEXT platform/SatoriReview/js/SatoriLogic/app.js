(function () {
    var app = angular.module("SatoriReader", [])

    app.controller("MainController", function () {
        this.productGroup = {};
    });

    app.directive("keypressEvents", [
  '$document',
  '$rootScope',
  function ($document, $rootScope) {
      return {
          restrict: 'A',
          link: function () {
              $document.bind('keypress', function (e) {
                  console.log('Got keypress:', e.which);
                  $rootScope.$broadcast('keypress', e);
                  $rootScope.$broadcast('keypress:' + e.which, e);
              });
          }
      };
  }
    ]);

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
                reviewContainer.q = 5;
                reviewContainer.getQStatus = ["You done goofed", "", "", "Hard", "Alright", "E-Z", "E-Z"];
                reviewContainer.isLoading = true;
                reviewContainer.error = "";
                reviewContainer.countDown = function () {
                    return $timeout(function () {
                        if ((reviewContainer.q > 3) && (reviewContainer.showAll == false)) {
                            reviewContainer.q--;
                        }
                        reviewContainer.countDown();
                    }, 10000);
                }


                $http.post("AJAX/ajaxMethods.aspx/getCards", { data: {} }).success(function (data) {
                    reviewContainer.cards = JSON.parse(data.d);
                    reviewContainer.currentCard = reviewContainer.cards[reviewContainer.cardCount];
                    reviewContainer.buildHiraganaExpression();
                    reviewContainer.buildKanjiExpression();
                    if (reviewContainer.currentCard.alternateDefinitions == null) {
                        reviewContainer.currentCard.alternateDefinitions = "";
                    }
                    reviewContainer.currentCard.full_definition = reviewContainer.currentCard.definition_text + "; " + reviewContainer.currentCard.alternateDefinitions;
                    reviewContainer.countDown();
                    reviewContainer.isLoading = false;
                }).error(function (error, status){
                    reviewContainer.error = " message: " + JSON.stringify(error) + ", status: " + status;
                    reviewContainer.isLoading = false;
                }); 

                this.nextCard = function () {
                    reviewContainer.cardCount++;
                    reviewContainer.currentCard = reviewContainer.cards[reviewContainer.cardCount];
                    reviewContainer.buildHiraganaExpression();
                    reviewContainer.buildKanjiExpression();
                    reviewContainer.showAll = false;
                    reviewContainer.answer = ""
                    if (reviewContainer.currentCard.alternateDefinitions == null) {
                        reviewContainer.currentCard.alternateDefinitions = "";
                    }
                    reviewContainer.currentCard.full_definition = reviewContainer.currentCard.definition_text + "; " + reviewContainer.currentCard.alternateDefinitions;
                    reviewContainer.q = 6;
                }

                this.checkAnswer = function () {
                    if (reviewContainer.currentCard.cardType == "EJ") {
                        if (reviewContainer.currentHiraganaExpression.toLowerCase().search(reviewContainer.answer.toLowerCase()) > -1 || reviewContainer.currentKanjiExpression.toLowerCase().search(reviewContainer.answer.toLowerCase()) > -1) {
                            alert("is bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId + "?q=" + reviewContainer.q })).success(function (data) {
                                // alert(data.d);
                            });
                        } else {
                            alert("no es bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId + "?q=0" })).success(function (data) {
                                // alert(data.d);
                            });
                            reviewContainer.q = 0;
                        }
                    } else {
                        if (reviewContainer.currentCard.full_definition.toLowerCase().replaceAll(" ", "").replaceAll("-", "").search(reviewContainer.answer.toLowerCase().replaceAll(" ", "").replaceAll("-", "")) > -1) {
                            alert("is bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId + "?q=" + reviewContainer.q })).success(function (data) {
                                // alert(data.d);
                            });
                        } else {
                            alert("no es bueno");
                            $http.post("AJAX/ajaxMethods.aspx/setCards", JSON.stringify({ data: reviewContainer.currentCard.cardId + "?q=0" })).success(function (data) {
                                // alert(data.d);
                            });
                            reviewContainer.q = 0;
                        }
                    }
                    reviewContainer.showAll = true;
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

                this.handleEnter = function (event) {
                    if (reviewContainer.showAll == true) {
                        if (event.keycode == 13) {
                            alert("test");
                        }
                    }
                }

                this.saveCard = function () {
                    $http.post("AJAX/ajaxMethods.aspx/saveCardData", JSON.stringify({ card: reviewContainer.currentCard }));
                }

                this.saveCard = function () {
                    $http.post("AJAX/ajaxMethods.aspx/saveCardData", JSON.stringify({ card: reviewContainer.currentCard }));
                }


                $scope.$on('keypress:13', function (onEvent, keypressEvent) {
                    if (reviewContainer.showAll == true) {
                        $scope.$apply(reviewContainer.nextCard());
                    }
                });

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


String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};