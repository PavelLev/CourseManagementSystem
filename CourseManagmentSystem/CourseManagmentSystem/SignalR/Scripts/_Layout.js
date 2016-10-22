var hub;
self.window.name = "blah";
$(function () {
    hub = $.connection.myHub;
    hub.client.updateTopEmail = (newEmail) => {
        $("#topEmail").html(newEmail + " <span class='caret'></span>");
    }
    hub.client.alert = (message) => {
        alert(message);
    }
    hub.client.reload = () => {
        setTimeout(() => {
                location.reload();
            },
            50);
    }
    hub.client.logOut = () => {
        document.getElementById("LogOutButton").click();
    }
    $.connection.hub.start().done(() => {
        //hub.server.echo();
    })
});