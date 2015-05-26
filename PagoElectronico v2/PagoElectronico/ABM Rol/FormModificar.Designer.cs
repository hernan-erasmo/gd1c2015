namespace PagoElectronico.ABM_Rol
{
    partial class FormModificar
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbxFunciones = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(100, 376);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 35);
            this.btnGuardar.TabIndex = 30;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(9, 376);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(85, 35);
            this.btnVolver.TabIndex = 29;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Nombre:";
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Location = new System.Drawing.Point(29, 62);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(73, 17);
            this.chkHabilitado.TabIndex = 26;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(29, 25);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(141, 20);
            this.txtRol.TabIndex = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbxFunciones);
            this.groupBox2.Location = new System.Drawing.Point(23, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 263);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FUNCIONES";
            // 
            // lbxFunciones
            // 
            this.lbxFunciones.FormattingEnabled = true;
            this.lbxFunciones.Location = new System.Drawing.Point(6, 22);
            this.lbxFunciones.Name = "lbxFunciones";
            this.lbxFunciones.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxFunciones.Size = new System.Drawing.Size(141, 238);
            this.lbxFunciones.TabIndex = 5;
            // 
            // FormModificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(195, 421);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkHabilitado);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "FormModificar";
            this.Text = "Rol - Modificar";
            this.Load += new System.EventHandler(this.FormModificar_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbxFunciones;
    }
}