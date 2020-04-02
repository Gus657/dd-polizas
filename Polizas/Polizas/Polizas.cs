using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polizas
{
	public partial class Polizas : Form
	{
		public Polizas()
		{
			InitializeComponent();
			refrescar();
		}

		public void AsignarColores(Color colorBarra , Color Titulo) {
			panel1.BackColor = colorBarra;
			label1.ForeColor = Titulo;
		}
		void refrescar()
		{
			SQL_LLenar_Tabla tramites = new SQL_LLenar_Tabla();
			OdbcDataAdapter dt = tramites.llenaTbl("poliza_detalles");
			DataTable table = new DataTable();
			dt.Fill(table);
			dataGridView1.DataSource = table;

			SQL_LLenar_Tabla tramites2 = new SQL_LLenar_Tabla();
			OdbcDataAdapter dt2 = tramites2.llenaTbl("poliza_encabezados");
			DataTable table2 = new DataTable();
			dt2.Fill(table2);
			dataGridView2.DataSource = table2;
		}


		private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{

		}

		private void Label9_Click(object sender, EventArgs e)
		{

		}
	}
}
