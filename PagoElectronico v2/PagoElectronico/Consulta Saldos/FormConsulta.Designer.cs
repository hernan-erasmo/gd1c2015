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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDepositos = new System.Windows.Forms.DataGridView();
            this.dgvRetiros = new System.Windows.Forms.DataGridView();
            this.dgvTransferencias = new System.Windows.Forms.DataGridView();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnBuscarCuenta = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepositos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cuenta:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ultimos 5 depósitos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ultimos 5 retiros";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ultimas 10 transferencias";
            // 
            // dgvDepositos
            // 
            this.dgvDepositos.AllowUserToAddRows = false;
            this.dgvDepositos.AllowUserToDeleteRows = false;
            this.dgvDepositos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepositos.Location = new System.Drawing.Point(15, 101);
            this.dgvDepositos.MultiSelect = false;
            this.dgvDepositos.Name = "dgvDepositos";
            this.dgvDepositos.ReadOnly = true;
            this.dgvDepositos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepositos.Size = new System.Drawing.Size(577, 112);
            this.dgvDepositos.TabIndex = 7;
            // 
            // dgvRetiros
            // 
            this.dgvRetiros.AllowUserToAddRows = false;
            this.dgvRetiros.AllowUserToDeleteRows = false;
            this.dgvRetiros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetiros.Location = new System.Drawing.Point(15, 232);
            this.dgvRetiros.MultiSelect = false;
            this.dgvRetiros.Name = "dgvRetiros";
            this.dgvRetiros.ReadOnly = true;
            this.dgvRetiros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRetiros.Size = new System.Drawing.Size(577, 112);
            this.dgvRetiros.TabIndex = 8;
            // 
            // dgvTransferencias
            // 
            this.dgvTransferencias.AllowUserToAddRows = false;
            this.dgvTransferencias.AllowUserToDeleteRows = false;
            this.dgvTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransferencias.Location = new System.Drawing.Point(15, 363);
            this.dgvTransferencias.MultiSelect = false;
            this.dgvTransferencias.Name = "dgvTransferencias";
            this.dgvTransferencias.ReadOnly = true;
            this.dgvTransferencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransferencias.Size = new System.Drawing.Size(577, 112);
            this.dgvTransferencias.TabIndex = 9;
            this.dgvTransferencias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // txtCuenta
            // 
            this.txtCuenta.Enabled = false;
            this.txtCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuenta.Location = new System.Drawing.Point(72, 34);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(187, 20);
            this.txtCuenta.TabIndex = 10;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(512, 29);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 28);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnBuscarCuenta
            // 
            this.btnBuscarCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarCuenta.Location = new System.Drawing.Point(265, 29);
            this.btnBuscarCuenta.Name = "btnBuscarCuenta";
            this.btnBuscarCuenta.Size = new System.Drawing.Size(23, 28);
            this.btnBuscarCuenta.TabIndex = 12;
            this.btnBuscarCuenta.Text = "...";
            this.btnBuscarCuenta.UseVisualStyleBackColor = true;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(426, 29);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(80, 28);
            this.btnVolver.TabIndex = 13;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 495);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnBuscarCuenta);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtCuenta);
            this.Controls.Add(this.dgvTransferencias);
            this.Controls.Add(this.dgvRetiros);
            this.Controls.Add(this.dgvDepositos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FormConsulta";
            this.Text = "Consulta Saldo";
            this.Load += new System.EventHandler(this.FormConsultaSaldo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepositos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvDepositos;
        private System.Windows.Forms.DataGridView dgvRetiros;
        private System.Windows.Forms.DataGridView dgvTransferencias;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnBuscarCuenta;
        private System.Windows.Forms.Button btnVolver;
    }
}