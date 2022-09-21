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

            var data = db.Rooms.ToList();
            return View(data);
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
            var data = new Revevarstion()
            {
                Room_Number = Id,
             
            };

            

            return View(data);

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

                var data = db.Revevarstions.Include(m => m.Customer).Where(m => m.Customer_Id == x).ToList();
                if (data!=null)
                {

                return View(data);
                }
               
            }
            return View();
        }

        #region Crud User

        public  IActionResult MyReversaion_Delete(int? Id)
        {
            var data = db.Revevarstions.Include(m=>m.Customer).Include(m=>m.Room_NumberNavigation).Where(m=>m.Reservatation_Number==Id).FirstOrDefault();
            if (data != null)
            {
                return View(data);
            }
            
            return View();
        }


        [HttpPost]
        public IActionResult MyReversaion_Delete(Revevarstion model)
        {
            var data = db.Revevarstions.Where(m => m.Reservatation_Number == model.Reservatation_Number).FirstOrDefault();
            if (data!=null)
            {
                db.Revevarstions.Remove(data);
                db.SaveChanges();
                return RedirectToAction("MyReversaion");
            }
            return View(model);
        }

        public IActionResult MyReversaion_Update(int? Id)
        {

            var data = db.Revevarstions.Include(m => m.Customer).Include(m => m.Room_NumberNavigation).Where(m => m.Reservatation_Number == Id).FirstOrDefault();
            if (data != null)
            {
                return View(data);
            }

            return View();
        }
        [HttpPost]
        public IActionResult MyReversaion_Update(Revevarstion model)
        {

            var data = db.Revevarstions.Where(m=>m.Reservatation_Number==model.Reservatation_Number).FirstOrDefault();
            if (data != null)
            {
                data.Reservation_Date = DateTime.Now;
                data.Room_Number = model.Room_Number;
                data.Customer_Id = model.Customer_Id;
                data.Expiry_Date = model.Expiry_Date;
                db.SaveChanges();
                return RedirectToAction("MyReversaion");
            }
            return View(model);
        }

        #endregion
    }

}
