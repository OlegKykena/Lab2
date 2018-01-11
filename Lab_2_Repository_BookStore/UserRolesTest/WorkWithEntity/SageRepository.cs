using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace WorkWithEntity
{
    public class SageRepository : BaseRepository<Sage>
    {
        public SageRepository(Context db) : base(db)
        {

        }
    }
}
