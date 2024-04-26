
document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("UserRegister");
    const submitButton = document.getElementById("btnCreate");

    form.addEventListener("submit", function (event) {

        event.preventDefault();

        const isFormValid = SubmitEventToCreate();

        if (isFormValid) {
            Swal.fire({
                icon: 'success',
                title: 'Usuario creado',
                text: 'El usuario ha sido creado exitosamente.',
                confirmButtonText: 'Aceptar'
            });
            form.submit();
        }
    });



    function SubmitEventToCreate() {


        const formElements = document.getElementById("UserRegister").elements;

        let isFormValid = true;

        //nada vacio
        for (let i = 0; i < formElements.length; i++) {
            const element = formElements[i];

            if (element.tagName === "INPUT") {
                const value = element.value.trim();

                if (value === "") {
                    element.classList.add("is-invalid");
                    isFormValid = false;
                } else {
                    element.classList.remove("is-invalid");
                }
            }
        }

        //validacion labels
        var elements = document.getElementById("UserRegister").getElementsByTagName("input");
        for (let i = 0; i < elements.length; i++) {
            var element = elements[i];
            const value = element.value.trim();

            switch (element.id) {
                case "Nombre":
                    const nameCharactersRegex = /^[A-Za-z]+$/;;
                    if (!nameCharactersRegex.test(value)) {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "Apellido":
                    const lastCharactersRegex = /^[A-Za-z]+$/;;
                    if (!lastCharactersRegex.test(value)) {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "Email":
                    const emailCharactersRegex = /^[\w.-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$/;
                    if (!emailCharactersRegex.test(value)) {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "Password":
                    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
                    if (!passwordRegex.test(value)) {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "Cedula":
                    const cedulaRegex = /^\d{10}$/;
                    if (!cedulaRegex.test(value)) {
                        element.classList.add("is-invalid");
                        element.nextElementSibling.style.display = "block";
                        isFormValid = false;
                    } else {
                        element.classList.remove("is-invalid");
                        element.nextElementSibling.style.display = "none";
                    }
                    break;

                case "Telefono":
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

                case "longitud":
                    if (value.trim() === "") {
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
        }
        return isFormValid;
    }
});