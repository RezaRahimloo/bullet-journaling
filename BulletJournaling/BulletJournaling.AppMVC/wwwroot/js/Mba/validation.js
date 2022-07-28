$(function () {
    let userLoginButton = $("button#addDayLesson").click(onUserAddMbaClick);
    let addLessonButton = $("button#add-lesson").click(onAddLessonClick);
    let userClearButton = $("button#clear-today").click(clearToday);
    function onUserAddMbaClick() {
        let url = "/Mba/AddMba";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        let antiForgeryToken = $("#add-log-form input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let type = $("#add-log-form input[name = 'Type']").val();
        let part = $("#add-log-form input[name = 'Part']").val();

        let userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Type: type,
            Part: Number(part)
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

                    userLoginButton = $("button#addDayLesson").click(onUserAddMbaClick);
                    //we must wire up the click event again for the case  when the login dialog is rendered to the screen
                    //through the asynchronous process after a failed login attempt
                } else {
                    location.href = "/Mba/Index";
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
            }
        });
    }
    function onAddLessonClick(){
        let url = "/Mba/AddLesson";

        let lesson = $("textarea[name = 'LessonContext']").val();
        let antiForgeryToken = $("#add-log-form input[name = '__RequestVerificationToken']").val();
        let userInput = {
            __RequestVerificationToken: antiForgeryToken,
            LessonContext: lesson
        }
        console.log(userInput);
        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: (data) => {//the data returned from server in this case its _UserLoginPartial, Partieal view
                console.log(data);
                let hasErrors = false
                let parsed = $.parseHTML(data);
                console.log(parsed);
                if($(parsed).find("span.property-errors.field-validation-error").length > 0){
                    hasErrors = true
                }
                console.log(hasErrors);
                
                if (hasErrors) {
                    $("#addLessonWrapper").html(data);

                    addLessonButton = $("button#add-lesson").click(onAddLessonClick);
                    //we must wire up the click event again for the case  when the login dialog is rendered to the screen
                    //through the asynchronous process after a failed login attempt
                } else {
                    location.href = "/Mba/Index";
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
        let url = `/Mba/ClearToday`;
        let antiForgeryToken = $("#add-log-form input[name = '__RequestVerificationToken']").val();
        let antiForgeryTokenObj = {
            __RequestVerificationToken: antiForgeryToken
        };
        $.ajax({
            type: "POST",
            url: url,
            data: antiForgeryTokenObj,
            success: (data) => {
                location.href = "/Mba/Index"; 
            },
            error: (xhr, ajaxOptions, thrownError) => {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
    
                //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
            }
        })
    }
    
});
function deleteLesson(elm){
    let url = `/Mba/DeleteLesson`;
    let antiForgeryToken = $("#add-log-form input[name = '__RequestVerificationToken']").val();
    let deleteId = {
        __RequestVerificationToken: antiForgeryToken,
        id: elm.dataset.id
    }
    $.ajax({
        type: "POST",
        url: url,
        data: deleteId,
        success: (data) => {
            location.href = "/Mba/Index";
        },
        error: (xhr, ajaxOptions, thrownError) => {
            var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

            //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
            console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
        }
    })
}