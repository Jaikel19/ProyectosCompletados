function HomePageInit() {

    this.InitView = function () {
        this.LoadEvents();
    }

    this.LoadEvents = function () {
        var apiUrl = "/Home/LoadEventsProduct";
        document.querySelector("#loader").style.visibility = "visible";
        $.ajax({
            url: apiUrl,
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "html"
        }).done(function (result) {
            var grid = document.getElementById("myGrid");
            grid.innerHTML = result;
            document.querySelector("#loader").style.visibility = "hidden";
        }).fail(function (error) {
            console.log(error);
            document.querySelector("#loader").style.visibility = "hidden";
        });
    }
}

// Run the script when the document is ready
$(document).ready(function () {
    var view = new HomePageInit();
    view.InitView();
});