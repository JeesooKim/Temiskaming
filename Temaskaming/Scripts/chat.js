$(document).ready(function () {
    $("#chatBox h2").click(function () {
        if ($("#chat").css('display') == 'none') {
            $("#chat").slideDown();
        } else {
            $("#chat").slideUp();
        }
    })
});