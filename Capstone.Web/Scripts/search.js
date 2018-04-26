$(document).ready(function () {
    $("button[name='searchButton'").on("click", function () {
        var searchString = $("#searchString").text();

        const root = "/API/Search/";


        $.ajax({
            url: root,
            method: "GET",
            data: {
                "searchString": searchString
            }
        }).done(function (data) {
            $("#searchTable").append(data);
        })
    })
}
    
