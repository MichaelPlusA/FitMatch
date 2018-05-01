$(document).ready(function () {

    $("button[name='exerciseButton'").on("click", function (event) {
        var exerciseToAdd = $("#exercises").val();
        var workoutToAddTo = $("workout").val();

        var service = new ExerciseService();
        service.add(exerciseToAdd, workout, ExerciseAdded);
        event.preventDefault();
    })

    function ExerciseAdded() {
        var div = $("<div class = exercise-success> Exercise added </div>");
        $(div).insertBefore("fieldtoinsert");
    }

    function ExerciseService() {
        const root = "/Trainer/Exercises";

        this.add = function (exerciseToAdd, workout, successCallback) {
            $.ajax({
                url: root,
                method: "POST",
                data: {
                    "exercise": exerciseToAdd,
                    "workout": workout
                }
            }).done(function (data) {
                successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while adding exercise", error);
            })
        }
    }
})