namespace PagoElectronico.Depositos
{
    partial class FormDepositos
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
            this.lblCuenta = new System.Windows.Forms.Label();
            this.lblImporte = new System.Windows.Forms.Label();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.lblTarjeta = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxCuenta = new System.Windows.Forms.ComboBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.cbxMoneda = new System.Windows.Forms.ComboBox();
            this.cbxTarjeta = new System.Windows.Forms.ComboBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnDepositar = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lklCliente = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(24, 59);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(41, 13);
            this.lblCuenta.TabIndex = 0;
            this.lblCuenta.Text = "Cuenta";
            // 
            // lblImporte
            // 
            this.lblImporte.AutoSize = true;
            this.lblImporte.Location = new System.Drawing.Point(24, 97);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(42, 13);
            this.lblImporte.TabIndex = 1;
            this.lblImporte.Text = "Importe";
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.Location = new System.Drawing.Point(24, 136);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(46, 13);
            this.lblMoneda.TabIndex = 2;
            this.lblMoneda.Text = "Moneda";
            // 
            // lblTarjeta
            // 
            this.lblTarjeta.AutoSize = true;
            this.lblTarjeta.Location = new System.Drawing.Point(24, 173);
            this.lblTarjeta.Name = "lblTarjeta";
            this.lblTarjeta.Size = new System.Drawing.Size(90, 13);
            this.lblTarjeta.TabIndex = 3;
            this.lblTarjeta.Text = "Tarjeta de credito";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Fecha";
            // 
            // cbxCuenta
            // 
            this.cbxCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCuenta.FormattingEnabled = true;
            this.cbxCuenta.Location = new System.Drawing.Point(132, 51);
            this.cbxCuenta.Name = "cbxCuenta";
            this.cbxCuenta.Size = new System.Drawing.Size(269, 21);
            this.cbxCuenta.TabIndex = 5;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(132, 90);
            this.txtImporte.MaxLength = 28;
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(269, 20);
            this.txtImporte.TabIndex = 6;
            // 
            // cbxMoneda
            // 
            this.cbxMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMoneda.FormattingEnabled = true;
            this.cbxMoneda.Location = new System.Drawing.Point(132, 128);
            this.cbxMoneda.Name = "cbxMoneda";
            this.cbxMoneda.Size = new System.Drawing.Size(269, 21);
            this.cbxMoneda.TabIndex = 7;
            // 
            // cbxTarjeta
            // 
            this.cbxTarjeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTarjeta.FormattingEnabled = true;
            this.cbxTarjeta.Location = new System.Drawing.Point(132, 165);
            this.cbxTarjeta.Name = "cbxTarjeta";
            this.cbxTarjeta.Size = new System.Drawing.Size(269, 21);
            this.cbxTarjeta.TabIndex = 8;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(27, 240);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(61, 31);
            this.btnVolver.TabIndex = 10;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnDepositar
            // 
            this.btnDepositar.Location = new System.Drawing.Point(340, 240);
            this.btnDepositar.Name = "btnDepositar";
            this.btnDepositar.Size = new System.Drawing.Size(61, 31);
            this.btnDepositar.TabIndex = 11;
            this.btnDepositar.Text = "Depositar";
            this.btnDepositar.UseVisualStyleBackColor = true;
            this.btnDepositar.Click += new System.EventHandler(this.btnDepositar_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(132, 12);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(269, 20);
            this.txtCliente.TabIndex = 13;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(24, 19);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(39, 13);
            this.lblCliente.TabIndex = 12;
            this.lblCliente.Text = "Cliente";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(132, 208);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(91, 20);
            this.dtpFecha.TabIndex = 14;
            // 
            // lklCliente
            // 
            this.lklCliente.AutoSize = true;
            this.lklCliente.Location = new System.Drawing.Point(24, 19);
            this.lklCliente.Name = "lklCliente";
            this.lklCliente.Size = new System.Drawing.Size(39, 13);
            this.lklCliente.TabIndex = 15;
            this.lklCliente.TabStop = true;
            this.lklCliente.Text = "Cliente";
            this.lklCliente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklCliente_LinkClicked);
            // 
            // FormDepositos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 283);
            this.Controls.Add(this.lklCliente);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.btnDepositar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.cbxTarjeta);
            this.Controls.Add(this.cbxMoneda);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.cbxCuenta);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTarjeta);
            this.Controls.Add(this.lblMoneda);
            this.Controls.Add(this.lblImporte);
            this.Controls.Add(this.lblCuenta);
            this.MaximizeBox = false;
            this.Name = "FormDepositos";
            this.Text = "Deposito";
            this.Load += new System.EventHandler(this.FormDepositos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.Label lblMoneda;
        private System.Windows.Forms.Label lblTarjeta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxCuenta;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.ComboBox cbxMoneda;
        private System.Windows.Forms.ComboBox cbxTarjeta;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnDepositar;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.LinkLabel lklCliente;
    }
}