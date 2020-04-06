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
		public OdbcDataAdapter llenaTbl(string tabla, string id)// metodo  que obtinene el contenio de una tabla
		{
			string presql = "SELECT  nombre FROM  cuentas WHERE id_cuenta = "+id+" LIMIT 1";
			string presql2 = "SELECT  id_documento FROM  documentos  LIMIT 1";
			string presql3 = "SELECT  fecha FROM  documentos  LIMIT 1";
			string sql = "SELECT ("+presql+") as Cuenta, ("+presql2+ ") AS Documento, (" + presql3 + ") AS Fecha, 300+100 AS Debe, 0+0 AS Haber FROM " + tabla + "";
			string sql2 = "SELECT (" + presql + ") as Cuenta, (" + presql2 + ") AS Documento, (" + presql3 + ") AS Fecha, 300+300 AS Debe, 0+0 AS Haber FROM " + tabla + "";
			string sql3 = "SELECT (" + presql + ") as Cuenta, (" + presql2 + ") AS Documento, (" + presql3 + ") AS Fecha, 300+200 AS Debe, 0+0 AS Haber FROM " + tabla + "";
			string sql4 = sql + " UNION " + sql2 + " UNION " + sql3;
			Console.WriteLine("--------------------------------------------- >" + sql4 + "< ---------------------------------------------");
			OdbcDataAdapter dataTable = new OdbcDataAdapter(sql4, conectar.conexion());
			return dataTable;
		}

		public OdbcDataAdapter llenaTbl2(string tabla, string tipo)// metodo  que obtinene el contenio de una tabla
		{
			string sql = "SELECT id_poliza, fecha,fecha_inicio, fecha_fin FROM " + tabla + " where estado=1 AND tipo_poliza='"+tipo+"';";
			OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, conectar.conexion());
			return dataTable;
		}

		public OdbcDataAdapter llenaTbl3(string tabla, string id)// metodo  que obtinene el contenio de una tabla
		{
			string sql = "SELECT  (select nombre from cuentas where cuentas.id_cuenta = poliza_detalles.id_cuenta) as Cuenta, id_documento_asociado as Documento, fecha as Fecha, debe As Debe, haber as Haber FROM poliza_detalles where poliza_detalles.id_poliza=" + id + ";";
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

		public OdbcDataAdapter LLenarTabla(string Query)// metodo  que obtinene el contenio de una tabla
		{
			string sql = Query;
			OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, conectar.conexion());
			return dataTable;
		}



	}
}
