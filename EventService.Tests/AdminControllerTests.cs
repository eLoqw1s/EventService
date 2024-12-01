using EventService.Controllers;
using EventService.Exceptions;
using EventService.Interfaces;
using EventService.Models.DTO.Admin;
using EventService.Models.DTO.MemorableDates;
using EventService.Models.DTO.News;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace EventService.Tests
{
    [TestFixture]
    public class AdminControllerTests
    {
        private AdminController _controller;
        private Mock<INewsRepository> _mockNewsRepository;
        private Mock<IMemorabeDateRepository> _mockMemorableDateRepository;

        [SetUp]
        public void Setup()
        {
            _mockNewsRepository = new Mock<INewsRepository>();
            _mockMemorableDateRepository = new Mock<IMemorabeDateRepository>();
            _controller = new AdminController(_mockNewsRepository.Object, _mockMemorableDateRepository.Object);

            var claims = new[] { new Claim("authorId", Guid.NewGuid().ToString()) };
            var identity = new ClaimsIdentity(claims);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) };
        }

        [Test]
        public async Task Index_ShouldReturnViewWithModel()
        {
            // Arrange
            _mockNewsRepository.Setup(repo => repo.GetAllNews()).ReturnsAsync(new List<GetNewsVm>());
            _mockMemorableDateRepository.Setup(repo => repo.GetAllMemDate()).ReturnsAsync(new List<GetMemDateVm>());

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<AdminVm>(result.Model);
        }

        [Test]
        public async Task CreateNews_ValidDto_ShouldRedirectToIndex()
        {
            // Arrange
            var createNewsDto = new CreateNewsDto(DateTime.Now, 
                DateTime.Now.AddDays(1), 
                "Test Topic", 
                "Test Text", 
                1);

            _mockNewsRepository.Setup(repo => repo.CreateNew(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<int>(), 
                It.IsAny<Guid>()))
               .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.CreateNews(createNewsDto) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public async Task CreateNews_InvalidDto_ShouldReturnView()
        {
            // Arrange
            _controller.ModelState.AddModelError("Topic", "Required");

            // Act
            var result = await _controller.CreateNews(new CreateNewsDto(DateTime.Now, 
                DateTime.Now.AddDays(1), 
                "", 
                "Test Text", 
                1)) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewData.ModelState.IsValid == false);
        }

        [Test]
        public async Task UpdateNews_ValidIdAndDto_ShouldRedirectToIndex()
        {
            // Arrange
            var updateDto = new UpdateNewsDto("Updated Topic", 
                "Updated Text", 
                1, 
                DateTime.Now, 
                DateTime.Now.AddDays(1));

            _mockNewsRepository.Setup(repo => repo.UpdateNews(It.IsAny<Guid>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<int>(), 
                It.IsAny<DateTime>(), 
                It.IsAny<DateTime>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.UpdateNews(Guid.NewGuid(), updateDto) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public async Task UpdateNews_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateDto = new UpdateNewsDto("Updated Topic", "Updated Text", 1, DateTime.Now, DateTime.Now.AddDays(1));

            _mockNewsRepository.Setup(repo => repo.UpdateNews(invalidId, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                               .ThrowsAsync(new CustomException("News not found", 404));

            // Act
            var ex = Assert.ThrowsAsync<CustomException>(async () =>
                await _controller.UpdateNews(invalidId, updateDto));

            //Assert
            Assert.AreEqual("News not found", ex.Message);
            Assert.AreEqual(404, ex.ErrorCode);
        }

        [Test]
        public async Task DeleteNews_ValidId_ShouldRedirectToIndex()
        {
            // Arrange
            var newsId = Guid.NewGuid();
            _mockNewsRepository.Setup(repo => repo.DeleteNews(newsId)).ReturnsAsync(newsId);

            // Act
            var result = await _controller.DeleteNews(newsId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public async Task CreateMemDate_ValidDto_ShouldRedirectToIndex()
        {
            // Arrange
            var createMemDateDto = new CreateMemDateDto(DateTime.Now, "Test Notification");
            _mockMemorableDateRepository.Setup(repo => repo.CreateMemDate(It.IsAny<DateTime>(), 
                It.IsAny<string>(), 
                It.IsAny<Guid>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.CreateMemDate(createMemDateDto) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public async Task UpdateMemDate_ValidIdAndDto_ShouldRedirectToIndex()
        {
            // Arrange
            var updateDto = new UpdateMemDateDto(DateTime.Now, "Updated Notification");
            _mockMemorableDateRepository.Setup(repo => repo.UpdateMemDate(It.IsAny<Guid>(), 
                It.IsAny<DateTime>(), 
                It.IsAny<string>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.UpdateMemDate(Guid.NewGuid(), updateDto) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public async Task DeleteMemDate_ValidId_ShouldRedirectToIndex()
        {
            // Arrange
            var memDateId = Guid.NewGuid();
            _mockMemorableDateRepository.Setup(repo => repo.DeleteMemDate(memDateId)).ReturnsAsync(memDateId);

            // Act
            var result = await _controller.DeleteMemDate(memDateId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }

}
