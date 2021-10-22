let map;
let markers = [];

function myMap(id) {

    try {

        const mapContainer = document.getElementById(`orderContainer-${id}`);

        const googleMap = document.createElement("div");
        googleMap.setAttribute("id", `googleMap-${id}`);
        googleMap.className = "mb-5";
        googleMap.style = "height:400px;width:100%;"

        mapContainer.appendChild(googleMap);
        
        const haightAshbury = { lat: 9.942130848156067, lng: -84.13357975757967};

        map = new google.maps.Map(googleMap, {
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

        
        // This event listener will call addMarker() when the map is clicked.
        map.addListener("click", (event) => {
            addMarker(event.latLng, id);
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
function addMarker(position, id) {

    let coords = JSON.parse(JSON.stringify(position));

    document.getElementById(`location-${id}`).value = `${coords.lat} ${coords.lng}`;

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

 
const startOrder = async (id, price) =>{
    
    let btnCancelOrder = document.getElementsByName("btnCancel");
    if(btnCancelOrder.length === 1) btnCancelOrder[0].click();
    
    const orderBtn = document.getElementById(`btnOrder-${id}`);
    orderBtn.remove();
    
    const container = document.getElementById(`orderContainer-${id}`);

    const title = document.createElement("h1");
    title.textContent = "Location to send the product";
    title.className = "text-white text-center mt-5";
    title.setAttribute("id", `title-${id}`);


    const divider = document.createElement("hr");
    divider.className = "bg-white";
    divider.setAttribute("id", `divider-${id}`);
    
    container.appendChild(title);
    container.appendChild(divider);
    
    myMap(id);
    
    const btnConfirmOrder = document.createElement("btn");
    btnConfirmOrder.setAttribute("id", `btnConfirm-${id}`)
    btnConfirmOrder.className = "btn btn-outline-primary";
    btnConfirmOrder.textContent = "Confirm order";
    btnConfirmOrder.addEventListener("click", async () => await buy(id,price) );
    
    btnCancelOrder = document.createElement("btn");
    btnCancelOrder.setAttribute("id", `btnCancel-${id}`)
    btnCancelOrder.className = "btn btn-outline-danger";
    btnCancelOrder.textContent = "Cancel order";
    btnCancelOrder.addEventListener("click", () =>{
        cancelOrder(id, price);
    });
    btnCancelOrder.setAttribute("name", "btnCancel")
    
    container.appendChild(btnConfirmOrder);
    container.appendChild(btnCancelOrder);
    
}

const cancelOrder = (id, price) => {
    const btnContainer = document.getElementById(`btnContainer-${id}`);

    document.getElementById(`googleMap-${id}`).remove();
    document.getElementById(`btnCancel-${id}`).remove();
    document.getElementById(`btnConfirm-${id}`).remove();
    document.getElementById(`title-${id}`).remove();
    document.getElementById(`divider-${id}`).remove();
    
    
    const btn = document.createElement("btn");
    btn.className = "btn btn-outline-light btn-lg";
    btn.setAttribute("id", `btnOrder-${id}`);
    btn.textContent = "Order";
    btn.addEventListener("click", async () =>{await startOrder(id, price)})
    
    btnContainer.appendChild(btn);
    
}

const changeTotal = (id, price) => {
    const total = document.getElementById(`total-${id}`);
    const newAmount = document.getElementById(`amount-${id}`).value;

    total.textContent = `Total: ${(price * newAmount).toFixed(2)}`;
}

const buy = async (id, price) => {
    const amount = document.getElementById(`amount-${id}`).value;

    const body = {
        id: id,
        amount: amount,
        price: price
    };

    const config = {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: "POST",
        body: JSON.stringify(body)
    }

    const response = await fetch("https://localhost:9000/v1/Cart", config);

    const result = await response.json();

    if (result.succeeded){
        Swal.fire({
            background: 'black',
            icon: 'success',
            title: 'New purchase',
            text: 'Product has been bought!',
            customClass: {
                'title': 'text-white',
                'text': 'text-white'
            }
        })

        return;
    }

    Swal.fire({
        background:'black',
        icon: 'error',
        title: 'Error',
        text: result.title,
        customClass: {
            'title':'text-white',
            'text':'text-white'
        }
    })

}


const deleteProduct = async (id) => {

    try {

        Swal.fire({
            background: 'black',
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            customClass: {
                'title': 'text-white',
                'text': 'text-white'
            }
        }).then(async (result) => {
            if (result.isConfirmed) {

                const result = await fetch(`https://localhost:9000/v1/Cart/${id}`, { method: 'DELETE' });

                const response = await result.json();

                if (response.statusCode !== 200) throw new Error(response.title);

                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'The product has been removed!',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        })


    } catch (e) {
        Swal.fire({
            background: 'black',
            icon: 'error',
            title: 'Error',
            text: e.message,
            customClass: {
                'title': 'text-white',
                'text': 'text-white'
            }
        })
    }
}