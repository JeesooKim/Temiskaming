$(document).ready(function () {
    $("#chatBox h2").click(function () {
        if ($("#chat").css('display') == 'none') {
            $("#chat").slideDown();
        } else {
            $("#chat").slideUp();
        }
    })

    function clearMessage(){
        $("#message").html("");
    }

    function loadChatLog() {
        var oldHeight = $("#chatLog").prop('scrollHeight') - 20;
        var chaturl = $("#chatUrl").html();
        $.ajax({
            url: chaturl,
            cache: false,
            success: function (html) {
                $("#chatLog").html(html);
                var newHeight = $("#chatLog").prop('scrollHeight') - 20;
                if (newHeight > oldHeight) {
                    $("#chatLog").animate({ scrollTop: newHeight }, 'normal');
                }
            }
        })
        
    }
    loadChatLog();
    var sHeight = $("#chatLog").prop('scrollHeight');
    $("#chatLog").animate({ scrollTop: sHeight }, 'normal');

    $("#fMessage").submit(function(){
        $("#message").html("");
})

    setInterval(loadChatLog, 500);
})