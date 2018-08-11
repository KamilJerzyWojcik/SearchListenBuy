//document.addEventListener("DOMContentLoaded", )
var buttons = document.getElementsByClassName("urlData");

console.log(buttons);

for (let i = 0; i <= buttons.length - 1; i++) {

        buttons[i].addEventListener("click", function () {
            var data = buttons[i].getAttribute("data-url");
            var iframe = document.querySelector("iframe");

            //var nr = data.indexOf("=");
            //data = data.substring(nr+1);
            iframe.setAttribute("src", data); 
            iframe = iframe.getAttribute("src");
            
            console.log(iframe);

            
    });

}




