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
                $timeout(function () {
                    var oTable = $('#editable').DataTable();

                    /* Apply the jEditable handlers to the table */
                    var modifiedValue
                    oTable.$('td').editable('default.aspx', {
                        "callback": function( sValue, y ) {
                            var aPos = oTable.fnGetPosition( this );
                            oTable.fnUpdate( modifiedValue, aPos[0], aPos[1] );
                        },
                        "submitdata": function (value, settings) {
                            modifiedValue = value;
                            return {
                                "row_id": this.parentNode.getAttribute('id'),
                             //   "column": oTable.fnGetPosition( this )[2]
                            };
                        },

                        "width": "90%",
                        "height": "100%"
                    } );

                function fnClickAddRow() {
                    $('#editable').dataTable().fnAddData( [
                        "Custom row",
                        "New row",
                        "New row",
                        "New row",
                        "New row" ] );

                }
                }, 200);

                table.dictionary = {};
                table.dictHeader = {};
                $http.post("AJAX/ajaxMethods.aspx/getDictionary", { data: {} }).success(function (data) {
                    table.dictionary = JSON.parse(data.d);
                });


                $http.post("AJAX/ajaxMethods.aspx/getDictionaryHeader", { data: {} }).success(function (data) {
                    table.dictHeader = JSON.parse(data.d);
                });
              //  this.init();
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