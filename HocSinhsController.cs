using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using NguyenDucBinh_2021603555.Models;

namespace NguyenDucBinh_2021603555.Controllers
{
    public class HocSinhsController : Controller
    {
        private Model1 db = new Model1();

        // GET: HocSinhs
        public ActionResult Xemdanhsach(string tim, string sbd)
        {
            var hocSinhs = db.HocSinhs.Select(h => h);
            if (!string.IsNullOrEmpty(tim)){
                hocSinhs = hocSinhs.Where(p => p.hoten.Contains(tim));
            }
            if (!string.IsNullOrEmpty(sbd))
            {
                hocSinhs = hocSinhs.Where(p => p.sbd.Contains(sbd));
            }
            return View(hocSinhs.ToList());
        }

        // GET: HocSinhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // GET: HocSinhs/Create
        public ActionResult ThemDuLieu()
        {
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop");
            return View();
        }

        // POST: HocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDuLieu([Bind(Include = "sbd,hoten,anhduthi,malop,diemthi")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["FileName"];
                if(f.ContentLength > 0)
                {
                    string tenfile=Path.GetFileName(f.FileName);
                    string duongdan = Path.Combine(Server.MapPath("~/Images/"), tenfile);
                    f.SaveAs(duongdan);
                    hocSinh.anhduthi = tenfile;
                }
                db.HocSinhs.Add(hocSinh);
                db.SaveChanges();
                return RedirectToAction("Xemdanhsach");
            }

            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // GET: HocSinhs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // POST: HocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sbd,hoten,anhduthi,malop,diemthi")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["FileName"];
                if (f.ContentLength > 0)
                {
                    string tenfile = Path.GetFileName(f.FileName);
                    string duongdan = Path.Combine(Server.MapPath("~/Images/"), tenfile);
                    f.SaveAs(duongdan);
                    hocSinh.anhduthi = tenfile;
                }
                db.Entry(hocSinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Xemdanhsach");
            }
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // GET: HocSinhs/Delete/5
        public ActionResult XoaDuLieu(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // POST: HocSinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaDuLieuConfirmed(string id)
        {
            HocSinh hocSinh = db.HocSinhs.Find(id);
            try
            {
                db.HocSinhs.Remove(hocSinh);
                db.SaveChanges();
            }
            catch (Exception ex) {
                ViewBag.err="Co loi xay ra" + ex.Message;
                return View(hocSinh);
            }
            return RedirectToAction("Xemdanhsach");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
