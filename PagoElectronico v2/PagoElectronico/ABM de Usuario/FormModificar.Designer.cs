namespace PagoElectronico.ABM_de_Usuario
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtRespuestaSec = new System.Windows.Forms.TextBox();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.txtPreguntaSec = new System.Windows.Forms.TextBox();
            this.lblRespuesta = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.lbxRoles = new System.Windows.Forms.ListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.lblUsuario);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtRespuestaSec);
            this.groupBox1.Controls.Add(this.lblPregunta);
            this.groupBox1.Controls.Add(this.txtPreguntaSec);
            this.groupBox1.Controls.Add(this.lblRespuesta);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 203);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Infomación del Usuario";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(14, 36);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(14, 70);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(129, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(191, 20);
            this.txtPassword.TabIndex = 9;
            // 
            // txtRespuestaSec
            // 
            this.txtRespuestaSec.Location = new System.Drawing.Point(130, 147);
            this.txtRespuestaSec.Name = "txtRespuestaSec";
            this.txtRespuestaSec.PasswordChar = '*';
            this.txtRespuestaSec.Size = new System.Drawing.Size(190, 20);
            this.txtRespuestaSec.TabIndex = 8;
            // 
            // lblPregunta
            // 
            this.lblPregunta.AutoSize = true;
            this.lblPregunta.Location = new System.Drawing.Point(14, 111);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(88, 13);
            this.lblPregunta.TabIndex = 4;
            this.lblPregunta.Text = "Pregunta secreta";
            // 
            // txtPreguntaSec
            // 
            this.txtPreguntaSec.Location = new System.Drawing.Point(130, 108);
            this.txtPreguntaSec.Name = "txtPreguntaSec";
            this.txtPreguntaSec.Size = new System.Drawing.Size(190, 20);
            this.txtPreguntaSec.TabIndex = 7;
            // 
            // lblRespuesta
            // 
            this.lblRespuesta.AutoSize = true;
            this.lblRespuesta.Location = new System.Drawing.Point(14, 150);
            this.lblRespuesta.Name = "lblRespuesta";
            this.lblRespuesta.Size = new System.Drawing.Size(96, 13);
            this.lblRespuesta.TabIndex = 5;
            this.lblRespuesta.Text = "Respuesta secreta";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(129, 33);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(191, 20);
            this.txtUsuario.TabIndex = 6;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 384);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(88, 29);
            this.btnVolver.TabIndex = 13;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.AutoEllipsis = true;
            this.btnModificar.Location = new System.Drawing.Point(230, 384);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(102, 28);
            this.btnModificar.TabIndex = 14;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.lbxRoles);
            this.groupBox.Location = new System.Drawing.Point(12, 221);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(336, 142);
            this.groupBox.TabIndex = 28;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Roles";
            // 
            // lbxRoles
            // 
            this.lbxRoles.FormattingEnabled = true;
            this.lbxRoles.Location = new System.Drawing.Point(10, 19);
            this.lbxRoles.Name = "lbxRoles";
            this.lbxRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxRoles.Size = new System.Drawing.Size(314, 108);
            this.lbxRoles.TabIndex = 5;
            this.lbxRoles.SelectedIndexChanged += new System.EventHandler(this.lbxRoles_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 180);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(73, 17);
            this.checkBox1.TabIndex = 29;
            this.checkBox1.Text = "Habilitado";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FormModificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(356, 425);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FormModificar";
            this.Text = "Usuario - Modificar";
            this.Load += new System.EventHandler(this.FormModificar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtRespuestaSec;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.TextBox txtPreguntaSec;
        private System.Windows.Forms.Label lblRespuesta;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.ListBox lbxRoles;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}