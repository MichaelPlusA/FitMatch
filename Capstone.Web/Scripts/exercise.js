$(document).ready(function () {

    $("button[name='exerciseButton'").on("click", function (event) {
        var exerciseToAdd = $("#exercises").val();

        var service = new ExerciseService();
        service.add(exerciseToAdd, ExerciseAdded);
        event.preventDefault();
    })

    function ExerciseAdded() {
        var div = $("<div class = exercise-success>");
        $(div).insertBefore("fieldtoinsert");
    }

    function ExerciseService() {
        const root = "/Trainer/Exercises";

        this.add = function (exerciseToAdd, successCallback) {
            $.ajax({
                url: root,
                method: "POST",
                data: {
                    "exercise": exerciseToAdd
                }
            }).done(function (data) {
                successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while adding exercise", error);
            })
        }
    }
})