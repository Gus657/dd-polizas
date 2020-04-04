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
			dateTimePicker1.Format = DateTimePickerFormat.Custom;
			dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			dateTimePicker2.Format = DateTimePickerFormat.Custom;
			dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			dateTimePicker3.Format = DateTimePickerFormat.Custom;
			dateTimePicker3.CustomFormat = "yyyy-MM-dd";
			
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
			SQL_LLenar_Tabla tramites2 = new SQL_LLenar_Tabla();
			OdbcDataAdapter dt2 = tramites2.llenaTbl2("poliza_encabezados", comboBox1.Text.ToString());
			DataTable table2 = new DataTable();
			dt2.Fill(table2);
			dataGridView2.DataSource = table2;
		}

		private void Button4_Click(object sender, EventArgs e)
		{
			SQL_LLenar_Tabla tramites2 = new SQL_LLenar_Tabla();
			OdbcDataAdapter dt2 = tramites2.llenaTbl2("poliza_encabezados", comboBox1.Text.ToString());
			DataTable table2 = new DataTable();
			dt2.Fill(table2);
			dataGridView2.DataSource = table2;
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			

			if (dataGridView1.Rows.Count-1 != 0)
			{
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					dataGridView1.Rows.RemoveAt(0);
				}
			}



		}

		private void Button6_Click(object sender, EventArgs e)
		{
			comboBox1.Text = "";
			if (dataGridView1.Rows.Count - 1 != 0)
			{
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					dataGridView1.Rows.RemoveAt(0);
				}
			}

			if (dataGridView2.Rows.Count - 1 != 0)
			{
				for (int i = 0; i < dataGridView2.Rows.Count; i++)
				{
					dataGridView2.Rows.RemoveAt(0);
				}
			}
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
			SQL_LLenar_Tabla tramites2 = new SQL_LLenar_Tabla();
			MessageBox.Show(sql);
			tramites2.ejecutarQuery(sql);
		}
	}
}
