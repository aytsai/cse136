define([], function () {
    $.support.cors = true;
    
    //// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
    //// Due to async nature of ajax, the Jasmine's compare function would throw an error during
    //// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
    //// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
    //// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
    //// be async when called by viewModel.
    function StudentModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.CreateEnrollment = function (enroll, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Student/EnrollSchedule",
                data: enroll,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding enrollment.  Is your service layer running?');
                }
            });
        };

        this.CreateStudent = function (student, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Student/InsertStudent",
                data: student,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding student.  Is your service layer running?');
                }
            });
        };

        this.CreateCape = function (cape, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Cape/InsertCape",
                data: cape,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding cape.  Is your service layer running?');
                }
            });
        };

        this.DeleteStudent = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Student/DeleteStudent?id=" + id,
                data: '',
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleteing student.  Is your service layer running?');
                }
            });
        };

        this.GetAll = function (callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Student/GetStudentList",
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading student list.  Is your service layer running?');
                }
            });
        };

        this.GetDetail = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Student/GetStudent?id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading student detail.  Is your service layer running?');
                }
            });
        };

        this.ViewCape = function (id, id2, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Cape/ViewCape?id1=" + id + "&id2=" + id2,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading cape detail.  Is your service layer running?');
                }
            });
        };

        this.Load = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: 'GET',
                url: "http://localhost:5419/Api/Student/GetStudent?id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading student detail.  Is your service layer running?');
                }
            });
        };

        this.UpdateStudent = function (studentData, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:5419/Api/Student/UpdateStudent",
                data: studentData,
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating student info');
                }
            });
        };
    }

    return StudentModel;
});