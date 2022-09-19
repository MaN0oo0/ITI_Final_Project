using ITI_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
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


    }
}
