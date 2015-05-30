<<<<<<< HEAD
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
=======
﻿$(document).ready(function () {

	function getQueryListTemplate() {
		var listItemTemplate = Handlebars.compile($("#listItemTemplate").html());

		return listItemTemplate;
	}

	function renderQueryList(data) {
		var template = getQueryListTemplate();
		$("#lisItemtHead").html(template(data));
	}

	$.ajax({
		method: "GET",
		url: "http://localhost:52499/api/queries",
		success: function (response) {

			var query = { query: response }
			renderQueryList(query);
		}
	});

});

function renderQueryDetail(data) {
	//TODO: function to render singleton query detail template
}

function getQueryDetail(idArg) {
	var id = parseInt(idArg);

	$.ajax({
		method: "GET",
		url: "http://localhost:52499/api/query/" + id,
		success: function (response) {

			var query = { query: response }
			renderQueryDetail(query);
		}
	});

	window.location.replace("http://localhost:3786/");

	window.onload = function () {
		var lblTitle = document.getElementById("queryTitle");
		lblTitle.setAttribute("data-id", id);
		lblTitle.innerHTML = "Title: " + title;

		var lblDescription = document.getElementById("queryDescription");
		lblDescription.innerHTML = "Description: " + description;
	};
}


function getQueryResult(id) {
	var intId = parseInt(id);
	$.ajax({
		method: "GET",
		url: "http://localhost:52499/api/report/" + intId,
		success: function (response) {
			$('#columns').columns({

				data: response
			}
			);
		}
	});
}

function validateSearchTerm() {
	var regExp = /\w+/;
	var val = document.getElementById("searchMain").value;
	if (regExp.test(val)) {
		alert(val);
	}
}
>>>>>>> c78551a4df3f7cd9865c62de769ceb4af8b72d67
