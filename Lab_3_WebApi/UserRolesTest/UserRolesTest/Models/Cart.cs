using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkWithEntity;

namespace UserRolesTest.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public UltimateModel BuyList { get; set; }
        public int Count { get; set; }
    }
}