using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UHFDemo
{
    public partial class Form1 : Form
    {
        private Reader.ReaderMethod reader;
        public Form1(string nombre)
        {

            InitializeComponent();
            label2.Text = nombre;

        }

        public Form1()
        {
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            btninicio_Click(null, e);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = true;
        }

        private void btnrptventa_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            AbrirFormHija(new ReporteUsuario());
        }

        private void btnrptcompra_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            AbrirFormHija(new ReporteProducto());
        }

        private void btnrptpagos_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            AbrirFormHija(new r_S_Pcs());
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
            login cerrado = new login();
            cerrado.Show();

        }
        private void AbrirFormHija(object formhija)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();

        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            AbrirFormHija(R2000UartDemo.Instances());
            //AbrirFormHija(new R2000UartDemo());
        }

        private void btninicio_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new inicio());
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            AbrirFormHija(new categoria());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new productos());
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new inicio());
        }

        private void BTNventas_Click(object sender, EventArgs e)
        {
           // AbrirFormHija(new R2000UartDemo());

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new proveedores());
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PanelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            //AbrirFormHija(salida_de_productos.InstanceS());
        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
            AbrirFormHija(listado.InstanceL());
        }
    }
}
