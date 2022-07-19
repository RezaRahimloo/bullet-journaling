let openFormButton = document.getElementById("add-class");
let form = document.querySelector('#log-form-wrapper');
let formGreyShade = document.querySelector('.grey-shade-form');

let mainActivitiesShow = document.getElementById("day-activites");
document.querySelectorAll('div.day.log[data-did="true"]').forEach( element => {
    element.addEventListener('focus', e => focusedDay(e))
})
function focusedDay(event){
    let elm = event.target;
    mainActivitiesShow.innerHTML = elm.children[1].innerHTML;
}

openFormButton.addEventListener('click', () => {
    form.classList.toggle("open");
    formGreyShade.classList.toggle("active");
})
formGreyShade.addEventListener('click', () => {
    formGreyShade.classList.toggle("active");
    form.classList.remove("open");
})

