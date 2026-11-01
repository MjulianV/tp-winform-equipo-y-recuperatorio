using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using tp_winform_equipo_y_recuperatorio.BusinessLogic;

namespace tp_winform_equipo_y_recuperatorio
{
    public partial class CatalogoForm : Form
    {
        private readonly ArticuloBusinessLogic logica = new ArticuloBusinessLogic();
        private DataTable articulos;
        private DataTable marcas;
        private DataTable categorias;
        private bool ocupado;

        public CatalogoForm()
        {
            InitializeComponent();
            Shown += async (s, e) => await CargarAsync();
            actualizar.Click += async (s, e) => await CargarAsync();
            nuevo.Click += async (s, e) => await AbrirArticuloAsync(false);
            editar.Click += async (s, e) => await AbrirArticuloAsync(true);
            eliminar.Click += async (s, e) => await EliminarAsync();
            administrarMarcas.Click += async (s, e) => await AbrirAdministracionAsync(true);
            administrarCategorias.Click += async (s, e) => await AbrirAdministracionAsync(false);
            limpiar.Click += (s, e) => { busqueda.Clear(); busqueda.Focus(); };
            grilla.CellDoubleClick += async (s, e) => { if (e.RowIndex >= 0) await AbrirArticuloAsync(true); };
            grilla.SelectionChanged += (s, e) =>
            {
                ActualizarBotones();
                MostrarImagenSeleccionada();
            };
            busqueda.TextChanged += (s, e) => Filtrar();
            campo.SelectedIndexChanged += (s, e) => Filtrar();
            FormClosing += (s, e) => { if (ocupado) e.Cancel = true; };
            ActualizarBotones();
        }

        private async Task CargarAsync()
        {
            if (ocupado) return;
            ocupado = true;
            ActualizarBotones();
            estado.Text = "Conectando con la base de datos…";
            try
            {
                var datos = await Task.Run(() => new[] { logica.ListarDesdeBaseDeDatos(), logica.ListarMarcas(), logica.ListarCategorias() });
                articulos = datos[0];
                marcas = datos[1];
                categorias = datos[2];
                Filtrar();
            }
            catch (Exception ex)
            {
                articulos = null;
                marcas = null;
                categorias = null;
                grilla.DataSource = null;
                estado.Text = "Sin conexión. Revisá SQL Server y presioná Actualizar para reintentar.";
                MessageBox.Show(this, "No se pudo cargar el catálogo. Revisá la instancia configurada en App.config y que exista CATALOGO_P3_DB.\n\n" + ex.Message,
                    "Conexión no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                ocupado = false;
                ActualizarBotones();
            }
        }

        private void Filtrar()
        {
            if (articulos == null) return;
            var vista = articulos.Clone();
            string texto = busqueda.Text.Trim();
            string[] columnas = { "Codigo", "Nombre", "Descripcion", "Marca", "Categoria" };
            if (campo.SelectedIndex > 0) columnas = new[] { columnas[campo.SelectedIndex - 1] };
            foreach (DataRow fila in articulos.Rows)
                if (texto.Length == 0 || columnas.Any(c => Convert.ToString(fila[c]).IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0))
                    vista.ImportRow(fila);
            grilla.DataSource = vista;
            foreach (string columna in new[] { "Id", "IdMarca", "IdCategoria", "ImagenUrl" }) grilla.Columns[columna].Visible = false;
            grilla.Columns["Codigo"].HeaderText = "Código";
            grilla.Columns["Descripcion"].HeaderText = "Descripción";
            grilla.Columns["Categoria"].HeaderText = "Categoría";
            grilla.Columns["Precio"].DefaultCellStyle.Format = "N2";
            grilla.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            estado.Text = vista.Rows.Count + " de " + articulos.Rows.Count + " artículos" + (vista.Rows.Count == 0 ? " · Sin resultados" : "");
            ActualizarBotones();
            MostrarImagenSeleccionada();
        }

        private void MostrarImagenSeleccionada()
        {
            vistaImagenCatalogo.ImageLocation = null;
            if (vistaImagenCatalogo.Image != null)
            {
                vistaImagenCatalogo.Image.Dispose();
                vistaImagenCatalogo.Image = null;
            }

            var fila = grilla.CurrentRow == null ? null : grilla.CurrentRow.DataBoundItem as DataRowView;
            if (fila == null) return;

            string imagen = fila["ImagenUrl"] == DBNull.Value ? "" : Convert.ToString(fila["ImagenUrl"]).Trim();
            if (imagen.Length == 0) return;

            Uri uri;
            if (Uri.TryCreate(imagen, UriKind.Absolute, out uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                try
                {
                    vistaImagenCatalogo.ImageLocation = imagen;
                    vistaImagenCatalogo.LoadAsync();
                }
                catch { }
                return;
            }

            string rutaLocal = ResolverRutaImagen(imagen);
            if (File.Exists(rutaLocal))
            {
                try
                {
                    using (var original = Image.FromFile(rutaLocal))
                        vistaImagenCatalogo.Image = new Bitmap(original);
                }
                catch { }
                return;
            }
        }

        private static string ResolverRutaImagen(string ruta)
        {
            if (Path.IsPathRooted(ruta)) return ruta;
            string rutaSalida = Path.Combine(Application.StartupPath, ruta);
            if (File.Exists(rutaSalida)) return rutaSalida;
            return Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..", ruta));
        }

        private void ActualizarBotones()
        {
            bool conectado = articulos != null && !ocupado;
            nuevo.Enabled = conectado && marcas != null && marcas.Rows.Count > 0 && categorias != null && categorias.Rows.Count > 0;
            editar.Enabled = eliminar.Enabled = conectado && grilla.CurrentRow != null;
            administrarMarcas.Enabled = administrarCategorias.Enabled = conectado;
            actualizar.Enabled = !ocupado;
        }

        private async Task AbrirAdministracionAsync(bool marcas)
        {
            if (ocupado) return;
            try
            {
                using (var formulario = new AdministrarValoresForm(logica, marcas))
                    formulario.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "No se pudo cargar la lista", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await CargarAsync();
        }

        private async Task AbrirArticuloAsync(bool modificacion)
        {
            if (ocupado || articulos == null) return;
            var fila = grilla.CurrentRow == null ? null : grilla.CurrentRow.DataBoundItem as DataRowView;
            if (modificacion && fila == null) return;
            using (var formulario = new ArticuloForm(logica, marcas.Copy(), categorias.Copy(), modificacion ? fila : null))
            {
                if (formulario.ShowDialog(this) != DialogResult.OK) return;
                busqueda.Clear();
                await CargarAsync();
                if (articulos == null) return;
                foreach (DataGridViewRow filaGuardada in grilla.Rows)
                {
                    if (Convert.ToString(filaGuardada.Cells["Codigo"].Value) == formulario.CodigoGuardado)
                    {
                        grilla.ClearSelection();
                        filaGuardada.Selected = true;
                        grilla.CurrentCell = filaGuardada.Cells["Codigo"];
                        grilla.FirstDisplayedScrollingRowIndex = filaGuardada.Index;
                        break;
                    }
                }
            }
        }

        private async Task EliminarAsync()
        {
            if (ocupado || grilla.CurrentRow == null) return;
            var fila = grilla.CurrentRow.DataBoundItem as DataRowView;
            if (fila == null) return;
            if (MessageBox.Show(this, "¿Eliminar el artículo «" + fila["Nombre"] + "» y sus imágenes? Esta acción no se puede deshacer.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            ocupado = true;
            ActualizarBotones();
            try
            {
                int id = Convert.ToInt32(fila["Id"]);
                await Task.Run(() => logica.Eliminar(id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ocupado = false;
            }
            await CargarAsync();
        }

    }
}
