using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace EmpManagement
{
    public partial class RegistrarEmpleado : Form
    {
        public RegistrarEmpleado()
        {
            InitializeComponent();
        }

        private void RegistrarEmpleado_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS ORDER BY DEPTID ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            toolStripComboBox1.ComboBox.DisplayMember = "DEPTNAME";
            toolStripComboBox1.ComboBox.ValueMember = "DEPTID";
            toolStripComboBox1.ComboBox.DataSource = dtComboDepts;
            conexion.cerrar();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevoEmpRHNOM frmnew = new NuevoEmpRHNOM();
            frmnew.opcion = 1;
            frmnew.Show();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevoEmpRHNOM frmnew = new NuevoEmpRHNOM();
            frmnew.opcion = 2;
            frmnew.Show();
            frmnew.textBoxID.Text = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            frmnew.textBoxNombre.Text = dataGridViewDatos.CurrentRow.Cells[1].Value.ToString();
            frmnew.comboBoxPuesto.Text = dataGridViewDatos.CurrentRow.Cells[2].Value.ToString();
            frmnew.comboBoxDep.Text= dataGridViewDatos.CurrentRow.Cells[3].Value.ToString();
            frmnew.comboBoxHor.Text = dataGridViewDatos.CurrentRow.Cells[4].Value.ToString();
            frmnew.labelHOR.Text= dataGridViewDatos.CurrentRow.Cells[4].Value.ToString();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
            {

                conexion.abrir();
                string query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                Debug.WriteLine(query);
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmployees);
                dataGridViewDatos.DataSource = dtEmployees;
                conexion.cerrar();
            }
            else
            {
                conexion.abrir();
                string query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                Debug.WriteLine(query);
                Debug.WriteLine(query);
                //string query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmployees);
                dataGridViewDatos.DataSource = dtEmployees;
                conexion.cerrar();
            }
        }

        private void toolStripTextBoxID_TextChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;
            if (toolStripTextBoxID.Text == "")
            {
                if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                    //query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE USERINFOCUS.DEFAULTDEPTID<>32";
                    Debug.WriteLine(query);
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    //query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "'";
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
            }
            else
            {
                toolStripTextBoxNombre.Clear();
                if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND  USERINFOCUS.Badgenumber LIKE '%" + toolStripTextBoxID.Text + "%'  AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    //query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE Badgenumber LIKE '%" + toolStripTextBoxID.Text + "%' AND USERINFOCUS.DEFAULTDEPTID<>32";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE HORARIOS.TIPOHOR=1 AND USERINFOCUS.Badgenumber LIKE '%" + toolStripTextBoxID.Text + "%' AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    // query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE Badgenumber LIKE '%" + toolStripTextBoxID.Text + "%' AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();

                }
            }
        }

        private void toolStripTextBoxNombre_TextChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;

            if (toolStripTextBoxNombre.Text == "")
            {

                if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    //query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE USERINFOCUS.DEFAULTDEPTID<>32";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE HORARIOS.TIPOHOR=1 AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    //query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
            }
            else
            {
                toolStripTextBoxID.Clear();
                if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND HORARIOS.TIPOHOR=1 AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    //query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI; ";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
                else
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HORARIOS.Descripcion as 'HORARIO',ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER) INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE HORARIOS.TIPOHOR=1 AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI  ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    // query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI; ";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
            }
        }

        private void bajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("El Empleado con el ID: " + dataGridViewDatos.CurrentRow.Cells[0].Value.ToString() + " y Nombre: " + dataGridViewDatos.CurrentRow.Cells[1].Value.ToString() + " será dado de baja del sistema.", "Baja Empleado", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            switch (result)
            {
                case DialogResult.OK:
                    try
                    {
                        conexionbd conexion = new conexionbd();
                        conexion.abrir();
                        string query = "UPDATE USERINFOCUS SET DEFAULTDEPTID=32 WHERE Badgenumber=" + dataGridViewDatos.CurrentRow.Cells[0].Value.ToString() + "";
                        SqlCommand comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();
                        conexion.cerrar();
                        MessageBox.Show("Empleado dado de baja correctamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("No se realizó ninguna acción", "Operación Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
    }
}
