using HW1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW1.Dal;
using HW1.ViewModel;
using System.Data.SqlClient;
using System.Threading;

namespace HW1.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        public ActionResult Index()
        {
            return RedirectToAction("ManagerPage");
        }

        public ActionResult ManagerPage()
        {
            DataLayer dal = new DataLayer();
            List<Product> objProducts = dal.Products.ToList<Product>();
            ProductViewModel pvm = new ProductViewModel();
            pvm.product = new Product();
            pvm.products = objProducts;
            return View(pvm);
        }

        public ActionResult GetProductsByJson()
        {
            DataLayer dal = new DataLayer();
            Thread.Sleep(3000);
            List<Product> objProducts = dal.Products.ToList<Product>();
            return Json(objProducts, JsonRequestBehavior.AllowGet); //get products json
        }
     

        [HttpPost]
        public ActionResult Submit()
        {

            Product objp = new Product();
            DataLayer dal = new DataLayer();

            objp.ProductId = Request.Form["product.ProductId"].ToString(); //gets the details of the product we want to add
            objp.ProName = Request.Form["product.ProName"].ToString();
            objp.ProAmount = Request.Form["product.ProAmount"].ToString();
            objp.Price = int.Parse(Request.Form["product.Price"]);


            if (ModelState.IsValid) //check validation
            {
                dal.Products.Add(objp); //add new product to data base
                dal.SaveChanges(); //save changes
            }
            //else
            //{
            //    return View("ManagerPage");//show message, please fill the required field
            //}

            List<Product> objProducts = dal.Products.ToList<Product>();
            return Json(objProducts, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDelete()
        {
            return View();
        }
       
        public ActionResult UpdateSubmit()
        {
            
            Product objp = new Product();
            DataLayer dal = new DataLayer();

            objp.ProductId = Request.Form["product.ProductId"].ToString(); //get the product id of the product we want to update
            objp.ProAmount = Request.Form["product.ProAmount"].ToString(); //get the updated amount
            

            if (ModelState.IsValid)
            {
                List<Product> prodToUpdate = (from x in dal.Products
                                              where x.ProductId == objp.ProductId
                                              select x).ToList<Product>();
 
                if (prodToUpdate.Count != 0) //if the query return with result
                {
                    prodToUpdate[0].ProAmount = objp.ProAmount; //update the product amount
                    dal.SaveChanges();
                    return View("UpdateDelete");
                }
            }

            List<Product> objProducts = dal.Products.ToList<Product>();
            return Json(objProducts, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult DeleteSubmit()
        {
            DataLayer dal = new DataLayer();
            Product objp = new Product();
            

            objp.ProductId = Request.Form["product.ProductId"].ToString(); //get the product id which the manager wants to delete

            if (ModelState.IsValid)
            {
                List<Product> prodForDelete = (from x in dal.Products
                                               where x.ProductId == objp.ProductId
                                               select x).ToList<Product>();
                if (prodForDelete.Count != 0)
                {

                    dal.Products.Remove(prodForDelete[0]); //remove the product from data base
                    dal.SaveChanges(); 
                    return View("UpdateDelete");
                }
            }
       
            List<Product> objProducts = dal.Products.ToList<Product>();
            return Json(objProducts, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Orders()
        {
            DataLayer dal = new DataLayer();
            List<Order> objOrders = dal.Orders.ToList<Order>();
            ProductViewModel pvm = new ProductViewModel();
            pvm.order = new Order();
            pvm.orders = objOrders;
            return View(pvm);
           
        }
    }
}