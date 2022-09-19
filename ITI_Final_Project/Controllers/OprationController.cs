using ITI_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalBL.Helpers;

namespace ITI_Final_Project.Controllers
{
    public class OprationController : Controller
    {
        Hotel_App db;

        public OprationController(Hotel_App db)
        {
            this.db = db;
        }


 
        public IActionResult Index()
        { 
            return View();
        }
        public IActionResult Details()
        {

            return View();
        } 
        public IActionResult Order()
        {

            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Create_Room()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create_Room(Room Model,IFormFile Img)
        {
            if (ModelState.IsValid)
            {
                Model.Img = UploadFiles.UploaderFiles("Rooms", Img);

                var data = new Room()
                {
                    Description = Model.Description,
                    Price = Model.Price,
                    Img = Model.Img,
                };
                db.Rooms.Add(data);
                db.SaveChanges();
                return RedirectToAction("OurRoom","Home");
            }
            return View(Model);
        }

        public IActionResult Reversation(int? Id)
        {
            ViewBag.Id = Id;
            return View();

        }
        [HttpPost]
        public IActionResult Reversation(Revevarstion model)
        {
            if (ModelState.IsValid)
            {
                int? x = HttpContext.Session.GetInt32("UserId");
                var data = new Revevarstion()
                {
                    Customer_Id = x,
                    Expiry_Date = model.Expiry_Date,
                    Reservation_Date = model.Reservation_Date,
                    Room_Number = model.Room_Number,
                };
                db.Revevarstions.Add(data);
                db.SaveChanges();
                return RedirectToAction("OurRoom", "Home");
            }
            else
            {
                return View(model);
            }
           
        }
        public IActionResult ReversationList()
        {
            var x = db.Customers.Where(x => x.IsAdmin == true).FirstOrDefault();
            if (HttpContext.Session.GetInt32("UserId") == x.Id)
            {
                var data = db.Revevarstions.Include(m => m.Customer).Include(s => s.Room_NumberNavigation).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }
        public IActionResult MyReversaion()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int? x= HttpContext.Session.GetInt32("UserId");

                var data = db.Revevarstions.Include(m => m.Customer).Where(m => m.Customer_Id == x).Include(m=>m.Room_NumberNavigation).ToList();
                if (data!=null)
                {

                return View(data);
                }
               
            }
            return View();
        }
    }
  
}
