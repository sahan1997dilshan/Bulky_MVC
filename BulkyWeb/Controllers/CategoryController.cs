using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			if (obi.Name == obi.DisplayOrder)
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
				TempData["success"] = "Category was added succesful"; //when loading view page this will generated the view page.....

				return RedirectToAction("Index", "Category");  //no need to mention controller name if both action are genereted same controller....
			}
			//There is some problem redirect to the current page....
			return View();
			
		}

		//Edit category
		public IActionResult Edit(int ? Id) {
			//retrive available data from the data base...
			if(Id== 0 || Id==null) 
			{
			return NotFound();
			}

			Category data = _db.Categories.Find(Id);
			if (data == null)
			{
				return NotFound();
			}
			return View(data);
		}

		//edit data stro data base again...
		[HttpPost]
		public IActionResult Edit(Category obj) 
		{

			if (obj == null)
			{
				ModelState.AddModelError("", "There is not value");
			}
			if (obj.Name == obj.DisplayOrder)
			{
				ModelState.AddModelError("name", "order cannot match the name");
			}
			if (obj.Name!=null && obj.Name.ToLower() == "test") 
			{
				ModelState.AddModelError("", "name cannot have test");
			}
			if(ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category was Edited succesful";
				return RedirectToAction("Index", "Category");

			}
			return View() ;
		
		}


		//Delete category

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Category obj = _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);

		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteRecord(Category obj)
		{
			//Category obj = _db.Categories.Find(id);

			if (obj == null) 
			{
				return NotFound();
			}
			    _db.Categories.Remove(obj);
			    _db.SaveChanges();
			    TempData["success"] = "Category was Deleted succesful";
			    return RedirectToAction("Index", "Category");
			
			
			
		}


	}
}
