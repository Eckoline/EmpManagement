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
        public int idcap;

        private void prueba_Load(object sender, EventArgs e)
        {
            prueba frm = new prueba();
            conexionbd conexion = new conexionbd();
            DataTable dtuser = new DataTable();
            conexion.abrir();
            string query = "SELECT EMPYCAP.BADGENUMBER as 'ID', USERINFOCUS.NAME AS 'Nombre', USERINFOCUS.PUESTO as 'Puesto',DEPARTMENTS.DEPTNAME as 'Departamento' FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID INNER JOIN EMPYCAP ON USERINFOCus.Badgenumber=EMPYCAP.BADGENUMBER WHERE EMPYCAP.ID_CAP=" +idcap.ToString();
            Debug.WriteLine(query);
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtuser);
            conexion.cerrar();
            dataGridView1.DataSource = dtuser;
        
        }
    }
}
