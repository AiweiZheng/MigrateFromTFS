var AccountDescriptionConctroller = function (accountService) {

    var modal;
    var descriptionTextArea;

    var userId = null;
    var user = null; 

    var receivedDescription = function (data) {

        user = data;

        descriptionTextArea.val(user.description);
    }

    var updatedDone = function() {
        modal.modal("hide");
    }

    var fail = function(error) {
        alertDialog(error.responseJSON);
    }

    var renderPopupModel = function (e) {

        userId = $(e.relatedTarget).data("user-id");
        var userEmail = $(e.relatedTarget).data("user-email");
   
        $("#descriptionFor").text("Description for : "+userEmail);

        accountService.getDescription(userId, receivedDescription, fail);
    }

    var addListenerToSubmitBtn = function (event) {
        event.preventDefault();
        user.description = descriptionTextArea.val();
       
        accountService.updateDescription(userId, user, updatedDone, fail);
    };

    var init = function (container) {

        $("#submit").click(addListenerToSubmitBtn);

        modal = $(container);
        descriptionTextArea = $("#user-description");
      
        modal.on("show.bs.modal", renderPopupModel);
    }

    return {
        init: init
    };
}(AccountService)