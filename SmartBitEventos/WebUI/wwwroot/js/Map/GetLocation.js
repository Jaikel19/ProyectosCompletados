var map;
var marker;

function initMap() {
    var initialPosition = { lat: 9.9280694, lng: -84.0907246 }; // San Jose, Costa Rica
    map = new google.maps.Map(document.getElementById('map'), {
        center: initialPosition,
        zoom: 14
    });

    marker = new google.maps.Marker({
        position: initialPosition,
        map: map,
        draggable: true
    });

    google.maps.event.addListener(marker, 'dragend', function (event) {
        document.getElementById('latitud').value = event.latLng.lat();
        document.getElementById('longitud').value = event.latLng.lng();
    });

    google.maps.event.addListener(map, 'click', function (event) {
        marker.setPosition(event.latLng);
        document.getElementById('latitud').value = event.latLng.lat();
        document.getElementById('longitud').value = event.latLng.lng();
    });
}