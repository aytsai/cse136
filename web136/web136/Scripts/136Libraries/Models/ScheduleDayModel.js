define([], function () {
    $.support.cors = true;

    //// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
    //// Due to async nature of ajax, the Jasmine's compare function would throw an error during
    //// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
    //// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
    //// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
    //// be async when called by viewModel.
    function ScheduleDayModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.CreateScheduleDay = function (sched, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Schedule/InsertScheduleDay",
                data: sched,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding schedule day.  Is your service layer running?');
                }
            });
        };

        this.Load = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: 'GET',
                url: "http://localhost:5419/Api/Schedule/GetScheduleDay?id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule day detail.  Is your service layer running?');
                }
            });
        };

        this.DeleteScheduleDay = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Schedule/DeleteScheduleDay?id=" + id,
                data: '',
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleting schedule day.  Is your service layer running?');
                }
            });
        };

        this.GetAll = function (callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Schedule/GetScheduleDayList",
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule day list.  Is your service layer running?');
                }
            });
        };

        this.Update = function (sched, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:5419/Api/Schedule/UpdateScheduleDay",
                data: sched,
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating schedule info');
                }
            });
        };
    }

    return ScheduleDayModel;
});