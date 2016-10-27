$(function () {

    hub.client.addView = (id) => {
            alert("1");
            $.get("/Lesson/Edit", { id: id })
                .done((response) => {
                    alert("Fuck!");
                    $("#asd").append(response);
                });
            return false;
    }
})

