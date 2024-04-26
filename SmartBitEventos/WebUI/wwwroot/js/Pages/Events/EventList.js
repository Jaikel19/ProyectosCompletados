function EventList() {
    this.InitView = function () {
        const table = document.getElementById("tblTipoBoletos");
        table.querySelectorAll("[capacity]").forEach(input => input.addEventListener('focusout', this.handleCapacityValues.bind(this)));
        document.getElementById("btnBuy").addEventListener("click", this.handleFormSubmit.bind(this));
        document.getElementById("compraBoletos").addEventListener("click", this.showEventDetails.bind(this));
    };

    this.handleCapacityValues = function (event) {
        event.preventDefault();
        const target = event.target;
        const value = parseInt(target.value);

        if (value < 0) {
            showMessage('No se permite números negativos', () => { });
            event.target.value = 0;
        }
        else { event.target.value = value; }

        const totalCapacity = parseInt(document.getElementById('asientosDisponibles').value);
        var capacity = value;

        if (capacity > totalCapacity) {
            target.classList.add("is-invalid");
            showMessage('La capacidad distribuida: ' + capacity + ', es mayor a la capacidad disponible: ' + totalCapacity, () => {
                target.focus()
            });
            document.getElementById("btnBuy").classList.add("disabled");
            return;
        }
        else {
            target.classList.remove("is-invalid");
            document.getElementById("btnBuy").classList.remove("disabled");
        }

        //add asientos to list 
        var tiposDeAsientos = document.getElementById("tiposDeAsiento");
        var asientos = [];

        if (tiposDeAsientos.value != '') {
            asientos = JSON.parse(tiposDeAsientos.value);
        }
        const tipoBoletoId = target.getAttribute("tipoboletoId");
        const precio = target.getAttribute("precio");

        const elemento = asientos.filter(function (val) {
            return val.tipoBoletoId == tipoBoletoId;
        });

        if (elemento.length > 0) {
            for (var i = 0; i < asientos.length; i++) {
                if (asientos[i].tipoBoletoId == tipoBoletoId) {
                    asientos[i].cantidad = capacity;
                }
            }
        }
        else {
            asientos.push({
                IdTipoBoleto: tipoBoletoId,
                Cantidad: capacity,
                Precio: precio
            })
        }
        tiposDeAsientos.value = JSON.stringify(asientos);
        //console.log('asientos: ' + asientos);
        //console.log('json: ' + JSON.stringify(asientos));
        localStorage.setItem("asientos", JSON.stringify(asientos));
    }

    this.handleFormSubmit = function (event) {
        event.preventDefault();

        var asientosSolicitados = document.getElementById("asientosSolicitados");
        var eventoId = document.getElementById("eventoId");
        var tiposDeAsiento = document.getElementById("tiposDeAsiento");

        var apiUrl = "/Events/ComprarBoleto";
        $.ajax({
            url: apiUrl,
            method: "Post",
            data: { asientosSolicitados: asientosSolicitados.value, eventoId: eventoId.value, tiposDeAsiento: tiposDeAsiento.value },
        }).done(function (boletoId) {
            Swal.fire({
                icon: 'success',
                title: 'Éxito',
                text: 'Boletos seleccionados satisfactoriamente, puedes proceder a realizar el pago.',
                confirmButtonText: 'Aceptar'
            }).then((result) => {
                // pasar de pantalla
                if (result.isConfirmed) {
                    var redirectUrl = "/Carrito/Carrito?IdBoleto=" + boletoId;
                    localStorage.setItem("boletoId", JSON.stringify(boletoId));
                    window.location.href = redirectUrl;
                   //console.log(boletoId);
                }
            });
        }).fail(function (error) {
            console.log(error);
        });
    };
    
    this.showEventDetails = function (e) {
        const capacidad = document.getElementById("capacidad");
        if (capacidad) {
            $('#myModal').modal('toggle');
            handleScenarioChange(capacidad.value);
        }
    };

    function generateSeats(rows, seatsPerRow) {
        const seatMap = document.getElementById("seat-map");
        const eventoAsientos = document.getElementById("eventoAsientos");
        const asientos = JSON.parse(eventoAsientos.value);
        var contador = 1;

        seatMap.innerHTML = "";

        for (let row = 1; row <= rows; row++) {
            const rowElement = document.createElement("div");
            rowElement.classList.add("row");

            for (let seat = 1; seat <= seatsPerRow; seat++) {
                const seatElement = document.createElement("div");
                const asientoOcupado = asientos.filter(function (val) {
                    return val.Asiento == contador;
                });

                if (asientoOcupado.length > 0) {
                    seatElement.classList.add("seat", "selected");
                    seatElement.classList.add("seat", "disabledClick");
                }
                else {
                    seatElement.classList.add("seat", "available");
                }

                seatElement.setAttribute("data-row", row);
                seatElement.setAttribute("data-seat", seat);
                seatElement.setAttribute("id-seat", contador);

                // Font Awesome
                seatElement.innerHTML = '<i class="fa fa-chair"></i>';

                seatElement.addEventListener("click", toggleSeat);
                rowElement.appendChild(seatElement);
                contador++;
            }

            seatMap.appendChild(rowElement);
        }
    }

    function toggleSeat() {

        if (this.classList.contains("available")) {
            var tiposDeAsientos = document.getElementById("tiposDeAsiento");
            var asientos = [];

            if (tiposDeAsientos.value == '') {
                showMessage("Debe asignar cantidad al tipo de boleto que desea comprar.", () => { });
                return;
            }
            var asientosSolicitados = document.getElementById("asientosSolicitados");
            var solicitados = [];

            asientos = JSON.parse(tiposDeAsientos.value);

            if (asientosSolicitados.value != '') {
                solicitados = JSON.parse(asientosSolicitados.value);
            }
            var cantidadSolicitada = 0;
            for (var i = 0; i < asientos.length; i++) {
                cantidadSolicitada = cantidadSolicitada + parseInt(asientos[i].Cantidad);
            }

            if (cantidadSolicitada == solicitados.length) {
                showMessage("Ha seleccionado la cantidad de boletos solicitados.", () => { });
                return;
            }
            var eventoId = document.getElementById("eventoId");
            var next = false;
            var asignados = 0;
            for (var i = 0; i < asientos.length; i++) {

                if (next) {
                    if (asientos[i].Cantidad > solicitados.length - asignados) {
                        solicitados.push({ Asiento: this.getAttribute("id-seat"), IdTipoBoleto: asientos[i].IdTipoBoleto, IdEvento: eventoId.value });
                    }
                }
                else {
                    if (asientos[i].Cantidad > solicitados.length) {
                        solicitados.push({ Asiento: this.getAttribute("id-seat"), IdTipoBoleto: asientos[i].IdTipoBoleto, IdEvento: eventoId.value });
                    }
                }

                asignados = asignados + asientos[i].cantidad;
                next = true;
            }

            asientosSolicitados.value = JSON.stringify(solicitados);
            this.classList.add("selected");
            this.classList.remove("available");
        }
        else if (this.classList.contains("selected")) {
            this.classList.remove("selected");
            this.classList.add("available");

            var asientosSolicitados = document.getElementById("asientosSolicitados");
            var solicitados = [];

            if (asientosSolicitados.value != '') {
                solicitados = JSON.parse(asientosSolicitados.value);

                var asiento = this.getAttribute("id-seat")

                for (var i = 0; i < solicitados.length; i++) {
                    if (solicitados[i].Asiento == asiento) {
                        solicitados.splice(i, 1);

                    }
                }
                asientosSolicitados.value = JSON.stringify(solicitados);
            }
            console.log(asientos);
        }
    }

    function handleScenarioChange(escenario) {
        var rows = 0;
        var seats = 0;
        switch (escenario) {
            case "60":
                rows = 6;
                seats = 10;
                break;
            case "120":
                rows = 10;
                seats = 12;
                break;
            case "80":
                rows = 8;
                seats = 10;
                break;
            case "50":
                rows = 5;
                seats = 10;
                break;
        }
        generateSeats(rows, seats);
    }

    function showMessage(msg, result) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: msg,
            confirmButtonText: 'Aceptar'
        }).then(result);
    }
}

$(document).ready(function () {
    var view = new EventList();
    view.InitView();
});
