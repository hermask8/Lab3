using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab3_Edwin_Ana.Models;
using System.IO;
using retornoArch = System.IO;
using Newtonsoft.Json;
using LibreriaArbol;

namespace Lab3_Edwin_Ana.Controllers
{
    public class PartidoController : Controller
    {

        List<Partido> miLista = new List<Partido>();
        public ActionResult Listado()
        {
            
            
            return View(miLista);
        }

        public ActionResult IngresoJson()
        {
            return View();
        }
        public void PreOrden(Partido2 aux)
        {
            if (aux != null)
            {
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo1);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo2);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo3);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo4);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo5);
                /*Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo6);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo7);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo8);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo9);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo10);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo11);
                Data.GuardarPartidos.Instance.arbol.Insertar(aux.nodo12);*/
                // PreOrden(aux.izquierdo);
                // PreOrden(aux.derecho);
            }
        }
        [HttpPost]
        public ActionResult IngresoJson(HttpPostedFileBase archivo)
        {

            var path = retornoArch.File.ReadAllText(archivo.FileName);
            var deserealizar = JsonConvert.DeserializeObject<Partido2>(path);

            
            //miLista.Clear();

            PreOrden(deserealizar);
            miLista = Data.GuardarPartidos.Instance.arbol.retornarLista();
            return View(miLista);

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
            int nuevo = int.Parse(eliminar);
            Partido miPartido = new Partido();
            miPartido.NoPartido = nuevo;
          //  miPartido.FechaPartido = eliminar["FechaPartido"];
            Data.GuardarPartidos.Instance.arbol.eliminar(miPartido);
            return View();
        }
    }
}