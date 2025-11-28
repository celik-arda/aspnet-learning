using AutoMapper;
using form_task_arda.Data;
using form_task_arda.DTOs;
using form_task_arda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace form_task_arda.Controllers
{
	public class GidersController : Controller
	{
		// It s the variable i will use for DB tasks //
		private readonly ApplicationDbContext __db;
		private readonly IMapper __mapper;

		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;


		// that s my all returned db-content from service //
		public GidersController(ApplicationDbContext _db, IMapper _mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			__db = _db;
			__mapper = _mapper;
			_userManager = userManager;
			_signInManager = signInManager;
		}


		[Route("/db-test")]
		public IActionResult DbTest()
		{
			try
			{
				var giderCountEf = __db.Gider_Table.Count();
				var giderCountSql = __db.Gider_Table.FromSqlRaw("SELECT * FROM Gider_Table").ToList().Count;

				return Content($"EF Count: {giderCountEf} - RAW SQL Count: {giderCountSql}");
			}
			catch (Exception ex)
			{
				return Content("HATA: " + ex.Message);
			}
		}




		//[HttpGet]
		//public async Task<IActionResult> Index ()
		//{
		//	int __giderNumber = await __db.Gider_Table.CountAsync();

		//	return View("Giders",__giderNumber);
		//}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}


		[HttpGet]
		public IActionResult AddNewGider ()
		{
			return View();
		}


		//[Authorize(Roles = "Admin")]
		[HttpPost]
		[Authorize]
		public IActionResult AddNewGider (GiderlerDTO _newGider)
		{

			_newGider.Gider_Tarihi = DateTime.Today;

			var _newGiderModel = __mapper.Map<GiderlerModel>(_newGider);

			__db.Gider_Table.Add(_newGiderModel);
			__db.SaveChanges();

			return View();
		}



		public IActionResult DisplayEditingGider (int _editId)
		{
			var displayingItem = __db.Gider_Table.FirstOrDefault(e => e.Gider_Id == _editId);

			if (displayingItem == null)
			{
				return NotFound();
			}

			var editingGiderDTO = __mapper.Map<GiderlerDTO>(displayingItem);

			return PartialView("_EditGiderPopUp", editingGiderDTO);
		}

		[HttpPost]
		[Authorize]
		public IActionResult UpdateGider (GiderlerDTO updatedGider)
		{

			updatedGider.Gider_Tarihi = DateTime.Today;
			var updatedGiderModel = __mapper.Map<GiderlerModel>(updatedGider);

			__db.Gider_Table.Update(updatedGiderModel);
			__db.SaveChanges();

			return RedirectToAction("Giderler");
		}



		[Authorize]
		public IActionResult RemoveThisGider (int _id)
		{
			var deletingGider = __db.Gider_Table.FirstOrDefault(e => e.Gider_Id == _id);

			if (deletingGider == null)
			{
				TempData["Error"] = "Element bulunamadı";


			}


				__db.Gider_Table.Remove(deletingGider);
				__db.SaveChanges();
				
				return RedirectToAction("Giderler"); 


		}


		public IActionResult Giderler()
		{
			var giderlerim = __db.Gider_Table.ToList();
			var _giderler_DTO = __mapper.Map<List<GiderlerDTO>>(giderlerim);

			return View(_giderler_DTO);
		}
	}
}