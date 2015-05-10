window.onload = init;

function init() {
    document.getElementsByName("searchForm").onsubmit = test;
    createListItem();
}

function test() {
    var regExp = /\w+/;
    var val = document.getElementById("searchMain").value;
    if (regExp.test(val)) {
        alert(val);
    }
}

function createListItem() {
    var linkItem = document.createElement("a");
    linkItem.class = "glyphicon glyphicon-eye-open pull-right";
    linkItem.href = "#";

    var spanItem = document.createElement("span");
    var stronItem = document.createElement("strong");

    var listItem = document.createElement("li");
    listItem.class = "list-group-item";

    spanItem.appendChild(linkItem);
    listItem.appendChild(spanItem);
    listItem.appendChild(document.createTextNode("Random text node"));

    AddItemToQueryList(listItem);
}


function AddItemToQueryList(item) {
    var queryList = document.getElementById("queryList");
    queryList.appendChild(item);
}