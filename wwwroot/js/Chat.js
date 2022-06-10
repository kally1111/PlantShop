"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
var username = "";

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&it;").replace(/>/g, "&gt;");
    var encodedMsg = user + " : " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("message").value;
    connection.invoke("SendMessage", username, message).catch(function () {
        document.getElementById("message").value = "";
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function SetUsername() {
    var usernameinput = document.getElementById("username").value;
    if (usernameinput === "") {
        alert("please enter");
        return;
    }
    username = usernameinput;
    document.getElementById("userinfo").style.display = 'none';
    document.getElementById("messagearea").style.display = 'block';
    document.getElementById("username1").innerText = usernameinput;

}