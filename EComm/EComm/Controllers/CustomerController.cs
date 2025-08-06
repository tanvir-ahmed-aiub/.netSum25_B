using EComm.Auth;
using EComm.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EComm.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        EComm_Sum25_BEntities db = new EComm_Sum25_BEntities();
        
        [Logged]
        public ActionResult Index()
        {
            var user = (User)Session["user"];
            var orders = (from o in db.Orders
                          where
                         o.CustomerId == user.CustomerId
                          select o).ToList();
            return View(orders);
        }
    }
}