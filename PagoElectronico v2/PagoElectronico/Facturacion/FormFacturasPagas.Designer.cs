namespace PagoElectronico.Facturacion
{
    partial class FormFacturasPagas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TablaDatos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.buttonDetalles = new System.Windows.Forms.Button();
            this.CargarFacturas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TablaDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // TablaDatos
            // 
            this.TablaDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaDatos.Location = new System.Drawing.Point(27, 52);
            this.TablaDatos.Name = "TablaDatos";
            this.TablaDatos.Size = new System.Drawing.Size(679, 205);
            this.TablaDatos.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccionar Factura :";
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(27, 281);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(107, 28);
            this.buttonVolver.TabIndex = 2;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // buttonDetalles
            // 
            this.buttonDetalles.Location = new System.Drawing.Point(601, 281);
            this.buttonDetalles.Name = "buttonDetalles";
            this.buttonDetalles.Size = new System.Drawing.Size(105, 28);
            this.buttonDetalles.TabIndex = 3;
            this.buttonDetalles.Text = "Ver Detalles";
            this.buttonDetalles.UseVisualStyleBackColor = true;
            this.buttonDetalles.Click += new System.EventHandler(this.buttonDetalles_Click);
            // 
            // CargarFacturas
            // 
            this.CargarFacturas.Location = new System.Drawing.Point(307, 282);
            this.CargarFacturas.Name = "CargarFacturas";
            this.CargarFacturas.Size = new System.Drawing.Size(127, 26);
            this.CargarFacturas.TabIndex = 4;
            this.CargarFacturas.Text = "Cargar Facturas";
            this.CargarFacturas.UseVisualStyleBackColor = true;
            this.CargarFacturas.Click += new System.EventHandler(this.CargarFacturas_Click);
            // 
            // FormFacturasPagas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 316);
            this.Controls.Add(this.CargarFacturas);
            this.Controls.Add(this.buttonDetalles);
            this.Controls.Add(this.buttonVolver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TablaDatos);
            this.Name = "FormFacturasPagas";
            this.Text = "FormFacturasPagas";
            this.Load += new System.EventHandler(this.FormFacturasPagas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TablaDatos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonDetalles;
        private System.Windows.Forms.Button CargarFacturas;
    }
}