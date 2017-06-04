var AccountRoleService = function () {

    var getAccountRoles = function (done, fail) {
        $.ajax({
                url: "/api/roles/",
                method: "GET"
            }).done(done)
            .fail(fail);
    }

    return {
        getAccountRoles: getAccountRoles
    }
}();