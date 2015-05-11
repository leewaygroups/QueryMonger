window.onload = init;

function init() {
	var lisItemtHead = document.getElementById("lisItemtHead");
	lisItemtHead.appendChild(listItemTemplate());

	document.getElementsByName("searchForm").onsubmit = validateSearchTerm;
}

function validateSearchTerm() {
	var regExp = /\w+/;
	var val = document.getElementById("searchMain").value;
	if (regExp.test(val)) {
		alert(val);
	}
}

function listItemTemplate() {
	var compile = _.template('<%_.forEach(items, function(item){%><a><%- user %></a><%}); %>');
	compile({ 'items': ["item1", "Item2", "Item3"] });
}
