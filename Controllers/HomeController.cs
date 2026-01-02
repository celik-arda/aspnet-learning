using System.Diagnostics;
using AutoMapper;
using form_task_arda.Models;
using Microsoft.AspNetCore.Mvc;
using form_task_arda.Data;
using form_task_arda.DTOs;
using Microsoft.AspNetCore.Identity;

namespace form_task_arda.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IMapper __mapper;

		private readonly ApplicationDbContext __db;
		private readonly UserManager<AppUser> __userManager;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext _db, UserManager<AppUser> _userManager, IMapper _mapper)
		{
			_logger = logger;
			__db = _db;
			__userManager = _userManager;
			__mapper = _mapper;
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


		// BÝLGÝ : .Key alt özelliði .GroupBy() Metodundan gelen bir C# özelliðidir. C# oluþturduðu her grubba rahat kullanabilmek için bir key numarasýný alt özellik olarak oluþturur.

		public JsonResult GetMonthlyGidersForChart ()
		{
			var allMonthNames = new[] {"ocak", "þubat", "mart", "nisan", "mayýs", "haziran", "temmuz", "aðustos", "eylül", "ekim", "kasým", "aralýk"};

			var GiderGroupByMonth = __db.Gider_Table
				.GroupBy(gider => gider.Gider_Tarihi.Month)
				.Select(each_group => new MonthlyGiderDTO
				{
					Month_Number = each_group.Key,
					Total_Monthly_Gider = each_group.Sum(e => e.Gider_Maliyeti)
				})
				.OrderBy(x => x.Month_Number)
				.ToList();
						   
			var myChartDataList = new List<int>();

			for (int i = 1; i <= 12; i++)
			{
				var giderForThisMonth = GiderGroupByMonth.FirstOrDefault(e => e.Month_Number == i);

				myChartDataList.Add(giderForThisMonth?.Total_Monthly_Gider ?? 0);
			}

			return Json(new
			{
				label = allMonthNames,
				data = myChartDataList
			});
		}


		public async Task<IActionResult> ProfilePage ()
		{

			// BÝLGÝ: bu tarz user info'larý identity'den çektiðimde týpký register-login iþlemlerindeki gibi <AppUser> tipinde geliyor. Kafan karýþmasýn, aþaðýda modeli deðil "currentUser"	objesini dönüþtürdüm. Yoksa esas arkaplanda dönüþüm yaptýðým map iþlemi <AppUser>'ý kastediyor, onu da mapper dosyamda zaten yazdým-eþleþtirdim. Controller'da dönüþecek objeyi belirtirsin, obje tipini deðil.

			var currentUser = await __userManager.GetUserAsync(User);

			if(currentUser == null)
			{
				return RedirectToAction("No_Login_Page");
			}

			else
			{
				var currentUserInfos = __mapper.Map<CurrentUserDto>(currentUser
					);
				return View(currentUserInfos);
			}

		}


		[HttpGet]
		public IActionResult No_Login_Page()
		{
			return View();
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
