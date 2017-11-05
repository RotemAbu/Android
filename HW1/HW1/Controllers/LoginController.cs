using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HW1.Models;
using HW1.Dal;
using System.Web.Security;

namespace HW1.Controllers
{
    
    public class LoginController : Controller
    {
        // GET: /LogIn/
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Authenticate(User usr)
        {
            DataLayer dal = new DataLayer();
            if (ModelState.IsValid) //check if the password match the user and exist in data base
            {
                List<User> userExist = (from u in dal.Users
                                        where (u.UserName == usr.UserName) && (u.Password == usr.Password)
                                        select u).ToList<User>(); //query to check manager

                if (userExist.Count==1) //if found in db
                {
                    FormsAuthentication.SetAuthCookie("cookie", true);
                    return RedirectToAction("Index","Manager");//get to manager page
                }
                else
                    return RedirectToAction("Index","Home"); //return to home page
            }
            else
                return View("Login", usr);

        }
    }
}