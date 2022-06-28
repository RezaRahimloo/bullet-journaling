let greyShade = document.getElementById("grey-shade");
function openNav() {
    document.querySelector('.links-wrapper').style.width = "250px";
    document.querySelector('body').style.marginLeft = "250px";
    greyShade.style.display = "block";
    
}
function closeNav() {
    document.querySelector('.links-wrapper').style.width = "0";
    document.querySelector('body').style.marginLeft= "0";
    greyShade.style.display = "none";
    
}
document.getElementById("grey-shade").addEventListener('click', e=> {
    closeNav();
    greyShade.style.display = "none";
    
});
console.log("murder")