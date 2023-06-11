using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
			_db = db;
        }
        public IActionResult Index()
		{ 
			List<Category> objCategoryList = _db.Categories.ToList();
			return View(objCategoryList);
		}

		//added category
		public IActionResult Create()
		{
			return View();
		}

		//added category save to databas and redirect to the view page again...
		[HttpPost]
		public IActionResult Create(Category obi)
		{  _db.Categories.Add(obi);
			_db.SaveChanges();
			return RedirectToAction("Index","Category");  //no need to mention controller name if both action are genereted same controller....
		}
	}
}
