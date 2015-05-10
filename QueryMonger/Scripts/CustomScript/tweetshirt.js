window.onload = function () {
    var button = document.getElementById("previewButton");
    button.onclick = previewHandler;    
};


function previewHandler() {
    var canvas = document.getElementById("tshirtCanvas");
    var context = canvas.getContext("2d");

    var backgroundColorElement = document.getElementById("backgroundColor");
    var backIndex = backgroundColorElement.selectedIndex;
    var backgroundColor = backgroundColorElement[backIndex].value;
    fillBackGroundColour(canvas, context, backgroundColor);

    var shapeElement = document.getElementById("shape");
    var shapeIndex = shapeElement.selectedIndex;
    var shape = shapeElement[shapeIndex].value;
    if (shape == "squares") {
        for (var squares = 0; squares < 20; squares++) {
            drawSquare(canvas, context);
        }
    }

    var foregroundColorElement = document.getElementById("foregroundColor");
    var foreIndex = foregroundColorElement.selectedIndex;
    var foregroundColor = foregroundColorElement[foreIndex].value;

    //context.fillRect(10, 10, 100, 100);
}

function fillBackGroundColour(canvas, context, bgColour) {
    context.fillStyle = bgColour;
    context.fillRect(0, 0, canvas.width, canvas.height);
}

function drawSquare(canvas, context) {
    var xcord = Math.floor(Math.random() * canvas.width);
    var ycord = Math.floor(Math.random() * canvas.height);
    var size = Math.floor(Math.random() * 40);

    context.fillStyle = "lightblue";
    context.fillRect(xcord, ycord, size, size);
}