var App = function () {

    var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();

    var addVerificaitonTokenToHeader = function (request) {
        request.setRequestHeader("__RequestVerificationToken", antiForgeryToken);
    }

    var hideNotificationWhenClickOnSpace = function(popover, notification) {
        $("body").on("click",
            function(e) {

                if (!$(e.target).is($(notification))) {
                    $(popover).popover("hide");
                };
            });
    };

    return {
        addVerificaitonTokenToHeader: addVerificaitonTokenToHeader,
        hideNotificationWhenClickOnSpace: hideNotificationWhenClickOnSpace
    }
}()

