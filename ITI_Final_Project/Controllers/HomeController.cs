using ITI_Final_Project.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ITI_Final_Project.Controllers
{
    public class HomeController : Controller
    {

        Hotel_App db;

        public HomeController(Hotel_App db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
    public IActionResult Index(Customer model)
        {
            if (model!=null)
            {
                var data = new Contact_U()
                {
                    Id = model.Id,
           
                };
         
                db.SaveChanges();

               
               
                return RedirectToAction("Box");
            }
            else
            {
                return View();
            }

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
        public IActionResult About()
        {
            return View();
        }
        public IActionResult OurRoom()
        {
            var data = db.Rooms.ToList();
            return View(data);
        }

        public IActionResult Gallery()
        {

            return View();
        }
       
        public IActionResult ContactUs()
        {
            int? Id = HttpContext.Session.GetInt32("UserId");
            if (Id == null)
            {
                return RedirectToAction("Login","User");
            }
            else
            {
                var data = db.Customers.Where(n => n.Id == Id).FirstOrDefault();
                return View(data);
           
            }
        }
        public IActionResult Delete(int? Id)
        {
           
            var data = db.Rooms.Find(Id);
            db.Remove(data);
            db.SaveChanges();
            return View();
        }
    }
}