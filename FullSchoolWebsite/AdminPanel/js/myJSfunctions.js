
function ShowOverlay()
{
    $("body").append("<div style='display:none;' class='overlay' id='overlay'></div>");
    $("#overlay").fadeIn("slow");
}

function RemoveOverlay()
{
    $("#overlay").fadeOut("slow", function () {
        $("#overlay").remove();
    });
}

function ShowArticle(id)
{
    ShowOverlay();
    $(id).fadeIn("slow");
}

function CloseArticle(id)
{
    RemoveOverlay();
    $(id).fadeOut("slow");
}

function PaintBackgroundRed(commentID, labelID)
{
    var element = document.getElementById(commentID).parentNode;
    var tableRow = element.parentNode;
    var label = document.getElementById(labelID);


    if ($(tableRow).css("background-color") == "rgb(255, 69, 69)")
    {
        $(tableRow).css("background-color", "white");
        label.value = label.value.replace(commentID + ",", "");
    }
    else
    {
        $(tableRow).css("background", "#FF4545");
        label.value += commentID + ",";
    }
}

//<div class="confirm">
//  <h1>Confirm your action</h1>
//  <p>Are you really <em>really</em> <strong>really</strong> sure that you want to exit this awesome application?</p>
//  <button>Cancel</button>
//  <button>Confirm</button>
//</div>

function ShowAlertBox(title, confirmButtonID, showOverlay, headerColor)
{
    if (showOverlay) ShowOverlay();

    var hColor;
    if (headerColor == "red") hColor = "#FC2222";
    else if (headerColor == "green") hColor = "#4DFF3A";

    var text = "<div id='alertBox' class='confirm' style='display:none;'><div id='header' style='background-color:" + hColor + "'><h1>" + title + "</h1></div><button runat='server' ID=" + confirmButtonID + ">Да</button>" +
        "<button onclick='RemoveAlertBox(" + showOverlay + ");return false;'>Не</button></div>";

    $("body").append(text);
    $("#alertBox").fadeIn("slow");
}

function RemoveAlertBox(removeOverlay)
{
    if(removeOverlay) RemoveOverlay();
    $("#alertBox").fadeOut("slow", function () {
        $("#alertBox").remove();
    });
}