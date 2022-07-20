$(function () {
    let userLoginButton = $("button#addDayLesson").click(onUserAddMbaClick);
    let addLessonButton = $("button#add-lesson").click(onAddLessonClick);
    let userClearButton = $("button#clear-today").click(clearToday);
    function onUserAddMbaClick() {
        let url = "/Mba/AddMba";
        //input[name = '__RequestVerificationToken'] this is a hidden input field that's automaticaly added by mvc
        //let antiForgeryToken = $("#UserLoginModal input[name = '__RequestVerificationToken']").val();
        //alert(antiForgeryToken);

        let type = $("#add-log-form input[name = 'Type']").val();
        let part = $("#add-log-form input[name = 'Part']").val();

        let userInput = {
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

        let lesson = $("textarea[name = 'Lesson']").val();

        let userInput = {
            Lesson: lesson,
            Id: -1
        }
        console.log(userInput.Lesson);
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
        $.ajax({
            type: "DELETE",
            url: url,
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
    let deleteId = {
        id: elm.dataset.id
    }
    $.ajax({
        type: "DELETE",
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