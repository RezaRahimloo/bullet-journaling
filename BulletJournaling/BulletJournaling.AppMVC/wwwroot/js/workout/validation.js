$(function () {
    let userLoginButton = $("button#addDayWorkout").click(onUserLoginClick);
    let userClearButton = $("button#clearToday").click(clearToday);
    function onUserLoginClick() {
        let url = "/workout/AddToday";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        let antiForgeryToken = $("#add-log-form input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let type = $("#add-log-form input[name = 'Type']").val();
        let durationMintues = $("#add-log-form input[name = 'DurationMintues']").val();

        let userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Type: type,
            DurationMintues: Number(durationMintues)
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

                    userLoginButton = $("button#addDayWorkout").click(onUserLoginClick);
                    //we must wire up the click event again for the case  when the login dialog is rendered to the screen
                    //through the asynchronous process after a failed login attempt
                } else {
                    location.href = "/workout/Index";
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
            }
        });
    }
    function clearToday(){
        let url = `/workout/ClearToday`;
        let antiForgeryToken = $("#add-log-form input[name = '__RequestVerificationToken']").val();
        let token = {
            __RequestVerificationToken: antiForgeryToken
        }
        $.ajax({
            type: "POST",
            url: url,
            data: token,
            success: (data) => {
                location.href = "/workout/Index"; 
            },
            error: (xhr, ajaxOptions, thrownError) => {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
    
                //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
            }
        })
    }
});
