using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace EmpManagement
{
    public partial class SolicitudesCap : Form
    {
        public SolicitudesCap()
        {
            InitializeComponent();
        }

        private void SolicitudesCap_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Add("En espera");
            toolStripComboBox1.Items.Add("Activas");
            toolStripComboBox1.Items.Add("Terminadas");
            toolStripComboBox1.Items.Add("Rechazadas");
            toolStripComboBox1.ComboBox.SelectedIndex = 0;

            DataTable dtvalid = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query = "select subclasificacion from users where nombre='" + Program.usuario + "' and subclasificacion is not null";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtvalid);
            if (dtvalid.Rows.Count > 0)
            {
                exportarToolStripMenuItem.Visible = false;
                reportesToolStripMenuItem.Visible = false;
            }
            else
            {
                exportarToolStripMenuItem.Visible = true;
                reportesToolStripMenuItem.Visible = true;
            }
            conexion.cerrar();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizagrid();
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0:
                    excelToolStripMenuItem.Visible = true;
                    rechazarToolStripMenuItem.Visible = true;
                    terminadaToolStripMenuItem.Visible = false;
                    reportesToolStripMenuItem.Enabled = true;
                    break;
                case 1:
                    excelToolStripMenuItem.Visible = false;
                    rechazarToolStripMenuItem.Visible = false;
                    terminadaToolStripMenuItem.Visible = true;
                    reportesToolStripMenuItem.Enabled = true;
                    break;
                case 2:
                    excelToolStripMenuItem.Visible = false;
                    rechazarToolStripMenuItem.Visible = false;
                    terminadaToolStripMenuItem.Visible = false;
                    reportesToolStripMenuItem.Enabled = true;
                    break;
                case 3:
                    excelToolStripMenuItem.Visible = true;
                    rechazarToolStripMenuItem.Visible = false;
                    terminadaToolStripMenuItem.Visible = false;
                    reportesToolStripMenuItem.Enabled = false;
                    break;
            }
        }
        public void actualizagrid()
        {
            DataTable dtcaps = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query;
            query = "SELECT ID_CAP AS ID, NombreInstru as 'Instructor',Nombrecap as Curso, Descripcion as 'Descripción del curso',fec_rec as 'Fecha recepción',Fec_in as 'Fecha inicio', Fec_fin as 'Fecha termino',duracion as 'Duración (HRS)',Solicitante FROM CAPACITACION where estatus=" + toolStripComboBox1.SelectedIndex.ToString();
            SqlDataAdapter adaptador0 = new SqlDataAdapter(query, conexion.con);
            adaptador0.Fill(dtcaps);
            dataGridViewDatos.DataSource = dtcaps;
            conexion.cerrar();
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.Rows.Count > 0)
            {
                conexionbd conexion = new conexionbd();
                DialogResult resultado = MessageBox.Show("¿Seguro que desea aprobar esta capacitación?", "Aprobación Capacitación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    conexion.abrir();
                    string query = "UPDATE CAPACITACION SET estatus=1 WHERE ID_CAP=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                    SqlCommand comando = new SqlCommand(query, conexion.con);
                    comando.ExecuteNonQuery();
                    conexion.cerrar();
                    actualizagrid();
                }
            }
            else
            {
                MessageBox.Show("No hay información.");
            }
            
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DialogResult resultado = MessageBox.Show("¿Seguro que desea marcar como Terminada?", "Terminación Capacitación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                conexion.abrir();
                string query = "UPDATE CAPACITACION SET estatus=2 WHERE ID_CAP=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                actualizagrid();
            }

        }

        private void hojaDeAsistenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.Rows.Count > 0)
            {
                // C:\Users\userf\source\repos\EmpManagement\EmpManagement\documentos\gafete.xlsx
                conexionbd conexion = new conexionbd();
                DataTable dtem = new DataTable();
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;
                Excel.Range oRng;

                int contador = 0;

                conexion.abrir();
                string query = "SELECT EMPYCAP.BADGENUMBER, USERINFOCUS.NAME, USERINFOCUS.PUESTO,DEPARTMENTS.DEPTNAME FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID INNER JOIN EMPYCAP ON USERINFOCus.Badgenumber=EMPYCAP.BADGENUMBER WHERE EMPYCAP.ID_CAP=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtem);
                conexion.cerrar();
                string[,] saNames = new string[dtem.Rows.Count, 2];
                //Debug.WriteLine(dataGridViewDatos.Rows.Count);

                foreach (DataRow row in dtem.Rows)
                {
                    saNames[contador, 0] = row["Name"].ToString();
                    saNames[contador, 1] = row["DEPTNAME"].ToString();
                    contador = contador + 1;
                }
                contador = 0;
                // try
                //{
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                string oldName = @"C:\Excel\ListaAsistencia.xlsx";
                string newName = @"C:\Excel\ListaAsistencia" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString() + ".xlsx";
                //System.IO.File.Copy(oldName, newName);

                bool result = System.IO.File.Exists(newName);
                if (result == true)
                {
                    Debug.WriteLine("File Found");
                }
                else
                {
                    System.IO.File.Copy(oldName, newName);
                    Debug.WriteLine("File Not Found");
                }

                //oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Excel\SolicitudCapacitacion.xlsx"));
                oWB = (Excel._Workbook)(oXL.Workbooks.Open(newName));

                //Get a new workbook.
                //oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Excel\ListaAsistencia.xlsx"));
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
                oSheet.get_Range("B8").Value2 = dataGridViewDatos.CurrentRow.Cells["Curso"].Value.ToString();
                oSheet.get_Range("B9").Value2 = dataGridViewDatos.CurrentRow.Cells["Instructor"].Value.ToString();
                oSheet.get_Range("B10").Value2 = dataGridViewDatos.CurrentRow.Cells["Duración (HRS)"].Value.ToString();

                oSheet.get_Range("E9").Value2 = DateTime.Parse(dataGridViewDatos.CurrentRow.Cells["Fecha inicio"].Value.ToString()).ToShortDateString();
                oSheet.get_Range("E11").Value2 = DateTime.Parse(dataGridViewDatos.CurrentRow.Cells["Fecha termino"].Value.ToString()).ToShortDateString();

                if (dtem.Rows.Count > 20)
                {
                    int incremento;
                    incremento = dtem.Rows.Count - 20;
                    for (int i = 0; i < incremento; i++)
                    {
                        oSheet.Range["A32"].EntireRow.Insert();
                    }

                }
                oSheet.get_Range("A13", "B" + (12 + dtem.Rows.Count).ToString()).Value2 = saNames;
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

            }
            else
            {
                MessageBox.Show("No hay información.");
            }

        }

        private void hojaDeEvaluaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.Rows.Count > 0)
            {
                // C:\Users\userf\source\repos\EmpManagement\EmpManagement\documentos\gafete.xlsx
                conexionbd conexion = new conexionbd();
                DataTable dtem = new DataTable();
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;
                Excel.Range oRng;

                int contador = 0;

                conexion.abrir();
                string query = "SELECT EMPYCAP.BADGENUMBER, USERINFOCUS.NAME, USERINFOCUS.PUESTO,DEPARTMENTS.DEPTNAME FROM USERINFOCus INNER JOIN DEPARTMENTS ON USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID INNER JOIN EMPYCAP ON USERINFOCus.Badgenumber=EMPYCAP.BADGENUMBER WHERE EMPYCAP.ID_CAP=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtem);
                conexion.cerrar();
                string[,] saNames = new string[dtem.Rows.Count, 2];
                //Debug.WriteLine(dataGridViewDatos.Rows.Count);

                foreach (DataRow row in dtem.Rows)
                {
                    saNames[contador, 0] = row["Name"].ToString();
                    saNames[contador, 1] = row["PUESTO"].ToString();
                    contador = contador + 1;
                }
                contador = 0;
                // try
                //{
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                string oldName = @"C:\Excel\EvaluaciondelInstructor.xlsx";
                string newName = @"C:\Excel\EvaluaciondelInstructor" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString() + ".xlsx";

                bool result = System.IO.File.Exists(newName);
                if (result == true)
                {
                    Debug.WriteLine("File Found");
                }
                else
                {
                    System.IO.File.Copy(oldName, newName);
                    Debug.WriteLine("File Not Found");
                }
                //oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Excel\SolicitudCapacitacion.xlsx"));
                oWB = (Excel._Workbook)(oXL.Workbooks.Open(newName));

                //Get a new workbook.
                //oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Excel\ListaAsistencia.xlsx"));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                //Excel.Worksheet newWorksheet;
                //newWorksheet = (Excel.Worksheet)oWB.Worksheets.Add();         

                for (int i = 0; i <= dtem.Rows.Count - 2; i++)
                {
                    oSheet.Copy(oWB.Sheets[1]);
                }
                int j = 0;

                for (int i = 1; i <= dtem.Rows.Count; i++)
                {
                    oSheet = (Excel._Worksheet)oWB.Sheets[i];
                    oSheet.get_Range("C9").Value2 = dataGridViewDatos.CurrentRow.Cells["Curso"].Value.ToString();
                    oSheet.get_Range("C10").Value2 = dataGridViewDatos.CurrentRow.Cells["Instructor"].Value.ToString();
                    oSheet.get_Range("C14").Value2 = DateTime.Parse(dataGridViewDatos.CurrentRow.Cells["Fecha termino"].Value.ToString()).ToShortDateString();

                    oSheet.get_Range("C12").Value2 = saNames[j, 0];
                    oSheet.get_Range("C13").Value2 = saNames[j, 1];
                    // oSheet.get_Range("A13", "B" + (12 + dtem.Rows.Count).ToString()).Value2 = saNames;
                    Debug.WriteLine(saNames[j, 0]);
                    Debug.WriteLine(saNames[j, 1]);

                    j++;
                }

                j = 0;
                //Add table headers going cell by cell.
                /* oSheet.Cells[1, 1] = "First Name";
                 oSheet.Cells[1, 2] = "Last Name";
                 oSheet.Cells[1, 3] = "Full Name";
                 oSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.

                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("B8").Value2 = dataGridViewDatos.CurrentRow.Cells["Curso"].Value.ToString();
                oSheet.get_Range("B9").Value2 = dataGridViewDatos.CurrentRow.Cells["Instructor"].Value.ToString();
                oSheet.get_Range("B10").Value2 = dataGridViewDatos.CurrentRow.Cells["Duración (HRS)"].Value.ToString();

                oSheet.get_Range("E9").Value2 = DateTime.Parse(dataGridViewDatos.CurrentRow.Cells["Fecha inicio"].Value.ToString()).ToShortDateString();
                oSheet.get_Range("E11").Value2 = DateTime.Parse(dataGridViewDatos.CurrentRow.Cells["Fecha termino"].Value.ToString()).ToShortDateString();

                if (dtem.Rows.Count > 20)
                {
                    int incremento;
                    incremento = dtem.Rows.Count - 20;
                    for (int i = 0; i < incremento; i++)
                    {
                        oSheet.Range["A32"].EntireRow.Insert();
                    }

                }
                oSheet.get_Range("A13", "B" + (12 + dtem.Rows.Count).ToString()).Value2 = saNames;
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

            }
            else
            {
                MessageBox.Show("No hay información");
            }
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

        private void rechazarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.Rows.Count > 0)
            {
                conexionbd conexion = new conexionbd();
                DialogResult resultado = MessageBox.Show("¿Seguro que desea rechazar esta capacitación?", "Rechazo Capacitación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    conexion.abrir();
                    string query = "UPDATE CAPACITACION SET estatus=3 WHERE ID_CAP=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                    SqlCommand comando = new SqlCommand(query, conexion.con);
                    comando.ExecuteNonQuery();
                    conexion.cerrar();
                    actualizagrid();
                }
            }
            else
            {
                MessageBox.Show("No hay información ");
            }
    

        }

        private void terminadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.Rows.Count > 0)
            {
                conexionbd conexion = new conexionbd();
                DialogResult resultado = MessageBox.Show("¿Seguro que desea dar por terminada esta capacitación?", "Terminación Capacitación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    conexion.abrir();
                    string query = "UPDATE CAPACITACION SET estatus=2 WHERE ID_CAP=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                    SqlCommand comando = new SqlCommand(query, conexion.con);
                    comando.ExecuteNonQuery();
                    conexion.cerrar();
                    actualizagrid();
                }
            }
            else
            {
                MessageBox.Show("No hay información");
            }
       
        }
    }
}
