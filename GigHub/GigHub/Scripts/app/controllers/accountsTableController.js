var AccountTableController = function () {

    var init = function(container) {
        $(container).DataTable({
            ajax: {
                url: "/api/accounts",
                dataSrc: ""
            },
            columns: [
                {
                    data: "email"
                },

                {
                    data: "name"
                },
                
                {
                    data: "role",
                    render: function (data, type, user) {
                        return "<a class='clickable' rel='popover' data-user-id = " +
                            user.id +
                            " class='role'>" +
                            data +
                            "</a>";
                    }
                },

                {
                    data: "activated",
                    render: function (data, type, user) {
                        return "<label id = " +
                            user.id +
                            "_status>" +
                            data +
                            "</label>";
                    }
                },

                {
                    data: "id",
                    render: function (data, type, user) {
                        return "<a  data-toggle='modal' data-target='#descriptionModel' data-user-id= " +
                            user.id + "  data-user-email="+user.email+
                            " class='js-toggle-user-description'><i class='glyphicon glyphicon-edit'></i></a> Description";
                    }
                },

                {
                    data: "activated",
                    render: function (data, type, user) {
                        return "<a data-user-id = " +
                            user.id +
                            " class='js-toggle-changeStatus' href='#'>" +
                            user.accountStatus +
                            "</a>";
                    }
                }
            ]
        });
    }

    return {
        init:init
    }
}();