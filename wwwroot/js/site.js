$(document).ready(function () {
    var x = 0;
    var s = "sasda";

    //alert("Hello Baby");

    var theForm = $("#theForm");

    //theForm.style.backgroundColor = "blue";
    var btn = $("#buy-button");

    btn.on("click", function () {
        //theForm.style.backgroundColor = "blue";
        alert("Buying Item");
        theForm.hide();
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on: ", $(this).text());
    });

    var loginToggle = $("#loginToggle");
    var popupForm = $(".popup-form");

    loginToggle.on("click", function () {
        popupForm.fadeToggle(200);
    })


});