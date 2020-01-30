using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UHFDemo
{
    
    public partial class productos : Form
    {
        
        
       cn_usuario objetoCN = new cn_usuario();
        //private string idUser ;
        private string idUser = null;
        private bool Editar = false;
        public productos()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();

        private void Buscador_TextChanged(object sender, EventArgs e)
        {
            var aux = new metodo();
            aux.filtrar(dataGridView1, this.buscador.Text.Trim());
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void MostrarProdctos()
        {

            cn_usuario objeto = new cn_usuario();
            dataGridView1.DataSource = objeto.MostrarUser();
        }

        private void productos_Load(object sender, EventArgs e)
        {
            string id = System.Guid.NewGuid().ToString();
            textBox5.Text = id;
            MostrarProdctos();
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void Borrar_mensaje_error()
        {
            errorProvider1.SetError(textBox1, "");
            errorProvider1.SetError(textBox2, "");
            errorProvider1.SetError(textBox3, "");
            errorProvider1.SetError(textBox4, "");
           

        }
        private bool validar_campos()
        {
            bool ok = true;
            if (textBox1.Text == "")
            {
                ok = false;
                errorProvider1.SetError(textBox1, "ingrese un nombre");
            }
            if (textBox4.Text == "")
            {
                ok = false;
                errorProvider1.SetError(textBox4, "ingrese un apellido");
            }
            if (textBox2.Text == "")
            {
                ok = false;
                errorProvider1.SetError(textBox2, "ingrese un usuario");
            }
            if (textBox3.Text == "")
            {
                ok = false;
                errorProvider1.SetError(textBox3, "ingrese un password");
            }
           

            return ok;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            
               
                //INSERTAR
                Borrar_mensaje_error();
                if (validar_campos())
                {
                    if (Editar == false)
                    {
                        try
                        {
                            
                            objetoCN.InsertarUsuario(textBox5.Text, textBox1.Text, textBox4.Text, textBox2.Text, textBox3.Text, comboBox1.Text);
                            MessageBox.Show("se inserto correctamente");
                            MostrarProdctos();
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
                           
                            objetoCN.EditarUsuario(textBox1.Text, textBox4.Text, textBox2.Text,textBox3.Text, comboBox1.Text, idUser);
                            MessageBox.Show("se edito correctamente");
                            MostrarProdctos();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                textBox1.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["apellido"].Value.ToString();
                textBox2.Text= dataGridView1.CurrentRow.Cells["usuario"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["contraseña"].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells["estado"].Value.ToString();
                idUser = dataGridView1.CurrentRow.Cells["id_usuario"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells["id_usuario"].Value.ToString();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }
        private void limpiarForm()
        {
            textBox1.Clear();
            textBox4.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            string id = System.Guid.NewGuid().ToString();
            textBox5.Text = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                idUser= dataGridView1.CurrentRow.Cells["id_usuario"].Value.ToString();
                objetoCN.EliminarUsuario(idUser);
                MessageBox.Show("Eliminado correctamente");
                MostrarProdctos();
            }
            else
                MessageBox.Show("seleccione una fila por favor");

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
