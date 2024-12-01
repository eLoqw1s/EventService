using EventService.Interfaces;
using EventService.Models.DTO.Admin;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : Controller
	{
		private readonly INewsRepository _newsRepository;
		private readonly IMemorabeDateRepository _memorabeDateRepository;

		public HomeController(INewsRepository newsRepository,
			IMemorabeDateRepository memorabeDateRepository)
        {
			_newsRepository = newsRepository;
			_memorabeDateRepository = memorabeDateRepository;
		}

		[HttpGet("GetEventsByDate")]
		public async Task<IActionResult> GetEventsByDate(DateTime selectedTime)
		{
			var newsList = await _newsRepository.GetNewsByDate(selectedTime);
			var memDatesList = await _memorabeDateRepository.GetMemDateByDate(selectedTime);

			var model = new AdminVm(
				newsList,
				memDatesList
				);

			return View(model);
		}

		[HttpGet("Index")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
