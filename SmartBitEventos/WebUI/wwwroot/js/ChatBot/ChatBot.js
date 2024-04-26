// para que se pueda haer grande y peque;o 
var collapsible = document.getElementsByClassName("collapsible");

for (let i = 0; i < collapsible.length; i++) {
    collapsible[i].addEventListener("click", function () {
        this.classList.toggle("active");

        var content = this.nextElementSibling;

        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }

    });
}


//funcion para el tiempo
function getTime() {
    let today = new Date();
    hours = today.getHours();
    minutes = today.getMinutes();

    if (hours < 10) {
        hours = "0" + hours;
    }

    if (minutes < 10) {
        minutes = "0" + minutes;
    }

    let time = hours + ":" + minutes;
    return time;
}

// mensaje inicial
function firstBotMessage() {
    let firstMessage = "¡Hola!\n¿En qué puedo ayudarte ?"
    document.getElementById("botStarterMessage").innerHTML = '<p class="botText"><span>' + firstMessage + '</span></p>';

    let time = getTime();

    $("#chat-time").append(time);
    document.getElementById("userInput").scrollIntoView(false);

    DisplayMenuOptions();
}

firstBotMessage();

function getHardResponse(userText) {
    let botResponse = getBotResponse(userText);
    let botHtml = '<p class="botText"><span>' + botResponse + '</span></p>';
    $("#chatbox").append(botHtml);

    document.getElementById("chat-bar-bottom").scrollIntoView(true);
}

function DisplayMenuOptions() {
    let innerHTML = '<p class="botText"><span> Menú: ' +
        '<button onclick="getEvents()" type="button" class="botones-faq">Lista de conciertos</button>' +
        '<button onclick="CompanyMenu()" type="button" class="botones-faq">Conózcanos</button>' +
        '<button onclick="getHardResponse(\'adios\')" type="button" class="botones-faq">No necesito más, gracias</button>' +
        '</span></p>';

    $("#chatbox").append(innerHTML);

    document.getElementById("userInput").scrollIntoView(false);
}

function CompanyMenu() {
    let innerHTML = '<p class="botText"><span> Conózcanos: ' +
        '<button onclick="getHardResponse(\'mision\')" type="button" class="botones-faq">Misión</button>' +
        '<button onclick="getHardResponse(\'vision\')" type="button" class="botones-faq">Visión</button>' +
        '<button onclick="DisplayMenuOptions()" type="button" class="botones-faq">Menú principal</button>' +
        '<button onclick="getHardResponse(\'adios\')" type="button" class="botones-faq">No necesito más, gracias</button>' +
        '</span></p>';

    $("#chatbox").append(innerHTML);

    document.getElementById("userInput").scrollIntoView(false);
}

// Function to send a user message to the server and display the response
function getEvents() {
    $.ajax({
        url: 'https://localhost:7270/api/Event/GetEventValue',
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
    }).done(function (events) {
        let eventButton = '<p class="botText"><span> Eventos: ';
        // Agrega un botón por cada evento en la lista
        for (let i = 0; i < events.length; i++) {
            eventButton += '<button onclick="sendMessage(\'' + encodeURIComponent(events[i].nombre) + '\')" type="button" class="botones-faq">' + events[i].nombre + '</button>';
        }
        eventButton += '<button onclick="DisplayMenuOptions()" type="button" class="btn btn-dark">Menú principal</button>';
        eventButton += '</span></p>';
        $("#chatbox").append(eventButton);

        document.getElementById("userInput").scrollIntoView(false);
    }).fail(function (error) {
        console.log(error);
    });
}

// Function to send a user message to the server and display the response
function sendMessage(message) {
    // Send the user message to the server
    $.ajax({
        url: 'https://localhost:7270/api/Event/GetEventsByName?Nombre=' + decodeURIComponent(message),
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
    }).done(function (result) {
        for (let i = 0; i < result.length; i++) {
            //addMessage('<p class="botText"><span>' + result[i].nombre + '</span></p>' + '<p class="botText"><span>' + result[i].fecha + '</span></p>' + '<p class="botText"><span>' + result[i].hora + '</span></p>' + '<button onclick="getEvents()" type="button" class="botones-faq">Lista de conciertos</button>');

            addMessage('<p class="botText"><span> Información del concierto: ' +
                '<p class="botText"><span>' + result[i].nombre + '</span></p>' +
                '<p class="botText"><span>' + result[i].fecha + '</span></p>' +
                '<p class="botText"><span>' + result[i].hora + '</span></p>' +
                '<button onclick="getEvents()" type="button" class="botones-faq">Lista de conciertos</button>' +
                '</span></p>');
        }

    }).fail(function (error) {
        console.log(error);
    });
}

// Helper function to display bot messages
function displayBotMessage(message) {
    let botHtml = '<p class="botText"><span>' + message + '</span></p>';
    $("#chatbox").append(botHtml);
    document.getElementById("chat-bar-bottom").scrollIntoView(true);
}


function Suggestions(e) {
    getHardResponse(e.target.innerText);
}


function addMessage(innerHtml) {
    $("#chatbox").append(innerHtml);
    document.getElementById("chat-bar-bottom").scrollIntoView(true);
}


//obtiene el texto, lo procesa y lo valida
function getResponse() {
    let userText = $("#textInput").val();

    if (userText == "") {
        userText = "Olvidé escribir en la caja de texto";
    }



    let userHtml = '<p class="userText"><span>' + userText + '</span></p>';

    $("#textInput").val("");
    $("#chatbox").append(userHtml);
    document.getElementById("chat-bar-bottom").scrollIntoView(true);

    setTimeout(() => {
        sendMessage(userText);
    }, 1000)

}

//envio de info dandole click al emoji
function buttonSendText(sampleText) {
    let userHtml = '<p class="userText"><span>' + sampleText + '</span></p>';

    $("#textInput").val("");
    $("#chatbox").append(userHtml);
    document.getElementById("chat-bar-bottom").scrollIntoView(true);

}

function sendButton() {
    getResponse();
}

// Mensajes con el enter
$("#textInput").keypress(function (e) {
    if (e.which == 13) { //13 es el codigo para el enter
        getResponse();
    }
});

//funcion de respuestas del bot
function getBotResponse(input) {
    if (input.toLowerCase() == "olvidé escribir en la caja de texto") {
        return "No hay problema!";
    }

    if (input.toLowerCase() == "mision") {
        return "Nuestra misión es ser un equipo altamente competente, pero en constante mejora de desarrolladores de software talentosos que están comprometidos a producir un software excelente y a fomentar un entorno de colaboración y confianza" + '<button onclick="CompanyMenu()" type="button" class="btn btn-dark">Menú sobre la compañia</button>';
    }

    if (input.toLowerCase() == "vision") {
        return "Nuestra visión es ser líderes en el desarrollo de soluciones tecnológicas innovadoras, enfocadas en satisfacer las necesidades de nuestros clientes y transformar positivamente su entorno digital" + '<button onclick="CompanyMenu()" type="button" class="btn btn-dark">Menú sobre la compañia</button>';
    }

    // Simple responses
    if (input.toLowerCase() == "hola") {
        return "¿Cómo estás?";
    } else if (input == "adios") {
        return "Hasta luego!" + '<button onclick="DisplayMenuOptions()" type="button" class="btn btn-dark">Menú principal</button>';
    } else {
        return "No tengo una respuesta programada, lo siento";
    }
}