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

		{   
			if(obi.Name==obi.DisplayOrder)
			{
				ModelState.AddModelError("name", "Display order cannot match the name");  //server side validation .......
			}

			if (obi.Name != null && obi.Name.ToLower() == "test")
			{
				ModelState.AddModelError("", "Test is not valid");//server validation
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Add(obi);
				_db.SaveChanges();
				//checked validation

				return RedirectToAction("Index", "Category");  //no need to mention controller name if both action are genereted same controller....
			}
			//There is some problem redirect to the current page....
			return View();
			
		}
	}
}
