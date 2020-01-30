using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;


namespace GUI_MODERNISTA
{
    class operaciones
    {
        public static SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");

        public static DataSet D=new DataSet();

        public static DataTable Mostrar()
        {

            SqlCommand cmd = new SqlCommand(string.Format("select * from usuarios"), cnx);

            try
            {
                cnx.Open();
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                D = new DataSet();
                DA.Fill(D, "usuarios");
                cnx.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { cnx.Close(); }

            return D.Tables["usuarios"];


        }
        public static void Insertar(usuarios A)
        {

            SqlCommand cmd = new SqlCommand(string.Format("insert into usuarios (id_usuario,nombre,apellido,usuario,contraseña,tipo_usuario ) values ('{0}','{1}','{2}','{3}','{4}','{5}')", A.Id_usuario, A.Nombre,A.Apellido,A.Usuario,A.Contraseña,A.Tipo_usuario), cnx);

            try
            {
                cnx.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registrado");
                cnx.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { cnx.Close(); }




        }
        public static void Eliminar(usuarios A)
        {

            SqlCommand cmd = new SqlCommand(string.Format("Delete from Articulo where idArticulo='{0}'"), cnx);

            try
            {
                cnx.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Eliminado");
                cnx.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { cnx.Close(); }




        }
        public static void Modificar(usuarios A)
        {

            SqlCommand cmd = new SqlCommand(string.Format("Update Articulo set Nombre='{0}', Precio='{1}' where idarticulo='{2}'"), cnx);

            try
            {
                cnx.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Modificado");
                cnx.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { cnx.Close(); }




        }
        public static DataTable Buscar(usuarios A)
        {

            SqlCommand cmd = new SqlCommand(string.Format("select * from Articulo where nombre LIKE '%{0}%'", A.Nombre), cnx);

            try
            {
                cnx.Open();
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                D = new DataSet();
                DA.Fill(D, "Articulo");
                cnx.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { cnx.Close(); }

            return D.Tables["Articulo"];

        }
    }

}
