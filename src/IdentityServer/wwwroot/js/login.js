const change = (lastRole, newRole) =>{
    
    const providers = document.getElementById("externalProviders").children;
    
    console.log(providers);
    
    for(let i = 0; i < providers.length; i++)
    {
        let provider = providers[i].children[0];
        
        console.log(provider)
        
        let route = provider.getAttribute("href");
        
        let newRoute = route.replace(`role=${lastRole}`, `role=${newRole}`);
        
        console.log(newRoute);
        
        provider.setAttribute('href', newRoute);
    }
    
}

document.getElementById("isCostumer").addEventListener("click", () =>{

    const value = document.getElementById("isCostumer").checked;

    document.getElementById("isOwner").checked = !value;

    (value)?change("owner","costumer"):change("costumer","owner");
});


document.getElementById("isOwner").addEventListener("click", () =>{

    const value = document.getElementById("isOwner").checked;

    document.getElementById("isCostumer").checked = !value;

    (value)?change("costumer","owner"):change("owner","costumer");
});