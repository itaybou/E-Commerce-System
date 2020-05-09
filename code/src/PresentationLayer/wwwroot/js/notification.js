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
        success: function (event) {
            toastr.success(event.notification, "Notification",
                {
                    timeOut: 3000, closeButton: true, positionClass: "toast-bottom-left", toastClass: 'alert', extendedTimeOut: 1000,
                    onclick: null,});
        }
    });
};