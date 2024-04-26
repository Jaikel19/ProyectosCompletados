document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("loginForm").addEventListener("submit", function (event) {
        var emailInput = document.getElementById("emailInput");
        var passwordInput = document.getElementById("passwordInput");

        if (emailInput.value.trim() === "" || passwordInput.value.trim() === "") {
            event.preventDefault(); // Prevent form submission
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Por favor, completa todos los campos',
                confirmButtonText: 'Aceptar'
            });
        }
    });

    document.getElementById("loginButton").addEventListener("click", function () {
        var emailInput = document.getElementById("emailInput");
        var passwordInput = document.getElementById("passwordInput");

        if (emailInput.value.trim() === "" || passwordInput.value.trim() === "") {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Por favor, completa todos los campos',
                confirmButtonText: 'Aceptar'
            });
        }
    });
});
