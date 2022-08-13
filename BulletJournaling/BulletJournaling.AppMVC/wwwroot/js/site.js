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

$(function () {
    let userRegisterButton = $("#register-wrapper #register").click(onUserRegisterClick);
    let userLoginButton = $("#login-wrapper button#login").click(onUserLoginClick);


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
    function onUserLoginClick() {
        let url = "/login";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        let antiForgeryToken = $("#login-form input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let userName = $("#login-form input[name = 'UserName']").val();
        let password = $("#login-form input[name = 'Password']").val();
        let rememberMe = $("#login-form input[name = 'RememberMe']").val();

        let userInput = {
            __RequestVerificationToken: antiForgeryToken,
            UserName: userName,
            Password: password,
            RememberMe: rememberMe
        }

        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: (data) => {//the data returned from server in this case its _UserLoginPartial, Partieal view
                
                let parsed = $.parseHTML(data);
                let hasErrors = $(parsed).find("input[name = 'LoginInvalid']").val() == "true";
                console.log(hasErrors);
                if (hasErrors) {
                    $("#login-wrapper").html(data);

                    userLoginButton = $("#login-wrapper #login").click(onUserLoginClick);
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
