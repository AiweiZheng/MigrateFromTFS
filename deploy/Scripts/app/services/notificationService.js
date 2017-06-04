var NotificationService = function () {

    var getNotifications = function (done) {
        $.ajax({
                url: "/api/Notifications",
                method: "GET",
                beforeSend: App.addVerificaitonTokenToHeader
            })
            .done(done);
    }

    var markAsRead = function (done, fail) {
        $.ajax({
                url: "/api/notifications/markAsRead",
                method: "POST",
                beforeSend: App.addVerificaitonTokenToHeader
            })
            .done(done)
            .fail(fail);
    }

  

    return {
        getNotifications: getNotifications,
         markAsRead: markAsRead
    }

}();