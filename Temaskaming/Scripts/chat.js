$(document).ready(function () {
    $("#chatBox h2").click(function () {
        if ($("#chat").css('display') == 'none') {
            $("#chat").slideDown();
        } else {
            $("#chat").slideUp();
        }
    })

    function loadChatLog() {
        var chaturl = $("#chatUrl").html();
        $.ajax({
            url: chaturl,
            cache: false,
            success: function (html) {
                $("#chatLog").html(html);
            }
        })
    }

    setInterval(loadChatLog, 1000);
})