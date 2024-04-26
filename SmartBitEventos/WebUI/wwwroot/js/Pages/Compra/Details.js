function Details() {
    this.InitView = function () {
        this.QRContent();
        $('#btnEnviarBoleto').click(function () {
            var details = new Details();
            details.SendEmail();
        });
    }

    //QR
    this.QRContent = function () {
        // get localStorage
        let asientos = JSON.parse(localStorage.getItem("asientos"));
        const boletoId = JSON.parse(localStorage.getItem("boletoId"));
        //console.log(JSON.stringify(asientos));

        let general, estudiantes, adultoMayor;
        for (asiento of asientos) {
            if (asiento.IdTipoBoleto == "1") {
                 general = asiento.Cantidad;
            } else if (asiento.IdTipoBoleto == "2") {
                 estudiantes = asiento.Cantidad;
            } else if (asiento.IdTipoBoleto == "3") {
                 adultoMayor = asiento.Cantidad;
            }
        }

        //inyeccion de datos a html
        $('#preQR').html(
            '<p>General: <span id="general">' + general + '</span></p>'  +
            '<p>Estudiantes: <span id="estudiantes">' + estudiantes + '</span></p>'  +
            '<p>Adulto Mayor: <span id="adulto-mayor">' + adultoMayor + '</span></p>' + 
            '<p>Id del boleto: <span id="boleto">' + boletoId + '</span></p>');

        //undefined
        document.getElementById('general').textContent = general === undefined ? 0 : general;
        document.getElementById('estudiantes').textContent = estudiantes === undefined ? 0 : estudiantes;
        document.getElementById('adulto-mayor').textContent = adultoMayor === undefined ? 0 : adultoMayor;

        let QR = document.getElementById('preQR').textContent;

        var apiUrl = API_URL_BASE + "/api/Utilities/GetQR?content=" + QR;

        $.ajax({
            url: apiUrl,
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "html",
        }).done(function (result) {
            $('#QRContent').html(result);
        }).fail(function (error) {
            Swal.fire({
                title: 'Mensaje',
                text: 'Hubo un error con el QR ' + error,
                icon: 'error'
            });
        });

        $('#myModal').modal('toggle');
    }
    this.SendEmail = function () {
        var email = $('#txtEmail').val();
        var phone = $('#txtPhone').val();

        // Validar que los campos no estén vacíos
        if (email.trim() === '' || phone.trim() === '') {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Por favor, completa todos los campos requeridos.',
                confirmButtonText: 'Aceptar'
            });

            if (email.trim() === '') {
                $('#txtEmail').addClass('is-invalid');
            } else {
                $('#txtEmail').removeClass('is-invalid');
            }

            if (phone.trim() === '') {
                $('#txtPhone').addClass('is-invalid');
            } else {
                $('#txtPhone').removeClass('is-invalid');
            }
            return; // Detener el envio del formulario si hay campos vacios
        }

        let QR = document.getElementById('preQR').textContent;

        // si todo estas bien, envio del correo y SMS
        var apiUrl = API_URL_BASE + "/api/Communications/SendEmail?emailAddress=" + email + "&qrContent=" + QR;
        var apiUrlSMS = API_URL_BASE + "/api/Communications/SendSMS?phone=" + phone;

        this.Communicate(apiUrl, apiUrlSMS);
    }
    this.Communicate = function (apiUrl, apiUrlSMS) {
        $.ajax({
            headers: {
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            method: "POST",
            url: apiUrl,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            hasContent: true
        }).done(function (data) {
            $.ajax({
                headers: {
                    'Accept': "application/json",
                    'Content-Type': "application/json"
                },
                method: "POST",
                url: apiUrlSMS,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                hasContent: true
            }).done(function () {
                Swal.fire({
                    title: 'Gracias por tu compra!',
                    text: 'Hemos enviado los detalles a tu correo electrónico.',
                    icon: 'success',
                }).then((result) => {
                    // cambiar pantalla
                    var redirectUrl = $('#redirectData').data('index-url');
                    window.location.href = redirectUrl;
                });
                // Se cierra el Modal
                $('#myModal').modal('toggle');
            }).fail(function (error) {
                Swal.fire({
                    title: 'Mensaje',
                    text: 'Se presentó un error en el proceso, por favor contacte al administrador',
                    icon: 'error'
                });
            });
        }).fail(function (error) {
            Swal.fire({
                title: 'Mensaje',
                text: 'Se presentó un error en el proceso, por favor contacte al administrador',
                icon: 'error'
            });
        });
    }
}
document.addEventListener("DOMContentLoaded", function () {
    var view = new Details();
    view.InitView();
});