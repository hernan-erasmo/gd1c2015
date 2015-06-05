namespace PagoElectronico
{
    partial class MenuPrincipal
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
            this.lklCerrarSesion = new System.Windows.Forms.LinkLabel();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lklLogin = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnABMRol = new System.Windows.Forms.Button();
            this.btnABMCliente = new System.Windows.Forms.Button();
            this.btnABMCuenta = new System.Windows.Forms.Button();
            this.btnABMTarjeta = new System.Windows.Forms.Button();
            this.btnDepositos = new System.Windows.Forms.Button();
            this.btnRetiros = new System.Windows.Forms.Button();
            this.btnTransferencias = new System.Windows.Forms.Button();
            this.btnConsultaSaldos = new System.Windows.Forms.Button();
            this.btnListados = new System.Windows.Forms.Button();
            this.btnFacturacion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lklCerrarSesion
            // 
            this.lklCerrarSesion.AutoSize = true;
            this.lklCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lklCerrarSesion.Location = new System.Drawing.Point(400, 9);
            this.lklCerrarSesion.Name = "lklCerrarSesion";
            this.lklCerrarSesion.Size = new System.Drawing.Size(83, 13);
            this.lklCerrarSesion.TabIndex = 33;
            this.lklCerrarSesion.TabStop = true;
            this.lklCerrarSesion.Text = "Cerrar Sesion";
            this.lklCerrarSesion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklCerrarSesion_LinkClicked);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Location = new System.Drawing.Point(12, 9);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(54, 13);
            this.lblLogin.TabIndex = 34;
            this.lblLogin.Text = "Usuario:";
            // 
            // lklLogin
            // 
            this.lklLogin.AutoSize = true;
            this.lklLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lklLogin.Location = new System.Drawing.Point(72, 9);
            this.lklLogin.Name = "lklLogin";
            this.lklLogin.Size = new System.Drawing.Size(87, 13);
            this.lklLogin.TabIndex = 41;
            this.lklLogin.TabStop = true;
            this.lklLogin.Text = "nombreCliente";
            this.lklLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklLogin_LinkClicked);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnABMRol);
            this.flowLayoutPanel2.Controls.Add(this.btnABMCliente);
            this.flowLayoutPanel2.Controls.Add(this.btnABMCuenta);
            this.flowLayoutPanel2.Controls.Add(this.btnABMTarjeta);
            this.flowLayoutPanel2.Controls.Add(this.btnDepositos);
            this.flowLayoutPanel2.Controls.Add(this.btnRetiros);
            this.flowLayoutPanel2.Controls.Add(this.btnTransferencias);
            this.flowLayoutPanel2.Controls.Add(this.btnConsultaSaldos);
            this.flowLayoutPanel2.Controls.Add(this.btnListados);
            this.flowLayoutPanel2.Controls.Add(this.btnFacturacion);
            this.flowLayoutPanel2.Controls.Add(this.btnSalir);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 43);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(471, 123);
            this.flowLayoutPanel2.TabIndex = 44;
            // 
            // btnABMRol
            // 
            this.btnABMRol.Location = new System.Drawing.Point(3, 3);
            this.btnABMRol.Name = "btnABMRol";
            this.btnABMRol.Size = new System.Drawing.Size(111, 34);
            this.btnABMRol.TabIndex = 40;
            this.btnABMRol.Text = "ABM Rol";
            this.btnABMRol.UseVisualStyleBackColor = true;
            this.btnABMRol.Click += new System.EventHandler(this.btnABMRol_Click);
            // 
            // btnABMCliente
            // 
            this.btnABMCliente.Location = new System.Drawing.Point(120, 3);
            this.btnABMCliente.Name = "btnABMCliente";
            this.btnABMCliente.Size = new System.Drawing.Size(111, 34);
            this.btnABMCliente.TabIndex = 38;
            this.btnABMCliente.Text = "ABM Cliente";
            this.btnABMCliente.UseVisualStyleBackColor = true;
            this.btnABMCliente.Click += new System.EventHandler(this.btnABMCliente_Click);
            // 
            // btnABMCuenta
            // 
            this.btnABMCuenta.Location = new System.Drawing.Point(237, 3);
            this.btnABMCuenta.Name = "btnABMCuenta";
            this.btnABMCuenta.Size = new System.Drawing.Size(111, 34);
            this.btnABMCuenta.TabIndex = 39;
            this.btnABMCuenta.Text = "ABM Cuenta";
            this.btnABMCuenta.UseVisualStyleBackColor = true;
            this.btnABMCuenta.Click += new System.EventHandler(this.btnABMCuenta_Click);
            // 
            // btnABMTarjeta
            // 
            this.btnABMTarjeta.Location = new System.Drawing.Point(354, 3);
            this.btnABMTarjeta.Name = "btnABMTarjeta";
            this.btnABMTarjeta.Size = new System.Drawing.Size(111, 34);
            this.btnABMTarjeta.TabIndex = 47;
            this.btnABMTarjeta.Text = "ABM Tarjetas";
            this.btnABMTarjeta.UseVisualStyleBackColor = true;
            this.btnABMTarjeta.Click += new System.EventHandler(this.btnABMTarjeta_Click);
            // 
            // btnDepositos
            // 
            this.btnDepositos.Location = new System.Drawing.Point(3, 43);
            this.btnDepositos.Name = "btnDepositos";
            this.btnDepositos.Size = new System.Drawing.Size(111, 34);
            this.btnDepositos.TabIndex = 42;
            this.btnDepositos.Text = "Depositos";
            this.btnDepositos.UseVisualStyleBackColor = true;
            this.btnDepositos.Click += new System.EventHandler(this.btnDepositos_Click);
            // 
            // btnRetiros
            // 
            this.btnRetiros.Location = new System.Drawing.Point(120, 43);
            this.btnRetiros.Name = "btnRetiros";
            this.btnRetiros.Size = new System.Drawing.Size(111, 34);
            this.btnRetiros.TabIndex = 45;
            this.btnRetiros.Text = "Retiros";
            this.btnRetiros.UseVisualStyleBackColor = true;
            this.btnRetiros.Click += new System.EventHandler(this.btnRetiros_Click);
            // 
            // btnTransferencias
            // 
            this.btnTransferencias.Location = new System.Drawing.Point(237, 43);
            this.btnTransferencias.Name = "btnTransferencias";
            this.btnTransferencias.Size = new System.Drawing.Size(111, 34);
            this.btnTransferencias.TabIndex = 46;
            this.btnTransferencias.Text = "Transferencias";
            this.btnTransferencias.UseVisualStyleBackColor = true;
            this.btnTransferencias.Click += new System.EventHandler(this.btnTransferencias_Click);
            // 
            // btnConsultaSaldos
            // 
            this.btnConsultaSaldos.Location = new System.Drawing.Point(354, 43);
            this.btnConsultaSaldos.Name = "btnConsultaSaldos";
            this.btnConsultaSaldos.Size = new System.Drawing.Size(111, 34);
            this.btnConsultaSaldos.TabIndex = 41;
            this.btnConsultaSaldos.Text = "Consulta Saldos";
            this.btnConsultaSaldos.UseVisualStyleBackColor = true;
            // 
            // btnListados
            // 
            this.btnListados.Location = new System.Drawing.Point(3, 83);
            this.btnListados.Name = "btnListados";
            this.btnListados.Size = new System.Drawing.Size(111, 34);
            this.btnListados.TabIndex = 44;
            this.btnListados.Text = "Listados";
            this.btnListados.UseVisualStyleBackColor = true;
            // 
            // btnFacturacion
            // 
            this.btnFacturacion.Location = new System.Drawing.Point(120, 83);
            this.btnFacturacion.Name = "btnFacturacion";
            this.btnFacturacion.Size = new System.Drawing.Size(111, 34);
            this.btnFacturacion.TabIndex = 43;
            this.btnFacturacion.Text = "Facturacion";
            this.btnFacturacion.UseVisualStyleBackColor = true;
            this.btnFacturacion.Click += new System.EventHandler(this.btnFacturacion_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(237, 83);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(111, 34);
            this.btnSalir.TabIndex = 48;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(494, 175);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.lklLogin);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lklCerrarSesion);
            this.MaximizeBox = false;
            this.Name = "MenuPrincipal";
            this.Text = "Pago Electronico";
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lklCerrarSesion;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.LinkLabel lklLogin;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnABMTarjeta;
        private System.Windows.Forms.Button btnTransferencias;
        private System.Windows.Forms.Button btnRetiros;
        private System.Windows.Forms.Button btnListados;
        private System.Windows.Forms.Button btnFacturacion;
        private System.Windows.Forms.Button btnDepositos;
        private System.Windows.Forms.Button btnConsultaSaldos;
        private System.Windows.Forms.Button btnABMRol;
        private System.Windows.Forms.Button btnABMCuenta;
        private System.Windows.Forms.Button btnABMCliente;
        private System.Windows.Forms.Button btnSalir;
    }
}

