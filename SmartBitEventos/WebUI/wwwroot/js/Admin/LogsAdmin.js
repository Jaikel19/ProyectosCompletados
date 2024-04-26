const data = [
    {
        logs: "evento1"
    },
    {
        logs: "evento2"
    },
    {
        logs: "evento3"
    },
    {
        logs: "evento4"
    },
    {
        logs: "evento5"
    },
    {
        logs: "evento6"
    },
    {
        logs: "evento7"
    },
    {
        logs: "evento8"
    },
    {
        logs: "evento9"
    },
];

// Columnas AG Grid
const columnDefinitions = [
    { field: "logs", headerName: "Logs", cellStyle: { textAlign: "left" } }
];

// Opciones AG Grid
const gridOptions = {
    columnDefs: columnDefinitions,
    rowData: data,
    rowSelection: 'single',
    defaultColDef: { sortable: true, filter: true },
    onRowClicked: params => {
        ProcessRowClicked(params);
    },
    onRowDoubleClicked: params => {
        ProcessRowDoubleClicked(params);
    }
};

//Eventos para clicks
//function ProcessRowClicked(params) {
//    var view = new EventList();
//    view.showEventDetails(params);
//}
////double click
//function ProcessRowDoubleClicked(params) {
//    //console.log("Double clicked on row");
//}

// Inicializa el AG Grid cuando el DOM se haya cargado completamente
document.addEventListener('DOMContentLoaded', () => {
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
});
