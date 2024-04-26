// Columnas AG Grid
const columnDefinitions = [
    { field: "numeroGestor", headerName: "Evento", cellStyle: { textAlign: "left" } },
    { field: "mesEvento", headerName: "Descripción", cellStyle: { textAlign: "left" } },
    { field: "ganancia", headerName: "Capacidad", cellStyle: { textAlign: "left" } },
    { field: "comision", headerName: "Escenario", cellStyle: { textAlign: "left" } }
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
        url: '/Reportes/FillGridByID',
        method: 'GET',
        dataType: 'json',
        data: { userID: userID }, // Pasar el parámetro userID al servidor
        success: function (data) {
            //console.log(data);
            const datosNecesarios = data.map(item => ({
                numeroGestor: item.nombre,
                mesEvento: item.descripcion,
                ganancia: item.capacidad,
                comision: item.escenario,
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