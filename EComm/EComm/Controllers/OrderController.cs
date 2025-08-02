using AutoMapper;
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

            cart.Add(pdto);
            Session["cart"] = cart;
            
            return RedirectToAction("Index");

        }
        public ActionResult Cart() {
            var cart = (List<ProductDTO>)Session["cart"];
            return View(cart);
        }
    }
}