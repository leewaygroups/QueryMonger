var queryMonger;
(function (queryMonger) {
    var Query = (function () {
        function Query() {
        }
        Query.validateSearchTerm = function () {
            var regExp = /\w+/;
            var val = document.getElementById("#searchMain").nodeValue;
            if (regExp.test(val)) {
                alert(val);
            }
        };
        Query.prototype.renderAllQuery = function () {
            $.ajax({
                method: "GET",
                url: "http://localhost:52499/api/queries",
                success: function (response) {
                    var query = { query: response };
                    this.renderData(query);
                }
            });
        };
        Query.prototype.renderData = function (data) {
            var template = this.renderTemplate();
            $("#lisItemtHead").html(template(data));
        };
        Query.prototype.renderTemplate = function () {
            var listItemTemplate = Handlebars.compile($("#listItemTemplate").html());
            return listItemTemplate;
        };
        return Query;
    })();
    queryMonger.Query = Query;
})(queryMonger || (queryMonger = {}));
$(document).ready(function () {
    var queryInstance = new queryMonger.Query();
    queryInstance.renderAllQuery();
    var querySearch = document.getElementById("#btnsearchMain");
    querySearch.addEventListener("click", function () {
        queryMonger.Query.validateSearchTerm();
    });
    var searchForm = document.getElementById("#frmSearchMain");
    searchForm.addEventListener("submit", function () {
        queryMonger.Query.validateSearchTerm();
    });
});
//# sourceMappingURL=App.js.map