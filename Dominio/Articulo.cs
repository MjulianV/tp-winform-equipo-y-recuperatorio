using System.Collections.Generic;

namespace tp_winform_equipo_y_recuperatorio.Dominio
{
    public class Articulo
    {
        public Articulo()
        {
            Imagenes = new List<ImagenArticulo>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Precio { get; set; }
        public List<ImagenArticulo> Imagenes { get; set; }
    }
}
