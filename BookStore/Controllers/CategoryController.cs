using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext _db)
        {
            this._db = _db;

        }
        public IActionResult Index()
        {
            List<Category> listCat = _db.categories.ToList();
            return View(listCat);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            var result = _db.categories.FirstOrDefault(x=>x.Name == category.Name);
            
            if(result != null)
            {
                ModelState.AddModelError("Name","Name is already Present in Database");
            }

            if(ModelState.IsValid && result == null)
            {
                _db.categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category Created Sucessfully";
                return RedirectToAction("Index");

            }

            return View(category);       
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var category = _db.categories.FirstOrDefault(x=>x.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                _db.categories.Update(category);
                _db.SaveChanges();
                TempData["Edit"] = "Category Edited Sucessfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            var category = _db.categories.FirstOrDefault(x=>x.Id == id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {

            _db.categories.Remove(category);
            _db.SaveChanges();
            TempData["Delete"] = "Category Deleted Sucessfully";
            return RedirectToAction("Index");
        }

    }
}