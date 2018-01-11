using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRolesTest.Models;
using Repository;
using WorkWithEntity;

namespace UserRolesTest.Controllers
{
    public class UltimateModelController : Controller
    {
        IRepository<Book> b;
        IRepository<Sage> s;

        // GET: UltimateModel
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            UltimateModel UM = new UltimateModel();
            UM.Book = b.GetById(id);

            return View();
        }
    }
}