

document.getElementById("isCostumer").addEventListener("click", () =>{
   
    const value = document.getElementById("isCostumer").checked;

    document.getElementById("isOwner").checked = !value;
 
});


document.getElementById("isOwner").addEventListener("click", () =>{

    const value = document.getElementById("isOwner").checked;

    document.getElementById("isCostumer").checked = !value;
     
});