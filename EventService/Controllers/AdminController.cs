using EventService.Interfaces;
using EventService.Models;
using EventService.Models.DTO.Admin;
using EventService.Models.DTO.MemorableDates;
using EventService.Models.DTO.News;
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
		private readonly IMemorabeDateRepository _memorabeDateRepository;

		private Guid authorId => new Guid(User.FindFirst("authorId").Value);

        public AdminController(ILogger<AdminController> logger, 
            INewsRepository newsRepository,
            IMemorabeDateRepository memorabeDateRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
			_memorabeDateRepository = memorabeDateRepository;
		}

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var newsList = await _newsRepository.GetAllNews();
            var memDatesList = await _memorabeDateRepository.GetAllMemDate();

            var model = new AdminVm(
                newsList,
                memDatesList
                );

            _logger.LogInformation("NewsId {@news}", newsList);
            return View(model);
        }

        [HttpPost("CreateNews")]
        public async Task<IActionResult> CreateNews([FromForm] CreateNewsDto createNewsDto)
        {
            var newsId = await _newsRepository.CreateNew(createNewsDto.StartPublication, createNewsDto.EndPublication,
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
                updateNewsDTO.Importance, updateNewsDTO.StartPublication, 
                updateNewsDTO.EndPublication);

                return RedirectToAction("Index");
            }
            return View(updateNewsDTO);
        }

        [HttpPost("DeleteNews/{id:guid}")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var newsId = await _newsRepository.DeleteNews(id);
            return RedirectToAction("Index");
        }

        //----------- MemorableDate ----------------

        [HttpPost("CreateMemDate")]
        public async Task<IActionResult> CreateMemDate([FromForm] CreateMemDateDto createMemDateDto)
        {
            var memDateId = await _memorabeDateRepository.CreateMemDate(createMemDateDto.EventDate,
				createMemDateDto.TextNotification, authorId);

            return RedirectToAction("Index");
        }

		[HttpGet("UpdateMemDate/{id:guid}")]
		public async Task<IActionResult> UpdateMemDate(Guid id)
		{
			var memDate = await _memorabeDateRepository.GetMemDateById(id);
			if (memDate == null)
			{
				return NotFound();
			}
			return View(memDate);
		}

		[HttpPost("UpdateMemDate/{id:guid}")]
		public async Task<IActionResult> UpdateMemDate(Guid id, [FromForm] UpdateMemDateDTO updateMemDateDTO)
		{
			if (ModelState.IsValid)
			{
				var newsId = await _memorabeDateRepository.UpdateMemDate(id, 
                    updateMemDateDTO.EventDate, updateMemDateDTO.TextNotification);

				return RedirectToAction("Index");
			}
			return View(updateMemDateDTO);
		}

		[HttpPost("DeleteMemDate/{id:guid}")]
		public async Task<IActionResult> DeleteMemDate(Guid id)
		{
			var memDateId = await _memorabeDateRepository.DeleteMemDate(id);
			return RedirectToAction("Index");
		}

		//-------------------------

		[HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
