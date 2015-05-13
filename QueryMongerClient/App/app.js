$(document).ready(function () {

	function renderTemplate() {
		var listItemTemplate = Handlebars.compile($("#listItemTemplate").html());

		return listItemTemplate;
	}

	function renderData(data) {
		var template = renderTemplate();
		$("#lisItemtHead").html(template(data));
	}

	$.ajax({
		method: "GET",
		url: "http://localhost:52499/api/queries",
		success: function (response) {
			var query = { query: response }
			renderData(query);
		}
	});


});

function renderQueryResult(data) {
    //TODO: Implement data rendring to dynamic grid in webpage
    alert("I was called!");
}

function getQueryResult(id) {
    var intId = parseInt(id);
	$.ajax({
		method: "GET",
		url: "http://localhost:52499/api/report/" + intId,
		success: function (response) {
			var result = { result: response }
			renderQueryResult(result);
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