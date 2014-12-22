define([], function () {
    $.support.cors = true;

    //// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
    //// Due to async nature of ajax, the Jasmine's compare function would throw an error during
    //// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
    //// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
    //// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
    //// be async when called by viewModel.
    function EnrollmentModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.DeleteEnrollment = function (enroll, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Student/DropEnrolledSchedule",
                data: enroll,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleting enrollment.  Is your service layer running?');
                }
            });
        };

        this.GetAllEnroll = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Student/GetEnrollments?studentId=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading enrollment list.  Is your service layer running?');
                }
            });
        };
    }

    return EnrollmentModel;
});