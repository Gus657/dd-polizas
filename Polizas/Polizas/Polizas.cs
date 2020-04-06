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
		string Query = "";
		SQL_LLenar_Tabla tramites = new SQL_LLenar_Tabla();
		public Polizas()
		{
			InitializeComponent();
			refrescar();
			dateTimePicker1.Format = DateTimePickerFormat.Custom;
			dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			dateTimePicker2.Format = DateTimePickerFormat.Custom;
			dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			dateTimePicker3.Format = DateTimePickerFormat.Custom;
			dateTimePicker3.CustomFormat = "yyyy-MM-dd";

			OdbcDataAdapter dt = tramites.llenaTbl("poliza_detalles","1");
			DataTable table = new DataTable();
			dt.Fill(table);
			dataGridView1.DataSource = table;
		}

		public void AsignarQuery(string Query) {
			this.Query = Query;
		}
		public void AsignarColores(Color colorBarra , Color Titulo) {
			panel1.BackColor = colorBarra;
			label1.ForeColor = Titulo;
		}
		void refrescar()
		{
			
			
			string[] Combo = tramites.llenarCombo();
			for (int i = 0; i <Combo.Length; i++)
			{
				if (Combo[i]!="" && tramites.llenarCombo()[i] != null)
				{
					comboBox1.Items.Add(Combo[i]);
				}
			}
		}


		private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{

		}

		private void Label9_Click(object sender, EventArgs e)
		{

		}

		private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		
			OdbcDataAdapter dt2 = tramites.llenaTbl2("poliza_encabezados", comboBox1.Text.ToString());
			DataTable table2 = new DataTable();
			dt2.Fill(table2);
			dataGridView2.DataSource = table2;
		}

		private void Button4_Click(object sender, EventArgs e)
		{
			
			OdbcDataAdapter dt2 = tramites.llenaTbl2("poliza_encabezados", comboBox1.Text.ToString());
			DataTable table = new DataTable();
			dt2.Fill(table);
			dataGridView2.DataSource = table;

			int filas = dataGridView1.Rows.Count - 1;
			for (int i = 0; i < filas; i++)
			{
				dataGridView1.Rows.RemoveAt(0);
			}

		}

		private void Button2_Click(object sender, EventArgs e)
		{
			int filas = dataGridView1.Rows.Count - 1;
				for (int i = 0; i < filas; i++)
				{
					dataGridView1.Rows.RemoveAt(0);
				}
			



		}

		private void Button6_Click(object sender, EventArgs e)
		{
			comboBox1.Text = "";
			int filas = dataGridView1.Rows.Count - 1;
			for (int i = 0; i < filas; i++)
			{
				dataGridView1.Rows.RemoveAt(0);
			}

			int filas2 = dataGridView2.Rows.Count - 1;
			for (int i = 0; i < filas2; i++)
			{
				dataGridView2.Rows.RemoveAt(0);
			}
		}
		string limpiarFecha(string fecha) {
			string fechaNew = "";
			bool fin = true;
			for (int i = 0; i < fecha.Length; i++)
			{
				if (fecha[i] == ' ')
				{
					fin = false;
				}
				if (fin)
				{
					if (fecha[i]=='/')
					{
						fechaNew += "-";
					}
					else
					{
						fechaNew += fecha[i];
					}
					
				}
				
			}
			fecha = fechaNew;
			fechaNew = "";
			int pos = 0;
			string ano = "";string mes = ""; string dia = "";
			for (int i = 0; i < fecha.Length; i++)
			{
				if (fecha[i] == '-')
				{
					pos++;
				}
				else {
					switch (pos)
					{
						case 0:
							dia += fecha[i];
							break;
						case 1:
							mes += fecha[i];
							break;
						case 2:
							ano += fecha[i];
							break;
					}
				}
				
			}
			fechaNew = ano+"-"+mes+"-"+dia;
		
			return fechaNew;
		}
		private void Button3_Click(object sender, EventArgs e)
		{
			string sql = "INSERT INTO poliza_encabezados " +
				"VALUES("+textBox1.Text.ToString()+"," +
				"'"+comboBox1.Text.ToString()+"'," +
				"'"+dateTimePicker3.Text.ToString()+"'," +
				"'"+dateTimePicker2.Text.ToString()+"'," +
				"1," +
				"'"+dateTimePicker1.Text.ToString()+"'" +
				");";
			tramites.ejecutarQuery(sql);
			int filas2 = dataGridView1.Rows.Count - 1;
			for (int i = 0; i < filas2; i++)
			{	
					string sqls = "SELECT id_cuenta FROM cuentas WHERE nombre = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
					string sql2 = "INSERT INTO poliza_detalles (id_transaccion, id_poliza, id_cuenta, id_documento_asociado, fecha, debe, haber) VALUES ( "+i.ToString()+"," +
					"" + textBox1.Text.ToString() + ", (" + sqls + "), " + dataGridView1.CurrentRow.Cells[1].Value.ToString() + " , '" + limpiarFecha(dataGridView1.CurrentRow.Cells[2].Value.ToString()) + "' , " + dataGridView1.CurrentRow.Cells[3].Value.ToString() + " , " + dataGridView1.CurrentRow.Cells[4].Value.ToString() + ")";
					tramites.ejecutarQuery(sql2);
					dataGridView1.Rows.RemoveAt(0);
				
				
			}
		}

		private void Btn_Buscar_Click(object sender, EventArgs e)
		{
			if (Query != "")
			{
				OdbcDataAdapter dt2 = tramites.LLenarTabla(Query);
				DataTable table = new DataTable();
				dt2.Fill(table);
				dataGridView1.DataSource = table;
			}
			else {

			}
			
		}

		private void Button5_Click(object sender, EventArgs e)
		{
			OdbcDataAdapter dt2 = tramites.llenaTbl3("poliza_detalles",dataGridView2.CurrentRow.Cells[0].Value.ToString());
			DataTable table = new DataTable();
			dt2.Fill(table);
			dataGridView1.DataSource = table;
		}
	}
}
