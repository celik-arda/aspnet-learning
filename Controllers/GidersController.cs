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

		public IActionResult Giderler()
		{
			var giderlerim = __db.Gider_Table.ToList();
			var _giderler_DTO = __mapper.Map<List<GiderlerDTO>>(giderlerim);

			return View(_giderler_DTO);
		}
	}
}
