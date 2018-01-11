using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WorkWithEntity;

namespace UserRolesTest.Controllers
{
    [AllowAnonymous]
    public class TestController : ApiController
    {
        private Context db = new Context();
        // GET api/<controller>
        public IEnumerable<Book> Get()
        {
            return db.Books;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}