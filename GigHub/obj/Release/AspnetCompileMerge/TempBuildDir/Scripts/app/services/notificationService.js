var NotificationService = function () {

    var getNotifications = function (done) {
        $.ajax({
                url: "/api/Notifications",
                method: "GET",
                beforeSend: window.antiForgery.addVerificaitonTokenToHeader
            })
            .done(done);
    }

    var markAsRead = function (done, fail) {
        $.ajax({
                url: "/api/notifications/markAsRead",
                method: "POST",
                beforeSend: window.antiForgery.addVerificaitonTokenToHeader
            })
            .done(done)
            .fail(fail);
    }

  

    return {
        getNotifications: getNotifications,
         markAsRead: markAsRead
    }

}();