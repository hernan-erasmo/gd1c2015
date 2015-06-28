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
            this.linkCliente = new System.Windows.Forms.LinkLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonVolver
            // 
            this.buttonVolver.Location = new System.Drawing.Point(136, 170);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(89, 27);
            this.buttonVolver.TabIndex = 0;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = true;
            this.buttonVolver.Click += new System.EventHandler(this.buttonVolver_Click);
            // 
            // buttonGenerar
            // 
            this.buttonGenerar.Location = new System.Drawing.Point(53, 55);
            this.buttonGenerar.Name = "buttonGenerar";
            this.buttonGenerar.Size = new System.Drawing.Size(163, 38);
            this.buttonGenerar.TabIndex = 1;
            this.buttonGenerar.Text = "Generar Factura";
            this.buttonGenerar.UseVisualStyleBackColor = true;
            this.buttonGenerar.Click += new System.EventHandler(this.buttonGenerar_Click);
            // 
            // buttonFacturasPagadas
            // 
            this.buttonFacturasPagadas.Location = new System.Drawing.Point(53, 113);
            this.buttonFacturasPagadas.Name = "buttonFacturasPagadas";
            this.buttonFacturasPagadas.Size = new System.Drawing.Size(163, 36);
            this.buttonFacturasPagadas.TabIndex = 2;
            this.buttonFacturasPagadas.Text = "Ver Facturas Pagas";
            this.buttonFacturasPagadas.UseVisualStyleBackColor = true;
            this.buttonFacturasPagadas.Click += new System.EventHandler(this.buttonFacturasPagadas_Click);
            // 
            // linkCliente
            // 
            this.linkCliente.AutoSize = true;
            this.linkCliente.Location = new System.Drawing.Point(12, 18);
            this.linkCliente.Name = "linkCliente";
            this.linkCliente.Size = new System.Drawing.Size(45, 13);
            this.linkCliente.TabIndex = 3;
            this.linkCliente.TabStop = true;
            this.linkCliente.Text = "Cliente :";
            this.linkCliente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCliente_LinkClicked);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(63, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 20);
            this.textBox1.TabIndex = 4;
            // 
            // FormFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 209);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.linkCliente);
            this.Controls.Add(this.buttonFacturasPagadas);
            this.Controls.Add(this.buttonGenerar);
            this.Controls.Add(this.buttonVolver);
            this.Name = "FormFacturacion";
            this.Text = "Facturacion";
            this.Load += new System.EventHandler(this.FormFacturacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonGenerar;
        private System.Windows.Forms.Button buttonFacturasPagadas;
        private System.Windows.Forms.LinkLabel linkCliente;
        private System.Windows.Forms.TextBox textBox1;
    }
}