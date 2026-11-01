using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using tp_winform_equipo_y_recuperatorio.Dominio;

namespace tp_winform_equipo_y_recuperatorio.DAO
{
    public class ArticuloDAO
    {
        private SqlConnection CrearConexion()
        {
            var configuracion = ConfigurationManager.ConnectionStrings["CatalogoDB"];
            if (configuracion == null || string.IsNullOrWhiteSpace(configuracion.ConnectionString))
                throw new InvalidOperationException("Falta configurar la conexión CatalogoDB en App.config.");
            return new SqlConnection(configuracion.ConnectionString);
        }

        public DataTable Listar()
        {
            return Consultar(@"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca,
A.IdCategoria, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Precio, I.ImagenUrl
FROM dbo.ARTICULOS A LEFT JOIN dbo.MARCAS M ON A.IdMarca=M.Id
LEFT JOIN dbo.CATEGORIAS C ON A.IdCategoria=C.Id
OUTER APPLY (SELECT TOP 1 ImagenUrl FROM dbo.IMAGENES WHERE IdArticulo=A.Id ORDER BY Id) I
ORDER BY A.Id;");
        }

        public DataTable ListarMarcas() { return Consultar("SELECT Id, Descripcion FROM dbo.MARCAS ORDER BY Descripcion"); }
        public DataTable ListarCategorias() { return Consultar("SELECT Id, Descripcion FROM dbo.CATEGORIAS ORDER BY Descripcion"); }

        public void GuardarMarca(string descripcion) { GuardarDescripcion("MARCAS", descripcion); }
        public void GuardarCategoria(string descripcion) { GuardarDescripcion("CATEGORIAS", descripcion); }

        private void GuardarDescripcion(string tabla, string descripcion)
        {
            descripcion = (descripcion ?? "").Trim();
            if (descripcion.Length == 0 || descripcion.Length > 50)
                throw new InvalidOperationException("Ingresá una descripción de hasta 50 caracteres.");
            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT COUNT(*) FROM dbo." + tabla + " WHERE UPPER(LTRIM(RTRIM(Descripcion)))=UPPER(@Descripcion)";
                comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 50).Value = descripcion;
                conexion.Open();
                if ((int)comando.ExecuteScalar() > 0)
                    throw new InvalidOperationException("Ya existe esa opción.");
                comando.CommandText = "INSERT INTO dbo." + tabla + " (Descripcion) VALUES (@Descripcion)";
                comando.ExecuteNonQuery();
            }
        }

        private DataTable Consultar(string sql)
        {
            var tabla = new DataTable();
            using (var conexion = CrearConexion())
            using (var adaptador = new SqlDataAdapter(sql, conexion))
                adaptador.Fill(tabla);
            return tabla;
        }

        public void Guardar(Articulo articulo, bool actualizarImagen, string imagenUrl)
        {
            using (var conexion = CrearConexion())
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                using (var comando = conexion.CreateCommand())
                {
                    comando.Transaction = transaccion;
                    comando.CommandText = "SELECT COUNT(*) FROM dbo.ARTICULOS WHERE Codigo=@Codigo AND Id<>@Id";
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = articulo.Id;
                    comando.Parameters.Add("@Codigo", SqlDbType.VarChar, 50).Value = articulo.Codigo;
                    if ((int)comando.ExecuteScalar() > 0)
                        throw new InvalidOperationException("Ya existe un artículo con ese código.");
                    comando.CommandText = articulo.Id == 0
                        ? "INSERT INTO dbo.ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,Precio) VALUES (@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@Precio); SELECT CAST(SCOPE_IDENTITY() AS int);"
                        : "UPDATE dbo.ARTICULOS SET Codigo=@Codigo,Nombre=@Nombre,Descripcion=@Descripcion,IdMarca=@IdMarca,IdCategoria=@IdCategoria,Precio=@Precio WHERE Id=@Id";
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = articulo.Nombre;
                    comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 150).Value = articulo.Descripcion ?? "";
                    comando.Parameters.Add("@IdMarca", SqlDbType.Int).Value = articulo.Marca.Id;
                    comando.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = articulo.Categoria.Id;
                    comando.Parameters.Add("@Precio", SqlDbType.Money).Value = articulo.Precio;
                    if (articulo.Id == 0)
                        articulo.Id = Convert.ToInt32(comando.ExecuteScalar());
                    else if (comando.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("El artículo ya no existe. Actualizá el catálogo.");

                    if (actualizarImagen)
                    {
                        comando.Parameters.Clear();
                        comando.Parameters.Add("@Id", SqlDbType.Int).Value = articulo.Id;
                        comando.CommandText = "DELETE FROM dbo.IMAGENES WHERE IdArticulo=@Id";
                        comando.ExecuteNonQuery();
                        if (!string.IsNullOrWhiteSpace(imagenUrl))
                        {
                            if (imagenUrl.Length > 1000)
                                throw new InvalidOperationException("La ruta de la imagen es demasiado larga.");
                            comando.Parameters.Add("@ImagenUrl", SqlDbType.VarChar, 1000).Value = imagenUrl;
                            comando.CommandText = "INSERT INTO dbo.IMAGENES (IdArticulo,ImagenUrl) VALUES (@Id,@ImagenUrl)";
                            comando.ExecuteNonQuery();
                        }
                    }
                    transaccion.Commit();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var conexion = CrearConexion())
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction())
                using (var comando = conexion.CreateCommand())
                {
                    comando.Transaction = transaccion;
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    comando.CommandText = "DELETE FROM dbo.IMAGENES WHERE IdArticulo=@Id";
                    comando.ExecuteNonQuery();
                    comando.CommandText = "DELETE FROM dbo.ARTICULOS WHERE Id=@Id";
                    if (comando.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("El artículo ya no existe. Actualizá el catálogo.");
                    transaccion.Commit();
                }
            }
        }
    }
}
