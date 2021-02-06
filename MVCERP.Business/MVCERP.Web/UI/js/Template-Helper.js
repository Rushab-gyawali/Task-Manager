$(document).ready(function ($) {
    $(window).scroll(function () {
        if ($(window).scrollTop() >= 75) {
            $("#stickHeader").addClass("stickyHeader");
        }
        else {
            $("#stickHeader").removeClass("stickyHeader");
        }
    });
});