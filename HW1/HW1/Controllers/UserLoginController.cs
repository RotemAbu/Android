using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HW1.Models;
using HW1.Dal;
using System.Web.Security;

namespace HW1.Controllers.Content
{

    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {

            return View("UserLogin");
        }
     
        public ActionResult Authentication(LoginUser us)
        {
            DataLayer dal = new DataLayer();
            if (ModelState.IsValid) //check if the password match the user and exist in data base
            {
                List<LoginUser> userExist = (from u in dal.LogUsr
                                             where (u.Email == us.Email) && (u.Pass == us.Pass)
                                             select u).ToList<LoginUser>(); //query to check if user exist in db

                if (userExist.Count == 1) //if found in db
                {
                    FormsAuthentication.SetAuthCookie("cookie", true);
                    return RedirectToAction("Index", "Home");//get to home page
                }
                else
                {
                    TempData["notice"] = "Sorry, we didn’t recognize that email/password combination. Please try again or sign up if you new here";
                    return RedirectToAction("Index", "UserLogin"); //return to login page for new user registration 
                }
            }
            else
                return View("LoginUser", us);




        }
       
        public ActionResult AddNewUser()
        {
            LoginUser usr = new LoginUser();
            DataLayer dal = new DataLayer();

            usr.Email = Request.Form["Email"].ToString();
            usr.Pass = Request.Form["Pass"].ToString();

            if (ModelState.IsValid)
            {
                List<LoginUser> userExist = (from u in dal.LogUsr
                                                where (u.Email == usr.Email) && (u.Pass == usr.Pass)
                                                select u).ToList<LoginUser>(); //query to check if user exist in db

                if (userExist.Count == 0) //if not found in db - new user
                {                  
                    FormsAuthentication.SetAuthCookie("cookie", true);
                    dal.LogUsr.Add(usr);//update db - add new user
                    dal.SaveChanges(); //save changes in db
                    TempData["notice"] = "Welcome to Computer Shop!!";
                    return RedirectToAction("Index", "Home");//get to home page
                   
                }
                else
                {
                    TempData["notice"] = "Sorry, your email address and password already exist. Please try again or sign in"; 
                    return RedirectToAction("Index", "UserLogin"); //return to login page for new user registration      
                }

            }
            else
                return View("LoginUser", usr);

            
        }
    }
  
}