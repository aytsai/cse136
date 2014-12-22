define([], function () {
    $.support.cors = true;

    //// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
    //// Due to async nature of ajax, the Jasmine's compare function would throw an error during
    //// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
    //// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
    //// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
    //// be async when called by viewModel.
    function ScheduleTimeModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.CreateScheduleTime = function (sched, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Schedule/InsertScheduleTime",
                data: sched,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding schedule time.  Is your service layer running?');
                }
            });
        };

        this.Load = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: 'GET',
                url: "http://localhost:5419/Api/Schedule/GetScheduleTime?id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule time detail.  Is your service layer running?');
                }
            });
        };

        this.DeleteScheduleTime = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Schedule/DeleteScheduleTime?id=" + id,
                data: '',
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleting schedule time.  Is your service layer running?');
                }
            });
        };

        this.GetAll = function (callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Schedule/GetScheduleTimeList",
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule time list.  Is your service layer running?');
                }
            });
        };

        this.Update = function (sched, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:5419/Api/Schedule/UpdateScheduleTime",
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

    return ScheduleTimeModel;
});