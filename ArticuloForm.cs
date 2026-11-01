using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using tp_winform_equipo_y_recuperatorio.BusinessLogic;
using tp_winform_equipo_y_recuperatorio.Dominio;

namespace tp_winform_equipo_y_recuperatorio
{
    public partial class ArticuloForm : Form
    {
        private readonly ArticuloBusinessLogic logica;
        private int id;
        private string imagenUrl;
        private bool imagenModificada;
        private bool guardando;
        public string CodigoGuardado { get; private set; }

        public ArticuloForm() : this(new ArticuloBusinessLogic(), new DataTable(), new DataTable(), null) { }

        public ArticuloForm(ArticuloBusinessLogic logica, DataTable marcas, DataTable categorias, DataRowView fila)
        {
            InitializeComponent();
            this.logica = logica;
            Text = fila == null ? "Nuevo artículo" : "Editar artículo";
            marca.DisplayMember = "Descripcion";
            marca.ValueMember = "Id";
            marca.DataSource = marcas;
            categoria.DisplayMember = "Descripcion";
            categoria.ValueMember = "Id";
            categoria.DataSource = categorias;
            marca.SelectedIndex = -1;
            categoria.SelectedIndex = -1;
            CargarVistaImagen(null);

            if (fila != null)
            {
                id = Convert.ToInt32(fila["Id"]);
                codigo.Text = Convert.ToString(fila["Codigo"]);
                nombre.Text = Convert.ToString(fila["Nombre"]);
                descripcion.Text = Convert.ToString(fila["Descripcion"]);
                if (fila["IdMarca"] != DBNull.Value) marca.SelectedValue = fila["IdMarca"];
                if (fila["IdCategoria"] != DBNull.Value) categoria.SelectedValue = fila["IdCategoria"];
                precio.Value = fila["Precio"] == DBNull.Value ? 0 : Math.Max(0, Math.Min(precio.Maximum, Convert.ToDecimal(fila["Precio"])));
                imagenUrl = fila["ImagenUrl"] == DBNull.Value ? null : Convert.ToString(fila["ImagenUrl"]);
                CargarVistaImagen(imagenUrl);
            }

            guardar.Click += Guardar_Click;
            cancelar.Click += (s, e) => Close();
            elegirImagen.Click += ElegirImagen_Click;
            quitarImagen.Click += (s, e) => CambiarImagen(null);
            AcceptButton = guardar;
            CancelButton = cancelar;
            FormClosing += (s, e) => { if (guardando) e.Cancel = true; };
        }

        private void ElegirImagen_Click(object sender, EventArgs e)
        {
            using (var selector = new OpenFileDialog())
            {
                selector.Title = "Elegir imagen del artículo";
                selector.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Todos los archivos|*.*";
                selector.CheckFileExists = true;
                if (selector.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    CambiarImagen(selector.FileName);
                }
                catch { }
            }
        }

        private void CambiarImagen(string ruta)
        {
            Image nuevaImagen = null;
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                using (var original = Image.FromFile(ruta))
                    nuevaImagen = new Bitmap(original);
            }
            LimpiarVistaImagen();
            imagenUrl = ruta;
            imagenModificada = true;
            vistaImagen.Image = nuevaImagen;
        }

        private void CargarVistaImagen(string ruta)
        {
            LimpiarVistaImagen();
            if (string.IsNullOrWhiteSpace(ruta)) return;
            Uri uri;
            if (Uri.TryCreate(ruta, UriKind.Absolute, out uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                vistaImagen.ImageLocation = ruta;
                vistaImagen.LoadAsync();
                return;
            }
            string rutaLocal = ResolverRutaImagen(ruta);
            if (File.Exists(rutaLocal))
            {
                try
                {
                    using (var original = Image.FromFile(rutaLocal))
                        vistaImagen.Image = new Bitmap(original);
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

        private static string CopiarImagenAlProyecto(string rutaOrigen)
        {
            string carpeta = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..", "Imagenes"));
            Directory.CreateDirectory(carpeta);
            string nombre = Guid.NewGuid().ToString("N") + Path.GetExtension(rutaOrigen).ToLowerInvariant();
            File.Copy(rutaOrigen, Path.Combine(carpeta, nombre));
            return Path.Combine("Imagenes", nombre);
        }

        private void GuardarArticulo(Articulo articulo)
        {
            string imagenGuardada = null;
            try
            {
                if (imagenModificada && !string.IsNullOrWhiteSpace(imagenUrl))
                    imagenGuardada = CopiarImagenAlProyecto(imagenUrl);
                logica.Guardar(articulo, imagenModificada, imagenGuardada);
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(imagenGuardada))
                {
                    string ruta = ResolverRutaImagen(imagenGuardada);
                    if (File.Exists(ruta)) File.Delete(ruta);
                }
                throw;
            }
        }

        private void LimpiarVistaImagen()
        {
            vistaImagen.ImageLocation = null;
            if (vistaImagen.Image != null)
            {
                vistaImagen.Image.Dispose();
                vistaImagen.Image = null;
            }
        }

        private async void Guardar_Click(object sender, EventArgs e)
        {
            if (guardando) return;
            var articulo = new Articulo
            {
                Id = id,
                Codigo = codigo.Text,
                Nombre = nombre.Text,
                Descripcion = descripcion.Text,
                Precio = precio.Value,
                Marca = marca.SelectedValue == null ? null : new Marca { Id = Convert.ToInt32(marca.SelectedValue) },
                Categoria = categoria.SelectedValue == null ? null : new Categoria { Id = Convert.ToInt32(categoria.SelectedValue) }
            };
            guardando = true;
            guardar.Enabled = cancelar.Enabled = false;
            UseWaitCursor = true;
            try
            {
                await Task.Run(() => GuardarArticulo(articulo));
                guardando = false;
                CodigoGuardado = articulo.Codigo;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                guardando = false;
                guardar.Enabled = cancelar.Enabled = true;
                UseWaitCursor = false;
            }
        }

    }
}
