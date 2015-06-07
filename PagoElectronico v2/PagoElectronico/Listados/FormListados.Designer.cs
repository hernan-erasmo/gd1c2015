namespace PagoElectronico.Listados
{
    partial class FormListados
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbxConsulta = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxTrimestre = new System.Windows.Forms.ComboBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtAño = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 71);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(713, 209);
            this.dataGridView1.TabIndex = 7;
            // 
            // cbxConsulta
            // 
            this.cbxConsulta.FormattingEnabled = true;
            this.cbxConsulta.Items.AddRange(new object[] {
            "Consulta 1 (1)",
            "Consulta 2 (2)",
            "Consulta 3 (3)",
            "Consulta 4 (4)",
            "Consulta 5 (5)"});
            this.cbxConsulta.Location = new System.Drawing.Point(282, 42);
            this.cbxConsulta.Name = "cbxConsulta";
            this.cbxConsulta.Size = new System.Drawing.Size(83, 21);
            this.cbxConsulta.TabIndex = 10;
            this.cbxConsulta.SelectedIndexChanged += new System.EventHandler(this.cbxConsulta_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Año";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Trimestre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Consulta";
            // 
            // cbxTrimestre
            // 
            this.cbxTrimestre.FormattingEnabled = true;
            this.cbxTrimestre.Items.AddRange(new object[] {
            "primero (1)",
            "segundo (2)",
            "tercero (3)"});
            this.cbxTrimestre.Location = new System.Drawing.Point(151, 41);
            this.cbxTrimestre.Name = "cbxTrimestre";
            this.cbxTrimestre.Size = new System.Drawing.Size(54, 21);
            this.cbxTrimestre.TabIndex = 15;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 292);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(80, 28);
            this.btnVolver.TabIndex = 16;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(425, 37);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 28);
            this.btnBuscar.TabIndex = 17;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 13);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(41, 13);
            this.lblInfo.TabIndex = 18;
            this.lblInfo.Text = "label4";
            // 
            // txtAño
            // 
            this.txtAño.Location = new System.Drawing.Point(44, 42);
            this.txtAño.MaxLength = 4;
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(40, 20);
            this.txtAño.TabIndex = 19;
            // 
            // FormListados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 328);
            this.Controls.Add(this.txtAño);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.cbxTrimestre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxConsulta);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "FormListados";
            this.Text = "Listados Estadísticos";
            this.Load += new System.EventHandler(this.FormListado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbxConsulta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxTrimestre;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtAño;

    }
}