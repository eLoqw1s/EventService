using EventService.Interfaces;
using EventService.Models.DTO.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthorService _authorService;

        public AccountController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        //[HttpGet("register")]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterAuthorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _authorService.Register(request.Name, request.Email, request.Password);

            return RedirectToAction("Login");
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginAuthorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var token = await _authorService.Login(request.Email, request.Password);

            if(token == null)
            {
                return View(request);
            }

            Response.Cookies.Append("notJwtToken", token);

            return RedirectToAction("Index", "Admin");
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			Response.Cookies.Delete("notJwtToken");

			return RedirectToAction("Index", "Home");
		}
	}
}
