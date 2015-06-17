namespace PagoElectronico.ABM_de_Usuario
{
    partial class FormModificarPass
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
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPasswordNuevo = new System.Windows.Forms.TextBox();
            this.lklInfoLogin = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario: ";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 46);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(89, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password nuevo:";
            // 
            // btnCambiar
            // 
            this.btnCambiar.Location = new System.Drawing.Point(236, 81);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(61, 28);
            this.btnCambiar.TabIndex = 3;
            this.btnCambiar.Text = "Cambiar";
            this.btnCambiar.UseVisualStyleBackColor = true;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // txtPasswordNuevo
            // 
            this.txtPasswordNuevo.Location = new System.Drawing.Point(114, 43);
            this.txtPasswordNuevo.Name = "txtPasswordNuevo";
            this.txtPasswordNuevo.PasswordChar = '*';
            this.txtPasswordNuevo.Size = new System.Drawing.Size(170, 20);
            this.txtPasswordNuevo.TabIndex = 6;
            // 
            // lklInfoLogin
            // 
            this.lklInfoLogin.AutoSize = true;
            this.lklInfoLogin.Location = new System.Drawing.Point(12, 89);
            this.lklInfoLogin.Name = "lklInfoLogin";
            this.lklInfoLogin.Size = new System.Drawing.Size(110, 13);
            this.lklInfoLogin.TabIndex = 7;
            this.lklInfoLogin.TabStop = true;
            this.lklInfoLogin.Text = "Información de sesion";
            this.lklInfoLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklInfoLogin_LinkClicked);
            // 
            // FormModificarPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(309, 122);
            this.Controls.Add(this.lklInfoLogin);
            this.Controls.Add(this.txtPasswordNuevo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCambiar);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModificarPass";
            this.Text = "Usuario - Cambiar Password";
            this.Load += new System.EventHandler(this.FormModificarPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnCambiar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPasswordNuevo;
        private System.Windows.Forms.LinkLabel lklInfoLogin;
    }
}