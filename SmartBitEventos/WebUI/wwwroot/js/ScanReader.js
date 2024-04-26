const btnValidate = document.getElementById("btn-validate");
function getState(boletoId) {
    $.ajax({
        url: '/Events/GetDetallesBoleto',
        method: 'GET',
        dataType: 'json',
        data: { boletoId: boletoId },
        success: function (data) {
            var estado = JSON.stringify(data[0].estado);
           // var data = JSON.stringify(data);
           // console.log('data: ' + data);
            //console.log('Estado: ' + estado);

            if (estado === 'true') {
                Swal.fire({
                    icon: 'warning',
                    title: 'Advertencia',
                    text: 'El boleto ya está validado.',
                });
            } else {
                changeState(boletoId);
                //console.log('validando:');
            }
        },
        error: function (error) {
            console.log('Error al cargar los datos: ', JSON.stringify(error));
        }
    });
}
function changeState(boletoId) {
    $.ajax({
        url: '/Events/ValidateQR',
        method: 'POST',
        contentType: 'application/json',
        data: boletoId,
        success: function (result) {
            Swal.fire({
                icon: 'success',
                title: 'Éxito',
                text: 'El boleto se validó correctamente.',
            }).then(function () {
                var redirectUrl = $('#redirectData').data('index-url');
                window.location.href = redirectUrl;
            });
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Hubo un problema al validar el boleto.',
            });
        }
    });
}
btnValidate.addEventListener("click", function () {
    const boletoId = document.getElementById("input-qr").value;
    getState(boletoId);
});