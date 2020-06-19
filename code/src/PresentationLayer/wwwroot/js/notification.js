"use strict";

var scheme = document.location.protocol == "https:" ? "wss" : "ws";
var port = document.location.port ? (":" + document.location.port) : "";
var connectionURL = scheme + "://" + document.location.hostname + port + "/wss";

function createWebSocket() {
    return new WebSocket(connectionURL);
}

var socket = createWebSocket(connectionURL);

socket.onopen = function (event) {
    console.log('Websocket opened');
};
socket.onclose = function (event) {
    console.log('Websocket closed');
};
socket.onerror = function (event) {
    console.log('Websocket error: ' + event.data);
};
socket.onmessage = function (event) {
    console.log('Websocket notification recieved: ' + event.data);
    $.ajax({
        async: true,
        type: 'POST',
        url: '/Notification/Notification',
        data: { notification: event.data },
        success: function (message) {
            if (message.success === true) {
                toastr.success(message.notification, "Notification",
                    {
                        timeOut: 3000, closeButton: true, positionClass: "toast-bottom-left", toastClass: 'alert', extendedTimeOut: 1500,
                        onclick: null,
                    });
            } else if (message.stats === true) {
                $.ajax({
                    url: "/Admin/RefreshStatistics",
                    type: "get",
                    data: $("form").serialize(), //if you need to post Model data, use this
                    success: function (result) {
                        $("#stats_partial").html(result);
                        toastr.warning(message.notification, "New Site Visit",
                            {
                                timeOut: 1500, closeButton: true, positionClass: "toast-bottom-left", toastClass: 'alert', extendedTimeOut: 1500,
                                onclick: null,
                        });
                    }
                });
            } else if (message.success === false) {
                toastr.info(message.notification, "Request",
                    {
                        timeOut: 3000, closeButton: true, positionClass: "toast-bottom-left", toastClass: 'alert', extendedTimeOut: 1500,
                        onclick: null,
                    });
            }
        }
    });
};