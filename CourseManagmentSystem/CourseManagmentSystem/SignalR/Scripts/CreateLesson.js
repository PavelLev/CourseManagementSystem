$(function() {
    hub.client.updateCreateData = (name, courseId, videoLink) => {
        $("#name").val(name);
        $("#id").val(courseId);
        $("videoLink").val(videoLink);
    }
})