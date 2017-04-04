using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EPS.BO.Models;
using System.Data.SqlClient;

namespace EPS.BO.ADO
{
	public class ClienesADO
	{
		private SqlConnection _connection;


#region "SQL"
        const string sqlConsulta = " SELECT  " +
                              "  CTE_NUMERO_EPS," +
                    " CTE_CEDULA," +
                    " CTE_RNC," +
                    " CTE_NOMBRE," +
                    " CTE_APELLIDO," +
                    " CTE_DIRECCION_CASA," +
                    " CTE_DIRECCION_OFICINA," +
                    " CTE_CIUDAD," +
                    " CTE_TELEFONO_CASA," +
                    " CTE_TELEFONO_OFICINA," +
                    " CTE_FECHA_NACIMIENTO," +
                    " CTE_TIPO," +
                    " RES_CODIGO," +
                    " CTE_ESTADO," +
                    " CTE_REPRESENTANTE," +
                    " CTE_CREDITO," +
                    " CTE_CODIGO_VOICE," +
                    " AGE_CODIGO," +
                    " SUC_CODIGO," +
                    " CTE_EMAIL," +
                    " TAR_CODIGO," +
                    " GRC_CODIGO," +
                    " CTE_LIMITE_CREDITO," +
                    " CTE_LIMITE_CREDITO," +
                    " CTE_DIAS_CREDITOS," +
                    " CTE_CREDITO_DISPONIBLE," +
                    " CTE_BALANCE_DISPONIBLE," +
                    " CTE_DIA_CORTE," +
                    " CTE_CELULAR," +
                    " CTE_CORRESPONDENCIA," +
                    " COM_CODIGO," +
                    " STC_CODIGO  " +
                            " FROM CLIENTES " +
                                " WHERE CTE_NUMERO_EPS = @CTE_NUMERO_EPS";


        #endregion

        public Clientes ReturnByEPS(string psCteEPS)
		{


			Clientes oClientes;

			using (_connection = Acceso.ObtenerConexion())
			{
                string sQuery = sqlConsulta;


                oClientes = _connection.Query<Clientes>(sQuery, new
                {
                    CTE_NUMERO_EPS = psCteEPS
                }).SingleOrDefault();
			}

			return oClientes;
		}


	}
}
