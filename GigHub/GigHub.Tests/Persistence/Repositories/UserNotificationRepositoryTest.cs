using System.Linq;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using GigHub.Core.Repositories;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class UserNotificationRepositoryTest
    {
        private UserNotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);

            _repository = new UserNotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetNewNotificationNumFor_NotificationsIsNotForTheGivenUser_ShouldNotBeCount()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new UserNotification[] { userNotification });

            var result = _repository.GetNewNotificationNumFor(user.Id + "_");

            result.ShouldBeEquivalentTo(0);
        }

        [TestMethod]
        public void GetNewNotificationNumFor_NotificationIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockUserNotifications.SetSource(new UserNotification[] { userNotification });

            var result = _repository.GetNewNotificationNumFor(user.Id);

            result.ShouldBeEquivalentTo(0);
        }

        [TestMethod]
        public void GetNewNotificationNumFor_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new UserNotification[] { userNotification });

            var result = _repository.GetNewNotificationNumFor(user.Id);

            result.ShouldBeEquivalentTo(1);
        }

        [TestMethod]
        public void GetNotificationsFor_NotificaitonsIsNotForTheGivenUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new UserNotification[] { userNotification });

            var result = _repository.GetNotificationsFor(user.Id + "_");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNotificationFor_NotificationsIsForTheGivenUser_ShouldBeReturned()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNotificationsFor(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);
        }

        [TestMethod]
        public void GetUnreadNotificationsFor_UnReadNotificationsIsNotForTheGivenUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new UserNotification[] { userNotification });

            var result = _repository.GetUnreadNotificationsFor(user.Id + "_");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadNotificationsFor_NotificationsForTheGivenUserIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockUserNotifications.SetSource(new UserNotification[] { userNotification });

            var result = _repository.GetUnreadNotificationsFor(user.Id);

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadNotificationsFor_UnreadNotificationsForTheGivenUser_ShouldBeReturned()
        {
            var notification = Notification.GigCancelled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new UserNotification[] { userNotification });

            var result = _repository.GetUnreadNotificationsFor(user.Id);

            result.Should().HaveCount(1);
            result.Should().Contain(userNotification);
        }
    }
}
