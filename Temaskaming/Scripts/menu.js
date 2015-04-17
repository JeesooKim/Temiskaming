$(document).ready(function () {

    $("#menuIcon").click(function () {
        if ($(".menu").css('display') == 'none') {
            $(".menu").css('display', 'block');
            $("#menuIcon").attr("id", "menuIconDown");
        } else {
            $(".menu").css('display', 'none');
            $("#menuIconDown").attr("id", "menuIcon");
        }
    })

});