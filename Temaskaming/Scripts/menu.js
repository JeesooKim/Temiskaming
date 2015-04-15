$(document).ready(function () {

    $("#menuIcon").click(function () {
        if ($(".menu").css('display') == 'none') {
            $(".menu").slideDown();
        } else {
            $(".menu").slideUp();
        }
    })

});