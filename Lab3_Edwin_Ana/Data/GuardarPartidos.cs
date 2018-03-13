using Lab3_Edwin_Ana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3_Edwin_Ana.Data
{
    public class GuardarPartidos
    {
        private static GuardarPartidos instance;
        public static GuardarPartidos Instance
        {
            get
            {
                if (instance == null) instance = new GuardarPartidos();
                return instance;
            }
        }


        public LibreriaArbol.ArbolAVL<Partido> arbol;
        public GuardarPartidos()
        {
            arbol = new LibreriaArbol.ArbolAVL<Partido>();
        }


    }
}