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
			string[] combo = tramites.llenarCombo().Split(',');
			comboBox1.Items.AddRange(combo);

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
			dataGridView1.Rows.Clear();
		}

		private void Button6_Click(object sender, EventArgs e)
		{
			comboBox1.Text = "";
			dataGridView1.Rows.Clear();
			dataGridView2.Rows.Clear();
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
			if (comboBox1.Text == "")
			{
				MessageBox.Show("Seleccione un tipo de Póliza");
			}
			else
			{
				if (dataGridView1.Rows.Count == 0 )
				{
					MessageBox.Show("Por favor Busque los registros a trasladar");
				}
				else
				{
					string sql = "INSERT INTO poliza_encabezados " +
				"VALUES (" + textBox1.Text.ToString() + ", " +
				"'" + dateTimePicker1.Text.ToString() + "' ," +
				"'" + comboBox1.Text.ToString() + "'," +
				"'" + dateTimePicker3.Text.ToString() + "'," +
				"'" + dateTimePicker2.Text.ToString() + "'," +
				"1" +
				");";

					tramites.ejecutarQuery(sql);
					foreach (DataGridViewRow row in dataGridView1.Rows)
					{

						string sql2 = "INSERT INTO poliza_detalles VALUES (	NULL, " + textBox1.Text + "," +
						"'" + row.Cells[0].Value.ToString() + "'," + row.Cells[1].Value.ToString() + " , " + row.Cells[2].Value.ToString() + ",1)";
						tramites.ejecutarQuery(sql2);

					}
					foreach (DataGridViewRow row in dataGridView1.Rows)
					{

						dataGridView1.Rows.Remove(row);

					}
					dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
					MessageBox.Show("La póliza ha sido enviada, exitosamente!");
					textBox1.Text = tramites.ObtenerIdPoliza();
					OdbcDataAdapter dt2 = tramites.llenaTbl2("poliza_encabezados", comboBox1.Text.ToString());
					DataTable table2 = new DataTable();
					dt2.Fill(table2);
					dataGridView2.DataSource = table2;
				}
			}
			


		}

		private void Btn_Buscar_Click(object sender, EventArgs e)
		{
			string aux = "";
			if (Query != "")
			{
				aux = Query.Replace("'FechaI'", "'" + dateTimePicker1.Text + "'");
				aux = aux.Replace("'FechaF'", "'"+ dateTimePicker2.Text+ "'");
				OdbcDataAdapter dt2 = tramites.LLenarTabla(aux);
				DataTable table = new DataTable();
				dt2.Fill(table);
				dataGridView1.DataSource = table;
			}
			else {
				MessageBox.Show("No se ha asignado una consulta al Componente");
			}
			
		}

		private void Button5_Click(object sender, EventArgs e)
		{
			
		}

		private void Polizas_Load(object sender, EventArgs e)
		{
			textBox1.Text = tramites.ObtenerIdPoliza();
		}
	}
}
