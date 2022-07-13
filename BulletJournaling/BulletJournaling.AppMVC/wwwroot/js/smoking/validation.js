$(function () {
    let userLoginButton = $("div#saveCigars").click(onUserLoginClick);

    function onUserLoginClick() {
        let url = "/smoking/AddToday";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        //let antiForgeryToken = $("#UserLoginModal input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let didSmoke = $("#add-smokes-form input[name = 'DidSmoke']").val();
        let number = $("#add-smokes-form input[name = 'Number']").val();
        let date = $("#add-smokes-form input[name = 'Date']").val();

        let userInput = {
            DidSmoke: didSmoke,
            Number: number,
            Date: date
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
                    $("#log-form-wrapper").html(data);

                    userLoginButton = $("button#addDayLog").click(onUserLoginClick);
                    //we must wire up the click event again for the case  when the login dialog is rendered to the screen
                    //through the asynchronous process after a failed login attempt
                } else {
                    location.href = "/smoking/Index";
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
