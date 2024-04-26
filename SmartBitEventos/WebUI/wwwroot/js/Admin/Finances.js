document.addEventListener("DOMContentLoaded", function () {
    const editSuperior = document.getElementById("editSuperior");
    const editInfinito = document.getElementById("editInfinito");
    const editGanancias = document.getElementById("editGanancias");
    const inputSuperior = document.getElementById("inputSuperior");
    const inputInfinito = document.getElementById("inputInfinito");
    const inputGanancias = document.getElementById("inputGanancias");

    function updateValue(inputElement, newValue) {
        inputElement.value = newValue;
    }

    function Superior() {
        const newValue = parseInt(inputSuperior.value);
        if (newValue < 0) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'El valor ingresado no puede ser menor a 0.',
                confirmButtonText: 'Aceptar'
            }).then(() => {
                updateValue(inputSuperior, 0);
            });
        } else {
            Swal.fire({
                icon: 'success',
                title: 'Valor modificado.',
                text: `El nuevo valor para la membresía superior es: ${newValue}`,
                confirmButtonText: 'Aceptar'
            });
        }
    }

    function Infinito() {
        const newValue = parseInt(inputInfinito.value);
        if (newValue < 0) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'El valor ingresado no puede ser menor a 0.',
                confirmButtonText: 'Aceptar'
            }).then(() => {
                updateValue(inputInfinito, 0);
            });
        } else {
            Swal.fire({
                icon: 'success',
                title: 'Valor modificado.',
                text: `El nuevo valor para la membresía ilimitada es: ${newValue}`,
                confirmButtonText: 'Aceptar'
            });
        }
    }

    function Ganancias() {
        const newValue = parseInt(inputGanancias.value);
        if (newValue < 0) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'El valor ingresado no puede ser menor a 0.',
                confirmButtonText: 'Aceptar'
            }).then(() => {
                updateValue(inputGanancias, 0);
            });
        } else {
            Swal.fire({
                icon: 'success',
                title: 'Valor modificado.',
                text: `El nuevo valor para el impuesto es: ${newValue}`,
                confirmButtonText: 'Aceptar'
            });
        }
    }

    editSuperior.addEventListener("click", Superior);
    editInfinito.addEventListener("click", Infinito);
    editGanancias.addEventListener("click", Ganancias);
});