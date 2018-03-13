using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaArbol
{
    public class Nodo<T>
    {
        public Nodo<T> derecho { get; set; }
        public Nodo<T> izquierdo { get; set; }
        public T data { get; set; }
    }
}
