var map;
var infowindow;

function initMap() {

    var pyrmont = { lat: -23.574105, lng: -46.623281 };

    map = new google.maps.Map(document.getElementById('map'), {
        center: pyrmont,
        zoom: 15
    });


    requests(map, pyrmont);

    // adiciona a marcação a localizaçao da pessoa
    var markerImage = '/Content/img/marker.png';

    var marker = new google.maps.Marker({
        position: pyrmont,
        map: map,
        icon: markerImage
    });

    var contentString = '<div class="info-window">' +
        '<h4>Você está aqui</h4>' +
        '</div>';

    marker.addListener('click', function () {
        infowindow.setContent(contentString);

        infowindow.open(map, this);
    });


    // verifica se o navegador tem suporte a geolocalização
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) { // callback de sucesso
            // ajusta a posição do marker para a localização do usuário
            const myPosition = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            marker.setPosition(myPosition);
            map.panTo(myPosition);
            requests(map, myPosition);
        },
            function (error) { // callback de erro
                toastr.error('Erro ao obter localização.', error);
            });
    } else {
        toastr.warning('Navegador não suporta Geolocalização!');
    }

    var input = document.getElementById('search-place');
    var searchBox = new google.maps.places.SearchBox(input);

    google.maps.event.addListener(searchBox, "places_changed", function () {
        const places = searchBox.getPlaces()[0];

        const myPosition = new google.maps.LatLng(places.geometry.location.lat(), places.geometry.location.lng());
        marker.setPosition(myPosition);
        map.panTo(myPosition);
        requests(map, myPosition);
    });
}

function requests(map, location) {
    var request = {
        location: location,
        radius: 1000,
        keyword: 'sexshop'
    };
    infowindow = new google.maps.InfoWindow();
    var service = new google.maps.places.PlacesService(map);
    service.nearbySearch(request, callback);
    var request2 = {
        location: location,
        radius: 1000,
        keyword: 'sex shop'
    };
    var service2 = new google.maps.places.PlacesService(map);
    service2.nearbySearch(request2, callback);
}

function callback(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarker(results[i]);
        }
    }
}

function createMarker(place) {
    var placeLoc = place.geometry.location;
    var marker = new google.maps.Marker({
        map: map,
        position: place.geometry.location
    });

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' +
            place.vicinity + '</div>');
        infowindow.open(map, this);
    });
}