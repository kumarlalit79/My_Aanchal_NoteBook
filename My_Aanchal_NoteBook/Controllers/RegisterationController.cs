using Microsoft.AspNetCore.Mvc;
using My_Aanchal_NoteBook.Models;
using My_Aanchal_NoteBook.Repository.Interface;

namespace My_Aanchal_NoteBook.Controllers
{
    public class RegisterationController : Controller
    {
        private readonly IRegisteration registeration;

        public RegisterationController(IRegisteration registeration)
        {
            this.registeration = registeration;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string phoneNumber)
        {
            var user = await registeration.GetUserByPhoneNumber(phoneNumber);
            if (user != null)
            {
                HttpContext.Session.SetString("MobileNumer", user.PhoneNumber);
                HttpContext.Session.SetString("Username", user.Name);
                HttpContext.Session.SetInt32("Userid", user.UserId);
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Code", user.Code);
                //return RedirectToAction("DashboardMethod", "Dashboard");
                return Json(new { success = true, redirectUrl = Url.Action("DashboardMethod", "Dashboard") });
            }
            else
            {
                HttpContext.Session.SetString("MobileNumer", phoneNumber);
                return Json(new { success = false , phoneNumber = phoneNumber });
            }

        }
        public IActionResult SignUp()
        {
            var mobileNumer = HttpContext.Session.GetString("MobileNumer");
            ViewBag.MobileNumer = mobileNumer;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User model)
        {
            await registeration.SignUpUser(model);
            HttpContext.Session.SetString("MobileNumer", model.PhoneNumber);
            HttpContext.Session.SetString("Username", model.Name);
            HttpContext.Session.SetInt32("Userid", model.UserId); // Assuming UserId is returned or available
            HttpContext.Session.SetString("IsLoggedIn", "true");
            return RedirectToAction("DashboardMethod", "Dashboard");
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "Registeration");
        }
        
        
    }
}
