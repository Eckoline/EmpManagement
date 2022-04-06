using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmpManagement
{
    public partial class RolTurnosActual : Form
    {
        public RolTurnosActual()
        {
            InitializeComponent();
        }
       public  int idhor1, idhor2, idhor3;
        private void RolTurnosActual_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtcomboarea = new DataTable();
            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS WHERE DEPTID IN(7,8,9,10,13,14,30,37,38)";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtcomboarea);
            toolStripComboBox1.ComboBox.DisplayMember = "DEPTNAME";
            toolStripComboBox1.ComboBox.ValueMember = "DEPTID";
            toolStripComboBox1.ComboBox.DataSource = dtcomboarea;
            conexion.cerrar();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            conexionbd conexion = new conexionbd();
            DataTable dthor = new DataTable();
         
            conexion.abrir();
            string query = "SELECT distinct HORARIOS.ID_HOR FROM USERINFOCus INNER JOIN HOREMPLEADO ON USERINFOCus.Badgenumber=HOREMPLEADO.Badgenumber INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCus.DEFAULTDEPTID=" + toolStripComboBox1.ComboBox.SelectedValue.ToString() + " ORDER BY HORARIOS.ID_HOR; ";
            Debug.WriteLine(query);
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dthor);
            int iddep;
            iddep = Int32.Parse(toolStripComboBox1.ComboBox.SelectedValue.ToString());
            int selector = 0;
            if (iddep == 13)
            {
                selector = 3;
            }
            else
            {
                if (iddep==30)
                {
                    selector = 2;
                }
                else
                {
                    if (iddep == 38)
                    {
                        selector = 3;
                    }
                    else
                    {
                        selector = dthor.Rows.Count;
                    }
                    
                }
            }
            switch (selector)
            {
                case 1:
                    idhor1 = Int32.Parse(dthor.Rows[0]["ID_HOR"].ToString());
                    Loadquery(dt1, idhor1,iddep);
                    break;
                case 2:
                    idhor1 = Int32.Parse(dthor.Rows[0]["ID_HOR"].ToString());
                    idhor2 = Int32.Parse(dthor.Rows[1]["ID_HOR"].ToString());
                    Loadquery(dt1, idhor1, iddep);
                    Loadquery(dt2, idhor2, iddep);

                    break;
                case 3:
                    idhor1 = Int32.Parse(dthor.Rows[0]["ID_HOR"].ToString());
                    idhor2 = Int32.Parse(dthor.Rows[1]["ID_HOR"].ToString());
                    idhor3 = Int32.Parse(dthor.Rows[2]["ID_HOR"].ToString());
                    Loadquery(dt1, idhor1, iddep);
                    Loadquery(dt2, idhor2, iddep);
                    Loadquery(dt3, idhor3, iddep);
                    break;
            }
            dataGridView1.DataSource = dt1;
            dataGridView2.DataSource = dt2;
            dataGridView3.DataSource = dt3;
            conexion.cerrar();
        }

        void Loadquery(DataTable dt, int idhor, int iddep)
        {
            conexionbd conexion = new conexionbd();
            string query;
            conexion.abrir();
            query = "SELECT USERINFOCus.Badgenumber AS ID,USERINFOCus.Name AS Nombre,HORARIOS.Descripcion FROM USERINFOCus inner join HOREMPLEADO ON USERINFOcus.Badgenumber=HOREMPLEADO.Badgenumber inner join horarios on HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR where HORARIOS.ID_HOR="+idhor+" AND USERINFOCUS.DEFAULTDEPTID="+iddep;
            Debug.WriteLine(query);
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dt);
            conexion.cerrar();
        }

        void Loadqueryconid(DataTable dt, int idhor, int iddep)
        {
            conexionbd conexion = new conexionbd();
            string query;
            conexion.abrir();
            query = "SELECT USERINFOCus.Badgenumber AS ID,USERINFOCus.Name AS Nombre,HORARIOS.Descripcion FROM USERINFOCus inner join HOREMPLEADO ON USERINFOcus.Badgenumber=HOREMPLEADO.Badgenumber inner join horarios on HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR where HORARIOS.ID_HOR=" + idhor + " AND USERINFOCUS.DEFAULTDEPTID=" + iddep + " AND USERINFOCUS.BADGENUMBER LIKE '%"+toolStripTextBoxID.Text+"%'";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dt);
            conexion.cerrar();
        }

        void Loadqueryconname(DataTable dt, int idhor, int iddep)
        {
            conexionbd conexion = new conexionbd();
            string query;
            conexion.abrir();
            query = "SELECT USERINFOCus.Badgenumber AS ID,USERINFOCus.Name AS Nombre,HORARIOS.Descripcion FROM USERINFOCus inner join HOREMPLEADO ON USERINFOcus.Badgenumber=HOREMPLEADO.Badgenumber inner join horarios on HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR where HORARIOS.ID_HOR=" + idhor + " AND USERINFOCUS.DEFAULTDEPTID=" + iddep+ "AND USERINFOCUS.NAME LIKE '%"+toolStripTextBoxNombre.Text+"%'";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dt);
            conexion.cerrar();
        }
        private void toolStripTextBoxID_TextChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            int iddep;
            iddep = Int32.Parse(toolStripComboBox1.ComboBox.SelectedValue.ToString());
            Loadqueryconid(dt1, idhor1, iddep);
            Loadqueryconid(dt2, idhor2, iddep);
            Loadqueryconid(dt3, idhor3, iddep);
            dataGridView1.DataSource = dt1;
            dataGridView2.DataSource = dt2;
            dataGridView3.DataSource = dt3;
        }

        private void toolStripTextBoxNombre_TextChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            int iddep;
            iddep = Int32.Parse(toolStripComboBox1.ComboBox.SelectedValue.ToString());
            Loadqueryconname(dt1, idhor1, iddep);
            Loadqueryconname(dt2, idhor2, iddep);
            Loadqueryconname(dt3, idhor3, iddep);
            dataGridView1.DataSource = dt1;
            dataGridView2.DataSource = dt2;
            dataGridView3.DataSource = dt3;


        }
    }
}
