using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace tp_winform_equipo_y_recuperatorio.DAO
{
    public class ArticuloDAO
    {
        public DataTable Listar()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CatalogoDB"].ConnectionString;

            string consulta = @"
SELECT
    A.Codigo AS Codigo,
    A.Nombre AS Nombre,
    A.Descripcion AS Descripcion,
    M.Descripcion AS Marca,
    C.Descripcion AS Categoria,
    A.Precio AS Precio
FROM dbo.ARTICULOS AS A
LEFT JOIN dbo.MARCAS AS M ON A.IdMarca = M.Id
LEFT JOIN dbo.CATEGORIAS AS C ON A.IdCategoria = C.Id
ORDER BY A.Id;";

            DataTable tabla = new DataTable();

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
            {
                adaptador.Fill(tabla);
            }

            return tabla;
        }
    }
}
