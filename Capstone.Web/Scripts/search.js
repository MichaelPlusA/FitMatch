$(document).ready(function () {
    $("button[name='searchButton'").on("click", function () {
        var searchString = $("#searchString").text();
        var searchType = $("#searchType").val();

        const root = "/API/Search/";


        $.ajax({
            url: root,
            method: "GET",
            data: {
                "searchString": searchString,
                "searchType": searchType
            }
        }).done(function (data) {
            $("#searchTable").append(data);
        })
    })
}
    
