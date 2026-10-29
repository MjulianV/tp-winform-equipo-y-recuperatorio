using System.Collections.Generic;
using tp_winform_equipo_y_recuperatorio.Dominio;

namespace tp_winform_equipo_y_recuperatorio.BusinessLogic
{
    public class ArticuloBusinessLogic
    {
        private readonly List<Articulo> articulos;
        private int proximoId;

        public ArticuloBusinessLogic()
        {
            articulos = new List<Articulo>();
            proximoId = 1;
        }

        public List<Articulo> Listar()
        {
            return articulos;
        }

        public void Agregar(Articulo articulo)
        {
            articulo.Id = proximoId;
            articulos.Add(articulo);
            proximoId++;
        }

        public void Modificar(Articulo articulo)
        {
            for (int i = 0; i < articulos.Count; i++)
            {
                if (articulos[i].Id == articulo.Id)
                {
                    articulos[i] = articulo;
                    return;
                }
            }
        }

        public void Eliminar(int id)
        {
            for (int i = 0; i < articulos.Count; i++)
            {
                if (articulos[i].Id == id)
                {
                    articulos.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
