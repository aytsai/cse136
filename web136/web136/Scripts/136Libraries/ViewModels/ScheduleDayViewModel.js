define(['Models/ScheduleDayModel'], function (ScheduleDayModel) {
    function ScheduleDayViewModel() {
        var self = this;
        var ScheduleDayModelObj = new ScheduleDayModel();
        var that = this;
        var initialBind = true;
        var scheduleDayListViewModel = ko.observableArray();

        this.Initialize = function () {
            var viewModel = {
                scheduleday: ko.observable("Mon-Thur"),
                add: function (data) {
                    that.CreateScheduleDay(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divScheduleDay"));
        };

        this.CreateScheduleDay = function (data) {
            var model = {
                SchedDay: data.scheduleday()
            }

            ScheduleDayModelObj.CreateScheduleDay(model, function (result) {
                if (result == "ok") {
                    alert("Create schedule day successful");
                } else {
                    alert("Error occurred");
                }
            });

        };

        this.UpdateScheduleDay = function (id, viewModel) {
            var ScheduleData = {
                ScheduleDayId: id,
                SchedDay: viewModel.scheduleday
            };

            ScheduleDayModelObj.Update(ScheduleData, function (message) {
                $('#divMessage').html(message);
            });

        };

        this.Load = function (id) {
            ScheduleDayModelObj.Load(id, function (result) {
                var viewModel = {
                    scheduledayid: id,
                    scheduleday: result.SchedDay,
                    update: function () {
                        self.UpdateScheduleDay(id, this);
                    }
                }

                ko.applyBindings(viewModel, document.getElementById("divScheduleDayEdit"));
            });
        };

        this.GetAll = function () {
            ScheduleDayModelObj.GetAll(function (scheduleDayList) {
                scheduleDayListViewModel.removeAll();

                for (var i = 0; i < scheduleDayList.length; i++) {
                    scheduleDayListViewModel.push({
                        scheduledayid: scheduleDayList[i].ScheduleDayId,
                        scheduleday: scheduleDayList[i].SchedDay
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: scheduleDayListViewModel }, document.getElementById("divScheduleDayListContent"));
                    initialBind = false;
                }
            });
        };

        ko.bindingHandlers.DeleteScheduleDay = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.scheduledayid;

                    ScheduleDayModelObj.DeleteScheduleDay(id, function (result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            scheduleDayListViewModel.remove(viewModel);
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

    return ScheduleDayViewModel;
}
);