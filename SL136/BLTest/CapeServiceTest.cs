namespace ServiceTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class CapeServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCapeErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeRepository>();
            var capeService = new CapeService(mockRepository.Object);

            //// Act
            capeService.InsertCape(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CapeErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeRepository>();
            var capeService = new CapeService(mockRepository.Object);

            //// Act
            capeService.ViewCape(null, 0, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCapeErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ICapeRepository>();
            var capeService = new CapeService(mockRepository.Object);

            //// Act
            capeService.DeleteCape(0, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCapeErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ICapeRepository>();
            var capeService = new CapeService(mockRepository.Object);

            //// Act
            capeService.UpdateCape(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);

            //// add in a verify for the update methods
        }

        [TestMethod]
        public void UpdateCapeErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ICapeRepository>();
            var capeService = new CapeService(mockRepository.Object);

            Cape cape = new Cape { CapeId = 5, StudentId = "A000001", Schedule = new Schedule { ScheduleId = 13 }, Review = "blah" };

            //// Act
            capeService.UpdateCape(cape, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);

            mockRepository.Verify(x => x.UpdateCape(cape, ref errors), Times.Once);
        }
    }
}
