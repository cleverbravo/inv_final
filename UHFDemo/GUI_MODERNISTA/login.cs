using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace GUI_MODERNISTA
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
        public void loguear(string usuario, string contraseña)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT nombre, tipo_usuario FROM usuarios WHERE usuario = @usuario AND contraseña = @contraseña", con);
                cmd.Parameters.AddWithValue("usuario", usuario);
                cmd.Parameters.AddWithValue("contraseña", contraseña);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    if (dt.Rows[0][1].ToString() == "administrador")
                    {
                        new Form1(dt.Rows[0][0].ToString()).Show();
                    }
                    else if (dt.Rows[0][1].ToString() == "usuario")
                    {
                        new Form1(dt.Rows[0][0].ToString()).Show();
                    }

                }
                else
                {
                    MessageBox.Show("Usuario Y/O contraseña incorrecta");
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
            if (textBox1.Text == "Ingrese su Contraseña")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.LightGray;
                textBox1.UseSystemPasswordChar = true;
            }
        }

        private void TextBox1_Leave_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Ingrese su Contraseña";
                textBox1.ForeColor = Color.Silver;
                textBox1.UseSystemPasswordChar = false;
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
            loguear(this.txtuser.Text, this.textBox1.Text);

        }
    }
}
