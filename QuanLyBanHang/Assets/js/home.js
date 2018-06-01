$(document).ready(function () {
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        console.log(scroll);
        if (scroll > 280) {
            $('.aboutus-col1').addClass('animated fadeInLeft');
            $('.aboutus-col2').addClass('animated fadeInRight');
        }

        if (scroll > 870) {
            $('.shopnow-col1').addClass('animated fadeInLeft');
            $('.shopnow-col2').addClass('animated fadeInUp');
        }

        if (scroll > 400) {
            $('.wrapper-menu').addClass('active');
        }
        else {
            $('.wrapper-menu').removeClass('active');
        }
    });

    $("#homebtndown").click(function () {
        var pos = $(".wrapper").offset().top - 40;
        $('html, body').animate({
            scrollTop: pos
        }, 800);
    });
});