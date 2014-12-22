define(['Models/ScheduleModel'], function (ScheduleModel) {
    function ScheduleViewModel() {
        var self = this;
        var ScheduleModelObj = new ScheduleModel();
        var that = this;
        var initialBind = true;
        var scheduleListViewModel = ko.observableArray();

        this.Initialize = function () {
            var viewModel = {
                scheduleid: ko.observable("333"),
                courseid: ko.observable("1"),
                year: ko.observable("2011"),
                quarter: ko.observable("Winter"),
                session: ko.observable("A00"),
                unit: ko.observable("4"),
                scheduledayid: ko.observable("1"),
                scheduletimeid: ko.observable("1"),
                instructorid: ko.observable("1"),
                add: function (data) {
                    that.CreateSchedule(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divSchedule"));
        };

        this.CreateSchedule = function (data) {
            var model = {
                ScheduleId: data.scheduleid(),
                Year: data.year(),
                Quarter: data.quarter(),
                Session: data.session(),
                Unit: data.unit(),
                ScheduleDay:
                {
                    ScheduleDayId: data.scheduledayid()
                },
                ScheduleTime:
                {
                    ScheduleTimeId: data.scheduletimeid()
                },
                Instructor:
                {
                    InstructorId: data.instructorid()
                },
                Course:
                {
                    CourseId: data.courseid()
                }
            }

            ScheduleModelObj.CreateSchedule(model, function (result) {
                if (result == "ok") {
                    alert("Create schedule successful");
                } else {
                    alert("Error occurred");
                }
            });

        };

        this.UpdateSchedule = function (viewModel) {
            var ScheduleData = {
                ScheduleId: viewModel.scheduleid,
                Year: viewModel.year,
                Quarter: viewModel.quarter,
                Session: viewModel.session,
                Unit: viewModel.unit,
                ScheduleDay:
                {
                    ScheduleDayId: viewModel.scheduledayid
                },
                ScheduleTime:
                {
                    ScheduleTimeId: viewModel.scheduletimeid
                },
                Instructor: 
                {
                    InstructorId: viewModel.instructorid
                },
                Course:
                {
                    CourseId: viewModel.courseid
                }
            };

            ScheduleModelObj.Update(ScheduleData, function (message) {
                $('#divMessage').html(message);
            });

        };

        this.Load = function (id) {
            ScheduleModelObj.Load(id, function (result) {
                var viewModel = {
                    scheduleid: id,
                    year: result.Year,
                    quarter: result.Quarter,
                    session: result.Session,
                    unit: result.Unit,
                    scheduledayid: result.ScheduleDay.ScheduleDayId,
                    scheduletimeid: result.ScheduleTime.ScheduleTimeId,
                    instructorid: result.Instructor.InstructorId,
                    courseid: result.Course.CourseId,
                    update: function () {
                        self.UpdateSchedule(this);
                    }
                }


                ko.applyBindings(viewModel, document.getElementById("divScheduleEdit"));
            });
        };

        this.GetAll = function (year, quarter) {
            ScheduleModelObj.GetAll(year, quarter, function (scheduleList) {
                scheduleListViewModel.removeAll();

                for (var i = 0; i < scheduleList.length; i++) {
                    scheduleListViewModel.push({
                        scheduleid: scheduleList[i].ScheduleId,
                        year: scheduleList[i].Year,
                        quarter: scheduleList[i].Quarter,
                        session: scheduleList[i].Session,
                        unit: scheduleList[i].Unit,
                        scheduledayid: scheduleList[i].ScheduleDay.ScheduleDayId,
                        scheduleday: scheduleList[i].ScheduleDay.SchedDay,
                        scheduletimeid: scheduleList[i].ScheduleTime.ScheduleTimeId,
                        scheduletime: scheduleList[i].ScheduleTime.SchedTime,
                        instructorid: scheduleList[i].Instructor.InstructorId,
                        firstname: scheduleList[i].Instructor.FirstName,
                        lastname: scheduleList[i].Instructor.LastName,
                        courseid: scheduleList[i].Course.CourseId,
                        coursedescription: scheduleList[i].Course.Description,
                        coursetitle: scheduleList[i].Course.Title
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: scheduleListViewModel }, document.getElementById("divScheduleListContent"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

        ko.bindingHandlers.DeleteSchedule = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.scheduleid;

                    ScheduleModelObj.DeleteSchedule(id, function (result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            scheduleListViewModel.remove(viewModel);
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

    return ScheduleViewModel;
}
);