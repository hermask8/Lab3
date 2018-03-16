using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab3_Edwin_Ana.Models;

namespace Lab3_Edwin_Ana.Data
{
    public class GuardarInt
    {
        private static GuardarInt instance;
        public static GuardarInt Instance
        {
            get
            {
                if (instance == null) instance = new GuardarInt();
                return instance;
            }
        }


        public LibreriaArbol.ArbolAVL<int> arbol;
        public GuardarInt()
        {
            arbol = new LibreriaArbol.ArbolAVL<int>();
        }
    }
}