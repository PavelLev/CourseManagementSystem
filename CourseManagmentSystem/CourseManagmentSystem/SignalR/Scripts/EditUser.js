$(function () {
    hub.client.updateEditData = (newName, newEmail) => {
        $("#editName").val(newName);
        $("#editEmail").val(newEmail);
    }
})