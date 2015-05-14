$(document).ready(function () {

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