let indexesCount = 1;
let currentIndex = 1;


function init() {

    try {
        const position = { lat: parseFloat(document.getElementById("website").getAttribute("lat")), lng: parseFloat(document.getElementById("website").getAttribute("lng"))};

        map = new google.maps.Map(document.getElementById("map"), {
            center: position,
            zoom: 12,
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

        const marker = new google.maps.Marker({
            position,
            map
        });

        console.log(map);

    } catch (e) {
        console.log(e);
    }
}

const addProductCategoriesAsync = async () => {
    try {

        const websiteId = document.getElementById("website").getAttribute("websiteId");

        const categories = document.getElementById("categories");

        const result = await fetch(`https://localhost:9000/v1/ProductCategories/${websiteId}`);

        const response = await result.json();

        if (!response.succeeded && response.statusCode !== 404) throw new Error(response.title);

        if (response.statusCode === 404) {
            let option = document.createElement("option");
            option.setAttribute("value", "00000000-0000-0000-0000-000000000000");
            option.textContent = "Without product categories";
            categories.appendChild(option);
            return;
        }

        const allCategoriesOption = document.createElement("option");
        allCategoriesOption.className = "text-white"
        allCategoriesOption.setAttribute("value", "00000000-0000-0000-0000-000000000000");
        allCategoriesOption.textContent = "All";

        categories.append(allCategoriesOption);


        for (let i = 0; i < response.content.length; i++) {
            const option = document.createElement("option");
            option.className = "text-white"
            option.setAttribute("value", response.content[i].categoryId);
            option.textContent = response.content[i].categoryName;

            categories.append(option);
        }

    } catch (e) {
        throw e;
    }
}

const getProducts = async() => {

    try {

        const categoryId = document.getElementById("categories").options[document.getElementById("categories").selectedIndex].value;
        const orderBy = document.getElementById("orderBy").options[document.getElementById("orderBy").selectedIndex].value;
        const size = document.getElementById("size").options[document.getElementById("size").selectedIndex].value;
        const index = document.getElementById("index").textContent;
        
        const response = await fetch(`https://localhost:9000/v1/Product/${categoryId}/${orderBy}/${size}/${index}`);
 
        const result = await response.json();
        
        if(!result.succeeded && result.statusCode !== 404) throw new Error(result.title); 
        
        if(result.statusCode === 404) return null;
        
        return result.content;
        
    } catch (e) {
        throw e;
    }

}

const enableDisablePrivousNextBtns = () => {

    try {
        document.getElementById("btnPrevious").className = "btn btn-outline-light";
        document.getElementById("btnNext").className = "btn btn-outline-light";
        document.getElementById("index").textContent = currentIndex;

        if (indexesCount === 0) {
            document.getElementById("btnPrevious").className = "btn btn-outline-light disabled";
            document.getElementById("btnNext").className = "btn btn-outline-light disabled";
            document.getElementById("index").textContent = "0";
            currentIndex = 0;
        }

        if (currentIndex === 1) {
            document.getElementById("btnPrevious").className = "btn btn-outline-light disabled";
        }

        if (currentIndex === indexesCount) {
            document.getElementById("btnNext").className = "btn btn-outline-light disabled";
        }
    } catch (e) {
        throw e;
    }
}

const thereAreNotProducts = () => {

    try {
        const container = document.getElementById("productsContainer");

        while(container.firstChild){
            container.removeChild(container.firstChild);
        }

        const rowCard = document.createElement("div");
        rowCard.className = "row justify-content-center mb-5";

        const card = document.createElement("div");
        card.className = "card bg-dark border border-light";
        card.style = "width: 28rem";

        const image = document.createElement("img");
        image.className = "card-img-top";
        image.setAttribute("src", "/Images/SadFace.png");

        const bodyCard = document.createElement("div");
        bodyCard.className = "card-body";

        const hr = document.createElement("hr");
        hr.className = "bg-white";

        const text = document.createElement("p");
        text.className = "text-white text-center fs-4";
        text.textContent = "There are no exercises for the specific category";

        bodyCard.appendChild(text);
        bodyCard.appendChild(hr);

        card.appendChild(bodyCard);
        card.appendChild(image);

        rowCard.appendChild(card);

        container.appendChild(rowCard);
    } catch (e) {
        throw e;
    }

}

const addProducts = (products) =>{
    try {
        const container = document.getElementById("productsContainer");

        while(container.firstChild){
            container.removeChild(container.firstChild);
        }

        for (let i = 0; i < products.length; i++) {

            let product = products[i];

            let col = document.createElement("div");
            col.className = "col";

            let card = document.createElement("div");
            card.className = "card bg-dark text-white";

            let image = document.createElement("img");
            image.src = product.imageUrl;
            image.className = "card-img-top";
            image.style = "height:15rem;";

            let hr = document.createElement("hr");
            hr.className = "bg-white";

            let cardBody = document.createElement("div");
            cardBody.className = "card-body";

            let title = document.createElement("h5");
            title.className = "card-title";
            title.textContent = product.productName;
 
            let text = document.createElement("p");
            text.className = "text-white";
            text.textContent = product.productPrice;
            
            let cardFooter = document.createElement("div");
            cardFooter.className = "card-footer";

            let btn1 = document.createElement("button");
            btn1.className = "btn btn-primary";
            btn1.textContent = "View";
            btn1.addEventListener(("click"), () => location.href = `https://localhost:9000/v1/Product/${website.websiteId}`);

            
            cardFooter.appendChild(btn1);
            
            cardBody.appendChild(title);
            cardBody.appendChild(text);
            
            card.appendChild(image);
            card.appendChild(hr);
            card.appendChild(cardBody);
            card.appendChild(cardFooter);

            col.appendChild(card);

            container.appendChild(col);
        }
    } catch (e) {
        throw e;
    }
}

const getIndexes = async () => {
    try {

        const categoryId = document.getElementById("categories").options[document.getElementById("categories").selectedIndex].value;
        const size = document.getElementById("size").options[document.getElementById("size").selectedIndex].value;
        
        const resp = await fetch(`https://localhost:9000/v1/Product/IndexesCount/${categoryId}/${size}`);

        const result = await resp.json();

        return (result.succeeded) ? result.content.count : 0;

    } catch (e) {
        throw e;
    }
}

const updateProducts = async () => {
    try {
        document.getElementById("index").textContent = currentIndex;

        const products = await getProducts();

        (products === null) ? thereAreNotProducts() : addProducts(products);

        indexesCount = await getIndexes();

        enableDisablePrivousNextBtns();

    } catch (e) {
        throw e;
    }
}


window.addEventListener("load", async () => {
    try {
        await addProductCategoriesAsync();
        await updateProducts();
    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
});

document.getElementById("categories").addEventListener("change", async() => {
    try {
        await updateProducts();
    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
});

document.getElementById("orderBy").addEventListener("change", async() => {
    try {
        await updateProducts();
    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
});

document.getElementById("size").addEventListener("change", async() => {
    try {
        await updateProducts();
    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
});



document.getElementById("btnPrevious").addEventListener("click", async () => {

    try {

        currentIndex -= 1;

        await updateProducts();

        scrollTo(0, 1000);

    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
});

document.getElementById("btnNext").addEventListener("click", async () => {

    try {

        currentIndex += 1;

        await updateProducts();

        scrollTo(0, 1000);

    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }


});