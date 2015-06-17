namespace PagoElectronico.ABM_Cuenta
{
    partial class FormCrear
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
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.gbCuenta = new System.Windows.Forms.GroupBox();
            this.cbxPais = new System.Windows.Forms.ComboBox();
            this.cbxMoneda = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaApertura = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxTipoCta = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbInformacion = new System.Windows.Forms.GroupBox();
            this.gbCuenta.SuspendLayout();
            this.gbInformacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 293);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(80, 28);
            this.btnVolver.TabIndex = 10;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(218, 293);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(80, 28);
            this.btnCrear.TabIndex = 9;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // gbCuenta
            // 
            this.gbCuenta.Controls.Add(this.cbxPais);
            this.gbCuenta.Controls.Add(this.cbxMoneda);
            this.gbCuenta.Controls.Add(this.label5);
            this.gbCuenta.Controls.Add(this.dtpFechaApertura);
            this.gbCuenta.Controls.Add(this.label6);
            this.gbCuenta.Controls.Add(this.cbxTipoCta);
            this.gbCuenta.Controls.Add(this.label4);
            this.gbCuenta.Controls.Add(this.label3);
            this.gbCuenta.Location = new System.Drawing.Point(12, 51);
            this.gbCuenta.Name = "gbCuenta";
            this.gbCuenta.Size = new System.Drawing.Size(286, 161);
            this.gbCuenta.TabIndex = 8;
            this.gbCuenta.TabStop = false;
            this.gbCuenta.Text = "Informacion de la Cuenta";
            // 
            // cbxPais
            // 
            this.cbxPais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPais.FormattingEnabled = true;
            this.cbxPais.Location = new System.Drawing.Point(121, 25);
            this.cbxPais.Name = "cbxPais";
            this.cbxPais.Size = new System.Drawing.Size(159, 21);
            this.cbxPais.TabIndex = 31;
            // 
            // cbxMoneda
            // 
            this.cbxMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMoneda.FormattingEnabled = true;
            this.cbxMoneda.Location = new System.Drawing.Point(121, 94);
            this.cbxMoneda.Name = "cbxMoneda";
            this.cbxMoneda.Size = new System.Drawing.Size(159, 21);
            this.cbxMoneda.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Moneda";
            // 
            // dtpFechaApertura
            // 
            this.dtpFechaApertura.CustomFormat = "";
            this.dtpFechaApertura.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaApertura.Location = new System.Drawing.Point(121, 128);
            this.dtpFechaApertura.Name = "dtpFechaApertura";
            this.dtpFechaApertura.Size = new System.Drawing.Size(86, 20);
            this.dtpFechaApertura.TabIndex = 6;
            this.dtpFechaApertura.Value = new System.DateTime(2015, 5, 17, 0, 0, 0, 0);
            this.dtpFechaApertura.ValueChanged += new System.EventHandler(this.dtpFechaApertura_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Pais";
            // 
            // cbxTipoCta
            // 
            this.cbxTipoCta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoCta.FormattingEnabled = true;
            this.cbxTipoCta.Location = new System.Drawing.Point(121, 60);
            this.cbxTipoCta.Name = "cbxTipoCta";
            this.cbxTipoCta.Size = new System.Drawing.Size(159, 21);
            this.cbxTipoCta.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Fecha apertura";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tipo de Cuenta";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(121, 28);
            this.txtNumero.MaxLength = 16;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.ReadOnly = true;
            this.txtNumero.Size = new System.Drawing.Size(159, 20);
            this.txtNumero.TabIndex = 5;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(133, 18);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(162, 20);
            this.txtCliente.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Se creo la Cuenta:";
            // 
            // gbInformacion
            // 
            this.gbInformacion.Controls.Add(this.label2);
            this.gbInformacion.Controls.Add(this.txtNumero);
            this.gbInformacion.Location = new System.Drawing.Point(12, 218);
            this.gbInformacion.Name = "gbInformacion";
            this.gbInformacion.Size = new System.Drawing.Size(286, 60);
            this.gbInformacion.TabIndex = 13;
            this.gbInformacion.TabStop = false;
            this.gbInformacion.Text = "Información de la Operación";
            // 
            // FormCrear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(310, 333);
            this.Controls.Add(this.gbInformacion);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.gbCuenta);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FormCrear";
            this.Text = "Cuenta - Crear";
            this.Load += new System.EventHandler(this.FormCrear_Load);
            this.gbCuenta.ResumeLayout(false);
            this.gbCuenta.PerformLayout();
            this.gbInformacion.ResumeLayout(false);
            this.gbInformacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.GroupBox gbCuenta;
        private System.Windows.Forms.DateTimePicker dtpFechaApertura;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxTipoCta;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxPais;
        private System.Windows.Forms.ComboBox cbxMoneda;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbInformacion;
    }
}