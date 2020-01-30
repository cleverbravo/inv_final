using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UHFDemo
{
    public partial class r_S_Pcs : Form
    {
        public r_S_Pcs()
        {
            InitializeComponent();
        }

        private void R_S_Pcs_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetUsuario.salida_producto' Puede moverla o quitarla según sea necesario.
            this.salida_productoTableAdapter.Fill(this.DataSetUsuario.salida_producto);

            this.reportViewer1.RefreshReport();
        }
    }
}
