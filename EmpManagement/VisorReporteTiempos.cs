using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace EmpManagement
{
    public partial class VisorReporteTiempos : Form
    {
        public VisorReporteTiempos()
        {
            InitializeComponent();
        }
        conexionbd conexion = new conexionbd();
        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userapp;
            userapp = Program.usuario;




            DataTable dtvalid = new DataTable();
            conexion.abrir();
            string query = "select subclasificacion from users where nombre='" + userapp + "' and subclasificacion is not null";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtvalid);


        }

        private void VisorReporteTiempos_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable detallediasbd = new DataTable();
            conexion.abrir();
            string query = "SELECT * FROM detalledias where badgenumber=" + dataGridViewDatos.CurrentRow.Cells[0].Value.ToString() + " and fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "' ORDER BY Fecha";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(detallediasbd);
            conexion.cerrar();
            dataGridViewDatos.DataSource = detallediasbd;
            pintagrilla();
        }

        public void pintagrilla()
        {
            string color;
            foreach (DataGridViewRow rowp in dataGridViewDatos.Rows)
            {
                if (rowp.Cells["tipoeven"].Value.ToString() != null)
                {
                    color = setcolor(rowp.Cells["tipoeven"].Value.ToString());
                    if (color == "Black")
                    {
                        rowp.DefaultCellStyle.ForeColor = Color.White;
                    }
                    rowp.DefaultCellStyle.BackColor = Color.FromName(color);
                }
                else
                {
                    rowp.DefaultCellStyle.BackColor = Color.White;
                }

            }
        }

        public string setcolor(string evento)
        {
            string col = "";
            string query1 = "";
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            DataTable dtEmpEven = new DataTable();
            query1 = "SELECT COLOR FROM EVENTO WHERE descripcion='" + evento + "'";
            SqlDataAdapter adaptador = new SqlDataAdapter(query1, conexion.con);
            adaptador.Fill(dtEmpEven);
            conexion.cerrar();
            if (dtEmpEven.Rows.Count > 0)
            {
                col = dtEmpEven.Rows[0]["COLOR"].ToString();
            }
            else
            {
                col = "White";
            }
            return col;
        }

    }
}
