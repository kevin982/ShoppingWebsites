let indexesCount = 1;
let currentIndex = 1;

const addWebsiteCategories = async () => {
    try {

        const result = await fetch("https://localhost:9000/v1/WebsiteCategories");

        const response = await result.json();

        if (response.statusCode !== 200) throw new Error(response.title);

        const categories = document.getElementById("websiteCategories");

        while (categories.firstChild) {
            categories.removeChild(categories.firstChild);
        }

        const allCategoriesOption = document.createElement("option");
        allCategoriesOption.setAttribute("value", "00000000-0000-0000-0000-000000000000");
        allCategoriesOption.textContent = "All";

        categories.appendChild(allCategoriesOption)

        for (let i = 0; i < response.content.length; i++) {
            let option = document.createElement("option");
            option.setAttribute("value", response.content[i].categoryId);
            option.textContent = response.content[i].categoryName;

            categories.appendChild(option);
        }

    } catch (e) {
        throw e;
    }
}

const addWebsites = async () => {
    try {
        
        const websitesContainer = document.getElementById("websitesContainer");
 
        const category = document.getElementById("websiteCategories").options[document.getElementById("websiteCategories").selectedIndex].value;

        const websiteName = (document.getElementById("search").value !== "")?document.getElementById("search").value:"nothing";

        const orderBy = document.getElementById("orderBy").options[document.getElementById("orderBy").selectedIndex].value;
        
        const size = document.getElementById("size").options[document.getElementById("size").selectedIndex].value;

        const index = document.getElementById("index").textContent;
        
        const response = await fetch(`https://localhost:9000/v1/Websites/${category}/${websiteName}/${index}/${size}/${orderBy}`)
        
        const result = await response.json();
        
        while(websitesContainer.firstChild){
            websitesContainer.removeChild(websitesContainer.firstChild);
        }
        
        if(result.succeeded)
        {
            for (let i = 0; i < result.content.length; i++) {

                let website = result.content[i];

                let col = document.createElement("div");
                col.className = "col";
                
                let card = document.createElement("div");
                card.className = "card bg-dark text-white";
                
                let image = document.createElement("img");
                image.src = website.imageUrl;
                image.className = "card-img-top";
                image.style = "height:15rem;";
                
                let hr = document.createElement("hr");
                hr.className = "bg-white";
                
                let cardBody = document.createElement("div");
                cardBody.className = "card-body";
                
                let title = document.createElement("h5");
                title.className = "card-title";
                title.textContent = website.websiteName;
                
                let text = document.createElement("p");
                text.className = "text-white";
                text.textContent = website.description;
                
                let cardFooter = document.createElement("div");
                cardFooter.className = "card-footer";
                
                let btn1 = document.createElement("button");
                btn1.className = "btn btn-primary";
                btn1.textContent = "View";
                btn1.addEventListener(("click"), () => location.href = `https://localhost:9000/v1/Website/${website.websiteId}`);

                let btn2 = document.createElement("button");
                btn2.className = "btn btn-outline-primary";
                btn2.textContent = "Save";
                btn2.addEventListener(("click"), async() => 
                {
                    const config = {
                        headers:{
                            'Content-Type': 'application/json'
                        },
                        method:"POST",
                        body:JSON.stringify(website)
                    };
                    
                    const response = await fetch("https://localhost:9000/v1/Website/Save", config);
 
                    
                    const result = await response.json();
 
                    btn2.disabled = true;
                    
                });
                
                cardFooter.appendChild(btn1);
                cardFooter.appendChild(btn2);
                
                cardBody.appendChild(title);
                cardBody.appendChild(text);
                
                card.appendChild(image);
                card.appendChild(hr);
                card.appendChild(cardBody);
                card.appendChild(cardFooter);
                
                col.appendChild(card);
                
                websitesContainer.appendChild(col);
 
            }
            
            
            return;
        }

        if(result.statusCode === 404){
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
            text.textContent = "Could not find any website";

            bodyCard.appendChild(text);
            bodyCard.appendChild(hr);

            card.appendChild(bodyCard);
            card.appendChild(image);

            rowCard.appendChild(card);

            websitesContainer.appendChild(rowCard);
            
            return;
        }
        
        throw new Error(result.title);
        
    } catch (e) {
        throw e;
    }
}

const getIndexes = async () => {
    try {

        const categoryId = document.getElementById("websiteCategories").options[document.getElementById("websiteCategories").selectedIndex].value;
        
        const size = document.getElementById("size").options[document.getElementById("size").selectedIndex].value;

        const resp = await fetch(`https://localhost:9000/v1/Website/IndexesCount/${categoryId}/${size}`);

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

const updateWebsites = async () => {
    try {
        document.getElementById("index").textContent = currentIndex;
        
        await addWebsites();

        indexesCount = await getIndexes();

        enableDisablePrivousNextBtns();

    } catch (e) {
        throw e;
    }
}

window.addEventListener("load", async () => {
    try {
        await addWebsiteCategories();
        await updateWebsites();
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
});

document.getElementById("btnPrevious").addEventListener("click", async () => {

    try {

        currentIndex -= 1;

        await updateWebsites();

        scrollTo(0, 1000);

    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }
});

document.getElementById("btnNext").addEventListener("click", async () => {

    try {

        currentIndex += 1;

        await updateWebsites();

        scrollTo(0, 1000);

    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'Error', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white' } })
    }


});

