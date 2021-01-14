"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

var _connectionId = '';

connection.on("RecieveMessage", function (data) {
    console.log(data);
})


var joinRoom = function () {
    var url = '/SignalR/JoinRoom/' + _connectionId + '/@Model.Name'
    axios.post(url, null)
        .then(res => {
            console.log("Room Joined!", res);
        })
        .catch(err => {
            console.err("Failed to join the room!", res);
        })
}

connection.start()
    .then(function () {
        connection.invoke('getConnectionId')
            .then(function (connectionId) {
                _connectionId = connectionId
                joinRoom();
            })
    })
    .catch(function (err) {
        console.log(err)
    })

var sendMessage = function (event) {
    event.preventDefault();
    var data = new FormData(event.target);

    axios.post('/SignalR/SendMessage', data)
        .then(res => {
            console.log("Message sent!")
            console.log(data)
        })
        .catch(er => {
            console.log("Failed to send a message!")
        })
}