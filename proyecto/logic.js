$(document).ready(function() {
    $('.scroll-down').click(function(event) {
        event.preventDefault(); // Evita el comportamiento predeterminado del enlace

        var target = $('.capa-logo'); // Obtiene el elemento de destino
        var offset = target.offset().top; // Obtiene la posición superior del elemento de destino

        $('html, body').animate({scrollTop: offset}, 'slow'); // Realiza la animación de desplazamiento
    });
});

$(document).ready(function() {
    $('#menuToggle').click(function(e) {
        e.preventDefault(); // Evitar el comportamiento predeterminado del enlace
        $('#dropdownMenu').slideToggle();
    });
});


// Función para cambiar el idioma
function toggleLanguage() {
    const content = document.getElementById("content");
    const languageSwitch = document.getElementById("languageSwitch");

    // Verifica el estado del interruptor
    if (languageSwitch.checked) {
        // Traduce el contenido al inglés
        translateContent(content, "en");
        translateHeader("en");
        translateNav("en");
    } else {
        // Traduce el contenido al español
        translateContent(content, "es");
        translateHeader("es");
        translateNav("es");
    }
}

// Función para traducir el contenido del header
function translateHeader(targetLanguage) {
    const header = document.getElementById("header");
    const h4Element = header.querySelector("h4");

    // Obtiene el texto del h4
    const textToTranslate = h4Element.textContent;

    // Realiza la traducción usando la API de Google Translate
    fetch(`https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=${targetLanguage}&dt=t&q=${encodeURI(textToTranslate)}`)
        .then(response => response.json())
        .then(data => {
            // Actualiza el contenido traducido
            const translatedText = data[0][0][0];
            h4Element.textContent = translatedText;
        })
        .catch(error => console.error("Error al traducir el h4:", error));
}

// Función para traducir el contenido del nav
function translateNav(targetLanguage) {
    const navContent = document.querySelector("nav.navbar");
    const textElements = navContent.querySelectorAll("a");

    // Itera sobre cada elemento y traduce su contenido
    textElements.forEach(element => {
        const textToTranslate = element.textContent;

        // Realiza la traducción usando la API de Google Translate
        fetch(`https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=${targetLanguage}&dt=t&q=${encodeURI(textToTranslate)}`)
            .then(response => response.json())
            .then(data => {
                // Actualiza el contenido traducido
                const translatedText = data[0][0][0];
                element.textContent = translatedText;
            })
            .catch(error => console.error("Error al traducir:", error));
    });
}

// Función para traducir el contenido del div #content
function translateContent(content, targetLanguage) {
    // Obtiene todos los elementos de texto dentro del contenedor
    const textElements = content.querySelectorAll("h1, p, h2, a, label, span, button, a:not(:has(img)), i:not(.icon)");

    // Itera sobre cada elemento y traduce su contenido
    textElements.forEach(element => {
        const textToTranslate = element.textContent;

        // Realiza la traducción usando la API de Google Translate
        fetch(`https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=${targetLanguage}&dt=t&q=${encodeURI(textToTranslate)}`)
            .then(response => response.json())
            .then(data => {
                // Actualiza el contenido traducido
                const translatedText = data[0][0][0];
                element.textContent = translatedText;
            })
            .catch(error => console.error("Error al traducir:", error));
    });
}


window.onscroll = function() {scrollFunction()};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("scrollButton").style.display = "block"; /* Mostrar el botón al desplazarse hacia abajo */
    } else {
        document.getElementById("scrollButton").style.display = "none"; /* Ocultar el botón al volver arriba */
    }
}

function scrollToTop() {
    document.body.scrollTop = 0; /* Para navegadores Safari */
    document.documentElement.scrollTop = 0; /* Para otros navegadores */
}


// Traducción inicial al español
toggleLanguage();


document.getElementById("downloadCVSpanish").addEventListener("click", function() {
    // Descargar el CV en español
    window.open('Tarea3CurriculimES.pdf', '_blank');
});

document.getElementById("downloadCVEnglish").addEventListener("click", function() {
    // Descargar el CV en inglés
    window.open('Tarea3CurriculumENG.pdf', '_blank');
});



const form = document.getElementById('sendEmail')

async function handleSendEmail(event) {
  event.preventDefault()

  const fd = new FormData(this)

  const response = await fetch('https://formspree.io/f/xgegrjpz', {
    method: 'POST',
    body: fd,
    headers: {
      Accept: 'application/json'
    }
  })

  if (response.ok) {
    this.reset()
    alert('Mensaje enviado')
  } else {
    alert('Error al enviar el mensaje')
  }
}

form.addEventListener('submit', handleSendEmail)