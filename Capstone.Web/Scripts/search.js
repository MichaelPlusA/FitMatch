$(document).ready(function () {
    $("button[name='searchButton'").on("click", function (event) {
        var searchString = $("#searchString").val();
        var searchType = $("#SearchTypeID").val();

        $("#searchTable").empty();

        var service = new SearchService();
        service.search(searchString, searchType, refreshSearchTable);
        event.preventDefault();
    })
   })

function refreshSearchTable(searchResults) {
    console.log("Search Results: ", searchResults);

    for (var i = 0; i < searchResults.length; i++) {

        var result = searchResults[i];

        var tr = $("<tr>");
        var searchCell = $("<td>")

        tr.append(searchCell);

        var firstNameCell = $("<td>").text(result.First_Name);
        var lastNameCell = $("<td>").text(result.Last_Name);
        var emailCell = $("<td>").text(result.Email);
        var priceCell = $("<td>").text(result.Price);

        tr.append(firstNameCell);
        tr.append(lastNameCell);
        tr.append(emailCell);


        $("#searchTable").append(tr);
    }
}

function SearchService() {
    const root = "/Trainee/SearchResult";

    this.search = function (searchString, searchType, successCallback) {
        console.log(searchString);
        console.log(searchType);
        $.ajax({
            url: root,
            method: "GET",
            data: {
                "searchString": searchString,
                "searchType": searchType
            }
        }).done(function (data) {
                successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while retrieving search results", error);
            })
    }
}