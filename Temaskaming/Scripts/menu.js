$(document).ready(function () {

    $("#menuIcon").click(function () {
        if ($(".menu nav").css('display') == 'none') {
            $(".menu nav").slideDown();
        } else {
            $(".menu nav").slideUp();
        }
    })

});