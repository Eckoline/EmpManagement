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
    public partial class Directorio : Form
    {
        public Directorio()
        {
            InitializeComponent();
        }

        private void Directorio_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            DataTable dtvalid = new DataTable();
            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS ORDER BY DEPTID ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            toolStripComboBox1.ComboBox.DisplayMember = "DEPTNAME";
            toolStripComboBox1.ComboBox.ValueMember = "DEPTID";
            toolStripComboBox1.ComboBox.DataSource = dtComboDepts;

            query = "select subclasificacion from users where nombre='"+Program.usuario+ "' and subclasificacion is not null";
            Debug.WriteLine(query);
            adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtvalid);
            if (dtvalid.Rows.Count > 0)
            {
                accionesToolStripMenuItem.Visible = false;
            }
            else
            {
                accionesToolStripMenuItem.Visible = true;
            }

            conexion.cerrar();
            
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            NuevoEmpleado frmEmpleados = new NuevoEmpleado();
            frmEmpleados.opcion = 2;
            frmEmpleados.textBoxID.Enabled =false;
            frmEmpleados.Show();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            NuevoEmpleado frmactualizar = new NuevoEmpleado();
            frmactualizar.opcion = 1;
            frmactualizar.textBoxID.Text = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            frmactualizar.textBoxID.Enabled = false;
            frmactualizar.textBoxNombre.Text = dataGridViewDatos.CurrentRow.Cells[1].Value.ToString();
            frmactualizar.textBoxNss.Text = dataGridViewDatos.CurrentRow.Cells[2].Value.ToString();
            frmactualizar.textBoxCurp.Text = dataGridViewDatos.CurrentRow.Cells[3].Value.ToString();
            frmactualizar.textBoxRFC.Text = dataGridViewDatos.CurrentRow.Cells[4].Value.ToString();
            frmactualizar.dateTimePickerFecNac.Text = dataGridViewDatos.CurrentRow.Cells[5].Value.ToString();
            frmactualizar.textBoxDireccion.Text = dataGridViewDatos.CurrentRow.Cells[6].Value.ToString();
            frmactualizar.textBoxCiudad.Text = dataGridViewDatos.CurrentRow.Cells[7].Value.ToString();
            frmactualizar.textBoxEstado.Text = dataGridViewDatos.CurrentRow.Cells[8].Value.ToString();
            frmactualizar.textBoxCP.Text = dataGridViewDatos.CurrentRow.Cells[9].Value.ToString();
            frmactualizar.textBoxTel.Text = dataGridViewDatos.CurrentRow.Cells[10].Value.ToString();
            frmactualizar.textBoxTelEme.Text = dataGridViewDatos.CurrentRow.Cells[11].Value.ToString();
            frmactualizar.comboBoxGenero.Text = dataGridViewDatos.CurrentRow.Cells[12].Value.ToString();
            frmactualizar.comboBoxEstCivil.Text = dataGridViewDatos.CurrentRow.Cells[13].Value.ToString();
            frmactualizar.comboBoxNivelE.Text = dataGridViewDatos.CurrentRow.Cells[14].Value.ToString();
            frmactualizar.labelpuesto.Text= dataGridViewDatos.CurrentRow.Cells[15].Value.ToString();
            frmactualizar.dateTimePickerFechaIngreso.Text = dataGridViewDatos.CurrentRow.Cells[17].Value.ToString();
            frmactualizar.textBoxVehiculo.Text = dataGridViewDatos.CurrentRow.Cells[18].Value.ToString();
            frmactualizar.Show();
            frmactualizar.comboBoxDep.Text = dataGridViewDatos.CurrentRow.Cells[16].Value.ToString();
            //frmactualizar.comboBoxHor.Text= dataGridViewDatos.CurrentRow.Cells[20].Value.ToString();
            frmactualizar.textBoxCorreo.Text = dataGridViewDatos.CurrentRow.Cells[19].Value.ToString();
            */
            NuevoEmpleado frmactualizar = new NuevoEmpleado();
            //frmactualizar.Enabled = false;
            frmactualizar.opcion = 1;
            frmactualizar.textBoxID.Text = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            frmactualizar.textBoxID.Enabled = false;
            frmactualizar.textBoxNombre.Text = dataGridViewDatos.CurrentRow.Cells[1].Value.ToString();
            frmactualizar.textBoxNss.Text = dataGridViewDatos.CurrentRow.Cells[2].Value.ToString();
            frmactualizar.textBoxCurp.Text = dataGridViewDatos.CurrentRow.Cells[3].Value.ToString();
            frmactualizar.textBoxRFC.Text = dataGridViewDatos.CurrentRow.Cells[4].Value.ToString();
            frmactualizar.dateTimePickerFecNac.Text = dataGridViewDatos.CurrentRow.Cells[5].Value.ToString();
            frmactualizar.textBoxDireccion.Text = dataGridViewDatos.CurrentRow.Cells[6].Value.ToString();
            frmactualizar.textBoxCiudad.Text = dataGridViewDatos.CurrentRow.Cells[7].Value.ToString();
            frmactualizar.textBoxEstado.Text = dataGridViewDatos.CurrentRow.Cells[8].Value.ToString();
            frmactualizar.textBoxCP.Text = dataGridViewDatos.CurrentRow.Cells[9].Value.ToString();
            frmactualizar.textBoxTel.Text = dataGridViewDatos.CurrentRow.Cells[10].Value.ToString();
            frmactualizar.textBoxTelEme.Text = dataGridViewDatos.CurrentRow.Cells[11].Value.ToString();
            frmactualizar.textBoxDescCon.Text = dataGridViewDatos.CurrentRow.Cells[12].Value.ToString();
            frmactualizar.comboBoxGenero.Text = dataGridViewDatos.CurrentRow.Cells[13].Value.ToString();
            
            frmactualizar.comboBoxNivelE.Text = dataGridViewDatos.CurrentRow.Cells[15].Value.ToString();
            frmactualizar.labelpuesto.Text = dataGridViewDatos.CurrentRow.Cells[16].Value.ToString();
            frmactualizar.dateTimePickerFechaIngreso.Text = dataGridViewDatos.CurrentRow.Cells[18].Value.ToString();
            frmactualizar.textBoxVehiculo.Text = dataGridViewDatos.CurrentRow.Cells[19].Value.ToString();
            frmactualizar.textBoxEdad.Text = dataGridViewDatos.CurrentRow.Cells[21].Value.ToString();
            frmactualizar.textBoxAntigu.Text = dataGridViewDatos.CurrentRow.Cells[22].Value.ToString();
            frmactualizar.textBoxVehiculo.Text = dataGridViewDatos.CurrentRow.Cells[19].Value.ToString();

            frmactualizar.Show();
            frmactualizar.comboBoxDep.Text = dataGridViewDatos.CurrentRow.Cells[17].Value.ToString();
            frmactualizar.textBoxCorreo.Text = dataGridViewDatos.CurrentRow.Cells[20].Value.ToString();
            frmactualizar.comboBoxEstCivil.Text = dataGridViewDatos.CurrentRow.Cells[14].Value.ToString();
        }

        private void bajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
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

                        query = "DELETE FROM HOREMPLEADO WHERE Badgenumber=" + dataGridViewDatos.CurrentRow.Cells[0].Value.ToString() + "";
                        comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();


                        query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID)) WHERE USERINFOCUS.DEFAULTDEPTID<>32 ORDER BY USERINFOCUS.Badgenumber;";
                        Debug.WriteLine(query);
                        SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                        adaptador.Fill(dtEmployees);
                        dataGridViewDatos.DataSource = dtEmployees;

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

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
            {

                conexion.abrir();
                string query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) ) WHERE USERINFOCUS.DEFAULTDEPTID<>32 ORDER BY USERINFOCUS.Badgenumber;";
                Debug.WriteLine(query);
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmployees);
                dataGridViewDatos.DataSource = dtEmployees;
                conexion.cerrar();
            }
            else
            {
                conexion.abrir();
                string query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) ) WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' ORDER BY USERINFOCUS.Badgenumber;";
                Debug.WriteLine(query);
                Debug.WriteLine(query);
                //string query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmployees);
                dataGridViewDatos.DataSource = dtEmployees;
                conexion.cerrar();
            }

            labelCant.Text = dataGridViewDatos.Rows.Count.ToString();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;
            if (toolStripTextBoxID.Text == "")
            {
                if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID)) WHERE USERINFOCUS.DEFAULTDEPTID<>32 ORDER BY USERINFOCUS.Badgenumber;";
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
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID))  WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' ORDER BY USERINFOCUS.Badgenumber;";
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
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID))  WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND  USERINFOCUS.Badgenumber LIKE '%" + toolStripTextBoxID.Text + "%'  ORDER BY USERINFOCUS.Badgenumber;";
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
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) ) WHERE USERINFOCUS.Badgenumber LIKE '%" + toolStripTextBoxID.Text + "%' AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    // query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE Badgenumber LIKE '%" + toolStripTextBoxID.Text + "%' AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();

                }
            }

        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            string query;

            if (toolStripTextBoxNombre.Text == "")
            {

                if (toolStripComboBox1.ComboBox.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    conexion.abrir();
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) ) WHERE USERINFOCUS.DEFAULTDEPTID<>32 ORDER BY USERINFOCUS.Badgenumber;";
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
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) ) WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' ORDER BY USERINFOCUS.Badgenumber;";
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
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) ) WHERE USERINFOCUS.DEFAULTDEPTID<>32 AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI ORDER BY USERINFOCUS.Badgenumber;";
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
                    query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID) ) WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI  ORDER BY USERINFOCUS.Badgenumber;";
                    Debug.WriteLine(query);
                    // query = "SELECT Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.ComboBox.Text + "' AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI; ";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmployees);
                    dataGridViewDatos.DataSource = dtEmployees;
                    conexion.cerrar();
                }
            }
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevoEmpleado frmactualizar = new NuevoEmpleado();
            frmactualizar.Enabled = false;
            frmactualizar.opcion = 1;
            frmactualizar.textBoxID.Text = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            frmactualizar.textBoxID.Enabled = false;
            frmactualizar.textBoxNombre.Text = dataGridViewDatos.CurrentRow.Cells[1].Value.ToString();
            frmactualizar.textBoxNss.Text = dataGridViewDatos.CurrentRow.Cells[2].Value.ToString();
            frmactualizar.textBoxCurp.Text = dataGridViewDatos.CurrentRow.Cells[3].Value.ToString();
            frmactualizar.textBoxRFC.Text = dataGridViewDatos.CurrentRow.Cells[4].Value.ToString();
            frmactualizar.dateTimePickerFecNac.Text = dataGridViewDatos.CurrentRow.Cells[5].Value.ToString();
            frmactualizar.textBoxDireccion.Text = dataGridViewDatos.CurrentRow.Cells[6].Value.ToString();
            frmactualizar.textBoxCiudad.Text = dataGridViewDatos.CurrentRow.Cells[7].Value.ToString();
            frmactualizar.textBoxEstado.Text = dataGridViewDatos.CurrentRow.Cells[8].Value.ToString();
            frmactualizar.textBoxCP.Text = dataGridViewDatos.CurrentRow.Cells[9].Value.ToString();
            frmactualizar.textBoxTel.Text = dataGridViewDatos.CurrentRow.Cells[10].Value.ToString();
            frmactualizar.textBoxTelEme.Text = dataGridViewDatos.CurrentRow.Cells[11].Value.ToString();
            frmactualizar.textBoxDescCon.Text = dataGridViewDatos.CurrentRow.Cells[12].Value.ToString();
            frmactualizar.comboBoxGenero.Text = dataGridViewDatos.CurrentRow.Cells[13].Value.ToString();
            
            frmactualizar.comboBoxNivelE.Text = dataGridViewDatos.CurrentRow.Cells[15].Value.ToString();
            frmactualizar.labelpuesto.Text = dataGridViewDatos.CurrentRow.Cells[16].Value.ToString();
            frmactualizar.dateTimePickerFechaIngreso.Text = dataGridViewDatos.CurrentRow.Cells[18].Value.ToString();
            frmactualizar.textBoxVehiculo.Text = dataGridViewDatos.CurrentRow.Cells[19].Value.ToString();
            frmactualizar.textBoxEdad.Text = dataGridViewDatos.CurrentRow.Cells[21].Value.ToString();
            frmactualizar.textBoxAntigu.Text = dataGridViewDatos.CurrentRow.Cells[22].Value.ToString();
            frmactualizar.textBoxVehiculo.Text = dataGridViewDatos.CurrentRow.Cells[19].Value.ToString();

            frmactualizar.Show();
            frmactualizar.comboBoxDep.Text = dataGridViewDatos.CurrentRow.Cells[17].Value.ToString();
            frmactualizar.textBoxCorreo.Text = dataGridViewDatos.CurrentRow.Cells[20].Value.ToString();
            frmactualizar.comboBoxEstCivil.Text = dataGridViewDatos.CurrentRow.Cells[14].Value.ToString();
            //frmactualizar.comboBoxHor.Text = dataGridViewDatos.CurrentRow.Cells[20].Value.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            toolStripTextBoxID.Text = "";
            toolStripTextBoxNombre.Text = "";
            DataTable dtEmployees = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query = "SELECT userinfocus.Badgenumber AS ID,Name AS NOMBRE,SSN,CURP,RFC,BIRTHDAY AS 'F. NACIMIENTO',STREET AS CALLE,CITY AS CIUDAD,STATE AS ESTADO,ZIP AS 'CP',OPHONE AS 'TEL',TELEMERGENCIA AS 'TEL. EM.',contactoeme AS 'DESCRIP. TEL. EM.',Gender AS GENERO,ESTADOCIVIL AS 'EST. CIVIL',Title AS ESTUDIOS,PUESTO,DEPARTMENTS.DEPTNAME AS DEPARTAMENTO,HIREDDAY AS 'F. INGRESO.',VEHICULO,CORREO,ROUND((cast(datediff(DD,USERINFOCus.BIRTHDAY,GETDATE()) / 365.25 as float)),1) as EDAD,ROUND((cast(datediff(DD,USERINFOCus.HIREDDAY,GETDATE()) / 365.25 as float)),1) as ANTIGUEDAD  FROM ((USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID)) WHERE USERINFOCUS.DEFAULTDEPTID<>32 ORDER BY USERINFOCUS.Badgenumber;";
            Debug.WriteLine(query);
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtEmployees);
            dataGridViewDatos.DataSource = dtEmployees;
            conexion.cerrar();
        }
    }
}
