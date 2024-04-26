// Columnas AG Grid
const columnDefinitions = [
    { field: "evento", headerName: "Evento", cellStyle: { textAlign: "left" } },
    { field: "gestor", headerName: "Gestor", cellStyle: { textAlign: "left" } },
    { field: "comision", headerName: "Comisión", cellStyle: { textAlign: "left" } },
    { field: "ganancias", headerName: "Ganancias", cellStyle: { textAlign: "left" } },
];
// Opciones AG Grid
const gridOptions = {
    columnDefs: columnDefinitions,
    rowData: [],
    rowSelection: 'single',
    defaultColDef: { sortable: true, filter: true },
};

//PDF
let gridData = [];
const pdfName = "ReporteAdministrador.pdf";
const reportTitle = "Reporte de Administrador";

//grid desde DB
function loadData() {
    $.ajax({
        url: '/Reportes/GetGananciasAdmin',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            const datosNecesarios = data.map(item => ({
                evento: item.evento,
                gestor: item.gestor,
                comision: "10%",
                ganancias: item.gananciastotales,
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