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
            this.lblPassNuevo = new System.Windows.Forms.Label();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPasswordNuevo = new System.Windows.Forms.TextBox();
            this.lklInfoLogin = new System.Windows.Forms.LinkLabel();
            this.txtPasswordNuevoVal = new System.Windows.Forms.TextBox();
            this.lblPassNuevoVal = new System.Windows.Forms.Label();
            this.txtPasswordActual = new System.Windows.Forms.TextBox();
            this.lblPassActual = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPassNuevo
            // 
            this.lblPassNuevo.AutoSize = true;
            this.lblPassNuevo.Location = new System.Drawing.Point(3, 78);
            this.lblPassNuevo.Name = "lblPassNuevo";
            this.lblPassNuevo.Size = new System.Drawing.Size(89, 13);
            this.lblPassNuevo.TabIndex = 2;
            this.lblPassNuevo.Text = "Password nuevo:";
            // 
            // btnCambiar
            // 
            this.btnCambiar.Location = new System.Drawing.Point(238, 143);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(61, 28);
            this.btnCambiar.TabIndex = 4;
            this.btnCambiar.Text = "Cambiar";
            this.btnCambiar.UseVisualStyleBackColor = true;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // txtPasswordNuevo
            // 
            this.txtPasswordNuevo.Location = new System.Drawing.Point(125, 75);
            this.txtPasswordNuevo.Name = "txtPasswordNuevo";
            this.txtPasswordNuevo.PasswordChar = '*';
            this.txtPasswordNuevo.Size = new System.Drawing.Size(170, 20);
            this.txtPasswordNuevo.TabIndex = 2;
            // 
            // lklInfoLogin
            // 
            this.lklInfoLogin.AutoSize = true;
            this.lklInfoLogin.Location = new System.Drawing.Point(3, 9);
            this.lklInfoLogin.Name = "lklInfoLogin";
            this.lklInfoLogin.Size = new System.Drawing.Size(164, 13);
            this.lklInfoLogin.TabIndex = 0;
            this.lklInfoLogin.TabStop = true;
            this.lklInfoLogin.Text = "Información de sesion del usuario";
            this.lklInfoLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklInfoLogin_LinkClicked);
            // 
            // txtPasswordNuevoVal
            // 
            this.txtPasswordNuevoVal.Location = new System.Drawing.Point(125, 110);
            this.txtPasswordNuevoVal.Name = "txtPasswordNuevoVal";
            this.txtPasswordNuevoVal.PasswordChar = '*';
            this.txtPasswordNuevoVal.Size = new System.Drawing.Size(170, 20);
            this.txtPasswordNuevoVal.TabIndex = 3;
            // 
            // lblPassNuevoVal
            // 
            this.lblPassNuevoVal.AutoSize = true;
            this.lblPassNuevoVal.Location = new System.Drawing.Point(3, 113);
            this.lblPassNuevoVal.Name = "lblPassNuevoVal";
            this.lblPassNuevoVal.Size = new System.Drawing.Size(124, 13);
            this.lblPassNuevoVal.TabIndex = 8;
            this.lblPassNuevoVal.Text = "Validar Password nuevo:";
            // 
            // txtPasswordActual
            // 
            this.txtPasswordActual.Location = new System.Drawing.Point(125, 38);
            this.txtPasswordActual.Name = "txtPasswordActual";
            this.txtPasswordActual.PasswordChar = '*';
            this.txtPasswordActual.Size = new System.Drawing.Size(170, 20);
            this.txtPasswordActual.TabIndex = 1;
            // 
            // lblPassActual
            // 
            this.lblPassActual.AutoSize = true;
            this.lblPassActual.Location = new System.Drawing.Point(3, 41);
            this.lblPassActual.Name = "lblPassActual";
            this.lblPassActual.Size = new System.Drawing.Size(88, 13);
            this.lblPassActual.TabIndex = 10;
            this.lblPassActual.Text = "Password actual:";
            // 
            // FormModificarPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(311, 183);
            this.Controls.Add(this.txtPasswordActual);
            this.Controls.Add(this.lblPassActual);
            this.Controls.Add(this.txtPasswordNuevoVal);
            this.Controls.Add(this.lblPassNuevoVal);
            this.Controls.Add(this.lklInfoLogin);
            this.Controls.Add(this.txtPasswordNuevo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCambiar);
            this.Controls.Add(this.lblPassNuevo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModificarPass";
            this.Text = "Usuario - Cambiar Password";
            this.Load += new System.EventHandler(this.FormModificarPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPassNuevo;
        private System.Windows.Forms.Button btnCambiar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPasswordNuevo;
        private System.Windows.Forms.LinkLabel lklInfoLogin;
        private System.Windows.Forms.TextBox txtPasswordNuevoVal;
        private System.Windows.Forms.Label lblPassNuevoVal;
        private System.Windows.Forms.TextBox txtPasswordActual;
        private System.Windows.Forms.Label lblPassActual;
    }
}