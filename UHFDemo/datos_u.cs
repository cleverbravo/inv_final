using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace UHFDemo
{
    class datos_u
    {
        private conexion conexion = new conexion();
      
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable Mostrar()
        {

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "MostrarUsuarios";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public DataTable MostrarCategoria()
        {

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "MostrarCategoria";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public DataTable MostrarProveedor()
        {

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "MostrarProveedor";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public void InsertarProveedor(string nom_proveedor, string direccion_prov, string telefono, string url, string correo,string pais)
        {
            //PROCEDIMNIENTO

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "InsetarProveedor";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("nomProveedor", nom_proveedor);
            comando.Parameters.AddWithValue("@direccion", direccion_prov);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@url", url);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.Parameters.AddWithValue("@pais", pais);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }
        public void ActualizarProveedor(string nom_proveedor, string direccion_prov, string telefono, string url, string correo, string pais,int id_proveedor)
        {
            //PROCEDIMNIENTO

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "ActualizarProveedor";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("nomProveedor", nom_proveedor);
            comando.Parameters.AddWithValue("@direccion", direccion_prov);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@url", url);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.Parameters.AddWithValue("@pais", pais);
            comando.Parameters.AddWithValue("@id", id_proveedor);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }
        public void EliminarProveedor(int id_proveedor)
        {

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EliminarProveedor";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@id", id_proveedor);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
        public void Insertar(string id_usuario,string nombre, string apellido, string usuario, string contraseña, string estado)
        {
            //PROCEDIMNIENTO
            
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "InsetarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@id_usuario", id_usuario);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@apellido", apellido);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@contraseña",contraseña);
            comando.Parameters.AddWithValue("@estado", estado);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }
        public void InsertarCategoria(string nombre_categ,string mensaje)
        {
            //PROCEDIMNIENTO
            
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "InsetarCategoria";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre_categ", nombre_categ);
            comando.Parameters.AddWithValue("@Mensaje", "");
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }

        public void Editar(string nombre, string apellido, string usuario, string contraseña,string estado, string id_usuario)
        {
            
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EditarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@apellido", apellido);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            comando.Parameters.AddWithValue("@estado", estado);
            comando.Parameters.AddWithValue("@id_usuario", id_usuario);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
        public void EditarCategoria(string nombre_categ, int id_categoria)
        {

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EditarCategoria";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre_categ", nombre_categ);;
            comando.Parameters.AddWithValue("@id_categ", id_categoria);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

        public void Eliminar(string id_usuario)
        {
            
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EliminarUsuario";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idUser", id_usuario);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
        public void EliminarCategoria(int id_categoria)
        {

            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EliminarCategoria";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@id_cat", id_categoria);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }


        public DataTable ListarProductos()
        {
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.CommandText = "ListarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(Tabla);
            tabla.Load(leer);
            leer.Close();
            conexion.CerrarConexion();
            return tabla;
        }
        
        public DataTable ListarSalidas()
        {
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "ListarSalidas";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            leer.Close();
            conexion.CerrarConexion();
            return tabla;
        }
        public void InsertarSalidas(string id_RFID)
        {
            //PROCEDIMNIENTO
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "AgregarSalidas";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idRFID", id_RFID);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }
        public void InsertarProductoLista(string id_RFID)
        {
            //PROCEDIMNIENTO
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "ListaAProducto";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idRFID", id_RFID);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }
        public void InsertarLista(string id_RFID)
        {
            //PROCEDIMNIENTO
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "AgregarLista";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idRFID", id_RFID);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();

        }
        public void InsertarProductos(string id_RFID, string nombre_p, int id_categoria,int id_proveedor, DateTime fecha_registro, string descripcion, PictureBox foto)
        {
             DataTable Tabla = new DataTable();
             SqlCommand comando = new SqlCommand();
             comando.Connection = conexion.ObtenerConexion();
             comando.CommandText = "AgregarProducto";
             comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idRFID", id_RFID);
             comando.Parameters.AddWithValue("@nomP",nombre_p);
             comando.Parameters.AddWithValue("@idCAT", id_categoria);
            comando.Parameters.AddWithValue("@id_prov", id_proveedor);
            comando.Parameters.AddWithValue("@fechaREG", fecha_registro);
             comando.Parameters.AddWithValue("@descrip", descripcion);
             comando.Parameters.AddWithValue("@foto", foto);

            System.IO.MemoryStream sm = new System.IO.MemoryStream();
            foto.Image.Save(sm, System.Drawing.Imaging.ImageFormat.Jpeg);
            comando.Parameters["@foto"].Value = sm.GetBuffer();



            comando.ExecuteNonQuery();
            
             comando.Parameters.Clear();
         
        }
       public void EditarProductos(string id_RFID, string nombre_p, int id_categoria,int id_proveedor, DateTime fecha_registro, string descripcion,PictureBox foto)
        {
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EditarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idRFID", id_RFID);
            comando.Parameters.AddWithValue("@nomP", nombre_p);
            comando.Parameters.AddWithValue("@idCAT", id_categoria);
            comando.Parameters.AddWithValue("@id_prov", id_proveedor);
            comando.Parameters.AddWithValue("@fechaREG", fecha_registro);
            comando.Parameters.AddWithValue("@descrip", descripcion);
            comando.Parameters.AddWithValue("@foto", foto);

            System.IO.MemoryStream sm = new System.IO.MemoryStream();
            foto.Image.Save(sm, System.Drawing.Imaging.ImageFormat.Jpeg);
            comando.Parameters["@foto"].Value = sm.GetBuffer();
           
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
        public void VerImagen(string id_RFID, PictureBox foto)
        {
            try
            {

                SqlConnection miconexion = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
                miconexion.Open();
                SqlCommand cmd = new SqlCommand("select foto from producto where id_RFID= @idRFID", miconexion);
                cmd.Parameters.AddWithValue("@idRFD",id_RFID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                //Representa un set de comandos que es utilizado para llenar un DataSet
                SqlDataAdapter dp = new SqlDataAdapter(cmd);
                //Representa un caché (un espacio) en memoria de los datos.
                DataSet ds = new DataSet("producto");

                //Arreglo de byte en donde se almacenara la foto en bytes
                byte[] MyData = new byte[0];


                //Llenamosel DataSet con la tabla. ESTUDIANTE es nombre de la tabla
                dp.Fill(ds, "producto");

                //Si dni existe ejecutara la consulta
                if (ds.Tables["producto"].Rows.Count > 0)
                {
                    //Inicializamos una fila de datos en la cual se almacenaran todos los datos de la fila seleccionada
                    DataRow myRow = ds.Tables["producto"].Rows[0];

                    //Se almacena el campo foto de la tabla en el arreglo de bytes
                    MyData = (byte[])myRow["foto"];
                    //Se inicializa un flujo en memoria del arreglo de bytes
                    MemoryStream stream = new MemoryStream(MyData);
                    //En el picture box se muestra la imagen que esta almacenada en el flujo en memoria 
                    //el cual contiene el arreglo de bytes
                    foto.Image = Image.FromStream(stream);

                }
            }
            catch(Exception ex) {
                MessageBox.Show("no se pudo" + ex);
            }
        }

        public void EliminarProducto(string id_RFID)
        {
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EliminarProducto";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idpro", id_RFID);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
        public void EliminarLista(string id_RFID)
        {
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EliminarLista";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idpro", id_RFID);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
        public void Eliminar_Salida_Producto(string id_RFID)
        {
            DataTable Tabla = new DataTable();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "EliminarSalidaProducto";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idpro", id_RFID);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

        public DataTable ListarDeProductos()
        {
            comando.Connection = conexion.ObtenerConexion();
            comando.CommandText = "ListaDeProductos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }
    }
}
