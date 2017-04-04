using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace EPS.BO.ADO
{
    public static class Acceso
    {


        private static SqlConnection _conexion;

        private static string _cadenaConexion = ConfigurationManager.ConnectionStrings["dbepsContext"].ToString();

        public static SqlConnection ObtenerConexion()
        {

            _conexion = new SqlConnection(_cadenaConexion);

            if (_conexion.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    _conexion.Open();
                }
                catch (Exception ex)
                {
                    //NLogLogger.Logger.Error("Ha ocurrido un error estableciendo conexión con el servidor. Favor de verificar si se encuentra conectado a la red.",
                      //  ex);

                    throw ex;
                }
            }


            return _conexion;
        }

        public static SqlConnection ObtenerConexion(string cadena)
        {
            var cadenaConexion = ConfigurationManager.ConnectionStrings["" + cadena + ""].ToString();
            _conexion = new SqlConnection(cadenaConexion);

            if (_conexion.State != System.Data.ConnectionState.Closed) return _conexion;
            try
            {
                _conexion.Open();
            }
            catch (Exception ex)
            {
                //NLogLogger.Logger.Error("Ha ocurrido un error estableciendo conexión con el servidor. Favor de verificar si se encuentra conectado a la red.", ex);
                throw new ArgumentException("Ha ocurrido un error estableciendo conexión con el servidor. Favor de verificar si se encuentra conectado a la red.");
            }

            return _conexion;
        }

        public static void CerrarConexion()
        {

            if (_conexion.State == ConnectionState.Open)
                _conexion.Close();
        }
        public static SqlDataReader ExecuteReader(CommandType cmdType,
      string cmdText, IDbConnection conexion = null, params SqlParameter[] commandParameters)
        {
            if (conexion == null)
                ObtenerConexion();
            else
                _conexion = (SqlConnection)conexion;
            using (var command = new SqlCommand(cmdText, _conexion))
            {
                command.CommandType = cmdType;
                command.Parameters.AddRange(commandParameters);
                command.CommandTimeout = 120;
                return command.ExecuteReader();
            }

        }
        public static int ExecuteNonQuery(CommandType cmdType,
   string cmdText, IDbConnection conexion = null, params SqlParameter[] commandParameters)
        {
            if (conexion == null)
                ObtenerConexion();
            else
                _conexion = (SqlConnection)conexion;
            using (var command = new SqlCommand(cmdText, _conexion))
            {
                command.CommandType = cmdType;
                command.Parameters.AddRange(commandParameters);
                command.CommandTimeout = 120;
                return command.ExecuteNonQuery();
            }

        }


    }
}
