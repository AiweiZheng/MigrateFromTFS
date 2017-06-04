using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using NUnit.Framework;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act 
            var result = _controller.Mine();

            //Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        //        public ActionResult Attending()
        //        {
        //            var userId = User.Identity.GetUserId();
        //
        //            var viewMode = new GigsViewModel
        //            {
        //                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
        //                ShowActions = User.Identity.IsAuthenticated,
        //                Heading = "Gigs I'm Attending",
        //                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
        //                    .ToLookup(a => a.GigId)
        //            };
        //
        //            return View("Gigs", viewMode);
        //        }

        [Test, Isolated]
        public void Attending_WhenCalled_ShouldReturnAttendingGigs()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var attendance = new Attendance { AttendeeId = user.Id };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            var result = _controller.Attending();

            //Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenGig()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();



            //Act 
            _controller.Update(new GigFormViewModel
            {
                Id = gig.Id,
                DateTime = DateTime.Today.AddMonths(1),

                Venue = "Venue",
                Genre = 2
            });

            //Assert
            _context.Entry(gig).Reload();

            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            gig.GenreId.Should().Be(2);
        }
    }
}
