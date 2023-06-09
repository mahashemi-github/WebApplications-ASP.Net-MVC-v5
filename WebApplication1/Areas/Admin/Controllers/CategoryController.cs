using Microsoft.AspNetCore.Mvc;
using WebApplications.DataAccess.Data;
using WebApplications.DataAccess.Repository.IRepository;
using WebApplications.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objectCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objectCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayOrder canNot match the name");
            }
            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "test is an invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category singleCategoryfromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //Category singleCategoryfromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category singleCategoryfromDb = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (singleCategoryfromDb == null)
            {
                return NotFound();
            }
            return View(singleCategoryfromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? singleCategoryfromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (singleCategoryfromDb == null)
            {
                return NotFound();
            }
            return View(singleCategoryfromDb);
        }

        [HttpPost, ActionName("delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
