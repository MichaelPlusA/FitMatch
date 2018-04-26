$(document).ready(function () {
    $("button[name='searchButton'").on("click", function (event) {
        var searchString = $("#searchString").text();
        var searchType = $("#SearchTypeID").val();

        var search = new SearchService();
        service.search(searchString, searchType)
    })
   })

function refreshSearchTable(searchResults) {

    console.log("Search Results: ", searchResults);

    for (var i = 0; i < searchResults.Search.length; i++) {

        var result = searchResults.Search[i];

        var tr = $("<tr>");
        var searchCell = $("<td>")

        tr.append(searchCell);

        var firstNameCell = $("<td>").text(result.firstName);
        var lastNameCell = $("<td>").text(result.lastName);
        var emailCell = $("<td>").text(result.email);

        tr.append(firstNameCell);
        tr.append(lastNameCell);
        tr.append(emailCell);


        $("#searchTable").append(tr);
    }
}

function SearchService() {
            const root = "/API/Search/";

            this.search = function (searchString, searchType, successCallback) {
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