using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkWithEntity;

namespace UserRolesTest.Models
{
    public class UltimateModel
    {
        public Book Book { get; set; }
        public ICollection<Sage> Sages { get; set; }
    }
}