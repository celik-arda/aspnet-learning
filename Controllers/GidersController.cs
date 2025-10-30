using AutoMapper;
using form_task_arda.Data;
using form_task_arda.DTOs;
using form_task_arda.Models;
using Microsoft.AspNetCore.Mvc;


namespace form_task_arda.Controllers
{
	public class GidersController : Controller
	{
		// It s the variable i will use for DB tasks //
		private readonly ApplicationDbContext __db;
		private readonly IMapper __mapper;


		// that s my all returned db-content from service //
		public GidersController(ApplicationDbContext _db, IMapper _mapper)
		{
			__db = _db;
			__mapper = _mapper;										  
		}


		[HttpGet]
		public IActionResult AddNewGider ()
		{
			return View();
		}


		[HttpPost]
		public IActionResult AddNewGider (GiderlerDTO _newGider)
		{

			_newGider.Gider_Tarihi = DateTime.Today;

			var _newGiderModel = __mapper.Map<GiderlerModel>(_newGider);

			__db.Gider_Table.Add(_newGiderModel);
			__db.SaveChanges();

			return View();
		}



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