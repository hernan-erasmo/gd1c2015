namespace PagoElectronico.Transferencias
{
    partial class FormTransferencias
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
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxCuentaDestino = new System.Windows.Forms.ComboBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.cbxCuenta = new System.Windows.Forms.ComboBox();
            this.lblImporte = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTransferir = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lklCuentaDestino = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(96, 16);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(269, 20);
            this.txtCliente.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Cliente";
            // 
            // cbxCuentaDestino
            // 
            this.cbxCuentaDestino.FormattingEnabled = true;
            this.cbxCuentaDestino.Location = new System.Drawing.Point(96, 94);
            this.cbxCuentaDestino.Name = "cbxCuentaDestino";
            this.cbxCuentaDestino.Size = new System.Drawing.Size(269, 21);
            this.cbxCuentaDestino.TabIndex = 21;
            this.cbxCuentaDestino.SelectedIndexChanged += new System.EventHandler(this.cbxCuentaDestino_SelectedIndexChanged);
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(96, 133);
            this.txtImporte.MaxLength = 28;
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(269, 20);
            this.txtImporte.TabIndex = 19;
            // 
            // cbxCuenta
            // 
            this.cbxCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCuenta.FormattingEnabled = true;
            this.cbxCuenta.Location = new System.Drawing.Point(96, 55);
            this.cbxCuenta.Name = "cbxCuenta";
            this.cbxCuenta.Size = new System.Drawing.Size(269, 21);
            this.cbxCuenta.TabIndex = 18;
            // 
            // lblImporte
            // 
            this.lblImporte.AutoSize = true;
            this.lblImporte.Location = new System.Drawing.Point(11, 136);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(42, 13);
            this.lblImporte.TabIndex = 15;
            this.lblImporte.Text = "Importe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Cuenta";
            // 
            // btnTransferir
            // 
            this.btnTransferir.Location = new System.Drawing.Point(285, 180);
            this.btnTransferir.Name = "btnTransferir";
            this.btnTransferir.Size = new System.Drawing.Size(80, 28);
            this.btnTransferir.TabIndex = 24;
            this.btnTransferir.Text = "Transferir";
            this.btnTransferir.UseVisualStyleBackColor = true;
            this.btnTransferir.Click += new System.EventHandler(this.btnTransferir_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(14, 180);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(80, 28);
            this.btnVolver.TabIndex = 25;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lklCuentaDestino
            // 
            this.lklCuentaDestino.AutoSize = true;
            this.lklCuentaDestino.Location = new System.Drawing.Point(11, 97);
            this.lklCuentaDestino.Name = "lklCuentaDestino";
            this.lklCuentaDestino.Size = new System.Drawing.Size(78, 13);
            this.lklCuentaDestino.TabIndex = 27;
            this.lklCuentaDestino.TabStop = true;
            this.lklCuentaDestino.Text = "Cuenta destino";
            this.lklCuentaDestino.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklCuentaDestino_LinkClicked);
            // 
            // FormTransferencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(374, 220);
            this.Controls.Add(this.lklCuentaDestino);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnTransferir);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxCuentaDestino);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.cbxCuenta);
            this.Controls.Add(this.lblImporte);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FormTransferencias";
            this.Text = "Transferencia";
            this.Load += new System.EventHandler(this.FormTransferencias_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxCuentaDestino;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.ComboBox cbxCuenta;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.LinkLabel lklCuentaDestino;
    }
}