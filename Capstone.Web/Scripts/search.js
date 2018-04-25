$(document).ready(function () {
    $("radioObject[name='search'").on("click", function () {
        var lastName = $("#lastName").text();
        var firstName = $("#firstName").text();
        var price = $("#pricePerHour").text();

        const root = "/API/Search/";


        $.ajax({
            url: root,
            method: "GET",
            data: {
                "firstName": firstName,
                "lastName": lastName,
                "price": price
            }
        }).done(function (data) {
            $("#resultField").html();
        })
    })
}
    
