$(document).ready(() => {
    $("#SubmitRegistration")
        .on("click",
            () => {
                $.connection.myHub.client.reload = () => {}
            });
})