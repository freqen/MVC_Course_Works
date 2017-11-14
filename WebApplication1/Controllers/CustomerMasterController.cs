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
    public class CustomerMasterController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: CustomerMaster
        public ActionResult Index(CustomerQuery query)
        {
            var customersList = db.客戶資料.Where(x => (x.客戶名稱.Contains(query.客戶名稱) || query.客戶名稱.Equals(null))
                                                    && (x.統一編號.Contains(query.統一編號) || query.統一編號.Equals(null))
                                                    && (x.電話.Contains(query.電話) || query.電話.Equals(null))
                                                    && (x.傳真.Contains(query.傳真) || query.傳真.Equals(null))
                                                    && (x.地址.Contains(query.地址) || query.地址.Equals(null))
                                                    && (x.Email.Contains(query.Email) || query.Email.Equals(null))
                                                    && (x.客戶分類.Contains(query.客戶分類) || query.客戶分類.Equals(null))
                                                   ).ToList();
            ViewBag.客戶分類選項 = db.客戶資料.Select(x => x.客戶分類).Distinct().ToList();
            return View(customersList);
        }

        // GET: CustomerMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: CustomerMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerMaster/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: CustomerMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: CustomerMaster/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: CustomerMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: CustomerMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            db.客戶資料.Remove(客戶資料);
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

        public ActionResult CustomerReport()
        {
            List<CustomerVM> CustomerVN = (from c in db.客戶資料
                                           select new CustomerVM()
                                           {
                                               客戶名稱 = c.客戶名稱,
                                               聯絡人數量 = c.客戶聯絡人.Count,
                                               銀行帳戶數量 = c.客戶銀行資訊.Count
                                           }).ToList();
            //ViewBag.ReturnList = CustomerVN;
            return View(CustomerVN);
        }
    }
}
