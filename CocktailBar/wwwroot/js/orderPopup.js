function SendHandShake() {
    let webSocket

    function GetWebSocketMessages(onMessageReceived) {
        var url = `wss://${location.host}/order/handshake`
        console.log('url is: ' + url);

        webSocket = new WebSocket(url);

        webSocket.onmessage = onMessageReceived;
    }

    GetWebSocketMessages(function (message) {
        alert(message.data);
    });
}

SendHandShake();