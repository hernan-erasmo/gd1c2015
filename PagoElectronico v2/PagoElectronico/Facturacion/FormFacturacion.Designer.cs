namespace PagoElectronico.Facturacion
{
    partial class FormFacturacion
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
            this.buttonVolver = new System.Windows.Forms.Button();
            this.buttonGenerar = new System.Windows.Forms.Button();
            this.buttonFacturasPagadas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(166, 148);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(89, 27);
            this.buttonVolver.TabIndex = 0;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // buttonGenerar
            // 
            this.buttonGenerar.Location = new System.Drawing.Point(63, 27);
            this.buttonGenerar.Name = "buttonGenerar";
            this.buttonGenerar.Size = new System.Drawing.Size(163, 38);
            this.buttonGenerar.TabIndex = 1;
            this.buttonGenerar.Text = "Generar Factura";
            this.buttonGenerar.UseVisualStyleBackColor = true;
            this.buttonGenerar.Click += new System.EventHandler(this.buttonGenerar_Click);
            // 
            // buttonFacturasPagadas
            // 
            this.buttonFacturasPagadas.Location = new System.Drawing.Point(63, 86);
            this.buttonFacturasPagadas.Name = "buttonFacturasPagadas";
            this.buttonFacturasPagadas.Size = new System.Drawing.Size(163, 37);
            this.buttonFacturasPagadas.TabIndex = 2;
            this.buttonFacturasPagadas.Text = "Ver Facturas Pagas";
            this.buttonFacturasPagadas.UseVisualStyleBackColor = true;
            this.buttonFacturasPagadas.Click += new System.EventHandler(this.buttonFacturasPagadas_Click);
            // 
            // FormFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 185);
            this.Controls.Add(this.buttonFacturasPagadas);
            this.Controls.Add(this.buttonGenerar);
            this.Controls.Add(this.buttonVolver);
            this.Name = "FormFacturacion";
            this.Text = "Facturacion";
            this.Load += new System.EventHandler(this.FormFacturacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonGenerar;
        private System.Windows.Forms.Button buttonFacturasPagadas;
    }
}