var xmlhttp = new XMLHttpRequest();
xmlhttp.open("POST", "MemoryGame/GetAlbums", false);
xmlhttp.send();
var json = xmlhttp.responseText;
var obj = JSON.parse(json);

var c0 = document.getElementById("c0").addEventListener("click", function () { revealCard(0); });
var c1 = document.getElementById("c1").addEventListener("click", function () { revealCard(1); });
var c2 = document.getElementById("c2").addEventListener("click", function () { revealCard(2); });
var c3 = document.getElementById("c3").addEventListener("click", function () { revealCard(3); });

var c4 = document.getElementById("c4").addEventListener("click", function () { revealCard(4); });
var c5 = document.getElementById("c5").addEventListener("click", function () { revealCard(5); });
var c6 = document.getElementById("c6").addEventListener("click", function () { revealCard(6); });
var c7 = document.getElementById("c7").addEventListener("click", function () { revealCard(7); });

var c8 = document.getElementById("c8").addEventListener("click", function () { revealCard(8); });
var c9 = document.getElementById("c9").addEventListener("click", function () { revealCard(9); });
var c10 = document.getElementById("c10").addEventListener("click", function () { revealCard(10); });
var c11 = document.getElementById("c11").addEventListener("click", function () { revealCard(11); });

var oneVisible = false;
var turnCounter = 0;
var visibleNumber;
var lock = false;
var pairsLeft = 6;


function revealCard(nr) {

    var opacityValue = $("#c" + nr).css("opacity");

    if (opacityValue != 0 && lock == false && nr != visibleNumber) {
        lock = true;
        var obraz = 'url("' + obj.album[nr] + '")';

        $("#c" + nr).css("background-image", obraz);
        $("#c" + nr).addClass("card-a");
        $("#c" + nr).removeClass("card");

        if (oneVisible == false) {
            oneVisible = true;
            visibleNumber = nr;
            lock = false;
        }
        else {
            if (obj.album[visibleNumber] == obj.album[nr]) {
                setTimeout(function () { hide2Cards(nr, visibleNumber) }, 600);

            }
            else {
                setTimeout(function () { restore2Cards(nr, visibleNumber) }, 600);
            }

            turnCounter++;
            $(".score").html("Turn counter: " + turnCounter);
            oneVisible = false;

        }
    }
}

function hide2Cards(nr1, nr2) {
    $("#c" + nr1).css("opacity", "0");
    $("#c" + nr2).css("opacity", "0");
    pairsLeft--;

    if (pairsLeft == 0) {
        $(".board").html("<h1> You Win! <br> Done in " + turnCounter + ' turns</h1><br /><br /><span class="reset" onclick="location.reload()"> Do you want to play again ?</span>');

    }
    lock = false;
}

function restore2Cards(nr1, nr2) {
    $("#c" + nr1).css("background-image", "url(img/memorize/cd.png)");
    $("#c" + nr1).addClass("card");
    $("#c" + nr1).removeClass("card-a");

    $("#c" + nr2).css("background-image", "url(img/memorize/cd.png)");
    $("#c" + nr2).addClass("card");
    $("#c" + nr2).removeClass("card-a");

    lock = false;
}