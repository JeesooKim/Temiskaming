$(document).ready(function(){

    function slide() {
        $('.imgSlider img:first-child').animate({ opacity: "0" }, 2500, "linear", function () {
            $(this).detach().appendTo('.imgSlider');
        });
        $('.imgSlider img:nth-child(2)').animate({ opacity: "1" }, 2500, "linear");
        
    }

    setInterval(slide, 4000);


})