using EventService.Controllers;
using EventService.Interfaces;
using EventService.Models.DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EventService.Tests
{
	[TestFixture]
	public class AccountControllerTests
	{
		private AccountController _controller;
		private Mock<IAuthorService> _mockAuthorService;

		[SetUp]
		public void SetUp()
		{
			_mockAuthorService = new Mock<IAuthorService>();
			_controller = new AccountController(_mockAuthorService.Object);

			var httpContext = new DefaultHttpContext();
			_controller.ControllerContext = new ControllerContext
			{
				HttpContext = httpContext
			};
		}

		[Test]
		public async Task Login_ValidCredentials_RedirectsToAdmin()
		{
			// Arrange
			var request = new LoginAuthorRequest
			(
				"test@example.com",
				"Password123"
			);
			_controller.ModelState.Clear();
			_mockAuthorService.Setup(s => s.Login(request.Email, request.Password)).ReturnsAsync("validToken");

			// Act
			var result = await _controller.Login(request) as RedirectToActionResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual("Admin", result.ControllerName);
		}

		[Test]
		public async Task Login_InvalidCredentials_ReturnsViewWithSameRequest()
		{
			// Arrange
			var request = new LoginAuthorRequest
			(
				"test@example.com",
				"WrongPassword"
			);
			_controller.ModelState.Clear();
			_mockAuthorService.Setup(s => s.Login(request.Email, request.Password)).ReturnsAsync((string)null);

			// Act
			var result = await _controller.Login(request) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(request, result.Model);
		}

		[Test]
		public async Task Logout_RemovesCookieAndRedirectsToHome()
		{
			// Act
			var result = await _controller.Logout() as RedirectToActionResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual("Home", result.ControllerName);
		}
	}
}
