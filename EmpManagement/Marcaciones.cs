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
    public partial class Marcaciones : Form
    {
        public int opcion=0;
        public Marcaciones()
        {
            InitializeComponent();
        }

        private void Marcaciones_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            comboBoxDept.DisplayMember = "DEPTNAME";
            comboBoxDept.ValueMember = "DEPTID";
            comboBoxDept.DataSource = dtComboDepts;
            conexion.cerrar();
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            DataTable dtMarcaciones = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;
            conexion.abrir();
            if (textBoxID.Text == "")
            {
                if (comboBoxDept.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    
                    query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";     
                }
                else
                {
                    query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND DEPARTMENTS.DEPTNAME='"+comboBoxDept.Text+"'";
           
                
                }
            }
            else
            { 
                textBoxNombre.Clear();
                if (comboBoxDept.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    
                    query= "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND USERINFOCUS.BADGENUMBER LIKE '%" + textBoxID.Text + "%'";


                }
                else
                {
                    query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND USERINFOCUS.BADGENUMBER LIKE '%" + textBoxID.Text + "%' AND DEPARTMENTS.DEPTNAME='"+comboBoxDept.Text+"'";
                 
                }
               
            }

            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtMarcaciones);
            dataGridViewDatos.DataSource = dtMarcaciones;
            conexion.cerrar();
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            DataTable dtMarcaciones = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;
            conexion.abrir();
            if (textBoxNombre.Text == "")
            {
                if (comboBoxDept.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    
                    query  = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
               
                    
                }
                else
                {
                    query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND DEPARTMENTS.DEPTNAME='"+comboBoxDept.Text+"'";
                    
                }
            }
            else
            {
                textBoxID.Clear();
                if (comboBoxDept.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    
                    query  = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND USERINFOCUS.NAME LIKE '%" + textBoxNombre.Text + "%'";
                   
                }
                else
                {
                    query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND USERINFOCUS.NAME LIKE '%" + textBoxNombre.Text + "%' AND DEPARTMENTS.DEPTNAME='"+comboBoxDept.Text+"'";
                }
            }
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtMarcaciones);
            dataGridViewDatos.DataSource = dtMarcaciones;
            conexion.cerrar();
        }

        private void buttonAcept_Click(object sender, EventArgs e)
        {
            /*
            DataTable dtMarcaciones = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;
            query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND DEPARTMENTS.DEPTNAME='" + comboBoxDept.Text + "'";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtMarcaciones);
            dataGridViewDatos.DataSource = dtMarcaciones;
            conexion.cerrar();
            */
        }

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query = "";
            if (comboBoxDept.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
            {
                query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            else
            {
                query = "SELECT distinct USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,CHECKINOUT.CHECKTIME AS MARCA  FROM(USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID = DEPARTMENTS.DEPTID)INNER JOIN CHECKINOUT ON USERINFOCUS.BADGENUMBER = CHECKINOUT.BADGENUMBER WHERE CHECKINOUT.CHECKTIME BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.AddDays(1).ToString("yyyy-MM-dd") + "' AND DEPARTMENTS.DEPTNAME='" + comboBoxDept.Text + "'";
            }
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtEmployees);
            dataGridViewDatos.DataSource = dtEmployees;
            conexion.cerrar();
        }
    }
}
