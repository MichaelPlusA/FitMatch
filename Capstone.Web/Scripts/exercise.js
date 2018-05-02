$(document).ready(function () {

    $("button[name='exerciseButton'").on("click", function (event) {
        var exerciseToAdd = $("#exercises").val();
        var workoutToAddTo = $("#workoutID").val();
        var sets = $("#sets").text();
        var reps = $("#reps").text();
        var duration = $("#duration").text();
        var intensity = $("#intensity").text();

        var service = new ExerciseService();
        service.add(exerciseToAdd, workout, ExerciseAdded);
        event.preventDefault();
    })

    function ExerciseAdded(exerciseAdded) {
        var tr = $("<tr scope='row'>");
        var resultCell = $("<td>").text(exerciseAdded.Name);
        tr.append(resultCell);
        $("#exerciseTable").append(tr);
    }

    function ExerciseService() {
        const root = "/Trainer/AddExercise";

        this.add = function (exerciseToAdd, workout, successCallback) {
            $.ajax({
                url: root,
                method: "GET",
                data: {
                    "exerciseID": exerciseToAdd,
                    "workoutID": workoutToAddTo,
                    "sets": sets,
                    "reps": reps,
                    "duration": duration,
                    "intensity": intensity
                }
            }).done(function (data) {
                successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while adding exercise", error);
            })
        }
    }
})