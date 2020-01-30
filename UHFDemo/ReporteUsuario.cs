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
    public partial class ReporteUsuario : Form
    {
        public ReporteUsuario()
        {
            InitializeComponent();
        }

        private void ReporteUsuario_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetUsuario.usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.DataSetUsuario.usuarios);

            this.reportViewer1.RefreshReport();
        }
    }
}
