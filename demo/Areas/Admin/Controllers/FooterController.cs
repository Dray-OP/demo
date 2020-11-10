using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Areas.Admin.Controllers
{
    public class FooterController : Controller
    {
        // GET: Admin/Footer
        public ActionResult Index()
        {
            var dao = new FooterDao();
            var model = dao.ListAll();
            return View(model);
        }
    }
}