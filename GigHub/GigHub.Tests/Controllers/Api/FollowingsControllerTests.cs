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
    public class FollowingsControllerTests
    {
        private Mock<IFollowingRepository> _repository;
        private FollowingsController _controller;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {

            _repository = new Mock<IFollowingRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(m => m.Followings).Returns(_repository.Object);

            _controller = new FollowingsController(mockUoW.Object);

            _userId = "1";
            _controller.MockCurrentUser(_userId, "tom@gmail.com");
        }

        [TestMethod]
        public void Follow_UserAttemptToFollowAnArtistHeAlreadyHasFollowed_ShouldReturnBadRequest()
        {
            var artistId = "1";
            var following = new Following();

            _repository.Setup(f => f.GetFollowing(artistId, _userId)).Returns(following);

            var result = _controller.Follow(new FollowingDto { FolloweeId = artistId });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Follow_VailidRequest_ShouldReturnOk()
        {
            var result = _controller.Follow(new FollowingDto { FolloweeId = "1" });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void UnFollow_NoFollowingWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.UnFollow("1");

            result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public void UnFollow_ValidRequest_ShouldReturnOk()
        {
            var artistId = "1";
            var following = new Following();

            _repository.Setup(f => f.GetFollowing(artistId, _userId)).Returns(following);

            var result = _controller.UnFollow(artistId);

            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [TestMethod]
        public void UnFollow_ValidRequest_ShouldReturnOkWithDeletedAttendanceId()
        {
            var artistId = "1";
            var following = new Following();

            _repository.Setup(f => f.GetFollowing(artistId, _userId)).Returns(following);

            var result = (OkNegotiatedContentResult<string>)_controller.UnFollow(artistId);

            result.Content.Should().Be(artistId);
        }
    }
}
