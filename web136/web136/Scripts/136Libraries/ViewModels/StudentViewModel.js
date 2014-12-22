// StudentViewModel depends on the Models/StudentModel to process requests (Load)
define(['Models/StudentModel'], function (StudentModel) {
    function StudentViewModel() {
        var StudentModelObj = new StudentModel();
        var that = this;
        var self = this;
        var initialBind = true;
        var studentListViewModel = ko.observableArray();
        var enrollListViewModel = ko.observableArray();

        this.Initialize = function() {

            var viewModel = {
                id: ko.observable("A0000111"),
                ssn: ko.observable("555-55-3333"),
                first: ko.observable("Bruce"),
                last: ko.observable("Wayne"),
                email: ko.observable("bwayne@ucsd.edu"),
                password: ko.observable("password"),
                shoesize: ko.observable("10"),
                weight: ko.observable("160"),
                add: function (data) {
                    that.CreateStudent(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divStudent"));
        };

        this.Initialize2 = function (id) {
            var viewModel = {
                studentid: id,
                scheduleid: ko.observable("102"),
                add: function (data) {
                    that.CreateEnrollment(id, data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divEnrollment"));
        };

        this.Initialize3 = function (id, id2) {
            var viewModel = {
                review: ko.observable("Input your review here. Not updating or deleting it afterwards."),
                add: function (data) {
                    that.CreateCape(id, id2, data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divCape"));
        };

        this.CreateEnrollment = function (id, data) {
            var model = {
                StudentId: id,
                Schedule:
                {
                    ScheduleId: data.scheduleid()
                }
            }

            StudentModelObj.CreateEnrollment(model, function (result) {
                if (result == "ok") {
                    alert("Create enrollment successful");
                } else {
                    alert("Error occurred");
                }
            });

        };

        this.Load = function (id) {
            StudentModelObj.Load(id, function (result) {

                var viewModel = {
                    id: result.StudentId,
                    first: result.FirstName,
                    last: result.LastName,
                    email: result.Email,
                    password: result.Password,
                    shoesize: result.ShoeSize,
                    weight: result.Weight,
                    ssn: result.SSN,
                    update: function () {
                        self.UpdateStudent(this);
                    }
                }

                ko.applyBindings(viewModel, document.getElementById("divStudentEdit"));
            });
        };

        this.CreateStudent = function(data) {
            var model = {
                StudentId: data.id(),
                SSN: data.ssn(),
                FirstName: data.first(),
                LastName: data.last(),
                Email: data.email(),
                Password: data.password(),
                ShoeSize: data.shoesize(),
                Weight: data.weight()
            }

            StudentModelObj.CreateStudent(model, function(result) {
                if (result == "ok") {
                    alert("Create student successful");
                } else {
                    alert("Error occurred");
                }
            });

        };

        this.CreateCape = function (id1, id2, data) {
            var model = {
                StudentId: id1,
                Schedule:
                {
                    ScheduleId: id2
                },
                Review: data.review()
            }

            StudentModelObj.CreateCape(model, function (result) {
                if (result == "ok") {
                    alert("Create cape successful");
                } else {
                    alert("Error occurred");
                }
            });

        };

        this.UpdateStudent = function (viewModel) {
            var StudentData = {
                StudentId: viewModel.id,
                SSN: viewModel.ssn,
                FirstName: viewModel.first,
                LastName: viewModel.last,
                Email: viewModel.email,
                Password: viewModel.password,
                ShoeSize: viewModel.shoesize,
                Weight: viewModel.weight
            };

            StudentModelObj.UpdateStudent(StudentData, function (message) {
                $('#divMessage').html(message);
            });

        };

        this.GetAll = function() {
            StudentModelObj.GetAll(function (studentList) {
                studentListViewModel.removeAll();

                for (var i = 0; i < studentList.length; i++) {
                    studentListViewModel.push({
                        id: studentList[i].StudentId,
                        first: studentList[i].FirstName,
                        last: studentList[i].LastName,
                        email: studentList[i].Email
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: studentListViewModel }, document.getElementById("divStudentListContent"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

        this.GetDetail = function (id) {

            StudentModelObj.GetDetail(id, function (result) {

                var student = {
                    id: result.StudentId,
                    first: result.FirstName,
                    last: result.LastName,
                    email: result.Email,
                    shoesize: result.ShoeSize,
                    weight: result.Weight,
                    ssn: result.SSN
                };

                if (initialBind) {
                    ko.applyBindings({ viewModel: student }, document.getElementById("divStudentContent"));
                }
            });
        };

        this.ViewCape = function (id, id2) {

            StudentModelObj.ViewCape(id, id2, function (result) {

                var cap = {
                    review: result.Review
                };

                if (initialBind) {
                    ko.applyBindings({ viewModel: cap }, document.getElementById("divCapeContent"));
                }
            });
        };

        ko.bindingHandlers.DeleteStudent = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.id;

                    StudentModelObj.DeleteStudent(id, function(result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            studentListViewModel.remove(viewModel);
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

    return StudentViewModel;
}
);