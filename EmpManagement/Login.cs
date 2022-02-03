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
    public partial class Login : Form
    {
      
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            string query = "";
            conexion.abrir();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable dtEmp = new DataTable();
            query = "SELECT * FROM users where nombre='" + textBoxUser.Text + "' and pass='" + textBoxPass.Text + "'COLLATE SQL_Latin1_General_CP1_CS_AS";
            adaptador = new SqlDataAdapter(query, conexion.con);
            conexion.cerrar();
            adaptador.Fill(dtEmp);
            if (dtEmp.Rows.Count > 0)
            {
                FPrincipal frmprin = new FPrincipal();
                frmprin.labelUser.Text = dtEmp.Rows[0]["Nombre"].ToString();
                frmprin.labelClase.Text= dtEmp.Rows[0]["clasificacion"].ToString();
                Program.usuario= dtEmp.Rows[0]["Nombre"].ToString();
                this.Hide();
                frmprin.ShowDialog();
                textBoxPass.Text = "";
                textBoxUser.Text = "";
                this.Show();
            }
            else
            {
                DialogResult resultado = MessageBox.Show("Acceso denegado", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPass.Text = "";
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult resultado=MessageBox.Show("¿Seguro que desea salir?","Salir",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
