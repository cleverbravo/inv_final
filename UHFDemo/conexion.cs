using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;



namespace UHFDemo
{
    class conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            try
            {
                SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
                conectar.Open();
                return conectar;
            }
            catch (Exception)
            {

                throw new Exception("Error en la conexion"); ;
            }
        }

        public static SqlConnection CerrarConexion()
        {
            SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
            conectar.Close();
            return conectar;
        }

    }
}
