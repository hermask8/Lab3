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
using System.Net;

namespace Lab3_Edwin_Ana.Controllers
{
    public class PartidoController : Controller
    {

        //StreamWriter escribir = new StreamWriter("Vitacora.txt");
        List<Partido> miLista = new List<Partido>();
        List<Partido> miLista2 = new List<Partido>();
        List<Partido> search = new List<Partido>();
       static List<string> ingresoVitacora = new List<string>();
        public ActionResult Listado()
        {
            miLista.Clear();
            miLista2.Clear();
            miLista = Data.GuardarPartidos.Instance.arbol.retornarLista();
            Partido mi = new Partido();
            var repetido = mi;
            foreach (var item in miLista)
            {
                if (item != mi)
                {
                    miLista2.Add(item);
                }
                repetido = item;
            }
            return View(miLista2);
        }
        public ActionResult ListadoBuscar()
        {
            return View();
        }
        public ActionResult IngresoJson()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult IngresoJson(HttpPostedFileBase archivo)
        {
            try
            {
                // TODO: Add insert logic here
                if (Path.GetFileName(archivo.FileName).EndsWith(".json"))
                {
                    archivo.SaveAs(Server.MapPath("~/JSONFiles" + Path.GetFileName(archivo.FileName)));
                    StreamReader sr = new StreamReader(Server.MapPath("~/JSONFiles" + Path.GetFileName(archivo.FileName)));
                    string data = sr.ReadToEnd();
                    List<Partido> partidos = new List<Partido>();
                    string[] g;
                    char[] separators = { '{', '}' };
                    g = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < g.Length; i++)
                    {
                        string a = "{" + g[i] + "}";
                        partidos.Add(JsonConvert.DeserializeObject<Partido>(a));
                        i++;
                    }

                    foreach (var item in partidos)
                    {
                        var escrito = ("INSERTAR: " + "Estadio: " + item.Estadio + ", " + "Numero de partido:" + item.noPartido + ", " + "Fecha del partido: " + item.FechaPartido + ", " + "Grupo: " + item.Grupo + ", " + "Pais1" + item.Pais1 + ", " + "Pais 2" + item.Pais2);
                        ingresoVitacora.Add(escrito);
                        Data.GuardarPartidos.Instance.arbol.Insertar(item);
                    }
                    //escribir.Close();
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                miLista.Clear();
                miLista = Data.GuardarPartidos.Instance.arbol.retornarLista();
                Partido mi = new Partido();
                var repetido = mi;
                foreach (var item in miLista)
                {
                    if (item != mi)
                    {
                        miLista2.Add(item);
                    }
                    repetido = item;
                }
                return View("Listado",miLista);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        

        public ActionResult Eliminar()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Eliminar(string Ingreso)
        {
            int nuevo = int.Parse(Ingreso);
            Partido miPartido = new Partido();
            miPartido.noPartido = nuevo;
            //  miPartido.FechaPartido = eliminar["FechaPartido"];
            var eliminarvitacora = Data.GuardarPartidos.Instance.arbol.buscar(miPartido);
            var escrito = ("Eliminar: " + "Estadio: " + eliminarvitacora.Estadio + ", " + "Numero de partido:" + eliminarvitacora.noPartido + ", " + "Fecha del partido: " + eliminarvitacora.FechaPartido + ", " + "Grupo: " + eliminarvitacora.Grupo + ", " + "Pais1: " + eliminarvitacora.Pais1 + ", " + "Pais 2: " + eliminarvitacora.Pais2);
            ingresoVitacora.Add(escrito);
            Data.GuardarPartidos.Instance.arbol.eliminar(miPartido);
            miLista.Clear();
            miLista = Data.GuardarPartidos.Instance.arbol.retornarLista();
            Partido mi = new Partido();
            var repetido = mi;
            foreach (var item in miLista)
            {
                if (item!=mi)
                {
                    miLista2.Add(item);
                }
                repetido = item;
            }
            return View("Listado",miLista2);
        }
        public ActionResult Busqueda()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Busqueda(string nuevo)
        {
            int encontrado;
            int dato = int.Parse(nuevo);
            Partido miPartido = new Partido();
            miPartido.noPartido = dato;
            
           var resultado = Data.GuardarPartidos.Instance.arbol.buscar(miPartido);
            miLista.Clear();
            miLista.Add(resultado);
            return View("ListadoBuscar",miLista);
        }

        public ActionResult IngresoManual()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IngresoManual(FormCollection nuevo)
        {
            Partido partido = new Partido
            {
                Estadio = nuevo["Estadio"],
                noPartido =int.Parse(nuevo["noPartido"]),
                FechaPartido = nuevo["FechaPartido"],
                Grupo = nuevo["Grupo"],
                Pais1 = nuevo["Pais1"],
                Pais2 = nuevo["Pais2"]
            };
            var escrito =("INGRESO: " + "Estadio: "+ nuevo["Estadio"] +", "+ "Numero de partido:" + nuevo["noPartido"]+ ", " + "Fecha del partido: " + nuevo["FechaPartido"] + ", " + "Grupo: " + nuevo["Grupo"]+ ", " + "Pais1: " + nuevo["Pais1"] + ", " + "Pais 2: " + nuevo["Pais2"]);
            ingresoVitacora.Add(escrito);
            Data.GuardarPartidos.Instance.arbol.Insertar(partido);
            //escribir.Close();
            return View();
        }

        public ActionResult Vitacora()
        {
            //hacertxt miArch = new hacertxt(ingresoVitacora);
           
            return View(ingresoVitacora);
        }
    }
}