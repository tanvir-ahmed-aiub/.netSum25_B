using AutoMapper;
using EComm.Auth;
using EComm.DTOs;
using EComm.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EComm.Controllers
{
    public class OrderController : Controller
    {
        EComm_Sum25_BEntities db = new EComm_Sum25_BEntities();
        static Mapper  GetMapper() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            });
            return new Mapper(config);
        }
        // GET: Order
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            var products = GetMapper().Map<List<ProductDTO>>(data);

            return View(products);
        }
        public ActionResult AddtoCart(int id) {
            var p = db.Products.Find(id);
            var pdto = GetMapper().Map<ProductDTO>(p);
            pdto.Qty = 1;
            List<ProductDTO> cart = null;
            if (Session["cart"] == null)
            {
                cart = new List<ProductDTO>();
            }
            else {
                cart =(List<ProductDTO>) Session["cart"];
            }
            var pro = (from pr in cart where pr.Id == id select pr).SingleOrDefault();
            if (pro != null) {
                pro.Qty++;
            }
            else cart.Add(pdto);
            Session["cart"] = cart;
            
            return RedirectToAction("Index");

        }
        public ActionResult Cart() {
            var cart = (List<ProductDTO>)Session["cart"];
            return View(cart);
        }
        [HttpPost]
        [Logged]
        public ActionResult PlaceOrder(decimal gTotal) {

           

            var user = (User)Session["user"];
            var cart = (List<ProductDTO>)Session["cart"];
            var od = new Order() {
                Date = DateTime.Now,
                Total = gTotal,
                CustomerId =(int) user.CustomerId,
                StatusId = 1,
            };
            db.Orders.Add(od);
            db.SaveChanges();

            foreach (var item in cart)
            {
                var odDetail = new OrderDetail() { 
                    PId = item.Id,
                    Qty = item.Qty,
                    Price = item.Price,
                    OId = od.Id
                };
                db.OrderDetails.Add(odDetail);
            }
            db.SaveChanges();
            TempData["Msg"] = "Order Placed Successfully";
            Session["cart"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult Increase(int id) {
            var cart = (List<ProductDTO>)Session["cart"];
            var p = (from pr in cart where pr.Id == id select pr).SingleOrDefault();
            p.Qty++;
            return RedirectToAction("Cart");
            
        }
        public ActionResult Decrease(int id)
        {
            var cart = (List<ProductDTO>)Session["cart"];
            var p = (from pr in cart where pr.Id == id select pr).SingleOrDefault();
            p.Qty--;
            return RedirectToAction("Cart");
        }
    }
}