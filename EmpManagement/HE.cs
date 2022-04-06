using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace EmpManagement
{
    public partial class HE : Form
    {
        public HE()
        {
            InitializeComponent();
        }
        DataTable dtHorex = new DataTable();
        DataTable dtHorexapr = new DataTable();
        

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Seguro que desea cancelar?", "Cancelar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void buttonAcep_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            int con = 0;
            string query;
            SqlCommand comando = new SqlCommand();
            DataTable dt1 = new DataTable();
            DialogResult resultado = MessageBox.Show("¿Seguro que desea actualizar las horas extra?", "Actualización", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                foreach (DataRow row in dtHorexapr.Rows)
                {
                    if(row["horexapr"].ToString()!=dtHorex.Rows[con]["hextra"].ToString())
                    {
                        Debug.WriteLine(dtHorex.Rows[con]["hextra"].ToString());
                        Debug.WriteLine("Cambio a;");
                        Debug.WriteLine(row["horexapr"].ToString());
                        conexion.abrir();
                        query = "INSERT INTO HOREXJUST VALUES("+labelid.Text+",'"+ DateTime.Parse(row["fecha"].ToString()).ToString("MM-dd-yyyy") + "','"+ TimeSpan.Parse(row["horexapr"].ToString()) + "',"+Program.id+")";
                        comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();

                        query = "UPDATE detalledias SET horexapr='" + row["horexapr"].ToString() + "' where fecha='" + DateTime.Parse(row["fecha"].ToString()).ToString("MM-dd-yyyy") + "' and badgenumber=" + labelid.Text;
                        comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();

                        conexion.cerrar();

                    }
                    else
                    {
                        conexion.abrir();
                        SqlDataAdapter adaptadorDias = new SqlDataAdapter();
                        query = "SELECT * FROM HOREXJUST WHERE badgenumber=" + labelid.Text + " AND fechadetalle='"+ DateTime.Parse(row["fecha"].ToString()).ToString("MM-dd-yyyy") + "'";
                        adaptadorDias = new SqlDataAdapter(query, conexion.con);
                        adaptadorDias.Fill(dt1);
                        conexion.cerrar();
                        if (dt1.Rows.Count > 0)
                        {
                            conexion.abrir();
                            query = "UPDATE HOREXJUST SET horex='" + row["horexapr"].ToString() + "' where fechadetalle='" + DateTime.Parse(row["fecha"].ToString()).ToString("MM-dd-yyyy") + "' and badgenumber="+labelid.Text;
                            comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            query = "UPDATE detalledias SET horexapr='" + row["horexapr"].ToString() + "' where fecha='" + DateTime.Parse(row["fecha"].ToString()).ToString("MM-dd-yyyy") + "' and badgenumber=" + labelid.Text;
                            comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                        }
                    }
                    con++;
                }
                MessageBox.Show("Registro actualizado con éxito");
                this.Close();
            }
          
        }

        private void HE_Load(object sender, EventArgs e)
        {
            dtHorex = new DataTable();
            dtHorexapr = new DataTable();
            conexionbd conexion = new conexionbd();

            conexion.abrir();
            string query = "SELECT fecha,hextra FROM detalledias WHERE badgenumber=" + labelid.Text + " AND fecha between'" + labelfecin.Text + "' and '"+labelfecfin.Text+"'";
            SqlDataAdapter adaptadorDias = new SqlDataAdapter(query, conexion.con);
            adaptadorDias.Fill(dtHorex);

            query = "SELECT fecha,horexapr FROM detalledias WHERE badgenumber=" + labelid.Text + " AND fecha between'" + labelfecin.Text + "' and '" + labelfecfin.Text + "'";
            adaptadorDias = new SqlDataAdapter(query, conexion.con);
            adaptadorDias.Fill(dtHorexapr);

            conexion.cerrar();

            dataGridViewDatos.DataSource = dtHorex;
            dataGridViewDatosMod.DataSource = dtHorexapr;
        }
    }
}
