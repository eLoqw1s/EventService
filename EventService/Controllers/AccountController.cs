using EventService.Models.DTO;
using EventService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly AuthorService _authorService;

        public AccountController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("/register")]
        public async Task<IResult> Register(RegisterAuthorRequest request)
        {
            await _authorService.Register(request.Name, request.Email, request.Password);

            return Results.Ok();
        }

        [HttpPost("/login")]
        public async Task<IResult> Login(LoginAuthorRequest request)
        {
            var token = await _authorService.Login(request.Email, request.Password);

            Response.Cookies.Append("notJwtToken", token);

            return Results.Ok();
        }
    }
}
