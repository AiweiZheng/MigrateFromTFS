if (!window.formDirtyCheck) {

    window.formDirtyCheck = {
        init: function () {
            window.onbeforeunload = this.onBeforeUnloadWindow;
            this.initialFormData = $("form[dirtyCheck=True]").serialize();
            this.submitting = false;
        },

        onBeforeUnloadWindow: function () {
             return window.formDirtyCheck.hasPendingChanges();
        },

    submitting: false,

    initialFormData: "",

    hasPendingChanges: function () {
        if (!window.formDirtyCheck.submitting &&
            window.formDirtyCheck.initialFormData !== $("form[dirtyCheck=True]").serialize()) {
      
            return "You have pending changes. If you leave current page, those changes will be lost.";
    }
        return undefined;
        }
    };
}

if (!window.antiForgery) {
    window.antiForgery = {
        addVerificaitonTokenToHeader: function (request) {
            var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
            request.setRequestHeader("__RequestVerificationToken", antiForgeryToken);
        }
    }
}


