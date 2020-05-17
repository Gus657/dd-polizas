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


		public OdbcDataAdapter llenaTbl2(string tabla, string tipo)// metodo  que obtinene el contenio de una tabla
		{
			string sql = "SELECT id_poliza, fecha,fecha_inicio, fecha_fin FROM " + tabla + " where estado=1 AND tipo_poliza='"+tipo+"';";
			OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, conectar.conexion());
			return dataTable;
		}


		public string llenarCombo()
		{
			string Combo = "";
			int numeroFIlas = 0;
			OdbcCommand command = new OdbcCommand("SELECT nombre FROM tipo_polizas WHERE estado =1;", conectar.conexion());
			OdbcDataReader reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					numeroFIlas++;
				}
				reader.Close();
				OdbcDataReader reader2 = command.ExecuteReader();
				for (int i = 0; i < numeroFIlas; i++)
				{
					reader2.Read();
					if (i == numeroFIlas - 1)
					{
						Combo += reader2.GetValue(0).ToString();
					}
					else
					{
						Combo += reader2.GetValue(0).ToString() + ",";
					}
				}
			}
			else
			{
				Combo = "No Hay Registros";
			}
			return Combo;
		}
		public string ObtenerIdPoliza()
		{
			string id = "";
			OdbcCommand command = new OdbcCommand("SELECT MAX(id_poliza) FROM poliza_encabezados ;", conectar.conexion());
			OdbcDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				id = reader.GetValue(0).ToString();
				if (id == "" || id == null)
				{
					id = "1";
				}
				else
				{
					id = reader.GetValue(0).ToString();
					int n = Convert.ToInt32(id) + 1;
					id = n.ToString();
				}


			}


			return id;
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

		public OdbcDataAdapter LLenarTabla(string Query)// metodo  que obtinene el contenio de una tabla
		{
			string sql = Query;
			OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, conectar.conexion());
			return dataTable;
		}



	}
}
