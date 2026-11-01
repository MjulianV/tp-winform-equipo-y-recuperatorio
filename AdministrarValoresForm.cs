using System;
using System.Data;
using System.Windows.Forms;
using tp_winform_equipo_y_recuperatorio.BusinessLogic;

namespace tp_winform_equipo_y_recuperatorio
{
    public partial class AdministrarValoresForm : Form
    {
        private readonly ArticuloBusinessLogic logica;
        private readonly bool administrarMarcas;

        public AdministrarValoresForm(ArticuloBusinessLogic logica, bool administrarMarcas)
        {
            InitializeComponent();
            this.logica = logica;
            this.administrarMarcas = administrarMarcas;
            Text = administrarMarcas ? "Administrar marcas" : "Administrar categorías";
            titulo.Text = administrarMarcas ? "Marcas disponibles" : "Categorías disponibles";
            descripcion.MaxLength = 50;
            agregar.Click += Agregar_Click;
            cerrar.Click += (s, e) => Close();
            Cargar();
            AcceptButton = agregar;
            CancelButton = cerrar;
        }

        private void Cargar()
        {
            DataTable datos = administrarMarcas ? logica.ListarMarcas() : logica.ListarCategorias();
            lista.DataSource = datos;
            lista.Columns["Id"].Visible = false;
            lista.Columns["Descripcion"].HeaderText = administrarMarcas ? "Marca" : "Categoría";
            lista.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (administrarMarcas) logica.GuardarMarca(descripcion.Text);
                else logica.GuardarCategoria(descripcion.Text);
                descripcion.Clear();
                Cargar();
                descripcion.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "No se pudo agregar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
