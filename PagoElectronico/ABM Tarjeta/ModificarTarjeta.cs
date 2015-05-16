﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Tarjeta
{
    public partial class ModificarTarjeta : Form
    {
        public Form formPadre;
        
        public ModificarTarjeta()
        {
            InitializeComponent();
        }
        
        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }


        private void ModificarTarjeta_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.formPadre.Show();
        }
    }
}