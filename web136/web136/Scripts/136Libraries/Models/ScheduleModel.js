define([], function () {
    $.support.cors = true;

    //// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
    //// Due to async nature of ajax, the Jasmine's compare function would throw an error during
    //// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
    //// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
    //// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
    //// be async when called by viewModel.
    function ScheduleModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.CreateSchedule = function (schedule, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Schedule/InsertSchedule",
                data: schedule,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding schedule.  Is your service layer running?');
                }
            });
        };

        this.Load = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: 'GET',
                url: "http://localhost:5419/Api/Schedule/GetSchedule?id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule detail.  Is your service layer running?');
                }
            });
        };

        this.DeleteSchedule = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Schedule/DeleteSchedule?id=" + id,
                data: '',
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleting schedule.  Is your service layer running?');
                }
            });
        };

        this.GetAll = function (year, quarter, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Schedule/GetScheduleList?year=" + year + "&quarter=" + quarter,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule list.  Is your service layer running?');
                }
            });
        };

        this.Update = function (scheduleData, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:5419/Api/Schedule/UpdateSchedule",
                data: scheduleData,
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating schedule info');
                }
            });
        };
    }

    return ScheduleModel;
});