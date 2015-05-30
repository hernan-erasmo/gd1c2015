namespace PagoElectronico.ABM_Cuenta
{
    partial class FormBuscar
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnBaja = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.lblEstadoBusqueda = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxTipoCta = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaAperturaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaAperturaDesde = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFechaApertura = new System.Windows.Forms.CheckBox();
            this.cbxMoneda = new System.Windows.Forms.ComboBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuscarClie = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.cbxPais = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnVolver);
            this.flowLayoutPanel2.Controls.Add(this.btnAceptar);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(13, 516);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(175, 33);
            this.flowLayoutPanel2.TabIndex = 20;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(3, 3);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(80, 28);
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(89, 3);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(80, 28);
            this.btnAceptar.TabIndex = 10;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnModificar);
            this.flowLayoutPanel1.Controls.Add(this.btnBaja);
            this.flowLayoutPanel1.Controls.Add(this.btnCrear);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(337, 516);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(258, 33);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(175, 3);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(80, 28);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnBaja
            // 
            this.btnBaja.Location = new System.Drawing.Point(89, 3);
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.Size = new System.Drawing.Size(80, 28);
            this.btnBaja.TabIndex = 8;
            this.btnBaja.Text = "Dar baja";
            this.btnBaja.UseVisualStyleBackColor = true;
            this.btnBaja.Click += new System.EventHandler(this.btnBaja_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(4, 3);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(79, 28);
            this.btnCrear.TabIndex = 10;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // lblEstadoBusqueda
            // 
            this.lblEstadoBusqueda.AutoSize = true;
            this.lblEstadoBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoBusqueda.ForeColor = System.Drawing.Color.Red;
            this.lblEstadoBusqueda.Location = new System.Drawing.Point(220, 314);
            this.lblEstadoBusqueda.Name = "lblEstadoBusqueda";
            this.lblEstadoBusqueda.Size = new System.Drawing.Size(0, 13);
            this.lblEstadoBusqueda.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(104, 314);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Estado Busqueda:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 341);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(577, 164);
            this.dataGridView1.TabIndex = 16;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(515, 307);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 28);
            this.btnBuscar.TabIndex = 15;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(18, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 274);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbxPais);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbxTipoCta);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.cbxMoneda);
            this.groupBox3.Controls.Add(this.txtNumero);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(6, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(565, 158);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cuenta";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Tipo de Cuenta";
            // 
            // cbxTipoCta
            // 
            this.cbxTipoCta.FormattingEnabled = true;
            this.cbxTipoCta.Location = new System.Drawing.Point(92, 59);
            this.cbxTipoCta.Name = "cbxTipoCta";
            this.cbxTipoCta.Size = new System.Drawing.Size(202, 21);
            this.cbxTipoCta.TabIndex = 22;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.dtpFechaAperturaHasta);
            this.groupBox4.Controls.Add(this.dtpFechaAperturaDesde);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.chkFechaApertura);
            this.groupBox4.Location = new System.Drawing.Point(6, 92);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(553, 55);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Hasta:";
            // 
            // dtpFechaAperturaHasta
            // 
            this.dtpFechaAperturaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaAperturaHasta.Location = new System.Drawing.Point(351, 19);
            this.dtpFechaAperturaHasta.Name = "dtpFechaAperturaHasta";
            this.dtpFechaAperturaHasta.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaAperturaHasta.TabIndex = 22;
            // 
            // dtpFechaAperturaDesde
            // 
            this.dtpFechaAperturaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaAperturaDesde.Location = new System.Drawing.Point(181, 19);
            this.dtpFechaAperturaDesde.Name = "dtpFechaAperturaDesde";
            this.dtpFechaAperturaDesde.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaAperturaDesde.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Desde:";
            // 
            // chkFechaApertura
            // 
            this.chkFechaApertura.AutoSize = true;
            this.chkFechaApertura.Location = new System.Drawing.Point(6, 0);
            this.chkFechaApertura.Name = "chkFechaApertura";
            this.chkFechaApertura.Size = new System.Drawing.Size(99, 17);
            this.chkFechaApertura.TabIndex = 23;
            this.chkFechaApertura.Text = "Fecha Apertura";
            this.chkFechaApertura.UseVisualStyleBackColor = true;
            this.chkFechaApertura.CheckedChanged += new System.EventHandler(this.chkFechaApertura_CheckedChanged);
            // 
            // cbxMoneda
            // 
            this.cbxMoneda.FormattingEnabled = true;
            this.cbxMoneda.Location = new System.Drawing.Point(357, 59);
            this.cbxMoneda.Name = "cbxMoneda";
            this.cbxMoneda.Size = new System.Drawing.Size(202, 21);
            this.cbxMoneda.TabIndex = 10;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(92, 29);
            this.txtNumero.MaxLength = 16;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(202, 20);
            this.txtNumero.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Moneda";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Numero";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBuscarClie);
            this.groupBox2.Controls.Add(this.txtCliente);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 73);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cliente";
            // 
            // btnBuscarClie
            // 
            this.btnBuscarClie.Location = new System.Drawing.Point(479, 23);
            this.btnBuscarClie.Name = "btnBuscarClie";
            this.btnBuscarClie.Size = new System.Drawing.Size(80, 28);
            this.btnBuscarClie.TabIndex = 8;
            this.btnBuscarClie.Text = "Buscar";
            this.btnBuscarClie.UseVisualStyleBackColor = true;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(60, 28);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(397, 20);
            this.txtCliente.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cliente";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(18, 306);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(80, 29);
            this.btnLimpiar.TabIndex = 21;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // cbxPais
            // 
            this.cbxPais.FormattingEnabled = true;
            this.cbxPais.Location = new System.Drawing.Point(357, 29);
            this.cbxPais.Name = "cbxPais";
            this.cbxPais.Size = new System.Drawing.Size(202, 21);
            this.cbxPais.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(310, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Pais";
            // 
            // FormBuscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(615, 560);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblEstadoBusqueda);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FormBuscar";
            this.Text = "Cuenta -Buscar";
            this.Load += new System.EventHandler(this.FormBuscar_Load);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBaja;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Label lblEstadoBusqueda;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaAperturaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaAperturaDesde;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFechaApertura;
        private System.Windows.Forms.ComboBox cbxMoneda;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBuscarClie;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxTipoCta;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxPais;
    }
}