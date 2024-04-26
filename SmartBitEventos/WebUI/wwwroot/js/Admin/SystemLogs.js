const data = [
    {
        usuario: "Javier Araya",
        rol: "Gestor",
        accion: "Crear evento",
        mensaje: "Creación de evento: Partido Saprissa vs LDA",
        fecha: "2023-07-27",
        hora: "18:00",
    },
    {
        usuario: "Jasmyn Cech",
        rol: "Gestor",
        accion: "Crear evento",
        mensaje: "Creación de evento: Concierto de AC/DC",
        fecha: "2023-07-27",
        hora: "19:00",
    },
    {
        usuario: "Manuel Arruaz",
        rol: "Administrador",
        accion: "Bloquear usuario",
        mensaje: "Bloqueo de usuario id: AFY12A",
        fecha: "2023-07-27",
        hora: "20:00",
    },
];

// Columnas AG Grid
const columnDefinitions = [
    { field: "usuario", headerName: "Usuario", cellStyle: { textAlign: "left" } },
    { field: "rol", headerName: "Rol", cellStyle: { textAlign: "left" } },
    { field: "accion", headerName: "Acción", cellStyle: { textAlign: "left" } },
    { field: "mensaje", headerName: "Mensaje", cellStyle: { textAlign: "left" } },
    { field: "fecha", headerName: "Fecha", cellStyle: { textAlign: "left" } },
    { field: "hora", headerName: "Hora", cellStyle: { textAlign: "left" } }
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
