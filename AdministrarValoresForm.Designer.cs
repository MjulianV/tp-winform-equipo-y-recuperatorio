namespace tp_winform_equipo_y_recuperatorio
{
    partial class AdministrarValoresForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel contenedor;
        private System.Windows.Forms.TableLayoutPanel panelNuevo;
        private System.Windows.Forms.FlowLayoutPanel botones;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.DataGridView lista;
        private System.Windows.Forms.TextBox descripcion;
        private System.Windows.Forms.Button agregar;
        private System.Windows.Forms.Button cerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.contenedor = new System.Windows.Forms.TableLayoutPanel();
            this.panelNuevo = new System.Windows.Forms.TableLayoutPanel();
            this.botones = new System.Windows.Forms.FlowLayoutPanel();
            this.titulo = new System.Windows.Forms.Label();
            this.lista = new System.Windows.Forms.DataGridView();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.agregar = new System.Windows.Forms.Button();
            this.cerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lista)).BeginInit();
            this.contenedor.SuspendLayout();
            this.panelNuevo.SuspendLayout();
            this.botones.SuspendLayout();
            this.SuspendLayout();

            this.titulo.AutoSize = true;
            this.titulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.titulo.Text = "Opciones disponibles";
            this.lista.AllowUserToAddRows = false;
            this.lista.AllowUserToDeleteRows = false;
            this.lista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.lista.BackgroundColor = System.Drawing.Color.White;
            this.lista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lista.MultiSelect = false;
            this.lista.ReadOnly = true;
            this.lista.RowHeadersVisible = false;
            this.lista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.descripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcion.MaxLength = 50;
            this.agregar.AutoSize = true;
            this.agregar.Text = "Agregar";
            this.cerrar.AutoSize = true;
            this.cerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cerrar.Text = "Cerrar";
            this.botones.AutoSize = true;
            this.botones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.botones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.botones.Controls.Add(this.cerrar);
            this.botones.Controls.Add(this.agregar);

            this.panelNuevo.ColumnCount = 2;
            this.panelNuevo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelNuevo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.panelNuevo.RowCount = 1;
            this.panelNuevo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelNuevo.Controls.Add(this.descripcion, 0, 0);
            this.panelNuevo.Controls.Add(this.botones, 1, 0);
            this.panelNuevo.Dock = System.Windows.Forms.DockStyle.Fill;

            this.contenedor.ColumnCount = 1;
            this.contenedor.RowCount = 3;
            this.contenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.contenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.contenedor.Controls.Add(this.titulo, 0, 0);
            this.contenedor.Controls.Add(this.lista, 0, 1);
            this.contenedor.Controls.Add(this.panelNuevo, 0, 2);
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Padding = new System.Windows.Forms.Padding(18);

            this.AcceptButton = this.agregar;
            this.CancelButton = this.cerrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 430);
            this.Controls.Add(this.contenedor);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdministrarValoresForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.lista)).EndInit();
            this.contenedor.ResumeLayout(false);
            this.contenedor.PerformLayout();
            this.panelNuevo.ResumeLayout(false);
            this.botones.ResumeLayout(false);
            this.botones.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
