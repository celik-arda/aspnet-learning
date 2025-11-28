using System.Diagnostics;
using form_task_arda.Models;
using Microsoft.AspNetCore.Mvc;
using form_task_arda.Data;
using Microsoft.AspNetCore.Identity;

namespace form_task_arda.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly ApplicationDbContext __db;
		private readonly UserManager<AppUser> __userManager;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext _db, UserManager<AppUser> _userManager)
		{
			_logger = logger;
			__db = _db;
			__userManager = _userManager;
		}

		public IActionResult Index()
		{

			// Anasayfada Sergilenecek Ýstatistikleri Ayarla // 

			//  kaç kullanýcý var? //
			var usersCount = __db.Users.Count();

			// Toplam Gider Miktarý Ne Kadar? //
			List<int> GidersPrices = __db.Gider_Table
				.Select(gider => gider.Gider_Maliyeti)
				.ToList();

			int totalGidersPrice = GidersPrices.Sum();


			// Kaç Farklý Tedarikçi Var	//
			List<string> tedarikcilerNoRepeat = __db.Gider_Table
				.Select(gider => gider.Gider_Tedarikcisi)
				.Distinct()
				.ToList();

			int companiesCount = tedarikcilerNoRepeat.Count();


		Debug.WriteLine($"- - - - - - - - - -\n\n\n  {totalGidersPrice}  \n\n - - - - - - - - - -");

			return View((usersCount, totalGidersPrice, companiesCount));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
