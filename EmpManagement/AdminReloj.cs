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
    public partial class AdminReloj : Form
    {
        public int rel;
        public AdminReloj()
        {
            InitializeComponent();
        }
        public SDKHelper SDK = new SDKHelper();

        private void AdminReloj_Load(object sender, EventArgs e)
        {
           
            groupBoxOp.Enabled = false;
            DataTable dtfec = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;
            conexion.abrir();
            query = "SELECT TOP 1 * FROM CHECKINOUT ORDER BY CHECKTIME DESC";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtfec);
            dateTimePickerIni.Text = dtfec.Rows[0]["CHECKTIME"].ToString();
            conexion.cerrar();
            dateTimePickerFin.Value = DateTime.Now;
           
        }

  
        private void buttonCon_Click(object sender, EventArgs e)
        {
            rel = SDK.sta_ConnectTCP(listBox2, textBoxID.Text.Trim(), textBoxPORT.Text.Trim(), textBoxCK.Text.Trim());
            if (rel == 1)
            {
                MessageBox.Show("Conexión Realizada con Éxito");
                groupBoxOp.Enabled = true;
                groupBoxReloj.Enabled = false;
            }
            else
            {
                MessageBox.Show("No se pudo realizar la conexión, compruebe su conexión a internet");
                groupBoxOp.Enabled = false;
                groupBoxReloj.Enabled = true;

            }
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            dataGridViewDatos.Columns.Clear();
            DataTable dt_period = new DataTable("dt_period");
            conexionbd conexion = new conexionbd();
            string query;
            dataGridViewDatos.AutoGenerateColumns = true;
            dataGridViewDatos.Columns.Clear();
            dt_period.Columns.Add("User ID", System.Type.GetType("System.String"));
            dt_period.Columns.Add("Verify Date", System.Type.GetType("System.String"));
            dt_period.Columns.Add("Verify Type", System.Type.GetType("System.Int32"));
            dt_period.Columns.Add("Verify State", System.Type.GetType("System.Int32"));
            dt_period.Columns.Add("WorkCode", System.Type.GetType("System.Int32"));
            dataGridViewDatos.DataSource = dt_period;
            int i = SDK.sta_readLogByPeriod(listBox2, dt_period, dateTimePickerIni.Text.Trim(), dateTimePickerFin.Text.ToString());
            SDK.sta_DisConnect();
            //int i = SDK.sta_readAttLog(listBox2, dt_period);
            if (i == 1)
            {    
                conexion.abrir();
                foreach (DataRow row in dt_period.Rows)
                {
                    query = "INSERT INTO CHECKINOUT(BADGENUMBER,CHECKTIME) VALUES(" + row["User ID"].ToString() + ",'" + DateTime.Parse(row["Verify Date"].ToString()).ToString("yyyy-MM-dd HH:mm")+ "');";
                    SqlCommand comando = new SqlCommand(query, conexion.con);
                    comando.ExecuteNonQuery();
                }
                MessageBox.Show("Registros realizados con éxito");
                query = "WITH cte AS (SELECT CHECKTIME,BADGENUMBER,ROW_NUMBER() OVER (PARTITION BY checktime,badgenumber ORDER BY checktime,badgenumber) row_num FROM CHECKINOUT) DELETE FROM cte WHERE row_num > 1;";
                SqlCommand comando0 = new SqlCommand(query, conexion.con);
                comando0.ExecuteNonQuery();
                conexion.cerrar();
            }
            //SDK.sta_readAttLog(listBox2, dt_period);
            this.Close();

        }
    }
}
