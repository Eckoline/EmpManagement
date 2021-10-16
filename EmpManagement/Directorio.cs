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
    public partial class Directorio : Form
    {
        public Directorio()
        {
            InitializeComponent();
        }

        private void Directorio_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            comboBoxDepts.DisplayMember = "DEPTNAME";
            comboBoxDepts.ValueMember = "DEPTID";
            comboBoxDepts.DataSource = dtComboDepts;
            conexion.cerrar();
        }

        private void comboBoxDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            if (comboBoxDepts.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
            {

                conexion.abrir();
                string query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmployees);
                dataGridViewDatos.DataSource = dtEmployees;
                conexion.cerrar();
            }
            else
            {
                conexion.abrir();
                string query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + comboBoxDepts.Text + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmployees);
                dataGridViewDatos.DataSource = dtEmployees;
                conexion.cerrar();
            }
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;

            if (textBoxNombre.Text == "")
            {

                if (comboBoxDepts.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + comboBoxDepts.Text + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
            }
            else
            {
                textBoxID.Clear();

                if (comboBoxDepts.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE USERINFOCus.Name LIKE '%" + textBoxNombre.Text + "%'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE USERINFOCus.Name LIKE '%" + textBoxNombre.Text + "%' AND DEPARTMENTS.DEPTNAME='" + comboBoxDepts.Text + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
            }
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;
            if (textBoxID.Text == "")
            {
                if (comboBoxDepts.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + comboBoxDepts.Text + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
            }
            else
            {
                textBoxNombre.Clear();
                if (comboBoxDepts.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE Badgenumber LIKE '%" + textBoxID.Text + "%'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    query = "SELECT Badgenumber AS ID,SSN,Name AS NOMBRE,CURP,PUESTO,RFC,Gender AS GENERO,Title AS ESTUDIOS,BIRTHDAY AS 'FECHA NAC.',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP,OPHONE AS 'TELÉFONO',TELEMERGENCIA AS 'TEL. EM.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE Badgenumber LIKE '%" + textBoxID.Text + "%' AND DEPARTMENTS.DEPTNAME='" + comboBoxDepts.Text + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();

                }
            }
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            NuevoEmpleado frmEmpleados = new NuevoEmpleado();
            frmEmpleados.Show();
        }
    }
}
