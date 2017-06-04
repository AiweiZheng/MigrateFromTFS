using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class NotificationControllerTest
    {
        private Mock<IUserNotificationRepository> _mockRepository;
        private NotificationsController _controller;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IUserNotificationRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.UserNotifications).Returns(_mockRepository.Object);

            _controller = new NotificationsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "tom@gmail.com");
        }
        //
        //        [TestMethod]
        //        public void GetNewNotifications_ValidRequest_ShouldReturnNotificationDtos()
        //        {
        //            var newMessageCount = 1;
        //            List<Notification> notifications = new List<Notification>
        //            {
        //                Notification.GigCreated(new Gig(){})
        //            };
        //            _mockRepository.Setup(m => m.GetNotificationsFor(_userId)).Returns(notifications);
        //            _mockRepository.Setup(m => m.GetNewNotificationNumFor(_userId)).Returns(newMessageCount);
        //
        //            var result = _controller.GetNewNotifications();
        //
        //            result.ShouldBeEquivalentTo(new NotificationsDto
        //            {
        //                NewNotificationCount = newMessageCount,
        //          
        //            });
        //        }
    }
}
