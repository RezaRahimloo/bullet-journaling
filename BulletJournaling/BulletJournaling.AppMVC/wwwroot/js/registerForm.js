
window.onload = () => {
    let registerButton = document.querySelector("a#register");
    let registerShade = document.getElementById("register-grey");
    let registerWrapper = document.getElementById("register-wrapper");
    let loginWrapper = document.getElementById("login-wrapper");
    let loginButton = document.querySelector("a#login")

    registerButton.addEventListener('click', openRegisterForm);
    registerShade.addEventListener("click", closeRegisterForm);
    loginButton.addEventListener("click", openLoginForm);
    registerShade.addEventListener('click', closeLoginForm);

    function openRegisterForm(){ 
        registerShade.classList.add("active");
        registerWrapper.classList.add("open");
    }
    function closeRegisterForm(){ 
        registerShade.classList.remove("active");
        registerWrapper.classList.remove("open");
    }

    function openLoginForm(){
        loginWrapper.classList.add("open");
        registerShade.classList.add("active");
    }
    function closeLoginForm(){ 
        registerShade.classList.remove("active");
        loginWrapper.classList.remove("open");
    }
}