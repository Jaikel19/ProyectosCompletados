// Columnas AG Grid
const columnDefinitions = [
    { field: "numeroGestor", headerName: "Acción", cellStyle: { textAlign: "left" } },
    { field: "mesEvento", headerName: "Descripcion", cellStyle: { textAlign: "left" } },
    { field: "ganancia", headerName: "Fecha", cellStyle: { textAlign: "left" } },
    { field: "comision", headerName: "Hora", cellStyle: { textAlign: "left" } }
];
// Opciones AG Grid
const gridOptions = {
    columnDefs: columnDefinitions,
    rowData: [],
    rowSelection: 'single',
    defaultColDef: { sortable: true, filter: true },
};

function formatDateAndTime(dateTimeStr) {
    const dateTime = new Date(dateTimeStr);

    // Formatear la fecha (yyyy-mm-dd)
    const fecha = dateTime.toLocaleDateString('es-ES', { year: 'numeric', month: '2-digit', day: '2-digit' });

    // Formatear la hora (hh:mm:ss)
    const hora = dateTime.toLocaleTimeString('es-ES', { hour: '2-digit', minute: '2-digit', second: '2-digit' });

    return { fecha, hora };
}

//grid desde DB
function loadData() {
    $.ajax({
        url: '/Reportes/FillGridAuditoria',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            const datosNecesarios = data.map(item => {
                const formattedDateAndTime = formatDateAndTime(item.fechaHora);
                return {
                    numeroGestor: item.accion,
                    mesEvento: item.descripcion,
                    ganancia: formattedDateAndTime.fecha,
                    comision: formattedDateAndTime.hora
                };
            });
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