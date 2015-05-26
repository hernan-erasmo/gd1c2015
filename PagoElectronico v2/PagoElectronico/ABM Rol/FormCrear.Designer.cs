namespace PagoElectronico.ABM_Rol
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
            this.txtRol = new System.Windows.Forms.TextBox();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbxFunciones = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(32, 24);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(141, 20);
            this.txtRol.TabIndex = 4;
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Location = new System.Drawing.Point(32, 61);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(73, 17);
            this.chkHabilitado.TabIndex = 8;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbxFunciones);
            this.groupBox2.Location = new System.Drawing.Point(26, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 263);
            this.groupBox2.TabIndex = 21;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Nombre:";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 375);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(85, 35);
            this.btnVolver.TabIndex = 23;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(103, 375);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(85, 35);
            this.btnAceptar.TabIndex = 24;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FormModificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(202, 422);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkHabilitado);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "FormModificar";
            this.Text = "Rol - Crear";
            this.Load += new System.EventHandler(this.FormCrear_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbxFunciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAceptar;
    }
}