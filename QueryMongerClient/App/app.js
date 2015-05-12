function getAllQueries() {
	var responseData = "";

	$.ajax({
		url: "localhost:52499/api/queries/GetAllQueries",
		success: function (response) {
			responseData = response.data;
		}
	});

	return responseData;
}

$(document).ready(function () {
	var testdata = {
		query: [
			{ itemlink: "/query1", item: "Query 1" },
			{ itemlink: "/query1", item: "Query 2" },
			{ itemlink: "#", item: "Query 3" },
			{ itemlink: "#", item: "Query 4" },
			{ itemlink: "#", item: "Query 5" }
		]
	};

	var listItemTemplate = Handlebars.compile($("#listItemTemplate").html());
	$("#lisItemtHead").html(listItemTemplate(getAllQueries()));
});

function validateSearchTerm() {
	var regExp = /\w+/;
	var val = document.getElementById("searchMain").value;
	if (regExp.test(val)) {
		alert(val);
	}
}