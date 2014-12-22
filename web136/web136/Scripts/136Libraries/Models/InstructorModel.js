define([], function () {
    $.support.cors = true;

    //// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
    //// Due to async nature of ajax, the Jasmine's compare function would throw an error during
    //// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
    //// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
    //// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
    //// be async when called by viewModel.
    function InstructorModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.Load = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: 'GET',
                url: "http://localhost:5419/Api/Instructor/ViewInstructor?id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading instructor detail.  Is your service layer running?');
                }
            });
        };

        this.Load2 = function (id1, id2, callback) {
            $.ajax({
                async: asyncIndicator,
                method: 'GET',
                url: "http://localhost:5419/Api/Student/ViewGrade?id1=" + id1 + "&id2=" + id2,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading grade detail.  Is your service layer running?');
                }
            });
        };

        this.CreateInstructor = function (instructor, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Instructor/InsertInstructor",
                data: instructor,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding instructor.  Is your service layer running?');
                }
            });
        };

        this.DeleteInstructor = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Instructor/DeleteInstructor?id=" + id,
                data: '',
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleting instructor.  Is your service layer running?');
                }
            });
        };

        this.GetAll = function (callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Instructor/ViewAllInstructors",
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading instructor list.  Is your service layer running?');
                }
            });
        };

        this.GetAllCape = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:5419/Api/Cape/ViewStudentCape?id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading cape list.  Is your service layer running?');
                }
            });
        };

        this.UpdateInstructor = function (instructor, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:5419/Api/Instructor/UpdateInstructor",
                data: instructor,
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating instructor info');
                }
            });
        };

        this.UpdateGrade = function (enroll, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:5419/Api/Instructor/UpdateGrade",
                data: enroll,
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating grade info');
                }
            });
        };
    }

    return InstructorModel;
});