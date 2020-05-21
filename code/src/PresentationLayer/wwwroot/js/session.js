"use strict";

var keepSessionAlive = false;
var keepSessionAliveUrl = null;
SetupSessionUpdater("/Home/KeepSessionAlive");

function SetupSessionUpdater(actionUrl) {
    keepSessionAliveUrl = actionUrl;
    //var container = $("#body");
    keepSessionAlive = true;
    //container.mousemove(function () { keepSessionAlive = true; });
    //container.keydown(function () { keepSessionAlive = true; });
    CheckToKeepSessionAlive();
}

function CheckToKeepSessionAlive() {
    setTimeout(KeepSessionAlive, 5 * 60 * 1000); // Keep session alive - called every 5 minutes
}

function KeepSessionAlive() {
    console.log("session alive");
    if (keepSessionAlive && keepSessionAliveUrl != null) {
        $.ajax({
            type: "POST",
            url: keepSessionAliveUrl,
            success: function () { keepSessionAlive = false; }
        });
    }
    CheckToKeepSessionAlive();
}

