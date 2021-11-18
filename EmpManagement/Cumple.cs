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
    public partial class Cumple : Form
    {
        public Cumple()
        {
            InitializeComponent();
        }

        private void Cumple_Load(object sender, EventArgs e)
        {
            DataTable dtcumple = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query;
            query = "SELECT USERINFOCUS.BADGENUMBER AS ID, USERINFOCUS.NAME AS NOMBRE,CONVERT(CHAR(6), USERINFOCus.BIRTHDAY, 100)   AS 'FECHA DE CUMPLEAÑOS',USERINFOCUS.PUESTO AS 'PUESTO',DEPARTMENTS.DEPTNAME AS DEPARTAMENTO  FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DATEPART(MONTH,BIRTHDAY)="+ DateTime.Now.Month.ToString() + " ORDER BY [FECHA DE CUMPLEAÑOS];"; 
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtcumple);
            conexion.cerrar();
            dataGridViewDatos.DataSource = dtcumple; 
        }
    }
}
