// StudentViewModel depends on the Models/StudentModel to process requests (Load)
define(['Models/EnrollmentModel'], function (EnrollmentModel) {
    function EnrollmentViewModel() {
        var EnrollmentModelObj = new EnrollmentModel();
        var that = this;
        var self = this;
        var initialBind = true;
        var enrollListViewModel = ko.observableArray();

        this.GetAllEnroll = function (id) {
            
            EnrollmentModelObj.GetAllEnroll(id, function (enrollList) {
                enrollListViewModel.removeAll();
                
                for (var i = 0; i < enrollList.length; i++) {
                    enrollListViewModel.push({
                        studentid: id,
                        scheduleid: enrollList[i].Schedule.ScheduleId,
                        year: enrollList[i].Schedule.Year,
                        quarter: enrollList[i].Schedule.Quarter,
                        session: enrollList[i].Schedule.Session,
                        unit: enrollList[i].Schedule.Unit,
                        scheduledayid: enrollList[i].Schedule.ScheduleDay.ScheduleDayId,
                        scheduleday: enrollList[i].Schedule.ScheduleDay.SchedDay,
                        scheduletimeid: enrollList[i].Schedule.ScheduleTime.ScheduleTimeId,
                        scheduletime: enrollList[i].Schedule.ScheduleTime.SchedTime,
                        instructorid: enrollList[i].Schedule.Instructor.InstructorId,
                        firstname: enrollList[i].Schedule.Instructor.FirstName,
                        lastname: enrollList[i].Schedule.Instructor.LastName,
                        courseid: enrollList[i].Schedule.Course.CourseId,
                        coursedescription: enrollList[i].Schedule.Course.Description,
                        coursetitle: enrollList[i].Schedule.Course.Title,
                        grade: enrollList[i].Grade
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: enrollListViewModel }, document.getElementById("divStudentEnrollment"));
                    initialBind = false;
                }
            });
        };

        ko.bindingHandlers.DeleteEnrollment = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.studentid;
                    var sid = viewModel.scheduleid;
                    var m = {
                        StudentId: id,
                        Schedule:
                        {
                            ScheduleId: sid
                        }
                    }

                    EnrollmentModelObj.DeleteEnrollment(m, function (result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            enrollListViewModel.remove(viewModel);
                        }
                    });
                });
            }
        }

        ko.bindingHandlers.sort = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var asc = false;
                element.style.cursor = 'pointer';

                element.onclick = function () {
                    var value = valueAccessor();
                    var prop = value.prop;
                    var data = value.arr;

                    asc = !asc;

                    data.sort(function (left, right) {
                        var rec1 = left;
                        var rec2 = right;

                        if (!asc) {
                            rec1 = right;
                            rec2 = left;
                        }

                        var props = prop.split('.');
                        for (var i in props) {
                            var propName = props[i];
                            var parenIndex = propName.indexOf('()');
                            if (parenIndex > 0) {
                                propName = propName.substring(0, parenIndex);
                                rec1 = rec1[propName]();
                                rec2 = rec2[propName]();
                            } else {
                                rec1 = rec1[propName];
                                rec2 = rec2[propName];
                            }
                        }

                        return rec1 == rec2 ? 0 : rec1 < rec2 ? -1 : 1;
                    });
                };
            }
        }
    }

    return EnrollmentViewModel;
}
);