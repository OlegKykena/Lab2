using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WorkWithEntity
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(Context db) : base(db)
        {

        }
    }
}