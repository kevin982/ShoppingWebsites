const addWebsitesAsync = async () => {
    try {

        const websites = document.getElementById("websites");

        while (websites.firstChild) {
            websites.removeChild(websites.firstChild);
        }

        const result = await fetch("https://localhost:9000/v1/Websites");

        const response = await result.json();

        if (!response.succeeded && response.statusCode !== 404) throw new Error(response.title);

        if (response.statusCode === 404) {
            let option = document.createElement("option");
            option.setAttribute("value", "00000000-0000-0000-0000-000000000000");
            option.textContent = "Without product categories";
            websites.appendChild(option);
            return;
        }

        const categories = document.getElementById("websiteCategories");

        for (let i = 0; i < response.content.length; i++) {
            const option = document.createElement("option");
            option.className = "text-white"
            option.setAttribute("value", response.content[i].websiteId);
            option.textContent = response.content[i].websiteName;

            websites.append(option);
        }

    } catch (e) {
        throw e;
    }
}

const getProductCategories = async () => {

    try {

        const websites = document.getElementById("websites");

        const websiteId = websites.options[websites.selectedIndex].value;

        const response = await fetch(`https://localhost:9000/v1/ProductCategories/${websiteId}`);

        return await response.json();


    } catch (e) {
        throw e;
    }

}

const getProductsByCategoryId = async () => {

    try {

        const categories = document.getElementById("ProductCategories");

        let categoryId = categories.options[categories.selectedIndex].value;

        const response = await fetch(`https://localhost:9000/v1/Product/Category/${categoryId}`);

        return await response.json();

    } catch (e) {
        throw e;
    }

}

const addCategories = (categoriesResponse) => {
    try {

        const categories = document.getElementById("ProductCategories");

        while (categories.firstChild) {
            categories.removeChild(categories.lastChild);
        }

        if (!categoriesResponse.succeeded && categoriesResponse.statusCode !== 404) throw new Error(`The categories could not be loaded due to ${categoriesResponse.title}`)

        if (categoriesResponse.statusCode === 404) {
            const option = document.createElement("option");
            option.setAttribute("value", "00000000-0000-0000-0000-000000000000");
            option.textContent = "No categories were found!";

            categories.appendChild(option);
            return;
        }

        for (let i = 0; i < categoriesResponse.content.length; i++) {
            let category = categoriesResponse.content[i];

            let option = document.createElement("option");
            option.setAttribute("value", category.categoryId);
            option.textContent = category.categoryName;

            categories.appendChild(option);
        }

    } catch (e) {
        throw e;
    }
}

const addProducts = (productsResponse) => {
    try {

        const products = document.getElementById("Products");

        while (products.firstChild) {
            products.removeChild(products.lastChild);
        }

        if (!productsResponse.succeeded && productsResponse.statusCode !== 404) throw new Error(`The products could not be loaded due to ${categoriesResponse.title}`)

        if (productsResponse.statusCode === 404) {
            const option = document.createElement("option");
            option.setAttribute("value", "00000000-0000-0000-0000-000000000000");
            option.textContent = "No products were found!";

            products.appendChild(option);
            return;
        }

        for (let i = 0; i < productsResponse.content.length; i++) {
            let product = productsResponse.content[i];

            let option = document.createElement("option");
            option.setAttribute("value", product.productId);
            option.textContent = product.productName;

            products.appendChild(option);
        }

    } catch (e) {
        throw e;
    }
}


window.addEventListener("load", async () => {

    try {

        await addWebsitesAsync();

        const productCategories = await getProductCategories();

        addCategories(productCategories);

        const products = await getProductsByCategoryId();

        addProducts(products);

    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }

});

document.getElementById("websites", async () => {

    try {
        const productCategories = await getProductCategories();

        addCategories(productCategories);

        const products = await getProductsByCategoryId();

        addProducts(products);
    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
});

document.getElementById("ProductCategories").addEventListener("change", async () => {

    try {
        const products = await getProductsByCategoryId();

        addProducts(products);
    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }

});




document.getElementById("removeBtn").addEventListener("click", async () => {
    try {
        const selected = document.querySelectorAll('#Products option:checked');
        const values = Array.from(selected).map(el => el.value);

        if (values.length === 0) throw new Error("You must select at least one product!");

        const config = {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "DELETE",
            body: JSON.stringify({ productsId : values })
        };

        const response = await fetch("https://localhost:9000/v1/Product/Remove", config);

        const result = await response.json();

        if (!result.succeeded) throw new Error(result.title);

        Swal.fire({ background: 'black', icon: 'success', title: 'Products Removed.', text: "The products have been removed!", customClass: { 'title': 'text-white', 'text': 'text-white'}});

    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
})