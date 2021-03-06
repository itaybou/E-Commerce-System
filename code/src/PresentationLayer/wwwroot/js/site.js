﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

$(document).on('click', '#backLink', function () {
    history.go(-1);
});

$(document).ready(function (e) {
    $('.search-panel .dropdown-menu').find('a').click(function (e) {
        e.preventDefault();
        var param = $(this).attr("href").replace("#", "");
        var concept = $(this).text();
        $('.search-panel span#search_concept').text(concept);
        $('.input-group #search_param').val(param);
    });
});

$(window).load(function () {
    $('#messageModal').modal('show');
});