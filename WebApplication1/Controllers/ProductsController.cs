using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using static WebApplication1.Models.QueryModel;
using static WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index(ProductQM query)
        {
            if (query.DoQuery)
            {
                var ProductsList = db.Product.Where(x => (x.ProductId.Equals(query.ProductId) || query.ProductId.Equals(null))
                                                    && (x.ProductName.Contains(query.ProductName) || query.ProductName.Equals(null))
                                                   ).ToList();
                ViewBag.QueryModel = query;
                return View(ProductsList);
            }
            else {
                ViewBag.QueryModel = query;
                return View(db.Product.Take(10).ToList());
            } 
            
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                TempData["ProductInsertedMsg"] = product.ProductName + "新增成功";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult CreateNew()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] ProductCreationVM data)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(new Models.Product() {
                    ProductId = 0,
                    ProductName = data.ProductName,
                    Price = data.Price,
                    Active = true,
                    Stock = 1
                });
                db.SaveChanges();
                TempData["ProductInsertedMsg"] = data.ProductName + "新增成功";
                return RedirectToAction("Index");
            }

            return View(data);
        }
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Top10()
        {
            //List<ProductCreationVM> returnList = (from p in db.Product.Take(10)
            //                                      select new ProductCreationVM() {
            //                                          ProductId = p.ProductId,
            //                                          ProductName = p.ProductName,
            //                                          Price = p.Price,
            //                                          OrderLineCount = p.OrderLine.Count
            //                                      }).ToList();
            var returnList = db.Database.SqlQuery<ProductCreationVM>("SELECT TOP 10 *, ORDERLINECOUNT = (SELECT COUNT(*) FROM DBO.ORDERLINE O WHERE O.PRODUCTID=P.PRODUCTID) FROM DBO.PRODUCT P").ToList();
            return View(returnList);
        }

        public ActionResult PriceUp()
        {
            bool test = false;
            //foreach (var item in db.Product)
            //{
            //    foreach (var item2 in db.Product)
            //    {
            //        if (item2.Price > 1) test = true;
            //    }
            //    item.Price += 1;
            //}
            db.Database.ExecuteSqlCommand("UPDATE PRODUCT SET PRICE = PRICE + 1");
            if(test) db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
