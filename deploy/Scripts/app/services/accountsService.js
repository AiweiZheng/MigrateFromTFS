var AccountService = function () {
    var getDescription = function(id, done, fail) {
        $.ajax({
                url: "/api/accounts/" + id,
                method: "GET"
            }).done(done)
            .fail(fail);
    };

    var updateDescription = function(id, data, done, fail) {
        $.ajax({
                url: "/api/accounts/" + id,
                method: "PUT",
                data: data,
                beforeSend: App.addVerificaitonTokenToHeader
            }).done(done)
            .fail(fail);
    };

    var changeAccountStatus = function(userId, data, done, fail) {
        $.ajax({
                url: "/api/accounts/" + userId + "/status",
                method: "PUT",
                data: data,
                beforeSend: App.addVerificaitonTokenToHeader

            }).done(done)
            .fail(fail);
    };

    var changeAccountRole = function(userId, data, done, fail) {
        $.ajax({
                url: "/api/accounts/" + userId + "/role",
                method: "PUT",
                data: data,
                beforeSend: App.addVerificaitonTokenToHeader
            }).done(done)
            .fail(fail);
    };



    return {
        getDescription: getDescription,
        updateDescription: updateDescription,
        changeAccountStatus: changeAccountStatus,
        changeAccountRole: changeAccountRole
    }

}()