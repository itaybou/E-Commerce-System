// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Scroll to top button functions
$(window).scroll(function () {
    var height = $(window).scrollTop();
    if (height > 100) {
        $('#scrollPageTop').fadeIn();
    } else {
        $('#scrollPageTop').fadeOut();
    }
});

$(document).ready(function () {
    $("#scrollPageTop").click(function (event) {
        event.preventDefault();
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });
});