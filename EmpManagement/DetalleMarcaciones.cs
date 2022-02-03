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
            DataTable dtTecerturno = new DataTable();
            conexionbd conexion = new conexionbd();
            int idemp;
            int[] semanalt = { 3, 6, 27 };
            string query = ""; 

            conexion.abrir();
            query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_IN,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA,HORARIOS.Descripcion FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFOCUS.BADGENUMBER= " + textBoxID.Text;
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtTecerturno);
            idemp = Int32.Parse(dtTecerturno.Rows[0]["ID_HOR"].ToString());
            
            if (Array.Exists(semanalt, x => x == idemp))
            {
                query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE USERINFOCUS.BADGENUMBER=" + textBoxID.Text + " AND  CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + " 17:00:00' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + " 10:00:00' ORDER BY CHECKTIME;";
                adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtMarcaciones);
                dataGridViewDatos.DataSource = dtMarcaciones;
            }
            else
            {
                query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND USERINFOCUS.BADGENUMBER=" + textBoxID.Text + ";";
                adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtMarcaciones);
                dataGridViewDatos.DataSource = dtMarcaciones;
            }
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
