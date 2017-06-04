var NotificationController = function (notificationService) {

    var newNotificationCount;
    var notifications;

    var displayNewNotificaitonCount = function (count) {

        $(".js-new-notifications-count")
            .text(count)
            .removeClass("hide")
            .addClass("animated bounceInDown");
    }

   
    var markAsReadDone = function() {
        $(".js-new-notifications-count")
            .text("")
            .addClass("hide");
    };

    var markAsReadFail = function (error) {
        alertDialog(error.responseJSON);
    }

    var sendMarkAsReadRequest = function () {
        notificationService.markAsRead(markAsReadDone, markAsReadFail);
    }

    var renderNotifications = function() {
        var compiled = _.template($("#notifications-template").html());
        return compiled({ notifications: notifications });
    }

    var getNotificaitonsDone = function (notificationData) {

        newNotificationCount = notificationData.newNotificationCount;
        notifications = notificationData.notifications;

        if (newNotificationCount !== 0)
            displayNewNotificaitonCount(newNotificationCount);

        $(".notifications").popover({
            html: true,
            title: "Notifications",
            content: renderNotifications,
            placement: "bottom",
            template:
                '<div class="popover popover-notifications" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'

        }).on("show.bs.popover", sendMarkAsReadRequest);
    }

    var init = function () {
        NotificationService.getNotifications(getNotificaitonsDone);
    }

    return {
        init: init
    }

}(NotificationService);


