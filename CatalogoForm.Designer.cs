namespace tp_winform_equipo_y_recuperatorio
{
    partial class CatalogoForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel estructura;
        private System.Windows.Forms.TableLayoutPanel filtros;
        private System.Windows.Forms.TableLayoutPanel panelVistaImagen;
        private System.Windows.Forms.SplitContainer panelContenido;
        private System.Windows.Forms.ToolStrip barra;
        private System.Windows.Forms.ToolStripButton nuevo;
        private System.Windows.Forms.ToolStripButton editar;
        private System.Windows.Forms.ToolStripButton eliminar;
        private System.Windows.Forms.ToolStripButton actualizar;
        private System.Windows.Forms.ToolStripButton administrarMarcas;
        private System.Windows.Forms.ToolStripButton administrarCategorias;
        private System.Windows.Forms.ToolStripSeparator separadorAcciones;
        private System.Windows.Forms.ToolStripSeparator separadorAdministracion;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label etiquetaBuscar;
        private System.Windows.Forms.Label estado;
        private System.Windows.Forms.Label tituloImagen;
        private System.Windows.Forms.TextBox busqueda;
        private System.Windows.Forms.ComboBox campo;
        private System.Windows.Forms.Button limpiar;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.PictureBox vistaImagenCatalogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.estructura = new System.Windows.Forms.TableLayoutPanel();
            this.titulo = new System.Windows.Forms.Label();
            this.barra = new System.Windows.Forms.ToolStrip();
            this.nuevo = new System.Windows.Forms.ToolStripButton();
            this.editar = new System.Windows.Forms.ToolStripButton();
            this.eliminar = new System.Windows.Forms.ToolStripButton();
            this.separadorAcciones = new System.Windows.Forms.ToolStripSeparator();
            this.actualizar = new System.Windows.Forms.ToolStripButton();
            this.separadorAdministracion = new System.Windows.Forms.ToolStripSeparator();
            this.administrarMarcas = new System.Windows.Forms.ToolStripButton();
            this.administrarCategorias = new System.Windows.Forms.ToolStripButton();
            this.filtros = new System.Windows.Forms.TableLayoutPanel();
            this.etiquetaBuscar = new System.Windows.Forms.Label();
            this.campo = new System.Windows.Forms.ComboBox();
            this.busqueda = new System.Windows.Forms.TextBox();
            this.limpiar = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.SplitContainer();
            this.grilla = new System.Windows.Forms.DataGridView();
            this.panelVistaImagen = new System.Windows.Forms.TableLayoutPanel();
            this.tituloImagen = new System.Windows.Forms.Label();
            this.vistaImagenCatalogo = new System.Windows.Forms.PictureBox();
            this.estado = new System.Windows.Forms.Label();
            this.estructura.SuspendLayout();
            this.barra.SuspendLayout();
            this.filtros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContenido)).BeginInit();
            this.panelContenido.Panel1.SuspendLayout();
            this.panelContenido.Panel2.SuspendLayout();
            this.panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilla)).BeginInit();
            this.panelVistaImagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vistaImagenCatalogo)).BeginInit();
            this.SuspendLayout();
            this.estructura.ColumnCount = 1;
            this.estructura.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.estructura.Controls.Add(this.titulo, 0, 0);
            this.estructura.Controls.Add(this.barra, 0, 1);
            this.estructura.Controls.Add(this.filtros, 0, 2);
            this.estructura.Controls.Add(this.panelContenido, 0, 3);
            this.estructura.Controls.Add(this.estado, 0, 4);
            this.estructura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.estructura.Location = new System.Drawing.Point(0, 0);
            this.estructura.Name = "estructura";
            this.estructura.Padding = new System.Windows.Forms.Padding(16);
            this.estructura.RowCount = 5;
            this.estructura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.estructura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.estructura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.estructura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.estructura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.estructura.Size = new System.Drawing.Size(1040, 600);
            this.estructura.TabIndex = 0;
            this.titulo.AutoSize = true;
            this.titulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.titulo.Location = new System.Drawing.Point(19, 16);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(292, 37);
            this.titulo.TabIndex = 0;
            this.titulo.Text = "Catálogo de artículos";
            this.barra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.barra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.barra.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.barra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevo,
            this.editar,
            this.eliminar,
            this.separadorAcciones,
            this.actualizar,
            this.separadorAdministracion,
            this.administrarMarcas,
            this.administrarCategorias});
            this.barra.Location = new System.Drawing.Point(16, 64);
            this.barra.Name = "barra";
            this.barra.Size = new System.Drawing.Size(1008, 38);
            this.barra.TabIndex = 1;
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(46, 35);
            this.nuevo.Text = "Nuevo";
            this.editar.Name = "editar";
            this.editar.Size = new System.Drawing.Size(41, 35);
            this.editar.Text = "Editar";
            this.eliminar.Name = "eliminar";
            this.eliminar.Size = new System.Drawing.Size(54, 35);
            this.eliminar.Text = "Eliminar";
            this.separadorAcciones.Name = "separadorAcciones";
            this.separadorAcciones.Size = new System.Drawing.Size(6, 38);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(63, 35);
            this.actualizar.Text = "Actualizar";
            this.separadorAdministracion.Name = "separadorAdministracion";
            this.separadorAdministracion.Size = new System.Drawing.Size(6, 38);
            this.administrarMarcas.Name = "administrarMarcas";
            this.administrarMarcas.Size = new System.Drawing.Size(49, 35);
            this.administrarMarcas.Text = "Marcas";
            this.administrarCategorias.Name = "administrarCategorias";
            this.administrarCategorias.Size = new System.Drawing.Size(67, 35);
            this.administrarCategorias.Text = "Categorías";
            this.filtros.ColumnCount = 4;
            this.filtros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.filtros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.filtros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filtros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.filtros.Controls.Add(this.etiquetaBuscar, 0, 0);
            this.filtros.Controls.Add(this.campo, 1, 0);
            this.filtros.Controls.Add(this.busqueda, 2, 0);
            this.filtros.Controls.Add(this.limpiar, 3, 0);
            this.filtros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtros.Location = new System.Drawing.Point(19, 105);
            this.filtros.Name = "filtros";
            this.filtros.Size = new System.Drawing.Size(1002, 38);
            this.filtros.TabIndex = 2;
            this.etiquetaBuscar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.etiquetaBuscar.AutoSize = true;
            this.etiquetaBuscar.Location = new System.Drawing.Point(3, 9);
            this.etiquetaBuscar.Name = "etiquetaBuscar";
            this.etiquetaBuscar.Size = new System.Drawing.Size(77, 19);
            this.etiquetaBuscar.TabIndex = 0;
            this.etiquetaBuscar.Text = "Buscar por:";
            this.campo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.campo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.campo.Items.AddRange(new object[] {
            "Todos",
            "Código",
            "Nombre",
            "Descripción",
            "Marca",
            "Categoría"});
            this.campo.Location = new System.Drawing.Point(93, 3);
            this.campo.Name = "campo";
            this.campo.Size = new System.Drawing.Size(144, 25);
            this.campo.TabIndex = 1;
            this.busqueda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.busqueda.Location = new System.Drawing.Point(243, 3);
            this.busqueda.Name = "busqueda";
            this.busqueda.Size = new System.Drawing.Size(666, 25);
            this.busqueda.TabIndex = 2;
            this.limpiar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.limpiar.Location = new System.Drawing.Point(915, 3);
            this.limpiar.Name = "limpiar";
            this.limpiar.Size = new System.Drawing.Size(84, 32);
            this.limpiar.TabIndex = 3;
            this.limpiar.Text = "Limpiar";
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(19, 149);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Panel1.Controls.Add(this.grilla);
            this.panelContenido.Panel1MinSize = 360;
            this.panelContenido.Panel2.Controls.Add(this.panelVistaImagen);
            this.panelContenido.Panel2MinSize = 230;
            this.panelContenido.Size = new System.Drawing.Size(1002, 398);
            this.panelContenido.SplitterDistance = 695;
            this.panelContenido.TabIndex = 3;
            this.grilla.AllowUserToAddRows = false;
            this.grilla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.grilla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grilla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grilla.BackgroundColor = System.Drawing.Color.White;
            this.grilla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(91)))), ((int)(((byte)(150)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grilla.DefaultCellStyle = dataGridViewCellStyle2;
            this.grilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grilla.Location = new System.Drawing.Point(0, 0);
            this.grilla.MultiSelect = false;
            this.grilla.Name = "grilla";
            this.grilla.ReadOnly = true;
            this.grilla.RowHeadersVisible = false;
            this.grilla.RowTemplate.Height = 30;
            this.grilla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilla.Size = new System.Drawing.Size(695, 398);
            this.grilla.TabIndex = 0;
            this.panelVistaImagen.ColumnCount = 1;
            this.panelVistaImagen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelVistaImagen.Controls.Add(this.tituloImagen, 0, 0);
            this.panelVistaImagen.Controls.Add(this.vistaImagenCatalogo, 0, 1);
            this.panelVistaImagen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVistaImagen.Location = new System.Drawing.Point(0, 0);
            this.panelVistaImagen.Name = "panelVistaImagen";
            this.panelVistaImagen.Padding = new System.Windows.Forms.Padding(8);
            this.panelVistaImagen.RowCount = 2;
            this.panelVistaImagen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.panelVistaImagen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelVistaImagen.Size = new System.Drawing.Size(303, 398);
            this.panelVistaImagen.TabIndex = 0;
            this.tituloImagen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tituloImagen.AutoSize = true;
            this.tituloImagen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tituloImagen.Location = new System.Drawing.Point(11, 20);
            this.tituloImagen.Name = "tituloImagen";
            this.tituloImagen.Size = new System.Drawing.Size(139, 19);
            this.tituloImagen.TabIndex = 0;
            this.tituloImagen.Text = "Imagen del artículo";
            this.vistaImagenCatalogo.BackColor = System.Drawing.Color.White;
            this.vistaImagenCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vistaImagenCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vistaImagenCatalogo.Location = new System.Drawing.Point(11, 55);
            this.vistaImagenCatalogo.Name = "vistaImagenCatalogo";
            this.vistaImagenCatalogo.Size = new System.Drawing.Size(281, 298);
            this.vistaImagenCatalogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.vistaImagenCatalogo.TabIndex = 1;
            this.vistaImagenCatalogo.TabStop = false;
            this.estado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.estado.Location = new System.Drawing.Point(19, 550);
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(1002, 34);
            this.estado.TabIndex = 4;
            this.estado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1040, 600);
            this.Controls.Add(this.estructura);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(780, 450);
            this.Name = "CatalogoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo de artículos";
            this.estructura.ResumeLayout(false);
            this.estructura.PerformLayout();
            this.barra.ResumeLayout(false);
            this.barra.PerformLayout();
            this.filtros.ResumeLayout(false);
            this.filtros.PerformLayout();
            this.panelContenido.Panel1.ResumeLayout(false);
            this.panelContenido.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContenido)).EndInit();
            this.panelContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grilla)).EndInit();
            this.panelVistaImagen.ResumeLayout(false);
            this.panelVistaImagen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vistaImagenCatalogo)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
