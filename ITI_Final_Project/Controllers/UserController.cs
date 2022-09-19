using ITI_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalBL.Helpers;

namespace ITI_Final_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly Hotel_App db;

        public UserController(Hotel_App db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            int? Id = HttpContext.Session.GetInt32("UserId");
            if (Id== null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                var data = db.Customers.ToList();
                return View(data);
            }

        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Customer model)
        {
            //check User In Data Base



#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Customer x = db.Customers.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (x != null)
            {
                HttpContext.Session.SetInt32("UserId", x.Id);
                return RedirectToAction("Profile");
            }

            else
            {
                ViewBag.clas = "true";
                ViewBag.error = "Email Or Password Is Not Correct";
                //ModelState.AddModelError("", "Email Or Password Is Not Correct");

                return View();
            }

        }
        #endregion

        #region LogOut

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            List<Nationalty> Nation = db.Nationalties.ToList();
            ViewBag.Nation = new SelectList(Nation, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ActionName("Register")]
        public IActionResult Register(Customer model,IFormFile Img)
        {
            if (ModelState.IsValid)
            {
                  model.Img = UploadFiles.UploaderFiles("Imgs", Img);
              
                var data = new Customer()
                {
                    Confirm_Password = model.Confirm_Password,
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
             
                    Phone_Number = model.Phone_Number,
                    Reception_Id = model.Reception_Id,
                    Notional_Number = model.Notional_Number,
                    Password = model.Password,
                    National_Id=model.National_Id,
                    Img=model.Img
                    
                };

                db.Customers.Add(data);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(model);


        }
        #endregion

        #region Profile
        public IActionResult Profile()
        {
            int? Id = HttpContext.Session.GetInt32("UserId");
            if (Id == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                var data = db.Customers.Where(n => n.Id == Id).Include(m=>m.National).FirstOrDefault();
                return View(data);
            }

        }

        #endregion

    }
}
