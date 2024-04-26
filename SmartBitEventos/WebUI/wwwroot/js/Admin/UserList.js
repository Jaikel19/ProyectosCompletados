// Columnas AG Grid
const columnDefinitions = [
    { field: "id", headerName: "Id", cellStyle: { textAlign: "left" } },
    { field: "correo", headerName: "Correo electrónico", cellStyle: { textAlign: "left" } },
    { field: "nombre", headerName: "Nombre", cellStyle: { textAlign: "left" } },
    { field: "apellido", headerName: "Apellido", cellStyle: { textAlign: "left" } },
    { field: "cedula", headerName: "Cédula", cellStyle: { textAlign: "left" } },
    { field: "telefono", headerName: "Teléfono", cellStyle: { textAlign: "left" } },
    { field: "activo", headerName: "Activo", cellStyle: { textAlign: "left" } },
    { field: "bloqueado", headerName: "Bloqueado", cellStyle: { textAlign: "left" } },
    {
        headerName: "Cambiar Estado",
        cellRenderer: function (params) {
            const button = document.createElement("button");
            button.innerText = "Editar";
            button.classList.add("btn-primary")
            button.addEventListener("click", function () {
                toggleUserStatus(params.data.id)
                //console.log("Editar fila con datos:", params.data.id);
                //console.log("Editar fila con datos:", params.data.bloqueado);
            });

            const cellDiv = document.createElement("div");
            cellDiv.appendChild(button);
            return cellDiv;
        },
        cellStyle: { textAlign: "left" },
    },
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
        url: '/Admin/GetAllUsers',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            //console.log(data[0]);
            const datosNecesarios = data.map(item => ({
                id: item.id,
                correo: item.email,
                nombre: item.nombre,
                apellido: item.apellido,
                cedula: item.cedula,
                telefono: item.telefono,
                activo: item.activo.toString(),
                bloqueado: item.bloqueado.toString(),
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

// Función para cambiar el estado de un usuario
function toggleUserStatus(idUsuario) {
    $.ajax({
        url: '/Admin/UpdateStatus', // Ruta a la función en tu controlador que actualiza el estado
        method: 'POST',
        contentType: 'application/json',
        data: idUsuario, // Envía el ID del usuario al servidor
        success: function (result) {
            console.log(result);
            // Llamada exitosa, recargar los datos en la tabla
            //loadData();
        },
        error: function (error) {
            console.log('Error al cambiar el estado del usuario: ', JSON.stringify(error));
        }
    });
}


//Eventos para clicks
//function ProcessRowClicked(params) {
//    var view = new EventList();
//    view.showEventDetails(params);
//}
//}

// Inicializa el AG Grid cuando el DOM se haya cargado completamente
document.addEventListener('DOMContentLoaded', () => {
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    loadData();
});
