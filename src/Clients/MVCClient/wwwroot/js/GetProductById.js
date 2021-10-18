 
document.getElementById("shoppingCart").addEventListener("click", async () => {
    Swal.fire({
        background: 'black',
        title: 'Enter the quantity of products',
        input: 'number',
        showCancelButton: true,
        confirmButtonText: 'Add',
        showLoaderOnConfirm: true,
        customClass: {
            title: 'text-white',
            input: 'text-white'
        },
        preConfirm: async (quantity) => {

            if (quantity === null || quantity == 0 || quantity === "") {
                Swal.showValidationMessage(`You must enter the quantity, and it has to be greater than zero.`);
            } else {
                const response = await fetch(`https://localhost:9000/v1/Cart/${document.getElementById("product").getAttribute("productId")}/${quantity}`)

                return await response.json();
            }
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: `${result.value.login}'s avatar`,
                imageUrl: result.value.avatar_url
            })
        }
    })
});


const addReviewsAsync = async () => {
    try {

        const response = await fetch(`https://localhost:9000/v1/Review/${document.getElementById("product").getAttribute("productId")}`)

        const result = await response.json();

        const allReviewsContainer = document.getElementById("allReviewsContainer");

        while (allReviewsContainer.firstChild) {
            allReviewsContainer.removeChild(allReviewsContainer.firstChild);
        }

        if (!result.succeeded){
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
            text.textContent = "There are no reviews for this product";

            bodyCard.appendChild(text);
            bodyCard.appendChild(hr);

            card.appendChild(bodyCard);
            card.appendChild(image);

            rowCard.appendChild(card);

            allReviewsContainer.appendChild(rowCard);

            return;
        }

        const reviews = result.content;

        for (let i = 0; i < reviews.length; i++) {

            let review = reviews[i];

            let card = document.createElement("div");
            card.className = "card bg-dark text-white";

            let cardBody = document.createElement("div");
            cardBody.className = "card-body";

            let row = document.createElement("div");
            row.className = "row";

            let col = document.createElement("div");
            col.className = "col-md";

            let p = document.createElement("p");

            let name = document.createElement("strong");
            name.textContent = review.userName;

            p.appendChild(name);

            for (let j = 0; j < 5; j++) {
                let span = document.createElement("span");
                span.className = "float-right";
                let icon = document.createElement("i");
                icon.className = (j < review.stars) ? "fa fa-star rating-color" : "fa fa-star";
                span.appendChild(icon);
                p.appendChild(span);
            }

            let date = document.createElement("p");
            date.className = "text-secondary text-center";
            date.textContent = review.date;

            let commentContainer = document.createElement("div");
            commentContainer.className = "clearfix";

            let comment = document.createElement("p");
            comment.textContent = review.comment;

            commentContainer.appendChild(comment);

            col.appendChild(p);
            col.appendChild(date);
            col.appendChild(commentContainer);

            row.appendChild(col);

            cardBody.appendChild(row);

            card.appendChild(cardBody);

            allReviewsContainer.appendChild(card);

            if (i == reviews.length - 1) continue;

            let hr = document.createElement("hr");
            hr.className = "bg-white";

            allReviewsContainer.appendChild(hr);
            
        }

    } catch (e) {
        throw e;
    }
}


window.addEventListener("load", async () => {
    try {
        await addReviewsAsync();
    } catch (e) {
        console.log(e);
    }
});

document.addEventListener("click", (event) => {
    try {
        const id = event.target.getAttribute("id").split('-');

        if (id[0] !== "star") return;

        const starNumber = parseInt(id[1]);

        document.getElementById("stars").value = starNumber+1;

        console.log('New stars value: ',document.getElementById("stars").value);

        for (let i = 4; i >= 0; i--) {
            let star = document.getElementById(`star-${i}`);
            star.className = (starNumber >= i) ? "fa fa-star rating-color" : "fa fa-star";
        }
            
    } catch (e) {
        console.log(e);
    }
});

document.getElementById("btnCreateReview").addEventListener("click", async () => {
    try {

        const stars = document.getElementById("stars").value;
        const comment = document.getElementById("comment").value.trim();

        if (stars == 0) throw new Error("You must select the stars you consider the product deserve.");
        if (comment === "") throw new Error("You must add a comment to this review");


    } catch (e) {
        Swal.fire({ background: 'black', icon: 'error', title: 'We could create the review', text: e.message, customClass: { 'title': 'text-white', 'text': 'text-white'}});
    }
});