
window.onload = () => {
    let registerButton = document.querySelector("a#register");
    let registerShade = document.getElementById("register-grey");
    let registerWrapper = document.getElementById("register-wrapper");
    registerButton.addEventListener('click', openRegisterForm);
    registerShade.addEventListener("click", closeRegisterForm);
    
    function openRegisterForm(){ 
        registerShade.classList.add("active");
        registerWrapper.classList.add("open");
    }
    function closeRegisterForm(){ 
        registerShade.classList.remove("active");
        registerWrapper.classList.remove("open");
    }
}