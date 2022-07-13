const smokingLevels = ["low", "medium-l", "medium-h", "h", "cronic"]
let smokeDays = document.querySelectorAll('.day.smoking');

smokeDays.forEach(day => {
    let cigarretsToday = Number(day.dataset.number);
    console.log(day, cigarretsToday);
    if(cigarretsToday > 10){
        day.classList.add(smokingLevels[4]);
    } else if(cigarretsToday > 8){
        day.classList.add(smokingLevels[3]);
    } else if(cigarretsToday > 5){
        day.classList.add(smokingLevels[2]);
    } else if(cigarretsToday > 2){
        day.classList.add(smokingLevels[1]);
    } else if(cigarretsToday > 0){
        day.classList.add(smokingLevels[0]);
    }
})