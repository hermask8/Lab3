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
        Nodo<T> buscado = new Nodo<T>();
       
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
            
             if (dato.CompareTo(pivote.data)==0)
            {
                if (pivote == raiz)
                {
                    EliminarRaiz(dato, pivote);
                }
                else if (pivote.derecho == null && pivote.izquierdo == null)
                {
                    pivote = null;
                    return pivote;
                }
                else if (pivote.derecho == null)
                {
                    Nodo<T> aux = pivote;
                    pivote = pivote.izquierdo;
                    aux = null;
                }
                else if (pivote.derecho == null)
                {
                    Nodo<T> aux = pivote;
                    pivote = pivote.derecho;
                    aux = null;
                }
                else
                {
                    Nodo<T> aux = Maximo(pivote.izquierdo);
                    pivote.data = aux.data;
                    pivote.derecho = EliminarConNodo(dato, pivote.izquierdo);
                }

            }
            else if (dato.CompareTo(pivote.data) == -1)
            {
                pivote.izquierdo = EliminarConNodo(dato, pivote.izquierdo);
            }
            else if (dato.CompareTo(pivote.data) == 1)
            {
                pivote.derecho = EliminarConNodo(dato, pivote.derecho);
            }

          
            
            return pivote;
        }

        private void EliminarRaiz(T dato, Nodo<T> aux)
        {
            if (aux != null)
            {
                if (dato.CompareTo(aux.data) == -1)
                {
                    EliminarRaiz(dato, aux.izquierdo);
                }
                else
                {
                    if (dato.CompareTo(aux.data) == 1)
                    {
                        EliminarRaiz(dato, aux.derecho);
                    }
                    else
                    {
                        Nodo<T> toDeleteNode = aux;
                        if (toDeleteNode.derecho == null)
                        {
                            aux = toDeleteNode.izquierdo;
                        }
                        else
                        {
                            if (toDeleteNode.izquierdo == null)
                            {
                                aux = toDeleteNode.derecho;
                            }
                            else
                            {
                                Nodo<T> pivote = null;
                                Nodo<T> aux2 = aux.izquierdo;
                                bool mark = false;
                                while (aux2.derecho != null)
                                {
                                    pivote = aux2;
                                    aux2 = aux2.derecho;
                                    mark = true;
                                }
                                aux.data = aux2.data;
                                toDeleteNode = aux;
                                if (mark == true)
                                {
                                    pivote.derecho = aux2.izquierdo;
                                }
                                else
                                {
                                    aux.izquierdo = aux.izquierdo;
                                }
                            }
                        }
                    }
                }
            }
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
           var nuevoRaiz= BalancearArbol(raiz);
            raiz = nuevoRaiz;
        }
        public void busqueda(T dato, Nodo<T> nuevo)
        {
            if (nuevo != null && buscado == null)
            {
                if (dato.CompareTo(nuevo.data) == 0)
                {
                    buscado = nuevo;
                    return;
                }
                if (nuevo.izquierdo != null)
                {
                    busqueda(dato, nuevo.izquierdo);
                }
                if (nuevo.derecho != null)
                {
                    busqueda(dato, nuevo.derecho);
                }
            }
        }
        public T buscar(T value)
        {
            if (value != null)
            {

                buscado = null;
                busqueda(value, raiz);
                return buscado.data;
            }
            else
               return value;
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


    }

}
