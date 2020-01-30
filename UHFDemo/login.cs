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
    public partial class login : Form
    {
        int contador = 0;
        public login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
        public void loguear(string usuario, string contraseña,string estado)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT nombre FROM usuarios WHERE usuario = @usuario AND contraseña = @contraseña AND estado = @estado", con);
                cmd.Parameters.AddWithValue("usuario", usuario);
                cmd.Parameters.AddWithValue("contraseña", contraseña);
                cmd.Parameters.AddWithValue("estado", estado);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                
                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    new Form1(dt.Rows[0][0].ToString()).Show();

                }
                else
                {
                    if (contador > 4)
                    {
                        MessageBox.Show("Cantidad de intentos superados");
                        Application.Exit();
                    }
                    else
                    { 
                    contador = contador + 1;
                    MessageBox.Show("Usuario Y/O contraseña incorrecta");
                    }
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();

            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }
  
        private void Txtuser_Enter_1(object sender, EventArgs e)
        {
            if (txtuser.Text == "Ingrese su Usuario")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void Txtuser_Leave_1(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "Ingrese su Usuario";
                txtuser.ForeColor = Color.Silver;
            }
        }

        private void TextBox1_Enter_1(object sender, EventArgs e)
        {
            if (txtPass.Text == "Ingrese su Contraseña")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void TextBox1_Leave_1(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Ingrese su Contraseña";
                txtPass.ForeColor = Color.Silver;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Btnlogin_Click(object sender, EventArgs e)
        {
            loguear(this.txtuser.Text, this.txtPass.Text,"Habilitado");

        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) {
                loguear(this.txtuser.Text, this.txtPass.Text, "Habilitado");
            }
        }
    }
}
