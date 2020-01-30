using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UHFDemo
{
    public partial class categoria : Form
    {
        cn_usuario objetoCN = new cn_usuario();
        private string idCat = null;
        private string nombre = null;
        private bool Editar = false;
        public categoria()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void MostrarCategoria()
        {

            cn_usuario objeto = new cn_usuario();
            dataGridView1.DataSource = objeto.MostrarCategoria();
        }
        private void Borrar_mensaje_error()
        {
            errorProvider1.SetError(textBox1, "");

        }
        private void no_repetir_categoria()
        {
            
        }
        private void categoria_Load(object sender, EventArgs e)
        {
            MostrarCategoria();
        }
        private bool validar_campos()
        {
            bool ok = true;
            if (textBox1.Text =="") {
                ok = false;
                errorProvider1.SetError(textBox1, "Ingrese una categoria");
            }
            return ok;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //INSERTAR
            string mensaje = "";
            Borrar_mensaje_error();
            if (validar_campos())
            {
                if (Editar == false)
                {
                    try
                    {
                        if (mensaje == "")
                        {
                            objetoCN.InsertarC(textBox1.Text,"");
                            MessageBox.Show("se inserto correctamente");
                            MostrarCategoria();
                            limpiarForm();
                        }
                        else {
                            MessageBox.Show("la categoria ya existe");
                        }
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
                        objetoCN.EditarC(textBox1.Text, Convert.ToInt32(idCat));
                        MessageBox.Show("se edito correctamente");
                        MostrarCategoria();
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
                textBox1.Text = dataGridView1.CurrentRow.Cells["nombre_categ"].Value.ToString();
                idCat = dataGridView1.CurrentRow.Cells["id_categoria"].Value.ToString();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }
        private void limpiarForm()
        {
            textBox1.Clear();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    idCat = dataGridView1.CurrentRow.Cells["id_categoria"].Value.ToString();
                    objetoCN.EliminarC(idCat);
                    MessageBox.Show("Eliminado correctamente");
                    MostrarCategoria();
                }
                else
                    MessageBox.Show("seleccione una fila por favor");
            }
            catch (Exception ex) {
                MessageBox.Show("existe un producto registrado con esta categoria no puede eliminarla");
            } 
        }

        private void buscador_TextChanged(object sender, EventArgs e)
        {
            var aux = new metodo();
            aux.filtrarC(dataGridView1, this.buscador.Text.Trim());
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
