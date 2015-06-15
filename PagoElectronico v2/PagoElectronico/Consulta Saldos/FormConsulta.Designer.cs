namespace PagoElectronico.Consulta_Saldos
{
    partial class FormConsulta
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDepositos = new System.Windows.Forms.DataGridView();
            this.dgvRetiros = new System.Windows.Forms.DataGridView();
            this.dgvTransferencias = new System.Windows.Forms.DataGridView();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvSaldo = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.lklCuenta = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepositos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaldo)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ultimos 5 depósitos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ultimos 5 retiros";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ultimas 10 transferencias";
            // 
            // dgvDepositos
            // 
            this.dgvDepositos.AllowUserToAddRows = false;
            this.dgvDepositos.AllowUserToDeleteRows = false;
            this.dgvDepositos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepositos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepositos.Location = new System.Drawing.Point(12, 73);
            this.dgvDepositos.MultiSelect = false;
            this.dgvDepositos.Name = "dgvDepositos";
            this.dgvDepositos.ReadOnly = true;
            this.dgvDepositos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepositos.Size = new System.Drawing.Size(577, 138);
            this.dgvDepositos.TabIndex = 7;
            // 
            // dgvRetiros
            // 
            this.dgvRetiros.AllowUserToAddRows = false;
            this.dgvRetiros.AllowUserToDeleteRows = false;
            this.dgvRetiros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRetiros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetiros.Location = new System.Drawing.Point(12, 230);
            this.dgvRetiros.MultiSelect = false;
            this.dgvRetiros.Name = "dgvRetiros";
            this.dgvRetiros.ReadOnly = true;
            this.dgvRetiros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRetiros.Size = new System.Drawing.Size(577, 138);
            this.dgvRetiros.TabIndex = 8;
            // 
            // dgvTransferencias
            // 
            this.dgvTransferencias.AllowUserToAddRows = false;
            this.dgvTransferencias.AllowUserToDeleteRows = false;
            this.dgvTransferencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransferencias.Location = new System.Drawing.Point(12, 387);
            this.dgvTransferencias.MultiSelect = false;
            this.dgvTransferencias.Name = "dgvTransferencias";
            this.dgvTransferencias.ReadOnly = true;
            this.dgvTransferencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransferencias.Size = new System.Drawing.Size(577, 200);
            this.dgvTransferencias.TabIndex = 9;
            // 
            // txtCuenta
            // 
            this.txtCuenta.Enabled = false;
            this.txtCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuenta.Location = new System.Drawing.Point(53, 22);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(187, 20);
            this.txtCuenta.TabIndex = 10;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(510, 17);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 28);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Consultar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvSaldo
            // 
            this.dgvSaldo.AllowUserToAddRows = false;
            this.dgvSaldo.AllowUserToDeleteRows = false;
            this.dgvSaldo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaldo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaldo.Location = new System.Drawing.Point(351, 19);
            this.dgvSaldo.MultiSelect = false;
            this.dgvSaldo.Name = "dgvSaldo";
            this.dgvSaldo.ReadOnly = true;
            this.dgvSaldo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSaldo.Size = new System.Drawing.Size(149, 24);
            this.dgvSaldo.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(246, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "SALDO ACTUAL";
            // 
            // lklCuenta
            // 
            this.lklCuenta.AutoSize = true;
            this.lklCuenta.Location = new System.Drawing.Point(9, 25);
            this.lklCuenta.Name = "lklCuenta";
            this.lklCuenta.Size = new System.Drawing.Size(44, 13);
            this.lklCuenta.TabIndex = 15;
            this.lklCuenta.TabStop = true;
            this.lklCuenta.Text = "Cuenta:";
            this.lklCuenta.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklCuenta_LinkClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(504, 602);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 30);
            this.button1.TabIndex = 16;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(602, 640);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lklCuenta);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvSaldo);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtCuenta);
            this.Controls.Add(this.dgvTransferencias);
            this.Controls.Add(this.dgvRetiros);
            this.Controls.Add(this.dgvDepositos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.Name = "FormConsulta";
            this.Text = "Consulta Saldo";
            this.Load += new System.EventHandler(this.FormConsultaSaldo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepositos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaldo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvDepositos;
        private System.Windows.Forms.DataGridView dgvRetiros;
        private System.Windows.Forms.DataGridView dgvTransferencias;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvSaldo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel lklCuenta;
        private System.Windows.Forms.Button button1;
    }
}