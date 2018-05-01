$(document).ready(function () {

    $("button[name='exerciseButton'").on("click", function (event) {
        var exerciseToAdd = $("#exercises").val();
        var workoutToAddTo = $("workout").val();

        var service = new ExerciseService();
        service.add(exerciseToAdd, workout, ExerciseAdded);
        event.preventDefault();
    })

    $("button[name='workoutButton'").on("click", function (event) {
        var workoutToAdd = $("#workouts").val();
        var planToAddTo = $("#plan").val();

        var service = new WorkoutService();
        service.add(workoutToAdd, planToAddTo, workoutAdded);
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

    function WorkoutService() {
        const root = "//";

        this.add = function (WorkoutToAdd, Plan, successCallback) {
            $.ajax({
                url: root,
                method: "POST",
                data: {
                    "WorkoutToAdd": WorkoutToAdd,
                    "Plan": Plan
                }
            }).done(function (data) {
                successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while adding workout", error);
            })
        }
    }
})