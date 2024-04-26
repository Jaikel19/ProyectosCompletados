function Carrito() {
    this.InitView = function () {
        let paypalPrice = this.getValues();
        $('#btn-comprar').click(function () {
            var carrito = new Carrito();
            carrito.PayPalPayment(paypalPrice);
        });
    }
    this.getValues = function () {
         //get localStorage
        let asientos = JSON.parse(localStorage.getItem("asientos"));
        //console.log("Carrito: " + JSON.stringify(asientos));

        let totalPrecio = 0;

        for (asiento of asientos) {
            let precio = parseInt(asiento.Precio);
           // console.log(JSON.stringify(asiento.Cantidad));
            if (asiento.IdTipoBoleto == "1") {
                let general = asiento.Cantidad;
                document.getElementById('general').textContent = " " + general;
            } else if (asiento.IdTipoBoleto == "2") {
                let estudiantes = asiento.Cantidad;
                document.getElementById('estudiantes').textContent = " " + estudiantes;
            } else if (asiento.IdTipoBoleto == "3") {
                let adultoMayor = asiento.Cantidad;
                document.getElementById('adulto-mayor').textContent = " " + adultoMayor;
            }
            totalPrecio += precio;
        }
        document.getElementById('total').textContent = "Monto Total: ₡" + totalPrecio;
        return totalPrecio;
    }
    this.PayPalPayment = function (monto) {
        //console.log("monto Paypal: " + monto);

        let amount = monto.toString();
        //console.log("amount: " + amount);

        let paypalDto = {
            Currency: 'USD',
            Amount: amount
        };
        var paypalLoginUrl = "https://www.paypal.com/us/signin";
        window.open(paypalLoginUrl, "_blank");

        var apiUrl = API_URL_BASE + "/api/Paypal/CreatePayment";

        $.ajax({
            url: apiUrl,
            method: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "text",
            data: JSON.stringify(paypalDto)
        }).done(function (result) {
            Swal.fire({
                icon: 'success',
                title: 'Pago exitoso',
                text: 'El pago se ha realizado con éxito.',
                confirmButtonText: 'Aceptar'
            }).then(function () {
                var redirectUrl = $('#redirectData').data('index-url');
                window.location.href = redirectUrl;
            });
        }).fail(function (error) {
            console.log('Error en la api de PayPal: ' + error);
        });
    }
}
document.addEventListener("DOMContentLoaded", function () {
    var view = new Carrito();
    view.InitView();
});