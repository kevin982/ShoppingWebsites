let indexesCount = 1;
let currentIndex = 1;

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
            title.textContent = product.name;

            let price = document.createElement("p");
            price.className = "text-white";
            price.textContent = product.price;

            let quantity = document.createElement("p");
            quantity.className = "text-white";
            quantity.textContent = `Quantity: ${product.quantity}`;
            
            let total = document.createElement("p");
            total.className = "text-white";
            total.textContent = `Total: ${product.price*product.quantity}`;
            
            let status = document.createElement("p");
            status.className = "text-white";
            status.textContent = `Status: ${product.status}`;

            cardBody.appendChild(title);
            cardBody.appendChild(price);
            cardBody.appendChild(quantity);
            cardBody.appendChild(total);
            cardBody.appendChild(status);

            card.appendChild(image);
            card.appendChild(hr);
            card.appendChild(cardBody);

            col.appendChild(card);

            container.appendChild(col);
        }
    } catch (e) {
        throw e;
    }
}

const getProducts = async () =>{
    try {
        
        const type = "costumer";
        const category = document.getElementById("categories").options[document.getElementById("categories").selectedIndex].value;
        const size = document.getElementById("size").options[document.getElementById("size").selectedIndex].value;
        const index = document.getElementById("index").textContent;
        
        const response = await fetch( `https://localhost:9000/v1/Purchases/${type}/${category}/${size}/${index}`)
        
        const result = await response.json();
        
        if(!result.succeeded && result.statusCode !== 404) throw new Error(result.title);
        
        return (result.succeeded)?result.content:null;
        
    }catch (e){
        throw e;
    }
}

const getIndexes = async () => {
    try {

        const categoryId = document.getElementById("categories").options[document.getElementById("categories").selectedIndex].value;
        const size = document.getElementById("size").options[document.getElementById("size").selectedIndex].value;

        const resp = await fetch(`https://localhost:9000/v1/Purchase/IndexesCount/${categoryId}/${size}`);

        const result = await resp.json();

        return (result.succeeded) ? result.content.count : 0;

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