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

		public OdbcDataAdapter llenaTbl2(string tabla, string tipo)// metodo  que obtinene el contenio de una tabla
		{
			string sql = "SELECT id_poliza, fecha,fecha_inicio, fecha_fin FROM " + tabla + " where estado=1 AND tipo_poliza='"+tipo+"';";
			OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, conectar.conexion());
			return dataTable;
		}

		public string [] llenarCombo()
		{
			string[] Combo = new string[30];
			int i = 0;
			OdbcCommand command = new OdbcCommand("SELECT tipo_poliza FROM poliza_encabezados WHERE estado=1 GROUP BY tipo_poliza ;", conectar.conexion());
			OdbcDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				Combo[i] = reader.GetValue(0).ToString();
				i++;
			}
			return Combo;
		}

		public void ejecutarQuery(string query)// ejecuta un query en la BD
		{
			try
			{
				OdbcCommand consulta = new OdbcCommand(query, conectar.conexion());
				consulta.ExecuteNonQuery();
			}
			catch (OdbcException ex) { Console.WriteLine(ex.ToString()); }

		}




	}
}
