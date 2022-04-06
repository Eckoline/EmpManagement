using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace EmpManagement
{
    public partial class Cumple : Form
    {
        public Cumple()
        {
            InitializeComponent();
        }
        private void Cumple_Load(object sender, EventArgs e)
        {

            getmes(DateTime.Now.Month);
            DataTable dtcumple = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query;
            query = "SELECT USERINFOCUS.BADGENUMBER AS ID, USERINFOCUS.NAME AS NOMBRE,DATEPART(day,birthday) AS 'DÍA',USERINFOCUS.PUESTO AS 'PUESTO',DEPARTMENTS.DEPTNAME AS DEPARTAMENTO  FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DATEPART(MONTH,BIRTHDAY)=" + DateTime.Now.Month.ToString() + " AND DEPARTMENTS.DEPTID NOT IN (1,32) ORDER BY [DÍA] ASC;"; 
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtcumple);
            conexion.cerrar();
            dataGridViewDatos.DataSource = dtcumple;
        }
        public void getmes(int mes)
        {
            switch (mes)
            {
                case 1:
                    label1.Text = "Cumpleañeros del mes de Enero";
                    break;
                case 2:
                    label1.Text = "Cumpleañeros del mes de Febrero";
                    break;
                case 3:
                    label1.Text = "Cumpleañeros del mes de Marzo";
                    break;
                case 4:
                    label1.Text = "Cumpleañeros del mes de Abril";
                    break;
                case 5:
                    label1.Text = "Cumpleañeros del mes de Mayo";
                    break;
                case 6:
                    label1.Text = "Cumpleañeros del mes de Junio";
                    break;
                case 7:
                    label1.Text = "Cumpleañeros del mes de Julio";
                    break;
                case 8:
                    label1.Text = "Cumpleañeros del mes de Agosto";
                    break;
                case 9:
                    label1.Text = "Cumpleañeros del mes de Septiembre";
                    break;
                case 10:
                    label1.Text = "Cumpleañeros del mes de Octubre";
                    break;
                case 11:
                    label1.Text = "Cumpleañeros del mes de Noviembre";
                    break;
                case 12:
                    label1.Text = "Cumpleañeros del mes de Diciembre";
                    break;
            }
        }

        private void dataGridViewDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridViewDatos.Columns[e.ColumnIndex].Name == "DÍA")
            {
                if (e.Value != null)
                {
                    if (e.Value.GetType() != typeof(System.DBNull))
                    {
                        //Stock menor a 20
                        if (Convert.ToInt32(e.Value) <= DateTime.Now.Day)
                        {
                            if(Convert.ToInt32(e.Value) == DateTime.Now.Day)
                            {
                                e.CellStyle.BackColor = Color.Blue;
                            }
                            else
                            {
                                e.CellStyle.BackColor = Color.Green;
                            }
                     
                           // e.CellStyle.ForeColor = Color.Red;
                        }
                   
                    }
                }
            }
        }
    }
}
