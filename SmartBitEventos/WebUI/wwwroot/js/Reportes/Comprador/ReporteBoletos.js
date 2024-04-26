// Columnas AG Grid
const columnDefinitions = [
    { field: "nombreEvento", headerName: "Evento", cellStyle: { textAlign: "left" } },
    { field: "fecha", headerName: "Fecha", cellStyle: { textAlign: "left" } },
    { field: "hora", headerName: "Hora", cellStyle: { textAlign: "left" } },
    { field: "cantidad", headerName: "Cantidad", cellStyle: { textAlign: "left" } },
    { field: "precio", headerName: "Precio", cellStyle: { textAlign: "left" } },
    { field: "tipoBoleto", headerName: "Tipo", cellStyle: { textAlign: "left" } },
    { field: "estado", headerName: "Estado", cellStyle: { textAlign: "left" } }
];
// Opciones AG Grid
const gridOptions = {
    columnDefs: columnDefinitions,
    rowData: [],
    rowSelection: 'single',
    defaultColDef: { sortable: true, filter: true },
};


//grid desde DB
function loadData(userID) {
    $.ajax({
        url: '/Reportes/FillGridBoletos',
        method: 'GET',
        dataType: 'json',
        data: { userID: userID }, // Pasar el parámetro userID al servidor
        success: function (data) {
            //console.log(data);
            const datosNecesarios = data.map(item => ({
                nombreEvento: item.nombreEvento,
                fecha: item.fecha,
                hora: item.hora,
                cantidad: item.cantidad,
                precio: item.precio,
                tipoBoleto: item.tipoBoleto,
                estado: item.estado,
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
    var userID = document.getElementById('myGrid').getAttribute('data-userid');
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    loadData(userID);
});