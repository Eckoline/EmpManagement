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
    public partial class MisCap : Form
    {
        public MisCap()
        {
            InitializeComponent();
        }

        private void MisCap_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            conexion.abrir();
            string query = "SELECT ID_CAP AS ID, NombreInstru as 'Instructor',Nombrecap as Curso, Descripcion as 'Descripción del curso',fec_rec as 'Fecha recepción',Fec_in as 'Fecha inicio', Fec_fin as 'Fecha termino',duracion as 'Duración (HRS)',Solicitante FROM CAPACITACION where solicitante='"+Program.usuario+"' ORDER BY Fec_fin DESC";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            dataGridViewDatos.DataSource = dtComboDepts;
            conexion.cerrar();
        }

        private void verAsistentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.Rows.Count > 0)
            {
                prueba frm = new prueba();
                frm.idcap = Int32.Parse(dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString());
                frm.Show();
            }
            else
            {
                MessageBox.Show("No hay información.");
            }
        }
    }
}
