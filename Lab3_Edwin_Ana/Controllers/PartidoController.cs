using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab3_Edwin_Ana.Models;
using System.IO;
using retornoArch = System.IO;
using Newtonsoft.Json;

namespace Lab3_Edwin_Ana.Controllers
{
    public class PartidoController : Controller
    {
        public List<Partido> listadoPartidos = new List<Partido>();

        public ActionResult Listado()
        {
            return View(listadoPartidos);
        }
        
        public ActionResult IngresoJson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IngresoJson(HttpPostedFileBase archivo)
        {
            if (archivo!=null)
            {
                var path = retornoArch.File.ReadAllText(archivo.FileName);
                var deserealizar = JsonConvert.DeserializeObject<Partido2>(path);
            }
            List<Partido> miLista = new List<Partido>();
            miLista.Clear();
            miLista = Data.GuardarPartidos.Instance.arbol.retornarLista();
            return View();
        }

        public ActionResult IngresoManual()
        {
            return View();
        }

        public ActionResult Eliminar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Eliminar(string eliminar)
        {
            return View();
        }
    }
}