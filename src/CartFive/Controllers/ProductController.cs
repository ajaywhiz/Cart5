using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CartFive.Utils;
using CartFive.Models;
using CartFive.ViewModel;

namespace CartFive.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/

        public ActionResult Index(int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            var query = DBSession.LuceneQuery<Product>("ProductIndex").OrderBy("Name").Skip((page.Value - 1) * 4).Take(4);

            ProductIndexViewModel productIndexView = new ProductIndexViewModel() { Products = query.ToList(), PageSize = 4, PageIndex = page.Value, TotalRecords = query.QueryResult.TotalResults };

            return View(productIndexView);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {


            if (ModelState.IsValid)
            {
                product.Id = "product/";
                DBSession.Store(product);
                DBSession.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(string id)
        {
            Product product = DBSession.Load<Product>(id);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {

            if (ModelState.IsValid)
            {
                DBSession.Store(product);
                DBSession.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(string id)
        {

            DBSession.DatabaseCommands.Delete(id, null);
            DBSession.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
