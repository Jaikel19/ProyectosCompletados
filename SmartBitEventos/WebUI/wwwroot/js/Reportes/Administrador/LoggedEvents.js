// Columnas AG Grid
const columnDefinitions = [
    { field: "nombreEvento", headerName: "Nombre del Evento", cellStyle: { textAlign: "left" } },
    { field: "modalidad", headerName: "Modalidad", cellStyle: { textAlign: "left" } },
    { field: "fecha", headerName: "Fecha", cellStyle: { textAlign: "left" } },
    { field: "hora", headerName: "Hora", cellStyle: { textAlign: "left" } },
    { field: "escenario", headerName: "Escenario", cellStyle: { textAlign: "left" } },
    { field: "contacto", headerName: "Teléfono del Organizador", cellStyle: { textAlign: "left" } },
];
// Opciones AG Grid
const gridOptions = {
    columnDefs: columnDefinitions,
    rowData: [],
    rowSelection: 'single',
    defaultColDef: { sortable: true, filter: true },
};

//grid desde DB
function loadData() {
    $.ajax({
        url: '/Reportes/FillGrid',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            //console.log(data[0]);
            const datosNecesarios = data.map(item => ({
                nombreEvento: item.nombre,
                modalidad: item.modalidad,
                fecha: item.fecha,
                hora: item.hora,
                escenario: item.escenario,
                contacto: item.contactoOrganizador,
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
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    loadData();
});
