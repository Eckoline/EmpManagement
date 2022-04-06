using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;

namespace EmpManagement
{
    public partial class Capacitacion : Form
    {
        public Capacitacion()
        {
            InitializeComponent();
        }
        private void Capacitacion_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            string query = "";
            SqlDataAdapter adaptador = new SqlDataAdapter();

            this.BringToFront();
            DataTable dtComboDepts = new DataTable();

            conexion.abrir();
            query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS WHERE DEPTNAME<>'BAJAS' ORDER BY DEPTID ";
            adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            toolStripComboBox1.ComboBox.DisplayMember = "DEPTNAME";
            toolStripComboBox1.ComboBox.ValueMember = "DEPTID";
            toolStripComboBox1.ComboBox.DataSource = dtComboDepts;

            query = "DELETE FROM temp";
            SqlCommand comando = new SqlCommand(query, conexion.con);
            comando.ExecuteNonQuery();
            conexion.cerrar();
            labelsoli.Text = Program.usuario;
            Cargacompplas();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
            {
                Cargacompplas();
            }
            else
            {
                Cargawit();
            }
        }

        public void Cargacompplas()
        {
            conexionbd conexion = new conexionbd();
            string query = "";
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable dtEmp = new DataTable();
            conexion.abrir();
            query = "SELECT BADGENUMBER AS ID,NAME AS NOMBRE,PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME NOT IN('BAJAS','COMPAÑIA PLASTICA INTERNACIONAL') AND BADGENUMBER NOT IN(SELECT campo1 FROM temp)";
            adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtEmp);
            conexion.cerrar();
            dataGridViewDatos.DataSource = dtEmp;
        }

        public void Cargawit()
        {
            conexionbd conexion = new conexionbd();
            string query = "";
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable dtEmp = new DataTable();
            conexion.abrir();
            query = "SELECT BADGENUMBER AS ID,NAME AS NOMBRE,PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME NOT IN('BAJAS','COMPAÑIA PLASTICA INTERNACIONAL') AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.Text + "' AND Badgenumber NOT IN(SELECT campo1 FROM temp)";
            adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtEmp);
            dataGridViewDatos.DataSource = dtEmp;
            conexion.cerrar();
        }

        public void Cargawitname()
        {
            conexionbd conexion = new conexionbd();
            string query = "";
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable dtEmp = new DataTable();
            conexion.abrir();
            if (toolStripComboBox1.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
            {
                query = "SELECT BADGENUMBER AS ID,NAME AS NOMBRE,PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME NOT IN('BAJAS','COMPAÑIA PLASTICA INTERNACIONAL') AND USERINFOCus.Badgenumber NOT IN(SELECT campo1 FROM temp) AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI; ";
            }
            else
            {
                query = "SELECT BADGENUMBER AS ID,NAME AS NOMBRE,PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTNAME NOT IN('BAJAS','COMPAÑIA PLASTICA INTERNACIONAL') AND DEPARTMENTS.DEPTNAME='" + toolStripComboBox1.Text + "' AND USERINFOCus.Badgenumber NOT IN(SELECT campo1 FROM temp) AND USERINFOCus.Name LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Modern_Spanish_CI_AI; ";
            }

            adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtEmp);
            dataGridViewDatos.DataSource = dtEmp;
            conexion.cerrar();
        }

        public void CargaAsis()
        {
            conexionbd conexion = new conexionbd();
            string query = "";
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable dtEmp = new DataTable();
            query = "SELECT campo1 AS ID, campo2 AS Nombre, campo3 AS Puesto, campo4 AS Departamento FROM temp";
            adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtEmp);
            dataGridViewAsistentes.DataSource = dtEmp;

        }

        private void buttonEnv_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.RowCount > 0)
            {
                conexionbd conexion = new conexionbd();
                string query = "";
                SqlDataAdapter adaptador = new SqlDataAdapter();
                string id, nombre, puesto, departamento;
                conexion.abrir();
                if (dataGridViewDatos.AreAllCellsSelected(true))
                {
                    foreach (DataGridViewRow row in dataGridViewDatos.Rows)
                    {
                        query = "INSERT INTO temp(campo1,campo2,campo3,campo4) values(" + row.Cells["ID"].Value.ToString() + ",'" + row.Cells["Nombre"].Value.ToString() + "','" + row.Cells["Puesto"].Value.ToString() + "','" + row.Cells["Departamento"].Value.ToString() + "')";
                        Console.WriteLine(query);
                        SqlCommand comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();
                    }
                }
                else
                {
                    id = dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                    nombre = dataGridViewDatos.CurrentRow.Cells["NOMBRE"].Value.ToString();
                    puesto = dataGridViewDatos.CurrentRow.Cells["PUESTO"].Value.ToString();
                    departamento = dataGridViewDatos.CurrentRow.Cells["DEPARTAMENTO"].Value.ToString();
                    query = "INSERT INTO temp(campo1,campo2,campo3,campo4) values(" + id + ",'" + nombre + "','" + puesto + "','" + departamento + "')";
                    SqlCommand comando = new SqlCommand(query, conexion.con);
                    comando.ExecuteNonQuery();
                }
                CargaAsis();
                Cargacompplas();
                conexion.cerrar();
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {

            if (dataGridViewAsistentes.RowCount > 0)
            {
                conexionbd conexion = new conexionbd();
                conexion.abrir();
                string query = "";
                SqlDataAdapter adaptador = new SqlDataAdapter();
                if (dataGridViewAsistentes.AreAllCellsSelected(true))
                {
                    foreach (DataGridViewRow row in dataGridViewAsistentes.Rows)
                    {
                        query = "DELETE FROM temp where campo1=" + row.Cells["ID"].Value.ToString();
                        SqlCommand comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();
                    }
                }
                else
                {
                    string id;
                    id = dataGridViewAsistentes.CurrentRow.Cells["ID"].Value.ToString();
                    query = "DELETE FROM temp WHERE campo1=" + id;
                    SqlCommand comando = new SqlCommand(query, conexion.con);
                    comando.ExecuteNonQuery();
                }
                conexion.cerrar();
                CargaAsis();
                Cargacompplas();
            }
        }

        private void toolStripTextBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBoxNombre.Text == "")
            {
                if (toolStripComboBox1.Text == "COMPAÑIA PLASTICA INTERNACIONAL")
                {
                    Cargacompplas();
                }
                else
                {
                    Cargawit();
                }
            }
            else
            {
                Cargawitname();
            }
        }

        private void excelToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void terminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewAsistentes.Rows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Seguro que desea terminar?", "Terminar solicitud", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {

                    if ((textBoxInstructor.Text.Equals("")) || (textBoxNomCap.Text.Equals("")) || textBoxDesc.Text.Equals("") || (textBoxHor.Text.Equals("")) || (DateTime.Parse(dateTimePicker1.Value.ToString("dd-MM-yyyy")) < DateTime.Parse(dateTimePicker2.Value.ToString("dd-MM-yyyy"))))
                    {
                        Debug.WriteLine(dateTimePicker1.Value.ToString());
                        Debug.WriteLine(dateTimePicker2.Value.ToString());
                        MessageBox.Show("Los datos introducidos no son correctos o no se han llenado todos los campos.");
                    }
                    else
                    {
                        conexionbd conexion = new conexionbd();
                        DataTable dtidcap = new DataTable();
                        string idcap;
                        string descripcion, instructor, nombrecap, solicitante;
                        string fecin, fecfin, fecrec;
                        nombrecap = textBoxNomCap.Text;
                        instructor = textBoxInstructor.Text;
                        descripcion = textBoxDesc.Text;
                        solicitante = labelsoli.Text;
                        fecin = dateTimePicker1.Value.ToString("MM-dd-yyyy");
                        fecfin = dateTimePicker2.Value.ToString("MM-dd-yyyy");
                        fecrec = DateTime.Now.ToString("MM-dd-yyyy");
                        conexion.abrir();
                        string query = "INSERT INTO CAPACITACION VALUES('" + nombrecap + "','" + descripcion + "','" + instructor + "','" + fecrec + "','" + fecin + "','" + fecfin + "','" + solicitante + "',0,'" + textBoxHor.Text + "')";
                        SqlCommand comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();
                        conexion.cerrar();

                        conexion.abrir();
                        query = "SELECT TOP 1 ID_CAP from capacitacion ORDER BY ID_CAP DESC;";
                        SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                        adaptador.Fill(dtidcap);
                        conexion.cerrar();
                        idcap = dtidcap.Rows[0]["ID_CAP"].ToString();

                        foreach (DataGridViewRow row in dataGridViewAsistentes.Rows)
                        {
                            conexion.abrir();
                            query = "INSERT INTO EMPYCAP VALUES(" + row.Cells["ID"].Value.ToString() + "," + idcap + ")";
                            comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                        }

                        conexion.abrir();
                        query = "INSERT INTO movimientos values(" + Program.id + ",'RESGISTRO CAPACITACIÓN CON EL ID: " + idcap + "','" + DateTime.Now + "','" + this.Text + "');";
                        comando = new SqlCommand(query, conexion.con);
                        comando.ExecuteNonQuery();
                        conexion.cerrar();

                        MessageBox.Show("Se ha realizado la solicitud.");
                        // C:\Users\userf\source\repos\EmpManagement\EmpManagement\documentos\gafete.xlsx
                        Excel.Application oXL;
                        Excel._Workbook oWB;
                        Excel._Worksheet oSheet;
                        Excel.Range oRng;
                        string[,] saNames = new string[dataGridViewAsistentes.Rows.Count, 3];
                        int contador = 0;
                        Debug.WriteLine(dataGridViewAsistentes.Rows.Count);
                        foreach (DataGridViewRow row in dataGridViewAsistentes.Rows)
                        {
                            saNames[contador, 0] = row.Cells["Nombre"].Value.ToString();
                            saNames[contador, 1] = row.Cells["PUESTO"].Value.ToString();
                            saNames[contador, 2] = row.Cells["DEPARTAMENTO"].Value.ToString();
                            contador = contador + 1;
                        }
                        contador = 0;
                        // try
                        //{
                        //Start Excel and get Application object.
                        oXL = new Excel.Application();
                        oXL.Visible = true;

                        //Get a new workbook.
                        string oldName = @"C:\Excel\SolicitudCapacitacion.xlsx";
                        string newName = @"C:\Excel\SolicitudCapacitacion" + idcap + ".xlsx";
                        System.IO.File.Copy(oldName, newName);

                        //oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Excel\SolicitudCapacitacion.xlsx"));
                        oWB = (Excel._Workbook)(oXL.Workbooks.Open(newName));
                        oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                        //Add table headers going cell by cell.
                        /* oSheet.Cells[1, 1] = "First Name";
                         oSheet.Cells[1, 2] = "Last Name";
                         oSheet.Cells[1, 3] = "Full Name";
                         oSheet.Cells[1, 4] = "Salary";
                        */
                        //Format A1:D1 as bold, vertical alignment = center.
                        /*
                        oSheet.get_Range("A1", "D1").Font.Bold = true;
                        oSheet.get_Range("A1", "D1").VerticalAlignment =
                        Excel.XlVAlign.xlVAlignCenter;
                        */
                        // Create an array to multiple values at once.
                        //Fill A2:B6 with an array of values (First and Last Names).
                        oSheet.get_Range("B9").Value2 = textBoxNomCap.Text;
                        oSheet.get_Range("B6").Value2 = DateTime.Now.ToShortDateString();

                        oSheet.get_Range("B10").Value2 = textBoxInstructor.Text;
                        oSheet.get_Range("B12").Value2 = textBoxHor.Text;

                        oSheet.get_Range("B11").Value2 = dateTimePicker1.Value.ToShortDateString();
                        oSheet.get_Range("D11").Value2 = dateTimePicker2.Value.ToShortDateString();

                        if (dataGridViewAsistentes.Rows.Count > 10)
                        {
                            int incremento;
                            incremento = dataGridViewAsistentes.Rows.Count - 10;
                            for (int i = 0; i < incremento; i++)
                            {
                                oSheet.Range["A20"].EntireRow.Insert();
                            }

                        }
                        oSheet.get_Range("A14", "C" + (13 + dataGridViewAsistentes.Rows.Count).ToString()).Value2 = saNames;
                        //oSheet.get_Range("A").Value2 = dataGridViewDatos.CurrentRow.Cells["PUESTO"].Value.ToString();
                        oXL.Visible = true;
                        oXL.UserControl = true;
                        // }
                        /*
                         catch (Exception theException)
                         {
                             String errorMessage;
                             errorMessage = "Error: ";
                             errorMessage = String.Concat(errorMessage, theException.Message);
                             errorMessage = String.Concat(errorMessage, " Line: ");
                             errorMessage = String.Concat(errorMessage, theException.Source);

                             MessageBox.Show(errorMessage, "Error");
                         }
                        */

                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe registrar al menos un asistente.");
            }
        }

        private void restablecerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            String query = "DELETE FROM TEMP";
            Console.WriteLine(query);
            SqlCommand comando = new SqlCommand(query, conexion.con);
            comando.ExecuteNonQuery();
            conexion.cerrar();

        }
    }
}
