function CreateEvent() {
    // Init view nos va a permitir inicializar funciones y eventos
    this.InitView = function () {
        this.InitTableListener();
        document.getElementById("btnCreate").addEventListener("click", this.handleFormSubmit.bind(this));
    }

    this.InitTableListener = function () {
        const table = document.getElementById("tblTipoBoletos");
        table.querySelectorAll("[capacity]").forEach(input => input.addEventListener('focusout', this.handleCapacityValues.bind(this)));
        table.querySelectorAll("[cost]").forEach(input => input.addEventListener('focusout', this.handleCostValues.bind(this)));
        table.querySelectorAll("[cortesia]").forEach(input => input.addEventListener('change', this.handleCortesia.bind(this)));
    }

    this.handleFormSubmit = function (event) {
        // Evitar que el formulario se envíe automáticamente
        event.preventDefault();

        // Realizar las validaciones del formulario
        const isFormValid = this.SubmitEventToCreate();

        if (isFormValid == undefined) return; 

        if (isFormValid) {
            const formElements = document.getElementById("frmEvents");
            formElements.submit();

            Swal.fire({
                icon: 'success',
                title: 'Éxito',
                text: 'Evento creado satisfactoriamente',
                confirmButtonText: 'Aceptar'
            }).then((result) => {
                if (result.isConfirmed) {
                    var redirectUrl = $('#redirectData').data('index-url');
                    window.location.href = redirectUrl;
                }
            });
        } else {
            // Display error message and scroll to the top
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Por favor, revisa los campos del formulario',
                confirmButtonText: 'Aceptar'
            }).then(() => {
              
            });
        }
    }

    this.handleCortesia = function (event) {
        event.preventDefault();

        const id = event.target.id;
        const costo = document.getElementById('costo_' + id); 

        if (event.target.checked) {
            costo.disabled = true;
            costo.value = 0;
        }
        else {
            costo.disabled = false;
        }
        
    };

    this.SubmitEventToCreate = function () {
        const formElements = document.getElementById("frmEvents").elements;
        let isFormValid = true;

        for (let i = 0; i < formElements.length; i++) {
            const element = formElements[i];

            if (
                (element.tagName === "INPUT" || element.tagName === "TEXTAREA" || element.tagName === "SELECT") &&
                element.type !== "hidden"
            ) {
                const value = element.value.trim();
                if (value === "") {
                    element.classList.add("is-invalid");
                    isFormValid = false;
                } else {
                    element.classList.remove("is-invalid");
                }
            }
        }
     
        var inputElements = document.getElementById("frmEvents").getElementsByTagName("input");
        var textareaElements = document.getElementById("frmEvents").getElementsByTagName("textarea");
        var selectElements = document.getElementById("frmEvents").getElementsByTagName("select");

        var elements = Array.from(inputElements)
            .concat(Array.from(textareaElements))
            .concat(Array.from(selectElements));

        for (let i = 0; i < elements.length; i++) {
            var element = elements[i];
            const value = element.value.trim();
            switch (element.id) {
                case "inputName":
                    if (value === "") {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;


                case "inputSlogan":
                    if (value === "") {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "EventoImagen":
                    const allowedExtensions = ["jpg", "jpeg", "png", "gif"];
                    const fileName = value.toLowerCase();
                    const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1);
                    
                    if (value === "" || !allowedExtensions.includes(fileExtension)) {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "inputGestor":
                    const telefonoRegex = /^\d{8}$/;
                    if (!telefonoRegex.test(value)) {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "inputDescription":
                case "txtDate":
                case "inputHour":
                case "longitud":
                case "inputRestrictions":
                    if (value.trim() === "") {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;
                case "modalidadSelect":
                case "scenarioSelect":
                    if (value === "") {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                default:
                    if (value === "") {
                        element.classList.add("is-invalid");
                    } else {
                        element.classList.remove("is-invalid");
                     
                    }

                    break;
            }
            const eventDate = new Date(document.getElementById('txtDate').value);

            // Validacion que la fecha del evento sea mayor o igual a la fecha actual
            const currentDate = new Date();
            currentDate.setHours(0, 0, 0, 0);
            if (eventDate < currentDate) {
                document.getElementById('txtDate').classList.add("is-invalid");
                this.showMessage('La fecha del evento seleccionada ya ha pasado.', null);
                isFormValid = false;
            }

            const table = document.getElementById("tblTipoBoletos");
            const tipoBoletosData = [];
            var totalBoletos = 0;

            table.querySelectorAll("tr[datarows]").forEach(row => {
                const id = row.getAttribute("tipoboletoid");
                const capacity = row.querySelector("[capacity]").value;
                const cost = row.querySelector("[cost]").value;
                const nombre = row.querySelector("td").textContent;
                const cortesia = row.querySelector("[cortesia]").checked;
                totalBoletos = totalBoletos + parseInt(capacity);

                tipoBoletosData.push({
                    Id: id,
                    Cantidad: capacity,
                    Precio: cost,
                    Nombre: nombre,
                    Cortesia: cortesia
                });
            });
            const tipoBoletosStr = JSON.stringify(tipoBoletosData);
            const tipoBoletosInput = document.getElementById("tipoBoletos");
            tipoBoletosInput.value = tipoBoletosStr;


            var boletosobj = document.getElementById("tipoBoletos");
            const table2 = document.getElementById("tblTipoBoletos");
            var capacidadTotal = table2.querySelectorAll("[capacity]");
            var CapacidadDTO = document.getElementById("capacidadTotal");
            CapacidadDTO.value = capacidadTotal;

            if (parseInt(CapacidadDTO.textContent) > totalBoletos) {
                this.showMessage('La cantidad de boletos no coincide con la capacidad total. Capacidad: ' + CapacidadDTO.textContent + '. Boletos distribuidos: ' + totalBoletos, () => { });
                return;
            }

            document.getElementById("Capacidad").value = CapacidadDTO.textContent;
        }
        return isFormValid;
    }

    this.handleCapacityValues = function (event) {
        event.preventDefault();
        const target = event.target;
        const value = parseInt(target.value);

        if (value < 0) {
            this.showMessage('No se permite números negativos', () => { });
            event.target.value = 0;
        }
        else { event.target.value = value; }

        const table = document.getElementById("tblTipoBoletos");
        const totalCapacity = parseInt(document.getElementById('capacidadTotal').textContent);
        var elements = table.querySelectorAll("[capacity]");
        var capacity = 0;

        for (var i = 0; i < elements.length; i++) {
            if (elements[i].value != "") {
                capacity = parseInt(capacity) + parseInt(elements[i].value);
            }
        }

        if (capacity > totalCapacity) {
            target.classList.add("is-invalid");
            this.showMessage('La capacidad distribuida: ' + capacity + ', es mayor a la capacidad total: ' + totalCapacity, () => {
                target.focus()
            });
            document.getElementById("btnCreate").classList.add("disabled");
        }
        else {
            target.classList.remove("is-invalid");
            document.getElementById("btnCreate").classList.remove("disabled");
        }
    }

    this.handleCostValues = function (event) {
        event.preventDefault();
        const target = event.target;
        const value = parseFloat(target.value);

        if (value < 0) {
            event.target.value = 0;
            this.showMessage('No se permite números negativos', () => { target.focus() });
        }
        else { event.target.value = value; }
    }

    this.showMessage = function (msg, result) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: msg,
            confirmButtonText: 'Aceptar'
        }).then(result);
    }
}
$(document).ready(function () {
    var view = new CreateEvent();
    view.InitView();
}); 