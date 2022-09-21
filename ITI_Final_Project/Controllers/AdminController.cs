using ITI_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Final_Project.Controllers
{
    public class AdminController : Controller
    {
        Hotel_App db;

        public AdminController(Hotel_App db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var data = db.Cleaners.ToList();
            return View(data);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Cleaner model)
        {
            if (ModelState.IsValid)
            {
                var data = new Cleaner()
                {
                    Name = model.Name,
                    Phone_Number = model.Phone_Number,
                    Address = model.Address,
                    Salary = model.Salary,
                };
                db.Cleaners.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Delete(int? Id)
        {
            var data = db.Cleaners.Where(m => m.Id == Id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(Cleaner model)
        {
            var data = db.Cleaners.Find(model.Id);
            if (data != null)
            {
                db.Cleaners.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public IActionResult Update(int? Id)
        {
            var data = db.Cleaners.Where(m => m.Id == Id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(Cleaner model)
        {
            var data = db.Cleaners.Where(m=>m.Id==model.Id).FirstOrDefault();
            if (data!=null)
            {
                data.Name = model.Name;
                data.Salary=model.Salary;
                data.Id = model.Id;
                data.Address = model.Address;
                data.Phone_Number=model.Phone_Number;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
