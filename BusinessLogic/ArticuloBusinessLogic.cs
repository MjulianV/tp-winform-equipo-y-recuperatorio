using System;
using System.Data;
using tp_winform_equipo_y_recuperatorio.DAO;
using tp_winform_equipo_y_recuperatorio.Dominio;

namespace tp_winform_equipo_y_recuperatorio.BusinessLogic
{
    public class ArticuloBusinessLogic
    {
        private readonly ArticuloDAO accesoDatos = new ArticuloDAO();
        public DataTable ListarDesdeBaseDeDatos() { return accesoDatos.Listar(); }
        public DataTable ListarMarcas() { return accesoDatos.ListarMarcas(); }
        public DataTable ListarCategorias() { return accesoDatos.ListarCategorias(); }
        public void GuardarMarca(string descripcion) { accesoDatos.GuardarMarca(descripcion); }
        public void GuardarCategoria(string descripcion) { accesoDatos.GuardarCategoria(descripcion); }
        public void Guardar(Articulo articulo, bool actualizarImagen, string imagenUrl)
        {
            if (articulo == null) throw new ArgumentNullException(nameof(articulo));
            articulo.Codigo = (articulo.Codigo ?? "").Trim();
            articulo.Nombre = (articulo.Nombre ?? "").Trim();
            articulo.Descripcion = (articulo.Descripcion ?? "").Trim();
            if (articulo.Codigo.Length == 0 || articulo.Codigo.Length > 50)
                throw new ArgumentException("Ingresá un código de hasta 50 caracteres.");
            if (articulo.Nombre.Length == 0 || articulo.Nombre.Length > 50)
                throw new ArgumentException("Ingresá un nombre de hasta 50 caracteres.");
            if (articulo.Descripcion.Length > 150) throw new ArgumentException("La descripción admite hasta 150 caracteres.");
            if (articulo.Marca == null || articulo.Categoria == null)
                throw new ArgumentException("Seleccioná una marca y una categoría.");
            if (articulo.Precio <= 0 || articulo.Precio > 922337203685477.5807m)
                throw new ArgumentException("Ingresá un precio mayor que cero y dentro del rango permitido.");
            if (actualizarImagen && imagenUrl != null && imagenUrl.Length > 1000)
                throw new ArgumentException("La ruta de la imagen es demasiado larga.");
            accesoDatos.Guardar(articulo, actualizarImagen, imagenUrl);
        }
        public void Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("Seleccioná un artículo válido.");
            accesoDatos.Eliminar(id);
        }
    }
}
