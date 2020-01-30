using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace UHFDemo
{
    class metodo
    {
        public void filtrar(DataGridView data, string buscarnombre)
        {
            try
            {
                SqlCommand sql = new SqlCommand("filtro_busqueda", conexion.ObtenerConexion());
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@filtro", SqlDbType.VarChar, 200).Value = buscarnombre;

                sql.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql);
                da.Fill(dt);
                data.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public void filtrarC(DataGridView data, string buscarcategoria)
        {
            try
            {
                SqlCommand sql = new SqlCommand("filtro_busqueda_categoria", conexion.ObtenerConexion());
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@filtroC", SqlDbType.VarChar, 200).Value = buscarcategoria;

                sql.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql);
                da.Fill(dt);
                data.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public void filtrarProveedor(DataGridView data, string buscarProveedor)
        {
            try
            {
                SqlCommand sql = new SqlCommand("filtro_busqueda_Proveedor", conexion.ObtenerConexion());
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@filtro", SqlDbType.VarChar, 200).Value = buscarProveedor;

                sql.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql);
                da.Fill(dt);
                data.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public void filtrarP(DataGridView data, string buscarcategoria)
        {
            try
            {
                SqlCommand sql = new SqlCommand("filtro_busqueda_producto", conexion.ObtenerConexion());
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@filtroP", SqlDbType.VarChar, 200).Value = buscarcategoria;

                sql.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql);
                da.Fill(dt);
                data.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public void filtrarSP(DataGridView data, string buscarcategoria)
        {
            try
            {
                SqlCommand sql = new SqlCommand("filtro_busqueda_salida", conexion.ObtenerConexion());
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@filtrosP", SqlDbType.VarChar, 200).Value = buscarcategoria;

                sql.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql);
                da.Fill(dt);
                data.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
