namespace PagoElectronico.ABM_Rol
{
    partial class ABM_Rol
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
            this.Crear_Rol = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Crear_Rol
            // 
            this.Crear_Rol.Location = new System.Drawing.Point(73, 29);
            this.Crear_Rol.Name = "Crear_Rol";
            this.Crear_Rol.Size = new System.Drawing.Size(121, 46);
            this.Crear_Rol.TabIndex = 0;
            this.Crear_Rol.Text = "Crear Rol";
            this.Crear_Rol.UseVisualStyleBackColor = true;
            this.Crear_Rol.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(73, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Modificar/Eliminar Rol";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(73, 170);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 43);
            this.button4.TabIndex = 3;
            this.button4.Text = "Volver";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ABM_Rol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 241);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Crear_Rol);
            this.Name = "ABM_Rol";
            this.Text = "ABM Rol";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Crear_Rol;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}