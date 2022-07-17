let openFormButton = document.getElementById("addToday");
let form = document.querySelector('#log-form-wrapper');
let formGreyShade = document.querySelector('.grey-shade-form');

openFormButton.addEventListener('click', () => {
    form.classList.toggle("open");
    formGreyShade.classList.toggle("active");
})
formGreyShade.addEventListener('click', () => {
    formGreyShade.classList.toggle("active");
    form.classList.remove("open");
})

