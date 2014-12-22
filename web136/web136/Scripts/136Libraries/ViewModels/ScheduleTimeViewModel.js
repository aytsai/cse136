define(['Models/ScheduleTimeModel'], function (ScheduleTimeModel) {
    function ScheduleTimeViewModel() {
        var self = this;
        var ScheduleTimeModelObj = new ScheduleTimeModel();
        var that = this;
        var initialBind = true;
        var scheduleTimeListViewModel = ko.observableArray();

        this.Initialize = function () {
            var viewModel = {
                scheduletime: ko.observable("11:30-12:30"),
                add: function (data) {
                    that.CreateScheduleTime(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divScheduleTime"));
        };

        this.CreateScheduleTime = function (data) {
            var model = {
                SchedTime: data.scheduletime()
            }

            ScheduleTimeModelObj.CreateScheduleTime(model, function (result) {
                if (result == "ok") {
                    alert("Create schedule time successful");
                } else {
                    alert("Error occurred");
                }
            });

        };

        this.UpdateScheduleTime = function (id, viewModel) {
            var ScheduleData = {
                ScheduleTimeId: id,
                SchedTime: viewModel.scheduletime
            };

            ScheduleTimeModelObj.Update(ScheduleData, function (message) {
                $('#divMessage').html(message);
            });

        };

        this.Load = function (id) {
            ScheduleTimeModelObj.Load(id, function (result) {
                var viewModel = {
                    scheduletimeid: id,
                    scheduletime: result.SchedTime,
                    update: function () {
                        self.UpdateScheduleTime(id, this);
                    }
                }

                ko.applyBindings(viewModel, document.getElementById("divScheduleTimeEdit"));
            });
        };

        this.GetAll = function () {
            ScheduleTimeModelObj.GetAll(function (scheduleTimeList) {
                scheduleTimeListViewModel.removeAll();

                for (var i = 0; i < scheduleTimeList.length; i++) {
                    scheduleTimeListViewModel.push({
                        scheduletimeid: scheduleTimeList[i].ScheduleTimeId,
                        scheduletime: scheduleTimeList[i].SchedTime
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: scheduleTimeListViewModel }, document.getElementById("divScheduleTimeListContent"));
                    initialBind = false;
                }
            });
        };

        ko.bindingHandlers.DeleteScheduleTime = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.scheduletimeid;

                    ScheduleTimeModelObj.DeleteScheduleTime(id, function (result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            scheduleTimeListViewModel.remove(viewModel);
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

    return ScheduleTimeViewModel;
}
);