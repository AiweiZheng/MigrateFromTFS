
var AccountRoleController = function (accountRoleService, changeAccountRoleService) {

    var template;
    var container;

    var setupRolesPopover = function (roles)
    {
        var renderRoles = function () {
            var compiled = _.template(template.html());
            return compiled({ roles: roles });
        }

        var popOverSettings = {
            html: true,
            title: "Please Select a Role",
            container: container,
            selector: '[rel="popover"]',
            content: renderRoles,
            template:
                '<div class="popover popover-notifications" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
        }  

        //send request when pop up window is hidden
        $(container).popover(popOverSettings)
            .on("show.bs.popover", function (e) {
                // hide all other popovers
                $("[rel=popover]").not(e.target).popover("destroy");
                $(".popover").remove();

            }).on("hidden.bs.popover",
                             changeAccountRoleService.sendChangeRoleRequest);
    }
     
    var done = function (roles) {

        changeAccountRoleService.init();

        setupRolesPopover(roles);
    } 

    var fail = function(error) {
        alertDialog(error.responseJSON);
    }


    var init = function (roleContainer, roleTemplate) {
        container = roleContainer;
        template = $(roleTemplate);
        accountRoleService.getAccountRoles(done, fail);
    } 

    return {
        init : init
    }

}(AccountRoleService, ChangeAccountRoleController)