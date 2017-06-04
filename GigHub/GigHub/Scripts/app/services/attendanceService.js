var AttendanceService = function () {

    var createAttendance = function (gigId, done, fail) {
        $.ajax({
                url: "/api/attendances",
                method: "POST",
                data: { GigId: gigId },
                beforeSend: window.antiForgery.addVerificaitonTokenToHeader
            })
            .done(done)
            .fail(fail);
    }

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
                url: "/api/attendances/" + gigId,
                method: "DELETE",
                beforeSend: window.antiForgery.addVerificaitonTokenToHeader
            })
            .done(done)
            .fail(fail);
    }
    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();