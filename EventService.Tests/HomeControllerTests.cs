using EventService.Controllers;
using EventService.Interfaces;
using EventService.Models.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using EventService.Models.DTO.News;
using EventService.Models.DTO.MemorableDates;

namespace EventService.Tests
{
	[TestFixture]
	public class HomeControllerTests
	{
		private Mock<INewsRepository> _newsRepositoryMock;
		private Mock<IMemorabeDateRepository> _memorabeDateRepositoryMock;
		private HomeController _controller;

		[SetUp]
		public void SetUp()
		{
			_newsRepositoryMock = new Mock<INewsRepository>();
			_memorabeDateRepositoryMock = new Mock<IMemorabeDateRepository>();
			_controller = new HomeController(_newsRepositoryMock.Object, _memorabeDateRepositoryMock.Object);
		}

		[Test]
		public async Task GetEventsByDate_ReturnsViewResult_WithUserVm()
		{
			// Arrange
			var testDate = new DateTime(2024, 12, 1);
			var newsList = new List<GetNewsVm>
			{
				new GetNewsVm(Guid.NewGuid(), testDate, testDate.AddDays(1), "Topic 1", "Text 1", 1, DateTime.Now, "Author 1")
			};
			var memDatesList = new List<GetMemDateVm>
			{
				new GetMemDateVm(Guid.NewGuid(), testDate, "Notification Text", DateTime.Now, "Name 1")
			};

			_newsRepositoryMock.Setup(repo => repo.GetNewsByDate(testDate)).ReturnsAsync(newsList);
			_memorabeDateRepositoryMock.Setup(repo => repo.GetMemDateByDate(testDate)).ReturnsAsync(memDatesList);

			// Act
			var result = await _controller.GetEventsByDate(testDate) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<UserVm>(result.Model);
			var model = result.Model as UserVm;
			Assert.AreEqual(newsList, model.News);
			Assert.AreEqual(memDatesList, model.MemDates);
		}

		[Test]
		public async Task GetEventsByDate_ReturnsEmptyLists_WhenNoEventsForSelectedDate()
		{
			// Arrange
			var testDate = new DateTime(2023, 12, 2);
			var newsList = new List<GetNewsVm>();
			var memDatesList = new List<GetMemDateVm>();

			_newsRepositoryMock.Setup(repo => repo.GetNewsByDate(testDate)).ReturnsAsync(newsList);
			_memorabeDateRepositoryMock.Setup(repo => repo.GetMemDateByDate(testDate)).ReturnsAsync(memDatesList);

			// Act
			var result = await _controller.GetEventsByDate(testDate) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<UserVm>(result.Model);
			var model = result.Model as UserVm;
			Assert.IsEmpty(model.News);
			Assert.IsEmpty(model.MemDates);
		}

		[Test]
		public void Index_Returns_View()
		{
			// Act
			var result = _controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
