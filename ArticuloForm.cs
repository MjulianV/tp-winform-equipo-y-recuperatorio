using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using tp_winform_equipo_y_recuperatorio.BusinessLogic;
using tp_winform_equipo_y_recuperatorio.Dominio;

namespace tp_winform_equipo_y_recuperatorio
{
    public partial class ArticuloForm : Form
    {
        private readonly ArticuloBusinessLogic logica;
        private readonly int id;
        private readonly TextBox codigo = new TextBox { MaxLength = 50, Dock = DockStyle.Fill };
        private readonly TextBox nombre = new TextBox { MaxLength = 50, Dock = DockStyle.Fill };
        private readonly TextBox descripcion = new TextBox { MaxLength = 150, Multiline = true, Dock = DockStyle.Fill };
        private readonly ComboBox marca = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        private readonly ComboBox categoria = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        private readonly NumericUpDown precio = new NumericUpDown { DecimalPlaces = 4, Maximum = 922337203685477.5807m, ThousandsSeparator = true, Dock = DockStyle.Fill };
        private readonly Button guardar = new Button { Text = "Guardar", AutoSize = true };
        private readonly Button cancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, AutoSize = true };
        private bool guardando;
        public string CodigoGuardado { get; private set; }

        public ArticuloForm() : this(new ArticuloBusinessLogic(), new DataTable(), new DataTable(), null) { }
        public ArticuloForm(ArticuloBusinessLogic logica, DataTable marcas, DataTable categorias, DataRowView fila)
        {
            this.logica = logica;
            Text = fila == null ? "Nuevo artículo" : "Editar artículo";
            StartPosition = FormStartPosition.CenterParent;
            Font = new Font("Segoe UI", 10);
            ClientSize = new Size(520, 390);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            var tabla = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 7 };
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            string[] etiquetas = { "Código *", "Nombre *", "Descripción", "Marca *", "Categoría *", "Precio *" };
            Control[] campos = { codigo, nombre, descripcion, marca, categoria, precio };
            for (int i = 0; i < campos.Length; i++)
            {
                tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 2 ? 90 : 40));
                tabla.Controls.Add(new Label { Text = etiquetas[i], AutoSize = true, Padding = new Padding(0, 5, 0, 0) }, 0, i);
                tabla.Controls.Add(campos[i], 1, i);
            }
            var botones = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            botones.Controls.Add(cancelar);
            botones.Controls.Add(guardar);
            tabla.Controls.Add(botones, 0, 6);
            tabla.SetColumnSpan(botones, 2);
            Controls.Add(tabla);
            marca.DisplayMember = "Descripcion"; marca.ValueMember = "Id"; marca.DataSource = marcas;
            categoria.DisplayMember = "Descripcion"; categoria.ValueMember = "Id"; categoria.DataSource = categorias;
            marca.SelectedIndex = categoria.SelectedIndex = -1;
            if (fila != null)
            {
                id = Convert.ToInt32(fila["Id"]);
                codigo.Text = Convert.ToString(fila["Codigo"]);
                nombre.Text = Convert.ToString(fila["Nombre"]);
                descripcion.Text = Convert.ToString(fila["Descripcion"]);
                if (fila["IdMarca"] != DBNull.Value) marca.SelectedValue = fila["IdMarca"];
                if (fila["IdCategoria"] != DBNull.Value) categoria.SelectedValue = fila["IdCategoria"];
                precio.Value = fila["Precio"] == DBNull.Value ? 0 : Math.Max(0, Math.Min(precio.Maximum, Convert.ToDecimal(fila["Precio"])));
            }
            AcceptButton = guardar; CancelButton = cancelar;
            guardar.Click += Guardar_Click;
            FormClosing += (s, e) => { if (guardando) e.Cancel = true; };
        }

        private async void Guardar_Click(object sender, EventArgs e)
        {
            if (guardando) return;
            var articulo = new Articulo
            {
                Id = id, Codigo = codigo.Text, Nombre = nombre.Text, Descripcion = descripcion.Text, Precio = precio.Value,
                Marca = marca.SelectedValue == null ? null : new Marca { Id = Convert.ToInt32(marca.SelectedValue) },
                Categoria = categoria.SelectedValue == null ? null : new Categoria { Id = Convert.ToInt32(categoria.SelectedValue) }
            };
            guardando = true; guardar.Enabled = cancelar.Enabled = false; UseWaitCursor = true;
            try
            {
                await Task.Run(() => logica.Guardar(articulo));
                guardando = false;
                CodigoGuardado = articulo.Codigo;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { guardando = false; guardar.Enabled = cancelar.Enabled = true; UseWaitCursor = false; }
        }
    }
}
