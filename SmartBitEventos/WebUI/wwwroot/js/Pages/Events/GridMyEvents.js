function MyEventsList() {
    this.InitView = function () {
        this.SearchEventsByUser();
    };

    this.SearchEventsByUser = function () {
        console.log(1);
        var apiUrl = "/Events/LoadEvents";
        $.ajax({
            url: apiUrl,
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "html"
        }).done(function (result) {
            var grid = document.getElementById("myGrid");
            grid.innerHTML = result;
        }).fail(function (error) {
            console.log(error);
            //alert('ERROR!!');
        });
    };
}

$(document).ready(function () {
    var view = new MyEventsList();
    view.InitView();
});

//var gridOptions;

//document.addEventListener("DOMContentLoaded", function () {
//    gridOptions = {
//        columnDefs: [
//            { headerName: "Id", field: "id" },
//            { headerName: "Nombre", field: "nombre" },
//            { headerName: "Descripcion", field: "descripcion" },
//            { headerName: "Modalidad", field: "modalidad" },
//            { headerName: "Fecha", field: "fecha" },
//            { headerName: "Hora", field: "hora" },
//            { headerName: "Contacto Organizador", field: "contactoOrganizador" },
//            { headerName: "Restricciones", field: "restricciones" },
//            { headerName: "Escenario", field: "escenario" },
//            { headerName: "Capacidad", field: "capacidad" },
//            { headerName: "Tipo de Boleto", valueGetter: getTipoBoletoNames },
//        ],
//        defaultColDef: {
//            flex: 1,
//            minWidth: 100,
//            resizable: true,
//        },
//        rowData: null,
//    };


//    var gridDiv = document.querySelector("#myGrid");
//    new agGrid.Grid(gridDiv, gridOptions);
//});

//function getTipoBoletoNames(params) {
//    return params.data.tipoBoletoEventos.map((boleto) => boleto.nombre).join(", ");
//}
