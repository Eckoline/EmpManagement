using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Data.SqlClient;

namespace EmpManagement
{
    public partial class prueba : Form
    {
        public prueba()
        {
            InitializeComponent();
        }

        private void prueba_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleadoEventos = new DataTable();
            DateTime fecini, fecfinal;
            string fechaing;
            fechaing = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            string badgenumber = textBox1.Text;
           
            conexion.abrir();
            string query = "SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN=EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER=" + badgenumber + " AND fecin>='" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "' and fecfin<='" + dateTimePicker2.Value.ToString("MM-dd-yyyy") + "'";
            Debug.WriteLine(query);
            SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
            adaptadorsum.Fill(dtempleadoEventos);
            conexion.cerrar();
            string[] eventos = new string[dtempleadoEventos.Rows.Count];
            int j = 0;
            foreach (DataRow row in dtempleadoEventos.Rows)
            {
                fecini = DateTime.Parse(row["fecin"].ToString());
                fecfinal = DateTime.Parse(row["fecfin"].ToString());
                Debug.WriteLine(fecini);
                Debug.WriteLine(fecfinal);
                TimeSpan dias;
                dias = fecfinal - fecini;
                Debug.WriteLine("Días:" + dias.Days);
                String[] fechas = new String[dias.Days + 1];
                Debug.WriteLine("Longitud array:" + fechas.Length);
                for (int i = 0; i < fechas.Length; i++)
                {
                    fechas[i] = fecini.AddDays(i).ToString("yyyy-MM-dd");
                    Debug.WriteLine("Fecha:" + fechas[i]);
                    if (fechas[i] == dateTimePicker3.Value.ToString("yyyy-MM-dd"))
                    {
                        eventos[j] = dtempleadoEventos.Rows[j]["Descripcion"].ToString();
                        Debug.WriteLine(eventos[j]);
                        Debug.WriteLine(dtempleadoEventos.Rows[j]["Grupo"].ToString());
                    }
                }
                j++;
            }
            Debug.WriteLine(eventos.Length);
        }
    }
}
