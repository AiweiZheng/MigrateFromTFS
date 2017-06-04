var confirmDialog = function(yesCallBack,message) {
    return bootbox.dialog({
        message: message,
        title: "Confirm",
        buttons: {
            no: {
                label: "No",
                className: "btn-default",
                callback: function() {
                    bootbox.hideAll();
                }
            },
            danger: {
                label: "Yes",
                className: "btn-danger",
                callback: yesCallBack
            }
        }
    });
};

var alertDialog = function (message) {
   
    bootbox.alert(message);
}