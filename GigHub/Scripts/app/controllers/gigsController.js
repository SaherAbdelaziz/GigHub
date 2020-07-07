var GigsController = function (attendanceService) {
    var button, gigId , userId;

    var toggleAttendance = function (e) {
        console.log("in Button");
        button = $(e.target);
        gigId=button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);

        else
            attendanceService.deleteAttendance(gigId, done, fail);

    };

    var fail = function () {
        alert("Something failed!");
    };

    var done = function () {
        var text = (button.text() == "Going?") ? "Going" : "Going?";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var init = function (container) {
        console.log("start");
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    return {
        init: init
    }

}(AttendanceService);