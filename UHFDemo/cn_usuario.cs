using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
namespace UHFDemo
{
    class cn_usuario
    {
        private datos_u objetoCD = new datos_u();
        public DataTable MostrarUser()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public DataTable MostrarCategoria()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarCategoria();
            return tabla;
        }
        public DataTable MostrarProveedor()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarProveedor();
            return tabla;
        }
        public DataTable ListarProducto()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.ListarProductos();
            return tabla;
        }
        public DataTable ListaDeProducto()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.ListarDeProductos();
            return tabla;
        }
        public DataTable ListarSalidas()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.ListarSalidas();
            return tabla;
        }
        public void InsertarUsuario(string id_usuario,string nombre, string apellido, string usuario, string contraseña,string estado)
        {

            objetoCD.Insertar(id_usuario,nombre, apellido, usuario, contraseña,estado);
        }
        public void InsertarProveedor(string nom_proveedor, string direccion, string telefono, string url, string correo,string pais)
        {

            objetoCD.InsertarProveedor(nom_proveedor, direccion, telefono, url, correo, pais);
        }
        public void InsertarC(string nombre_categ,string mensaje )
        {

            objetoCD.InsertarCategoria(nombre_categ,"");
        }
        public void InsertarProducto(string id_RFID, string nombre_p, int id_categoria, int id_proveedor, DateTime fecha_registro, string descripcion,PictureBox foto)
        {

            objetoCD.InsertarProductos( id_RFID, nombre_p, id_categoria, id_proveedor,  fecha_registro, descripcion,foto);
        }
        public void VerImagen(string id_RFID, PictureBox foto)
        {

            objetoCD.VerImagen(id_RFID, foto);
        }

        public void InsertarSalidas(string id_RFID)
        {

            objetoCD.InsertarSalidas(id_RFID);
        }
        public void InsertarProductoLista(string id_RFID)
        {

            objetoCD.InsertarProductoLista(id_RFID);
        }
        public void InsertarLista(string id_RFID)
        {

            objetoCD.InsertarLista(id_RFID);
        }

        public void EditarUsuario(string nombre, string apellido, string usuario, string contraseña,string estado, string id_usuario)
        {
            objetoCD.Editar(nombre, apellido, usuario, contraseña,estado,id_usuario);
        }
        public void ActualizarProveedor(string nom_proveedor, string direccion, string telefono, string url, string correo, string pais,int id_proveedor)
        {

            objetoCD.ActualizarProveedor(nom_proveedor, direccion, telefono, url, correo, pais,Convert.ToInt32(id_proveedor));
        }
        public void EditarC(string nombre_categ, int id_categoria)
        {
            objetoCD.EditarCategoria(nombre_categ, Convert.ToInt32(id_categoria));
        }
        public void EditarP(string id_RFID, string nombre_p, int id_categoria, int id_proveedor, DateTime fecha_registro, string descripcion,PictureBox foto)
        {
            objetoCD.EditarProductos(id_RFID,  nombre_p, Convert.ToInt32(id_categoria), Convert.ToInt32(id_proveedor), Convert.ToDateTime( fecha_registro),  descripcion,foto);
        }

        public void EliminarUsuario(string id_usuario)
        {

            objetoCD.Eliminar(id_usuario);
        }
        public void EliminarProveedor(string id_Proveedor)
        {

            objetoCD.EliminarProveedor(Convert.ToInt32(id_Proveedor));
        }
        public void EliminarC(string id_categoria)
        {

            objetoCD.EliminarCategoria(Convert.ToInt32(id_categoria));
        }
        public void EliminarP(string id_RFID)
        {
             
            objetoCD.EliminarProducto(id_RFID);
        }
        public void EliminarL(string id_RFID)
        {

            objetoCD.EliminarLista(id_RFID);
        }
        public void EliminarSalidaP(string id_RFID)
        {

            objetoCD.Eliminar_Salida_Producto(id_RFID);
        }

    }
}
