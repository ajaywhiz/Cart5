using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CartFive.Utils;
using CartFive.ViewModel;
using CartFive.Models;

namespace CartFive.Controllers
{
    public class ShopController : BaseController
    {
        //
        // GET: /Shop/

        public ActionResult Index(int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            

            var query = DBSession.LuceneQuery<Product>("ProductIndex").OrderBy("Name").Skip((page.Value - 1) * 6).Take(6);

            ProductIndexViewModel productIndexView = new ProductIndexViewModel() { Products = query.ToList(), PageSize = 6, PageIndex = page.Value, TotalRecords = query.QueryResult.TotalResults };

            return View(productIndexView);
        }

        [HttpPost]
        [JsonObjectFilter(Param = "cartItems", FormElement = "cart", RootType = typeof(List<OrderItem>))]
        public ActionResult Index(FormCollection data, List<OrderItem> cartItems)
        {

            string action = data["goto"];

            TempData["CartItems"] = cartItems;

            if (action == "checkout")
            {               
                return RedirectToAction("Checkout");
            }
            else if (action == "cart")
            {
                return RedirectToAction("Cart");
            }
            else
            {
                return View();
            }
        }

        public void ProcessOrder(Order order)
        {
            //applying some business logic :)
            order.TotalAmount = order.OrderItems.Sum(x => x.Price * x.Qty);

            if (order.TotalAmount > 500)
            {
                //give a disocunt of 5% if total is greater than 500
                order.Discount = order.TotalAmount * 0.05m;
            }
            else
            {
                if (order.OrderItems.Count > 4)
                {
                    //give a discount of 1% if there are more than 4 items in an order
                    order.Discount = order.TotalAmount * 0.01m;
                }
            }

            order.PayableAmount = order.TotalAmount - order.Discount;
            
        }


        public ActionResult Checkout()
        {
            List<OrderItem> cartItems = (List<OrderItem>)TempData["CartItems"];

            TempData["CartItems"] = cartItems;

             Order order = new Order();
            order.OrderItems = cartItems;

            ProcessOrder(order);            

            return View(order);
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection formData)
        {
            List<OrderItem> cartItems = (List<OrderItem>)TempData["CartItems"];

            TempData["CartItems"] = cartItems;

            Order order = new Order();
            order.OrderItems = cartItems;

            ProcessOrder(order);

            string action = formData["goto"];
            if (action == "Shop More!")
            {
                return RedirectToAction("Index");
            }
            else if (action == "Enter Delivery Information")
            {
                return RedirectToAction("Order");
            }
            else
            {
                return View(order);
            }
        }

        public ActionResult Order()
        {
            List<OrderItem> cartItems = (List<OrderItem>)TempData["CartItems"];

            Order order = new Order();
            order.OrderItems = cartItems;

            ProcessOrder(order);

            TempData["CartItems"] = cartItems;
            
            TempData["Order"] = order;            

            return View(order);
        }

        [HttpPost]
        public ActionResult Order(Customer customer)
        {
            Order order = (Order)TempData["Order"];
            if (ModelState.IsValid)
            {
                customer.Id = "customer/";
                DBSession.Store(customer);
                order.CustomerInfo = customer;
                order.OrderDate = DateTime.Now;
                order.Id = "order/";
                DBSession.Store(order);
                DBSession.SaveChanges();

                return RedirectToAction("Process");
            }
            else
            {
                TempData["Order"] = order;
                return View(order);
            }
        }
        
        public ActionResult Cart()
        {
            List<OrderItem> cartItems = (List<OrderItem>)TempData["CartItems"];

            TempData["CartItems"] = cartItems;

            Order order = new Order();
            order.OrderItems = cartItems;

            ProcessOrder(order);            

            return View(order);
        }

        [HttpPost]
        public ActionResult Cart(FormCollection formData)
        {
            List<OrderItem> cartItems = (List<OrderItem>)TempData["CartItems"];

            TempData["CartItems"] = cartItems;

            Order order = new Order();
            order.OrderItems = cartItems;

            ProcessOrder(order);

            string action=formData["goto"];
            if (action == "Shop More!")
            {
                return RedirectToAction("Index");
            }
            else if (action == "Checkout")
            {
                return RedirectToAction("Checkout");
            }
            else
            {
                return View(order);
            }

        }

        public ActionResult Process()
        {
            return View();
        }
    }
}
