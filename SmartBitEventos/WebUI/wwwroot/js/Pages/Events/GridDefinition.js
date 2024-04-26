// Columnas AG Grid
const columnDefinitions = [
    { field: "nombreEvento", headerName: "Nombre del evento", cellStyle: { textAlign: "left" } },
    { field: "modalidad", headerName: "Modalidad", cellStyle: { textAlign: "left" } },
    { field: "fechaEvento", headerName: "Fecha", cellStyle: { textAlign: "left" } },
    { field: "horaEvento", headerName: "Hora", cellStyle: { textAlign: "left" } },
    { field: "escenario", headerName: "Escenario", cellStyle: { textAlign: "left" } },
    { field: "contacto", headerName: "Contacto", cellStyle: { textAlign: "left" } }
];

// Opciones AG Grid
const gridOptions = {
    columnDefs: columnDefinitions,
    rowData: [],
    rowSelection: 'single',
    defaultColDef: { sortable: true, filter: true },
    onRowClicked: params => {
        ProcessRowClicked(params);
    }
};

//Eventos para clicks
function ProcessRowClicked(params) {
    var view = new EventList();
    view.showEventDetails(params);
}

//grid desde DB
function loadData() {
    $.ajax({
        url: '/Reportes/FillGrid',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            //console.log(data);
            const datosNecesarios = data.map(item => ({
                nombreEvento: item.nombre,
                modalidad: item.modalidad,
                fechaEvento: item.fecha,
                horaEvento: item.hora,
                escenario: item.escenario,
                contacto: item.contactoOrganizador
            }));
            //console.log(datosNecesarios);
            gridData = datosNecesarios;
            gridOptions.api.setRowData(datosNecesarios);
        },
        error: function (error) {
            console.log('Error al cargar los datos: ', JSON.stringify(error));
        }
    });
}
// Inicializa el AG Grid cuando el DOM se haya cargado completamente
document.addEventListener('DOMContentLoaded', () => {
    loadData();
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
});
