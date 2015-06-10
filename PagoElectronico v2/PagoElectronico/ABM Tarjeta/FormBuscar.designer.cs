namespace PagoElectronico.ABM_Tarjeta
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaVencimientoHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaVencimientoDesde = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.chkFechaVencimiento = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaEmisionHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaEmisionDesde = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFechaEmision = new System.Windows.Forms.CheckBox();
            this.cbxEmisor = new System.Windows.Forms.ComboBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuscarClie = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.lblEstadoBusqueda = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnDesasociar = new System.Windows.Forms.Button();
            this.btnAsociar = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(17, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 306);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.cbxEmisor);
            this.groupBox3.Controls.Add(this.txtNumero);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(6, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(565, 200);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tarjeta";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.dtpFechaVencimientoHasta);
            this.groupBox5.Controls.Add(this.dtpFechaVencimientoDesde);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.chkFechaVencimiento);
            this.groupBox5.Location = new System.Drawing.Point(6, 137);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(553, 55);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Hasta:";
            // 
            // dtpFechaVencimientoHasta
            // 
            this.dtpFechaVencimientoHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimientoHasta.Location = new System.Drawing.Point(351, 19);
            this.dtpFechaVencimientoHasta.Name = "dtpFechaVencimientoHasta";
            this.dtpFechaVencimientoHasta.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaVencimientoHasta.TabIndex = 22;
            // 
            // dtpFechaVencimientoDesde
            // 
            this.dtpFechaVencimientoDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimientoDesde.Location = new System.Drawing.Point(181, 19);
            this.dtpFechaVencimientoDesde.Name = "dtpFechaVencimientoDesde";
            this.dtpFechaVencimientoDesde.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaVencimientoDesde.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(134, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Desde:";
            // 
            // chkFechaVencimiento
            // 
            this.chkFechaVencimiento.AutoSize = true;
            this.chkFechaVencimiento.Location = new System.Drawing.Point(6, 0);
            this.chkFechaVencimiento.Name = "chkFechaVencimiento";
            this.chkFechaVencimiento.Size = new System.Drawing.Size(117, 17);
            this.chkFechaVencimiento.TabIndex = 23;
            this.chkFechaVencimiento.Text = "Fecha Vencimiento";
            this.chkFechaVencimiento.UseVisualStyleBackColor = true;
            this.chkFechaVencimiento.CheckedChanged += new System.EventHandler(this.chkFechaVencimiento_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.dtpFechaEmisionHasta);
            this.groupBox4.Controls.Add(this.dtpFechaEmisionDesde);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.chkFechaEmision);
            this.groupBox4.Location = new System.Drawing.Point(6, 61);
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
            // dtpFechaEmisionHasta
            // 
            this.dtpFechaEmisionHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmisionHasta.Location = new System.Drawing.Point(351, 19);
            this.dtpFechaEmisionHasta.Name = "dtpFechaEmisionHasta";
            this.dtpFechaEmisionHasta.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaEmisionHasta.TabIndex = 22;
            // 
            // dtpFechaEmisionDesde
            // 
            this.dtpFechaEmisionDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmisionDesde.Location = new System.Drawing.Point(181, 19);
            this.dtpFechaEmisionDesde.Name = "dtpFechaEmisionDesde";
            this.dtpFechaEmisionDesde.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaEmisionDesde.TabIndex = 24;
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
            // chkFechaEmision
            // 
            this.chkFechaEmision.AutoSize = true;
            this.chkFechaEmision.Location = new System.Drawing.Point(6, 0);
            this.chkFechaEmision.Name = "chkFechaEmision";
            this.chkFechaEmision.Size = new System.Drawing.Size(95, 17);
            this.chkFechaEmision.TabIndex = 23;
            this.chkFechaEmision.Text = "Fecha Emision";
            this.chkFechaEmision.UseVisualStyleBackColor = true;
            this.chkFechaEmision.CheckedChanged += new System.EventHandler(this.chkFechaEmision_CheckedChanged);
            // 
            // cbxEmisor
            // 
            this.cbxEmisor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEmisor.FormattingEnabled = true;
            this.cbxEmisor.Location = new System.Drawing.Point(357, 25);
            this.cbxEmisor.Name = "cbxEmisor";
            this.cbxEmisor.Size = new System.Drawing.Size(202, 21);
            this.cbxEmisor.TabIndex = 10;
            this.cbxEmisor.SelectedIndexChanged += new System.EventHandler(this.cbxEmisor_SelectedIndexChanged);
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(76, 25);
            this.txtNumero.MaxLength = 16;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(202, 20);
            this.txtNumero.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Emisor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 28);
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
            this.btnBuscarClie.Click += new System.EventHandler(this.btnBuscarClie_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(60, 28);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(397, 20);
            this.txtCliente.TabIndex = 7;
            this.txtCliente.TextChanged += new System.EventHandler(this.txtCliente_TextChanged);
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
            this.btnLimpiar.Location = new System.Drawing.Point(17, 334);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(80, 28);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(514, 335);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 28);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 369);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(577, 164);
            this.dataGridView1.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(103, 342);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Estado Busqueda:";
            // 
            // lblEstadoBusqueda
            // 
            this.lblEstadoBusqueda.AutoSize = true;
            this.lblEstadoBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoBusqueda.ForeColor = System.Drawing.Color.Red;
            this.lblEstadoBusqueda.Location = new System.Drawing.Point(219, 342);
            this.lblEstadoBusqueda.Name = "lblEstadoBusqueda";
            this.lblEstadoBusqueda.Size = new System.Drawing.Size(0, 13);
            this.lblEstadoBusqueda.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnModificar);
            this.flowLayoutPanel1.Controls.Add(this.btnDesasociar);
            this.flowLayoutPanel1.Controls.Add(this.btnAsociar);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(336, 544);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(258, 33);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(175, 3);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(80, 28);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnDesasociar
            // 
            this.btnDesasociar.Location = new System.Drawing.Point(89, 3);
            this.btnDesasociar.Name = "btnDesasociar";
            this.btnDesasociar.Size = new System.Drawing.Size(80, 28);
            this.btnDesasociar.TabIndex = 8;
            this.btnDesasociar.Text = "Desasociar";
            this.btnDesasociar.UseVisualStyleBackColor = true;
            this.btnDesasociar.Click += new System.EventHandler(this.btnDesasociar_Click);
            // 
            // btnAsociar
            // 
            this.btnAsociar.Location = new System.Drawing.Point(4, 3);
            this.btnAsociar.Name = "btnAsociar";
            this.btnAsociar.Size = new System.Drawing.Size(79, 28);
            this.btnAsociar.TabIndex = 10;
            this.btnAsociar.Text = "Asociar";
            this.btnAsociar.UseVisualStyleBackColor = true;
            this.btnAsociar.Click += new System.EventHandler(this.btnAsociar_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnVolver);
            this.flowLayoutPanel2.Controls.Add(this.btnAceptar);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 544);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(175, 33);
            this.flowLayoutPanel2.TabIndex = 12;
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
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FormBuscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(606, 589);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblEstadoBusqueda);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FormBuscar";
            this.Text = "Tarjeta - Buscar";
            this.Load += new System.EventHandler(this.FormBuscar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBuscarClie;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxEmisor;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpFechaEmisionHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaEmisionDesde;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFechaEmision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimientoHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimientoDesde;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkFechaVencimiento;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblEstadoBusqueda;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnDesasociar;
        private System.Windows.Forms.Button btnAsociar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAceptar;
    }
}