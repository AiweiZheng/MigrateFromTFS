using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);

            _userId = "1";
            _controller.MockCurrentUser(_userId, "tom@gmail.com");
        }

        [TestMethod]
        public void Attend_UserAttemptToAttendAGigWhichHeAlreadyHasAnAttendance_ShouldReturnBadRequest()
        {
            var gigId = 1;
            var attendance = new Attendance();

            _mockRepository.Setup(a => a.GetAttendance(gigId, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto { GigId = gigId });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Delete_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnOk()
        {
            var gigId = 1;
            var attendance = new Attendance();

            _mockRepository.Setup(a => a.GetAttendance(gigId, _userId)).Returns(attendance);

            var result = _controller.DeleteAttendance(gigId);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnOkWithDeletedAttendanceId()
        {
            var gigId = 1;
            var attendance = new Attendance();

            _mockRepository.Setup(a => a.GetAttendance(gigId, _userId)).Returns(attendance);

            var result = (OkNegotiatedContentResult<int>)_controller.DeleteAttendance(1);

            result.Content.Should().Be(gigId);
        }
    }
}
