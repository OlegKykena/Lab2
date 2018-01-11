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
using System.IO;



namespace UserRolesTest.Controllers
{
    public class BookController : Controller
    {
        private Context db = new Context();

        // GET: Books
        public async Task<ActionResult> Index()
        {
            return View(await db.Books.ToListAsync());
        }

        public ActionResult Api()
        {
            return View();
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            //ViewBag.Sages = new SelectList(db.Sages, "SageId", "Name");
            //ViewBag.Clients = new SelectList(db.Clients, "ClientId", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookId,Name,Pages,Photo,SageId,ClientId")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        book.Photo = ms.GetBuffer();
                    }
                }
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookId,Name,Pages,Photo,SageId,ClientId")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        book.Photo = ms.GetBuffer();
                    }
                }

                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> AddSage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.BookId = id;
            Book bk = await db.Books.FindAsync(id);
            ViewBag.BookName = bk.Name;
            return View(await db.Sages.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> AddSageParam(int BookId , int SageId)
        {
            Book b = await db.Books.FindAsync(BookId);
            Sage s = await db.Sages.FindAsync(SageId);
            b.Sages.Add(s);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.BookId = id;
            Book bk = await db.Books.FindAsync(id);
            ViewBag.BookName = bk.Name;

            return View(bk.Sages);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSageParam(int BookId, int SageId)
        {
            Book b = await db.Books.FindAsync(BookId);
            Sage s = await db.Sages.FindAsync(SageId);
            b.Sages.Remove(s);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //Add to cart and manage session   

        [HttpGet]
        public ActionResult Cart()
        {
            //Client cl = new Client();
            List<Book> bks = new List<Book>();
            if (Session["sessionString"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                bks = Session["sessionString"] as List<Book>;
            }

            return View(bks);
        }

        [HttpGet]
        public async Task<ActionResult> AddToCart(int? id)
        {
            //Client cl = new Client();
            List<Book> bks = new List<Book>();
            if (id == null)
            {
                if (Session["sessionString"] == null)
                {
                    return RedirectToAction("Index");
                }                

                return RedirectToAction("Cart");
            }

            Book book = await db.Books.FindAsync(id);
            db.Set<Book>().Attach(book);
            if (book == null)
            {
                return HttpNotFound();
            }
            
            if (Session["sessionString"] == null)
            {                
                //cl.Books.Add(book);
                bks.Add(book);
                Session["sessionString"] = bks;
            }
            else
            {
                bks = Session["sessionString"] as List<Book>;
                //cl.Books.Add(book);
                bks.Add(book);
                Session["sessionString"] = bks;
            }

            return RedirectToAction("Cart");
        }

                
        [HttpGet]
        public async Task<ActionResult> RemoveCartItem(int id)
        {  
            if (Session["sessionString"] == null)
            {
                return RedirectToAction("Index");
            }
                     

            List<Book> bks = Session["sessionString"] as List<Book>;
            Book book = await db.Books.FindAsync(id);          

            //db.Set<Book>().Load;            

            bks.Remove(bks.Where(bk => bk.BookId == book.BookId).FirstOrDefault());
                         
            Session["sessionString"] = bks;
            
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public ActionResult CheckOut()
        {                 
            Client cl = new Client();
            return View(cl);
        }

        [HttpPost]
        public async Task<ActionResult> CheckOut(Client cl)
        {
            List<Book> tmp = Session["sessionString"] as List<Book>;            
            foreach (var item in tmp)
            {
               db.Set<Book>().Attach(item);  
               cl.Books.Add(item);
            }         
            db.Clients.Add(cl);
            await db.SaveChangesAsync();
            Session["sessionString"] = null;
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
