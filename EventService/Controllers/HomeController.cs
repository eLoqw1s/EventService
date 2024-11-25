using EventService.Interfaces;
using EventService.Models;
using EventService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsRepository _newsRepository;
        private Guid authorId => new Guid(User.FindFirst("authorId").Value);

        public HomeController(ILogger<HomeController> logger, INewsRepository newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNewsDto createNewsDto)
        {
            var newsId = await _newsRepository.CreateNew(Guid.NewGuid(), createNewsDto.StartPublication, createNewsDto.EndPublication,
                createNewsDto.Topic, createNewsDto.Text, createNewsDto.Importance, authorId);

            return Ok(newsId);
        }

        //[AllowAnonymous]
        [HttpGet("NewGetAll")]
        public async Task<ActionResult<List<New>>> Get()
        {
            var news = await _newsRepository.GetAllNews();
            return Ok(news);
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
