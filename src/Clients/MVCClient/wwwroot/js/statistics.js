
const getWebsites = async() =>{
    try {
        const response = await fetch(`https://localhost:9000/v1/Websites/name`);

        return await response.json();

    }catch(e){
        throw e;
    }
}
 

const addWebsitesToSelect = (response) =>{
    try {

        if(!response.succeeded && response.statusCode !==404) throw new Error("Error while getting the exercises!");

        let websites = document.getElementById("websites");

        while (websites.firstChild) {
            websites.removeChild(websites.firstChild);
        }

        if (response.statusCode === 404) {
            return;
        }

        for(let i = 0; i < response.content.length; i++){
            let website = response.content[i];

            let option = document.createElement("option");
            option.className = "text-white";
            option.setAttribute("value", website.websiteId);
            option.textContent = website.websiteName;

            websites.appendChild(option);
        }

    }catch(e){
        throw e;
    }
}

const addFiltersForm = () =>{

    document.getElementById("instructions").className = "text-white";
    document.getElementById("months").className = "bg-dark text-white";
    document.getElementById("years").className = "bg-dark text-white";
    document.getElementById("limit").className = "bg-white";

}

const removeFiltersForm = () =>{

    document.getElementById("instructions").className ="text-white d-none";
    document.getElementById("months").className ="bg-dark text-white d-none";
    document.getElementById("years").className ="bg-dark text-white d-none";
    document.getElementById("limit").className = "bg-white d-none";

}

const getStatistics = async () =>{
    try {

        const websiteIndex = document.getElementById("websites").selectedIndex;

        if(websiteIndex === -1) return null;

        const websiteId = document.getElementById("websites").options[websiteIndex].value;

        const type = document.getElementById("type").options[document.getElementById("type").selectedIndex].value;
        
        const monthElement = document.getElementById("months");
        const yearElement = document.getElementById("years");

        let year = 0;
        let month = 0;

        const allStatistics = document.getElementById("flexCheckChecked").checked;

        if (!allStatistics) {
            month = monthElement.options[monthElement.selectedIndex].value;
            year = yearElement.options[yearElement.selectedIndex].value;
        }

        const response = await fetch(`https://localhost:9000/v1/Statistics/${websiteId}/${type}/${month}/${year}`);
        return await response.json();

    }catch(e){
        throw e;
    }
}

const getColor = (number) => {
    switch (number) {
        case 1:
            return { background: 'rgba(255, 99, 132, 0.2)', border: 'rgba(255, 99, 132, 1)'};
        case 2:
            return { background: 'rgba(54, 162, 235, 0.2)', border: 'rgba(54, 162, 235, 1)'};
        case 3:
            return { background: 'rgba(255, 206, 86, 0.2)', border: 'rgba(255, 206, 86, 1)'};
        case 4:
            return { background: 'rgba(75, 192, 192, 0.2)', border: 'rgba(75, 192, 192, 1)'};
        case 5:
            return { background: 'rgba(153, 102, 255, 0.2)', border: 'rgba(153, 102, 255, 1)'};
        default:
            return { background: 'rgba(255, 159, 64, 0.2)', border: 'rgba(255, 159, 64, 1)'};
    }
}

const prepareStatisticsForChart = (statistics) => {
    const dates = [];
    const values = [];
    const backgroundColors = [];
    const borderColors = [];

    const type = document.getElementById("type").options[document.getElementById("type").selectedIndex].value;

    for (var i = 0; i < statistics.length; i++) {

        dates.push(statistics[i].date);
        values.push(statistics[i].value);
        colors = getColor(Math.floor(Math.random() * (7 - 1)) + 1);
        backgroundColors.push(colors.background);
        borderColors.push(colors.border);
    }

    return { dates: dates, values: values, backgroundColors: backgroundColors, borderColors: borderColors };
}

const addStatisticsToView = (response) =>{
    try {

        const statisticsContainer = document.getElementById("statistics");

        while (statisticsContainer.firstChild) {
            statisticsContainer.removeChild(statisticsContainer.firstChild);
        }


        if ((response === null) || (!response.succeeded && response.statusCode === 404)) {

            const chart = document.getElementById("myChart");

            if(chart !== null)chart.remove();

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
            text.textContent = "There are no statistics for the specific exercise";

            bodyCard.appendChild(text);
            bodyCard.appendChild(hr);

            card.appendChild(bodyCard);
            card.appendChild(image);

            statisticsContainer.appendChild(card);

            return;
        }

        if (!response.succeeded && response.statusCode !== 404) throw new Error("Error while getting the statistics for the exercise");

        const chart = document.createElement("canvas");
        chart.setAttribute("id", "myChart");
        chart.setAttribute("width", "500");
        chart.setAttribute("height", "400");


        statisticsContainer.appendChild(chart);

        const { dates, values, backgroundColors, borderColors } = prepareStatisticsForChart(response.content);

        var ctx = document.getElementById('myChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: dates,
                datasets: [{
                    label: document.getElementById("type").options[document.getElementById("type").selectedIndex].value,
                    data: values,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });


    }catch(e){
        throw e;
    }
}

const updateStatistics = async () =>{
    try {
        document.getElementById("spinner").className = "spinner-border centered text-white";
        const statistics = await getStatistics();
        addStatisticsToView(statistics);
        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
        
    }catch (e){
        Swal.fire({ background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`, customClass: { title: 'text-white', text: 'text-white' } });
    }
}

window.addEventListener("load", async () =>{
    try {
 
        const websitesResult = await getWebsites();
        addWebsitesToSelect(websitesResult);

        await updateStatistics();

    }catch (e){
        Swal.fire({ background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`, customClass: { title: 'text-white', text: 'text-white' } });

    }
})

document.getElementById("flexCheckChecked").addEventListener("change", async() =>{

    try {

        const value = document.getElementById("flexCheckChecked").checked;

        (value)?removeFiltersForm():addFiltersForm();

        await updateStatistics();

    }catch(e){
        Swal.fire({background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`, customClass: {title: 'text-white',text: 'text-white'}});
    }

});
 