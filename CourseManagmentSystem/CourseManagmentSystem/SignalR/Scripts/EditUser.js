$(function () {
    hub.client.updateEditData = (newName, newEmail, id) => {
        $("#editName").val(newName);
        $("#editEmail").val(newEmail);
        $("#id").val(id);
    }
    hub.client.updateCreateData = (name, courseId, videoLink) => {
        $("#name").val(name);
        $("#id").val(courseId);
        $("videoLink").val(videoLink);
    }
})