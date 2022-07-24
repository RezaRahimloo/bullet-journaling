let greyShade = document.getElementById("grey-shade");
let registerShade = document.getElementById("register-grey");
let registerWrapper = document.getElementById("register-wrapper");
let registerButton = document.querySelector("a#register");

registerButton.addEventListener('click', openRegisterForm);
registerShade.addEventListener("click", closeRegisterForm);

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
function openRegisterForm(){ 
    registerShade.classList.add("active");
    registerWrapper.classList.add("open");
}
function closeRegisterForm(){ 
    registerShade.classList.remove("active");
    registerWrapper.classList.remove("open");
}
$(function () {
    let userRegisterButton = $("#register-wrapper #register").click(onUserRegisterClick);
    let userLogoutButton = $("#button#logout").click(onUserRegisterClick);
    function onUserRegisterClick() {
        let url = "/register";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        let antiForgeryToken = $("#register-form input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let email = $("#register-form input[name = 'Email']").val();
        let password = $("#register-form input[name = 'Password']").val();
        let confirmPassword = $("#register-form input[name = 'ConfirmPassword']").val();
        let firstName = $("#register-form input[name = 'FirstName']").val();
        let lastName = $("#register-form input[name = 'LastName']").val();

        let userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Email: email,
            Password: password,
            ConfirmPassword: confirmPassword,
            FirstName: firstName,
            LastName: lastName
        }

        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: (data) => {//the data returned from server in this case its _UserLoginPartial, Partieal view
                let hasErrors = false
                let parsed = $.parseHTML(data);
                if($(parsed).find("span.property-errors.field-validation-error").length > 0){
                    hasErrors = true
                }
                console.log(hasErrors);
                if (hasErrors) {
                    $("#register-wrapper").html(data);

                    userRegisterButton = $("#register-wrapper #register").click(onUserRegisterClick);
                    //we must wire up the click event again for the case  when the login dialog is rendered to the screen
                    //through the asynchronous process after a failed login attempt
                } else {
                    document.location.reload(true);
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
            }
        });
    }
    function onUserLogoutClick() {
        let url = "/account/logout";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        let antiForgeryToken = $("#register-form input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let email = $("#register-form input[name = 'Email']").val();
        let password = $("#register-form input[name = 'Password']").val();
        let confirmPassword = $("#register-form input[name = 'ConfirmPassword']").val();
        let firstName = $("#register-form input[name = 'FirstName']").val();
        let lastName = $("#register-form input[name = 'LastName']").val();

        let userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Email: email,
            Password: password,
            ConfirmPassword: confirmPassword,
            FirstName: firstName,
            LastName: lastName
        }

        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: (data) => {//the data returned from server in this case its _UserLoginPartial, Partieal view
                let hasErrors = false
                let parsed = $.parseHTML(data);
                if($(parsed).find("span.property-errors.field-validation-error").length > 0){
                    hasErrors = true
                }
                console.log(hasErrors);
                if (hasErrors) {
                    $("#register-wrapper").html(data);

                    userRegisterButton = $("#register-wrapper #register").click(onUserRegisterClick);
                    //we must wire up the click event again for the case  when the login dialog is rendered to the screen
                    //through the asynchronous process after a failed login attempt
                } else {
                    document.location.reload(true);
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
            }
        });
    }
});
