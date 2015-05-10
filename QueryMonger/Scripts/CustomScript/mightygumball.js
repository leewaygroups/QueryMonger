window.onload = function() {
    var urlArg = "http://localhost:52499/api/accounts/users";
    var request = new XMLHttpRequest();
    

    setInterval(getUserdata(request, urlArg), 3000);
};

function getUserdata(request, urlParam) {
    var url = urlParam + "/?timer=" + (new Date()).getTime();
    request.open("GET", url);
    request.onload = function() {
        if (request.status == 200) {
            displayUsers(request.responseText);
        }
    };

    request.send(null);
}

function displayUsers(responseText) {
    var userDiv = document.getElementById("location");
    userDiv.setAttribute("class", "row");
    var users = JSON.parse(responseText);
    for (var i = 0; i < users.length; i++) {
        var user = users[i];
        var div = document.createElement("div");
        div.setAttribute("class", "col-md-5");
        div.innerHTML = "UserName: " + user.userName + " Email: " + user.email;
        userDiv.appendChild(div);
    }

    var act = delList;
}

var delList = function() {
    var delScript = document.createElement("script");
    delScript.setAttribute("src", "~/Scripts/CustomScript/deleteUsers");
    delScript.setAttribute("id", "del");

    var divElement = document.getElementById("scriptDiv");
    if (document.getElementById("del") == null) {
        divElement.appendChild(delScript);
    } else {
        divElement.replaceChild(delScript);
    }
};