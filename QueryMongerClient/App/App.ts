module queryMonger {
    export class Query {

        static validateSearchTerm() {
            var regExp = /\w+/;
            var val = document.getElementById("#searchMain").nodeValue;
            if (regExp.test(val)) {
                alert(val);
            }
        }

        renderAllQuery() {
            $.ajax({
                method: "GET",
                url: "http://localhost:52499/api/queries",
                success: function (response) {
                    var query = { query: response }
                    this.renderData(query);
                }
            });
        }

        renderData(data) {
            var template = this.renderTemplate();
            $("#lisItemtHead").html(template(data));
        }

        renderTemplate() {
            var listItemTemplate = Handlebars.compile($("#listItemTemplate").html());

            return listItemTemplate;
        }

    }
}

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
