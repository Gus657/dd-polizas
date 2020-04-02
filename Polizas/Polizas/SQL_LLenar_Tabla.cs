using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizas
{
	class SQL_LLenar_Tabla
	{
		SQL_Conexion conectar = new SQL_Conexion();
		public OdbcDataAdapter llenaTbl(string tabla)// metodo  que obtinene el contenio de una tabla
		{
			string sql = "SELECT * FROM " + tabla + ";";
			OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, conectar.conexion());
			return dataTable;
		}

	}
}
