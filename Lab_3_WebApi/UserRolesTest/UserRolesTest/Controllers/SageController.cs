using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkWithEntity;

namespace UserRolesTest.Controllers
{
    public class SageController : Controller
    {
        private Context db = new Context();

        // GET: Sages
        public async Task<ActionResult> Index()
        {
            return View(await db.Sages.ToListAsync());
        }

        public ActionResult Api()
        {
            return View();
        }

        // GET: Sages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SageId,Name,Age,BookId")] Sage sage)
        {
            if (ModelState.IsValid)
            {
                db.Sages.Add(sage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sage);
        }

        // GET: Sages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sage sage = await db.Sages.FindAsync(id);
            if (sage == null)
            {
                return HttpNotFound();
            }
            return View(sage);
        }

        // POST: Sages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SageId,Name,Age,BookId")] Sage sage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sage);
        }

        // GET: Sages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sage sage = await db.Sages.FindAsync(id);
            if (sage == null)
            {
                return HttpNotFound();
            }
            return View(sage);
        }

        // POST: Sages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sage sage = await db.Sages.FindAsync(id);
            db.Sages.Remove(sage);
            await db.SaveChangesAsync();
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
    }
}
