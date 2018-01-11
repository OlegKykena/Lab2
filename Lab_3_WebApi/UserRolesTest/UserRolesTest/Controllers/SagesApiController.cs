using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WorkWithEntity;

namespace UserRolesTest.Controllers
{
    [Authorize]
    public class SagesApiController : ApiController
    {
        private Context db = new Context();

        // GET: api/SagesApi
        public IEnumerable GetSages()
        { 
            return db.Sages.Include("Books").ToList().Select(c => new
            {
                c.SageId,
                c.Name, 
                c.Age,
                Books = c.Books.Select(p => new
                {
                    p.BookId,
                    p.Name

                }).ToArray()
            }); 
        }

        // GET: api/SagesApi/5
        [ResponseType(typeof(Sage))]
        public async Task<IHttpActionResult> GetSage(int id)
        {
            Sage sage = await db.Sages.FindAsync(id);
            if (sage == null)
            {
                return NotFound();
            }

            return Ok(sage);
        }

        // PUT: api/SagesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSage(int id, Sage sage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sage.SageId)
            {
                return BadRequest();
            }

            db.Entry(sage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SagesApi
        [ResponseType(typeof(Sage))]
        public async Task<IHttpActionResult> PostSage(Sage sage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sages.Add(sage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sage.SageId }, sage);
        }

        // DELETE: api/SagesApi/5
        [ResponseType(typeof(Sage))]
        public async Task<IHttpActionResult> DeleteSage(int id)
        {
            Sage sage = await db.Sages.FindAsync(id);
            if (sage == null)
            {
                return NotFound();
            }

            db.Sages.Remove(sage);
            await db.SaveChangesAsync();

            return Ok(sage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SageExists(int id)
        {
            return db.Sages.Count(e => e.SageId == id) > 0;
        }
    }
}