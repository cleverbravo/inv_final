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
    public partial class proveedores : Form
    {
        cn_usuario objetoCN = new cn_usuario();
        private string idProv = null;
        private bool Editar = false;
        public proveedores()
        {
            InitializeComponent();
        }
        
        private void MostrarProveedor()
        {

            cn_usuario objeto = new cn_usuario();
            dataGridView1.DataSource = objeto.MostrarProveedor();
        }
        private void Borrar_mensaje_error()
        {
            errorProvider1.SetError(textBox1, "");

        }
        private bool validar_campos()
        {
            bool ok = true;
            if (textBox1.Text == "")
            {
                ok = false;
                errorProvider1.SetError(textBox1, "imgrese una proveedor");
            }
            return ok;
        }
        private void limpiarForm()
        {
            textBox1.Clear();

        }
        private void Button4_Click(object sender, EventArgs e)
        {
            //INSERTAR
            Borrar_mensaje_error();
            if (validar_campos())
            {
                if (Editar == false)
                {
                    try
                    {
                        objetoCN.InsertarProveedor(textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text,comboBox1.Text);
                        MessageBox.Show("se inserto correctamente");
                        MostrarProveedor();
                        limpiarForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("no se pudo insertar los datos por: " + ex);
                    }
                }
                //EDITAR
                if (Editar == true)
                {

                    try
                    {
                        objetoCN.ActualizarProveedor(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.Text, Convert.ToInt32(idProv));
                        MessageBox.Show("se edito correctamente");
                        MostrarProveedor();
                        limpiarForm();
                        Editar = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("no se pudo editar los datos por: " + ex);
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                textBox1.Text = dataGridView1.CurrentRow.Cells["nom_proveedor"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["direccion_prov"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["telefono"].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["url"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells["correo"].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells["pais"].Value.ToString();
                idProv = dataGridView1.CurrentRow.Cells["id_proveedor"].Value.ToString();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    idProv = dataGridView1.CurrentRow.Cells["id_proveedor"].Value.ToString();
                    objetoCN.EliminarProveedor(idProv);
                    MessageBox.Show("Eliminado correctamente");
                    MostrarProveedor();
                }
                else
                    MessageBox.Show("seleccione una fila por favor");
            }
            catch (Exception ex)
            {
                MessageBox.Show("existe un producto registrado con esta categoria no puede eliminarla");
            }
        }

        private void Buscador_TextChanged(object sender, EventArgs e)
        {

            var aux = new metodo();
            aux.filtrarProveedor(dataGridView1, this.buscador.Text.Trim());
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            MostrarProveedor();
        }
    }
}
