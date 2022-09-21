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
            var data = db.Rooms.ToList();
            return View(data);
        }
         
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Box()
        {
            var data = db.Contact_Us.Include(m=>m.Customer).ToList();
            if (data != null)
            {

                return View(data);
            }
            else
            {
                return RedirectToAction("ContactUs");
            }
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
            
                var contact = db.Contact_Us.FirstOrDefault();
              
                return View(contact);
           
            }
        }
        [HttpPost]
        public IActionResult ContactUs(Contact_U model)
        {
            if (model!=null)
            {
                int? x= HttpContext.Session.GetInt32("UserId"); 
                var data = new Contact_U()
                {
                    Customer_Id =x,
                    Message = model.Message
                };
                db.Contact_Us.Add(data);
                db.SaveChanges();
                return RedirectToAction("Box");
            }
            else return View();
        }
 
        public  IActionResult Delete(int? Id)
        {

            var data = db.Rooms.Where(m => m.Room_number == Id).FirstOrDefault();
             db.Remove(data);
           db.SaveChanges();
             return RedirectToAction("OurRoom");
        }
    }
}