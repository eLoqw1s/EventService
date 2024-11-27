using EventService.Interfaces;
using EventService.Models;
using EventService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly INewsRepository _newsRepository;
        private Guid authorId => new Guid(User.FindFirst("authorId").Value);

        public AdminController(ILogger<AdminController> logger, INewsRepository newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var news = await _newsRepository.GetAllNews();
            _logger.LogInformation("NewsId {@news}", news);
            return View(news);
        }

        [HttpPost("CreateNews")]
        public async Task<IActionResult> CreateNews([FromForm] CreateNewsDto createNewsDto)
        {
            var newsId = await _newsRepository.CreateNew(Guid.NewGuid(), createNewsDto.StartPublication, createNewsDto.EndPublication,
                createNewsDto.Topic, createNewsDto.Text, createNewsDto.Importance, authorId);

            return RedirectToAction("Index");
        }

        [HttpGet("UpdateNews/{id:guid}")]
        public async Task<IActionResult> UpdateNews(Guid id)
        {
            var news = await _newsRepository.GetNewById(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        [HttpPost("UpdateNews/{id:guid}")]
        public async Task<IActionResult> UpdateNews(Guid id, [FromForm] UpdateNewsDTO updateNewsDTO)
        {
            if (ModelState.IsValid)
            {
                var newsId = await _newsRepository.UpdateNews(id, updateNewsDTO.Topic, updateNewsDTO.Text,
                updateNewsDTO.Importance, DateTime.Now);

                return RedirectToAction("Index");
            }
            return View(updateNewsDTO);
        }

        [HttpPost("News/{id:guid}")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var newsId = await _newsRepository.DeleteNews(id);
            return RedirectToAction("Index");
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
