define(['Models/InstructorModel'], function (InstructorModel) {
    function InstructorViewModel() {
        var self = this;
        var InstructorModelObj = new InstructorModel();
        var that = this;
        var initialBind = true;
        var initialBind2 = true;
        var instructorListViewModel = ko.observableArray();
        var capeListViewModel = ko.observableArray();

        this.Initialize = function () {
            var viewModel = {
                first: ko.observable("Bruce"),
                last: ko.observable("Wayne"),
                title: ko.observable("Batman"),
                add: function (data) {
                    that.CreateInstructor(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divInstructor"));
        };

        this.Load = function (id) {
            InstructorModelObj.Load(id, function (result) {

                var viewModel = {
                    id: result.InstructorId,
                    first: result.FirstName,
                    last: result.LastName,
                    title: result.Title,
                    update: function () {
                        self.UpdateInstructor(this);
                    }
                }

                ko.applyBindings(viewModel, document.getElementById("divInstructorEdit"));
            });
        };

        this.Load2 = function (id1, id2) {
            InstructorModelObj.Load2(id1, id2, function (result) {
                
                var viewModel = {
                    studentid: result.StudentId,
                    scheduleid: result.Schedule.ScheduleId,
                    grade: result.Grade,
                    update: function () {
                        self.UpdateGrade(this);
                    }
                }

                ko.applyBindings(viewModel, document.getElementById("divGradeEdit"));
            });
        };

        this.CreateInstructor = function (data) {
            var model = {
                FirstName: data.first(),
                LastName: data.last(),
                Title: data.title()
            }

            InstructorModelObj.CreateInstructor(model, function (result) {
                if (result == "ok") {
                    alert("Create instructor successful");
                } else {
                    alert("Error occurred");
                }
            });
        };

        this.UpdateInstructor = function (viewModel) {
            var InstructorData = {
                InstructorId: viewModel.id,
                FirstName: viewModel.first,
                LastName: viewModel.last,
                Title: viewModel.title
            };

            InstructorModelObj.UpdateInstructor(InstructorData, function (message) {
                $('#divMessage').html(message);
            });

        };

        this.UpdateGrade = function (viewModel) {
            var EnrollData = {
                StudentId: viewModel.studentid,
                Schedule: {
                    ScheduleId: viewModel.scheduleid
                },
                Grade: viewModel.grade
            };

            InstructorModelObj.UpdateGrade(EnrollData, function (message) {
                $('#divMessage').html(message);
            });

        };

        this.GetAll = function () {
            InstructorModelObj.GetAll(function (instrList) {
                instructorListViewModel.removeAll();
                
                for (var i = 0; i < instrList.length; i++) {
                    instructorListViewModel.push({
                        id: instrList[i].InstructorId,
                        first: instrList[i].FirstName,
                        last: instrList[i].LastName,
                        title: instrList[i].Title
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: instructorListViewModel }, document.getElementById("divInstructorListContent"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

        this.GetAllCape = function (id) {
            InstructorModelObj.GetAllCape(id, function (capeList) {
                capeListViewModel.removeAll();

                for (var i = 0; i < capeList.length; i++) {
                    capeListViewModel.push({
                        scheduleid: capeList[i].Schedule.ScheduleId,
                        review: capeList[i].Review,
                        quarter: capeList[i].Schedule.Quarter,
                        year: capeList[i].Schedule.Year,
                        coursetitle: capeList[i].Schedule.Course.Title
                    });
                }

                if (initialBind2) {
                    ko.applyBindings({ viewModel: capeListViewModel }, document.getElementById("divCapeListContent"));
                    initialBind2 = false;
                }
            });
        };

        ko.bindingHandlers.DeleteInstructor = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.id;

                    InstructorModelObj.DeleteInstructor(id, function (result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            instructorListViewModel.remove(viewModel);
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

    return InstructorViewModel;
}
);