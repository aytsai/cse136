require(['Models/StudentModel'], function (StudentModel) {

    var student = {
        StudentId: "test1234",
        SSN: "555991234",
        FirstName: "TestFirst",
        LastName: "TestLast",
        Email: "Test@Test.com",
        Password: "Password",
        ShoeSize: 10,
        Weight: 200
    };

    var student2 = {
        StudentId: "test1234",
        SSN: "555991234",
        FirstName: "TestFirst2",
        LastName: "TestLast2",
        Email: "Test@Test.com",
        Password: "Password",
        ShoeSize: 200,
        Weight: 10
    };

    var studentModelObj = new StudentModel(false);

    describe("Student Object", function () {

        it("can create a new student", function () {
            studentModelObj.CreateStudent(student, function (result) {
                expect(result).toEqual('ok');
            });
        });

        it("can return a student detail info by id", function () {
            studentModelObj.GetDetail(student.StudentId, function (detailResult) {
                expect(detailResult.StudentId).toEqual(student.StudentId);
                expect(detailResult.SSN).toEqual(student.SSN);
                expect(detailResult.FirstName).toEqual(student.FirstName);
                expect(detailResult.LastName).toEqual(student.LastName);
                expect(detailResult.Email).toEqual(student.Email);
                expect(detailResult.Password).toEqual(student.Password);
                expect(detailResult.ShoeSize).toEqual(student.ShoeSize);
                expect(detailResult.Weight).toEqual(student.Weight);
            });
        });

        it("can update a student in the database", function () {
            studentModelObj.UpdateStudent(student2, function (result) {
                expect(result).toEqual('ok');
            });
        });

        it("can delete an existing student by id", function () {
            studentModelObj.DeleteStudent(student2.StudentId, function (result) {
                expect(result).toEqual('ok');
            });
        });


        it("can return a list of students in the database", function () {
            studentModelObj.GetAll(function (result) {
                expect(result.length).toBeGreaterThan(0);
            });
        });
    });
});
