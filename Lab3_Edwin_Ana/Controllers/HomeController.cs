using Lab3_Edwin_Ana.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab3_Edwin_Ana.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string ingreso)
        {
            int nuevo = int.Parse(ingreso);
          //  GuardarPartidos.Instance.arbol.Insertar(nuevo);
            GuardarPartidos.Instance.arbol.Balancear(); 
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult About(string ingreso)
        {
            int nuevo = int.Parse(ingreso);
           // GuardarPartidos.Instance.arbol.eliminar(nuevo);
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}