using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using tp_winform_equipo_y_recuperatorio.BusinessLogic;

namespace tp_winform_equipo_y_recuperatorio
{
    public partial class CatalogoForm : Form
    {
        private readonly ArticuloBusinessLogic logica = new ArticuloBusinessLogic();
        private readonly DataGridView grilla = new DataGridView();
        private readonly TextBox busqueda = new TextBox { Dock = DockStyle.Fill };
        private readonly ComboBox campo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        private readonly Label estado = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        private readonly ToolStripButton nuevo = new ToolStripButton("Nuevo");
        private readonly ToolStripButton editar = new ToolStripButton("Editar");
        private readonly ToolStripButton eliminar = new ToolStripButton("Eliminar");
        private readonly ToolStripButton actualizar = new ToolStripButton("Actualizar");
        private DataTable articulos;
        private DataTable marcas;
        private DataTable categorias;
        private bool ocupado;

        public CatalogoForm()
        {
            Text = "Catálogo de artículos";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1040, 600);
            MinimumSize = new Size(780, 450);
            Font = new Font("Segoe UI", 10);
            BackColor = Color.FromArgb(245, 247, 250);
            var estructura = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 1, RowCount = 5 };
            estructura.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            estructura.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            estructura.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            estructura.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            estructura.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            estructura.Controls.Add(new Label { Text = "Catálogo de artículos", Font = new Font("Segoe UI", 20, FontStyle.Bold), AutoSize = true }, 0, 0);
            var barra = new ToolStrip { Dock = DockStyle.Fill, GripStyle = ToolStripGripStyle.Hidden, BackColor = BackColor, Font = Font };
            barra.Items.AddRange(new ToolStripItem[] { nuevo, editar, eliminar, new ToolStripSeparator(), actualizar });
            estructura.Controls.Add(barra, 0, 1);
            var filtros = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4 };
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            filtros.Controls.Add(new Label { Text = "Buscar por:", AutoSize = true, Padding = new Padding(0, 5, 0, 0) }, 0, 0);
            campo.Items.AddRange(new object[] { "Todos", "Código", "Nombre", "Descripción", "Marca", "Categoría" });
            campo.SelectedIndex = 0;
            filtros.Controls.Add(campo, 1, 0); filtros.Controls.Add(busqueda, 2, 0);
            var limpiar = new Button { Text = "Limpiar", Dock = DockStyle.Fill };
            limpiar.Click += (s, e) => { busqueda.Clear(); busqueda.Focus(); };
            filtros.Controls.Add(limpiar, 3, 0);
            estructura.Controls.Add(filtros, 0, 2);
            grilla.Dock = DockStyle.Fill; grilla.ReadOnly = true; grilla.MultiSelect = false;
            grilla.AllowUserToAddRows = grilla.AllowUserToDeleteRows = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.RowHeadersVisible = false; grilla.BackgroundColor = Color.White; grilla.BorderStyle = BorderStyle.None;
            grilla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(239, 244, 249);
            grilla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(36, 91, 150);
            grilla.DefaultCellStyle.SelectionForeColor = Color.White;
            grilla.RowTemplate.Height = 30;
            estructura.Controls.Add(grilla, 0, 3); estructura.Controls.Add(estado, 0, 4);
            Controls.Add(estructura);
            Shown += async (s, e) => await CargarAsync();
            actualizar.Click += async (s, e) => await CargarAsync();
            nuevo.Click += async (s, e) => await AbrirArticuloAsync(false);
            editar.Click += async (s, e) => await AbrirArticuloAsync(true);
            eliminar.Click += async (s, e) => await EliminarAsync();
            grilla.CellDoubleClick += async (s, e) => { if (e.RowIndex >= 0) await AbrirArticuloAsync(true); };
            grilla.SelectionChanged += (s, e) => ActualizarBotones();
            busqueda.TextChanged += (s, e) => Filtrar();
            campo.SelectedIndexChanged += (s, e) => Filtrar();
            FormClosing += (s, e) => { if (ocupado) e.Cancel = true; };
            ActualizarBotones();
        }

        private async Task CargarAsync()
        {
            if (ocupado) return;
            ocupado = true; ActualizarBotones(); estado.Text = "Conectando con la base de datos…";
            try
            {
                var datos = await Task.Run(() => new[] { logica.ListarDesdeBaseDeDatos(), logica.ListarMarcas(), logica.ListarCategorias() });
                articulos = datos[0]; marcas = datos[1]; categorias = datos[2];
                Filtrar();
            }
            catch (Exception ex)
            {
                articulos = null; marcas = null; categorias = null; grilla.DataSource = null;
                estado.Text = "Sin conexión. Revisá SQL Server y presioná Actualizar para reintentar.";
                MessageBox.Show(this, "No se pudo cargar el catálogo. Revisá la instancia configurada en App.config y que exista CATALOGO_P3_DB.\n\n" + ex.Message,
                    "Conexión no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { ocupado = false; ActualizarBotones(); }
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
            foreach (string columna in new[] { "Id", "IdMarca", "IdCategoria" }) grilla.Columns[columna].Visible = false;
            grilla.Columns["Codigo"].HeaderText = "Código";
            grilla.Columns["Descripcion"].HeaderText = "Descripción";
            grilla.Columns["Categoria"].HeaderText = "Categoría";
            grilla.Columns["Precio"].DefaultCellStyle.Format = "N2";
            grilla.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            estado.Text = vista.Rows.Count + " de " + articulos.Rows.Count + " artículos" + (vista.Rows.Count == 0 ? " · Sin resultados" : "");
            ActualizarBotones();
        }

        private void ActualizarBotones()
        {
            bool conectado = articulos != null && !ocupado;
            nuevo.Enabled = conectado && marcas != null && marcas.Rows.Count > 0 && categorias != null && categorias.Rows.Count > 0;
            editar.Enabled = eliminar.Enabled = conectado && grilla.CurrentRow != null;
            actualizar.Enabled = !ocupado;
        }

        private async Task AbrirArticuloAsync(bool modificacion)
        {
            if (ocupado || articulos == null) return;
            var fila = grilla.CurrentRow == null ? null : grilla.CurrentRow.DataBoundItem as DataRowView;
            if (modificacion && fila == null) return;
            using (var formulario = new ArticuloForm(logica, marcas.Copy(), categorias.Copy(), modificacion ? fila : null))
            {
                if (formulario.ShowDialog(this) != DialogResult.OK) return;
                // Un filtro anterior no debe ocultar el artículo recién guardado.
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
            ocupado = true; ActualizarBotones();
            try { int id = Convert.ToInt32(fila["Id"]); await Task.Run(() => logica.Eliminar(id)); }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { ocupado = false; }
            await CargarAsync();
        }
    }
}
