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
    public partial class DetalleMarcaciones : Form
    {
        public DetalleMarcaciones()
        {
            InitializeComponent();
        }

        private void DetalleMarcaciones_Load(object sender, EventArgs e)
        {
            DataTable dtMarcaciones = new DataTable();
            conexionbd conexion = new conexionbd();
            string query = "";
            query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND USERINFOCUS.BADGENUMBER="+textBoxID.Text+";";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtMarcaciones);
            dataGridViewDatos.DataSource = dtMarcaciones;
            conexion.cerrar();
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            DataTable dtMarcaciones = new DataTable();
            conexionbd conexion = new conexionbd();
            string query = "";
            query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND USERINFOCUS.BADGENUMBER=" + textBoxID.Text + "";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtMarcaciones);
            dataGridViewDatos.DataSource = dtMarcaciones;
            conexion.cerrar();
        }
    }
}
