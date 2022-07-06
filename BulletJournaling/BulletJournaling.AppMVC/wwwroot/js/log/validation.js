$(function () {
    let userLoginButton = $("button#addDayLog").click(onUserLoginClick);

    function onUserLoginClick() {
        let url = "Log/AddToday";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        //let antiForgeryToken = $("#UserLoginModal input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let title = $("#add-log-form input[name = 'Title']").val();
        let description = $("#add-log-form input[name = 'Description']").val();
        let durationMinutes = $("#add-log-form input[name = 'DurationMinutes']").val();

        let userInput = {
            Title: title,
            Description: description,
            DurationMinutes: durationMinutes
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
                    location.href = "Log/Index";
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
