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
    public partial class ReporteLista : Form
    {
        public ReporteLista()
        {
            InitializeComponent();
        }

        private void ReporteLista_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetUsuario.Llamarlista' Puede moverla o quitarla según sea necesario.
            this.LlamarlistaTableAdapter.Fill(this.DataSetUsuario.Llamarlista);
            // TODO: esta línea de código carga datos en la tabla 'DataSetUsuario.producto' Puede moverla o quitarla según sea necesario.
            this.productoTableAdapter.Fill(this.DataSetUsuario.producto);

            this.reportViewer1.RefreshReport();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
