let mainActivitiesShow = document.getElementById("day-activites");
document.querySelectorAll('div.day.log[data-did="true"]').forEach( element => {
    element.addEventListener('focus', e => focusedDay(e))
})

function focusedDay(event){
    let elm = event.target;
    mainActivitiesShow.innerHTML = elm.children[1].innerHTML;
}