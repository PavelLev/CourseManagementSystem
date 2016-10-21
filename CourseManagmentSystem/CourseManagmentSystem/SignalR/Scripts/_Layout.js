var hub;
$(function () {
    hub = $.connection.myHub;
    hub.client.updateTopEmail = (newEmail) => {
        $("#topEmail").html(newEmail + " <span class='caret'></span>");
    }
    hub.client.alert = (message) => {
        alert(message);
    }
    $.connection.hub.start()
});