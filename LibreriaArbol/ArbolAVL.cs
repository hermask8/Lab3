using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaArbol
{
    public class ArbolAVL<T> where T:IComparable<T>
    {
        Nodo<T> raiz;
        List<T> listaRetorno = new List<T>();
        private void PreOrden(Nodo<T> nAux)
        {
            if (nAux != null)
            {
                listaRetorno.Add(nAux.data);
                PreOrden(nAux.izquierdo);
                PreOrden(nAux.derecho);
            }
        }
        public void PreOrden()
        {
            listaRetorno.Clear();
            PreOrden(raiz);
        }
        public List<T> retornarLista()
        {
            PreOrden();
            return listaRetorno;
        }
        public void Insertar(T dtInfo)
        {
            Nodo<T> ntemp = new Nodo<T>();
            //ntemp = null;
            ntemp.data = dtInfo;
            ntemp.derecho = null;
            ntemp.izquierdo = null;

            if (raiz == null)
            {
                raiz = ntemp;
            }
            else
            {
                Nodo<T> nAux = raiz;
                Nodo<T> nPafre = raiz;
                bool bDerecha = false;
                while (nAux != null)
                {

                    nPafre = nAux;
                    if (dtInfo.CompareTo(nAux.data) == 1)
                    {
                        nAux = nAux.derecho;
                        bDerecha = true;
                    }
                    else
                    {
                        nAux = nAux.izquierdo;
                        bDerecha = false;
                    }
                }
                if (bDerecha == true)
                {
                    nPafre.derecho = ntemp;
                }
                else
                {
                    nPafre.izquierdo = ntemp;
                }

            }
        }

        public void Balancear()
        {
            raiz = BalancearArbol(raiz);
        }
        private Nodo<T> BalancearArbol(Nodo<T> IngresoRaiz)
        {
            int b_factor = VerificarBalance(IngresoRaiz);
            if (b_factor > 1)
            {
                if (VerificarBalance(IngresoRaiz.izquierdo) > 0)
                {
                    IngresoRaiz = RotarLL(IngresoRaiz);
                }
                else
                {
                    IngresoRaiz = RotarLR(IngresoRaiz);
                }
            }
            else if (b_factor < -1)
            {
                if (VerificarBalance(IngresoRaiz.derecho) > 0)
                {
                    IngresoRaiz = RotarRL(IngresoRaiz);
                }
                else
                {
                    IngresoRaiz = RotarRR(IngresoRaiz);
                }
            }
            return IngresoRaiz;
        }

        private int RetornarAltura(Nodo<T> IngresoRaiz)
        {
            int altura = 0;
            if (IngresoRaiz != null)
            {
                int l = RetornarAltura(IngresoRaiz.izquierdo);
                int r = RetornarAltura(IngresoRaiz.derecho);
                int m = max(l, r);
                altura = m + 1;
            }
            return altura;
        }
        private int VerificarBalance(Nodo<T> current)
        {
            if (current!=null)
            {
                int alturaIzquieda = RetornarAltura(current.izquierdo);
                int alturaDerecha = RetornarAltura(current.derecho);
                int b_factor = alturaIzquieda - alturaDerecha;
                return b_factor;
            }
            else
            {
                return 0;
            }
           
        }

        private int max(int l, int r)
        {
            return l > r ? l : r;
        }

        private Nodo<T> RotarRR(Nodo<T> rotar)
        {
            Nodo<T> pivote = rotar.derecho;
            rotar.derecho = pivote.izquierdo;
            pivote.izquierdo = rotar;
            return pivote;
        }
        private Nodo<T> RotarLL(Nodo<T> rotar)
        {
            Nodo<T> pivote = rotar.izquierdo;
            rotar.izquierdo = rotar.derecho;
            pivote.derecho = rotar;
            return pivote;
        }
        private Nodo<T> RotarLR(Nodo<T> rotar)
        {
            Nodo<T> pivote = rotar.izquierdo;
            rotar.izquierdo = RotarRR(pivote);
            return RotarLL(rotar);
        }
        private Nodo<T> RotarRL(Nodo<T> rotar)
        {
            Nodo<T> pivote = rotar.derecho;
            rotar.derecho = RotarLL(pivote);
            return RotarRR(rotar);
        }
        
        public Nodo<T> EliminarConNodo(T dato, Nodo<T> pivote) //Eliminar
        {
            if (pivote == null)
            {
                return null;
            }
            if (dato.CompareTo(pivote.data) < 0)
            {
                pivote.izquierdo = EliminarConNodo(dato, pivote.izquierdo);
            }
            else if (dato.CompareTo(pivote.data) > 0)
            {
                pivote.derecho = EliminarConNodo(dato, pivote.derecho);
            }

            else
            {
                if (pivote.derecho == null && pivote.izquierdo == null)
                {
                    pivote = null;
                    //BalancearArbol(pivot);
                    return pivote;
                }
                else if (pivote.derecho == null)
                {
                    Nodo<T> aux = pivote;
                    pivote = pivote.izquierdo;
                    aux = null;
                   // BalancearArbol(pivot);
                }
                else if (pivote.derecho == null)
                {
                    Nodo<T> aux = pivote;
                    pivote = pivote.derecho;
                    aux = null;
                    //BalancearArbol(pivot);
                }
                else
                {
                    Nodo<T> aux = Maximo(pivote.izquierdo);
                    pivote.data = aux.data;
                    pivote.izquierdo = EliminarConNodo(dato, pivote.izquierdo);
                    //BalancearArbol(pivot);
                }
               
            }
            
            return pivote;
        }

 
        public Nodo<T> FindMin(Nodo<T> nodo)
        {
            if (nodo==null)
            {
                return null;
            }
            else
            {
                while (nodo.izquierdo != null)
                {
                    nodo = nodo.izquierdo;
                }
                return nodo;
            }
        }
        public void eliminar(T value)
        {
            raiz = EliminarConNodo(value, raiz);
            BalancearArbol(raiz);
        }

        public Nodo<T> Maximo(Nodo<T> n)
        {
            if (n == null)
            {
                return null;
            }
            else
            {
                while (n.derecho != null)
                {
                    n = n.derecho;
                }
                return n;
            }
        }

        Nodo<T> padre = new Nodo<T>();
        private void Eliminar2 (T dato,Nodo<T> recorrer)
        { 
            if (recorrer!=null)
            {
                if (recorrer.data.CompareTo(dato)==0)
                {
                    if (recorrer.derecho==null & recorrer.izquierdo==null)
                    {
                        recorrer = null;
                    }
                    else if (recorrer.izquierdo==null || recorrer.derecho==null)
                    {
                        if (recorrer.izquierdo==null)
                        {

                        }
                        else
                        {
                            
                        }
                    }
                    else
                    {

                    }
                }
                padre = recorrer;
                Eliminar(dato,recorrer.izquierdo);
                Eliminar(dato, recorrer.derecho);
            }
          
        }
    }
}
