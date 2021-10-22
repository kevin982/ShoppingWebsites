
const addWebsitesAsync = async() => {
    try {

        const websites = document.getElementById("websites");

        while (websites.firstChild) {
            websites.removeChild(websites.firstChild);
        }

        const result = await fetch("https://localhost:9000/v1/Websites/name");

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

const addProductCategoriesAsync = async () => {
    try {

        const productCategories = document.getElementById("websiteCategories");

        while (productCategories.firstChild) {
            productCategories.removeChild(productCategories.firstChild);
        }

        const websites = document.getElementById("websites");

        const websiteId = websites.options[websites.selectedIndex].value;

        const result = await fetch(`https://localhost:9000/v1/ProductCategories/{websiteId}`);

        const response = await result.json();

        if (!response.succeeded && response.statusCode !== 404) throw new Error(response.title);

        if (response.statusCode === 404) {
            let option = document.createElement("option");
            option.setAttribute("value", "00000000-0000-0000-0000-000000000000");
            option.textContent = "Without product categories"

            productCategories.appendChild(option)
        }

        for (let i = 0; i < response.content.length; i++) {
            let option = document.createElement("option");
            option.className = "text-white"
            option.setAttribute("value", response.content[i].categoryId);
            option.textContent = response.content[i].categoryName;

            productCategories.append(option);
        }

    } catch (e) {
        throw e;
    }
}


window.addEventListener("load", async () => {
    try {
        await addWebsitesAsync();
        await addProductCategoriesAsync();
    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'We could add the website categories', text: e.message, customClass: { title: 'text-white', text: 'text-white' } })

    }
});

document.getElementById("websites").addEventListener("change", async () => {
    try {
        await addProductCategoriesAsync();
    } catch (e) {
        Swal.fire({ background: 'black',icon: 'error', title: 'We could add the website categories', text: e.message, customClass: { title: 'text-white', text: 'text-white' } })
    }
});

