const addWebsites = async () => {
    try {

        const websitesContainer = document.getElementById("websitesContainer");
        
        const response = await fetch(`https://localhost:9000/v1/Websites/Saved`)

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
                btn1.className = "btn btn-outline-primary btn-lg";
                btn1.textContent = "Visit";
                btn1.addEventListener(("click"), () => location.href = `https://localhost:9000/v1/Website/${website.websiteId}`);
 
                
                let btn2 = document.createElement("button");
                btn2.className = "btn btn-outline-danger btn-lg";
                btn2.textContent = "Remove";
                btn2.addEventListener(("click"), async () =>{
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
                            'title':'text-white',
                            'text':'text-white'
                        }
                    }).then(async (result) => {
                        if (result.isConfirmed) {
                            const response = await fetch(`https://localhost:9000/v1/Website/Saved/${website.websiteId}`, {method:'DELETE'});

                            location.reload();
                        }
                    })
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

window.addEventListener("load", async ()=>{
    try {
        await addWebsites();    
    }catch(e){
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
})