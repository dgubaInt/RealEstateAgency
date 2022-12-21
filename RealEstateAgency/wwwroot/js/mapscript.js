function initMap() {
    let mapElement = 'estate_map';
    let address = document.getElementById("address").value;
    let title = document.getElementById("estate_name").value;
    let infowindow = new google.maps.InfoWindow({
        content: "<strong>Name:</strong> " + title + "<br><strong>Address:</strong> " + address
            
    });
    //debugger;
    if (address != "") {
        geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var mapOptions = {
                    zoom: 15,
                    center: results[0].geometry.location,
                    disableDefaultUI: true
                };
                var map = new google.maps.Map(document.getElementById(mapElement), mapOptions);
                var marker = new google.maps.Marker({
                    title: title,
                    position: results[0].geometry.location
                });
                marker.setMap(map);
                google.maps.event.addListener(marker, 'click', function () {
                    map.setZoom(18);
                    map.setCenter(marker.getPosition());
                    infowindow.open(map, marker);
                });

                document.getElementById("estate_map").style.height = "400px";
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
    }
}