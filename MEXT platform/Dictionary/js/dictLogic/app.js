(function () {
    var app = angular.module("Dictionary", [])

    app.controller("MainController", function () {
        this.productGroup = {};
    });

    app.directive("dictTable", function () {
        return {
            restrict: "E",
            templateUrl: "HTML/dictTable.html",
            controllerAs: "dictTableCtrl",
            controller: ['$http', '$timeout', function ($http, $timeout) {
                var table = this;
                var oTable;
                $timeout(function () {
                    oTable = $('#editable').DataTable();

                    /* Apply the jEditable handlers to the table */
                    var modifiedValue
                    //oTable.$('td').editable('AJAX/validateChange.aspx', {

                    //    "width": "90%",
                    //    "height": "100%"
                    //} );

                }, 600);

                table.dictionary = {};
                table.dictHeader = {};
                $http.post("AJAX/ajaxMethods.aspx/getDictionary", { data: {} }).success(function (data) {
                    table.dictionary = JSON.parse(data.d);
                });


                $http.post("AJAX/ajaxMethods.aspx/getDictionaryHeader", { data: {} }).success(function (data) {
                    table.dictHeader = JSON.parse(data.d);
                });
                //  this.init();

                this.fnClickAddRow = function () {
                //    $('#editable').dataTable().fnAddData([
                //        "Custom row",
                //        "New row",
                //        "New row",
                //        "New row",
                //        "New row"]);
                    //    $('#editable').dataTable();
                    var nextHashKey = parseInt(table.dictionary[table.dictionary.length - 1].$$hashKey.split(":")[1])
                    nextHashKey++;
                    table.dictionary.push({ "rowKey": "New row", "strings": ["new row", "new row", "new row"], "$$hashKey": "object:" + nextHashKey });
                    $timeout(function () {
                        oTable.destroy();
                        $('#editable').empty();
                        $('#editable').DataTable();
                    }, 600);
                    
                };
                this.checkTableValue = function () {
                    alert(JSON.stringify(table.dictionary));
                }

                this.saveValue = function () {
                    $http.post("AJAX/ajaxMethods.aspx/setDictionaryData", angular.toJson({ data: table.dictionary })).success(function (data) {
                        table.dictHeader = JSON.parse(data.d);
                    });
                }
            }],
        }
    });

    app.directive("editModal", function () {
        return {
            restrict: "E",
            templateUrl: "/admin/Modals/editModal.html",
            controller: ['$http', function ($http) {
            }],
            controllerAs: "editModalCtrl",
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
