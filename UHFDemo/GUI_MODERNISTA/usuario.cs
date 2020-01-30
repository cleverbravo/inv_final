using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_MODERNISTA
{
    public partial class productos : Form
    {
        public productos()
        {
            InitializeComponent();
        }

        private void Buscador_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = operaciones.Mostrar();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = operaciones.Mostrar();
        }
    }
}
