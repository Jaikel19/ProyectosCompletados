//Generar las butacas
function generateSeats(rows, seatsPerRow) {
    const seatMap = document.getElementById("seat-map");
    // Limpiar el contenido del contenedor antes de agregar nuevos íconos
    seatMap.innerHTML = "";

    for (let row = 1; row <= rows; row++) {
        const rowElement = document.createElement("div");
        rowElement.classList.add("row");

        for (let seat = 1; seat <= seatsPerRow; seat++) {
            const seatElement = document.createElement("div");
            seatElement.classList.add("seat", "available");
            seatElement.setAttribute("data-row", row);
            seatElement.setAttribute("data-seat", seat);

            // Insertar el ícono de Font Awesome como HTML dentro del div del asiento
            seatElement.innerHTML = '<i class="fa fa-chair"></i>';

            //  seatElement.addEventListener("click", toggleSeat);
            rowElement.appendChild(seatElement);
        }

        seatMap.appendChild(rowElement);
    }
}

function toggleSeat() {
    if (this.classList.contains("selected")) {
        this.classList.remove("selected");
    } else if (this.classList.contains("available")) {
        this.classList.add("selected");
    }
}

// Función para manejar el evento "change" del select y sacar capacity
let capacity = 0;
function handleScenarioChange() {
    const capacidadTotal = document.getElementById('capacidadTotal');
    const selectedOption = scenarioSelect.options[scenarioSelect.selectedIndex].value;

    const rows = selectedOption === "Cine" ? 6 : selectedOption === "Estadio" ? 10 : selectedOption === "Salón de eventos" ? 8 : selectedOption === "Teatro" ? 5 : 0;
    const seatsPerRow = selectedOption === "Cine" ? 10 : selectedOption === "Estadio" ? 12 : selectedOption === "Salón de eventos" ? 10 : selectedOption === "Teatro" ? 10 : 0;

    capacity = rows * seatsPerRow;
    capacidadTotal.textContent = capacity;

    generateSeats(rows, seatsPerRow);

}

//evento onchange para el dropdown de escenarios
const scenarioSelect = document.getElementById("scenarioSelect");
scenarioSelect.addEventListener("change", handleScenarioChange);

// Ejecutar la función al arrancar la app
handleScenarioChange();
