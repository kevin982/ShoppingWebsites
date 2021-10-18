let map;
let markers = [];


function myMap() {

    try {
        const haightAshbury = { lat: 9.942130848156067, lng: -84.13357975757967};

        map = new google.maps.Map(document.getElementById("googleMap"), {
            zoom: 12,
            center: haightAshbury,
            styles: [{
                "featureType": "all",
                "elementType": "labels.text.fill",
                "stylers": [{
                    "saturation": 36
                }, {
                    "color": "#000000"
                }, {
                    "lightness": 40
                }]
            }, {
                "featureType": "all",
                "elementType": "labels.text.stroke",
                "stylers": [{
                    "visibility": "on"
                }, {
                    "color": "#000000"
                }, {
                    "lightness": 16
                }]
            }, {
                "featureType": "all",
                "elementType": "labels.icon",
                "stylers": [{
                    "visibility": "off"
                }]
            }, {
                "featureType": "administrative",
                "elementType": "geometry.fill",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 20
                }]
            }, {
                "featureType": "administrative",
                "elementType": "geometry.stroke",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 17
                }, {
                    "weight": 1.2
                }]
            }, {
                "featureType": "administrative",
                "elementType": "labels",
                "stylers": [{
                    "visibility": "off"
                }]
            }, {
                "featureType": "administrative.country",
                "elementType": "all",
                "stylers": [{
                    "visibility": "simplified"
                }]
            }, {
                "featureType": "administrative.country",
                "elementType": "geometry",
                "stylers": [{
                    "visibility": "simplified"
                }]
            }, {
                "featureType": "administrative.country",
                "elementType": "labels.text",
                "stylers": [{
                    "visibility": "simplified"
                }]
            }, {
                "featureType": "administrative.province",
                "elementType": "all",
                "stylers": [{
                    "visibility": "off"
                }]
            }, {
                "featureType": "administrative.locality",
                "elementType": "all",
                "stylers": [{
                    "visibility": "simplified"
                }, {
                    "saturation": "-100"
                }, {
                    "lightness": "30"
                }]
            }, {
                "featureType": "administrative.neighborhood",
                "elementType": "all",
                "stylers": [{
                    "visibility": "off"
                }]
            }, {
                "featureType": "administrative.land_parcel",
                "elementType": "all",
                "stylers": [{
                    "visibility": "off"
                }]
            }, {
                "featureType": "landscape",
                "elementType": "all",
                "stylers": [{
                    "visibility": "simplified"
                }, {
                    "gamma": "0.00"
                }, {
                    "lightness": "74"
                }]
            }, {
                "featureType": "landscape",
                "elementType": "geometry",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 20
                }]
            }, {
                "featureType": "landscape.man_made",
                "elementType": "all",
                "stylers": [{
                    "lightness": "3"
                }]
            }, {
                "featureType": "poi",
                "elementType": "all",
                "stylers": [{
                    "visibility": "off"
                }]
            }, {
                "featureType": "poi",
                "elementType": "geometry",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 21
                }]
            }, {
                "featureType": "road",
                "elementType": "geometry",
                "stylers": [{
                    "visibility": "simplified"
                }]
            }, {
                "featureType": "road.highway",
                "elementType": "geometry.fill",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 17
                }]
            }, {
                "featureType": "road.highway",
                "elementType": "geometry.stroke",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 29
                }, {
                    "weight": 0.2
                }]
            }, {
                "featureType": "road.arterial",
                "elementType": "geometry",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 18
                }]
            }, {
                "featureType": "road.local",
                "elementType": "geometry",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 16
                }]
            }, {
                "featureType": "transit",
                "elementType": "geometry",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 19
                }]
            }, {
                "featureType": "water",
                "elementType": "geometry",
                "stylers": [{
                    "color": "#000000"
                }, {
                    "lightness": 17
                }]
            }]
        });

        console.log(map);

        // This event listener will call addMarker() when the map is clicked.
        map.addListener("click", (event) => {
            addMarker(event.latLng);
        });
        // add event listeners for the buttons
        document
            .getElementById("show-markers")
            .addEventListener("click", showMarkers);
        document
            .getElementById("hide-markers")
            .addEventListener("click", hideMarkers);
        document
            .getElementById("delete-markers")
            .addEventListener("click", deleteMarkers);
        // Adds a marker at the center of the map.
        addMarker(haightAshbury);
    } catch (e) {
        console.log(e);
    }
}

// Adds a marker to the map and push to the array.
function addMarker(position) {

    let coords = JSON.parse(JSON.stringify(position));

    document.getElementById("location").value = `${coords.lat} ${coords.lng}`;
 
    if (markers.length > 0) deleteMarkers();

    const marker = new google.maps.Marker({
        position,
        map,
    });

    markers.push(marker);
}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function hideMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    hideMarkers();
    markers = [];
}

const addMap = () => {

    const mapContainer = document.getElementById("mapContainer");

    const map = document.createElement("div");
    map.setAttribute("id", "googleMap");
    map.style = "height:400px;width:100%;"

    mapContainer.appendChild(map);

    myMap();
}

const removeMap = () => {
    document.getElementById("location").value ="";
    document.getElementById("googleMap").remove();
}

const addCategories = async () => {

    try {

        const result = await fetch("https://localhost:9000/v1/WebsiteCategories");

        const response = await result.json();

        if (!response.succeeded) throw new Error(response.title);

        const categories = document.getElementById("websiteCategories");

        for (let i = 0; i < response.content.length; i++) {
            const option = document.createElement("option");
            option.className="text-white"
            option.setAttribute("value", response.content[i].categoryId);
            option.textContent = response.content[i].categoryName;

            categories.append(option);
        }

    } catch (e) {
        throw e;
    }
}

window.addEventListener("load", async () => {

    try {

        await addCategories();

    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'We could add the website categories', text: e.message, customClass: {'title':'text-white', 'text':'text-white'}})
    }
});

document.getElementById("physicalLocationCheckbox").addEventListener("change", () => {
    const value = document.getElementById("physicalLocationCheckbox").checked;

    (value) ? addMap() : removeMap();
});
 