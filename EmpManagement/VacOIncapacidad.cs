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
    public partial class VacOIncapacidad : Form
    {
        conexionbd conexion = new conexionbd();
        public VacOIncapacidad()
        {
            InitializeComponent();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAsig_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resultado = MessageBox.Show("¿Seguro que desea asignar este evento?", "Asignación Evento", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    string id, nombre, tipoeve, observaciones;
                    string fecin, fecfin;
                    int cuenta;

                    nombre = dataGridViewDatos.CurrentRow.Cells[1].Value.ToString();
                    fecin = dateTimePickerFecIn.Value.ToString("MM-dd-yyyy");
                    fecfin = dateTimePickerFecFin.Value.ToString("MM-dd-yyyy");
                    tipoeve = comboBoxTipo.SelectedValue.ToString();
                    if (DateTime.Parse(dateTimePickerFecFin.Value.ToString("dd-MM-yyyy")) >= DateTime.Parse(dateTimePickerFecIn.Value.ToString("dd-MM-yyyy")))
                    {
                        if (comboBoxTipo.SelectedIndex >= 0)
                        {
                            Int32 selectedRowCount = dataGridViewDatos.Rows.GetRowCount(DataGridViewElementStates.Selected);
                            if (selectedRowCount > 0)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                for (int i = 0; i < selectedRowCount; i++)
                                {
                                    //Debug.WriteLine(dataGridViewDatos.Rows[Int32.Parse(dataGridViewDatos.SelectedRows[i].Index.ToString())].Cells[0].Value.ToString());
                                    id = dataGridViewDatos.Rows[Int32.Parse(dataGridViewDatos.SelectedRows[i].Index.ToString())].Cells[0].Value.ToString();
                                    DataTable dtEmpValid = new DataTable();
                                    string query = "SELECT * FROM EVENEMP where id_even=" + tipoeve + " and badgenumber='" + id + "' and fecin='" + fecin + "' and fecfin='" + fecfin + "' and fecreg='" + DateTime.Now.ToString("MM-dd-yyyy") + "'";
                                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                                    adaptador.Fill(dtEmpValid);
                                    cuenta = dtEmpValid.Rows.Count;
                                    if (cuenta > 0)
                                    {
                                        MessageBox.Show("Registro repetido. Para id: " + id);
                                    }
                                    else
                                    {
                                        conexion.abrir();
                                        query = "INSERT INTO EVENEMP(ID_EVEN,BADGENUMBER,FECIN,FECFIN,FECREG,OBSERVACIONES) VALUES(" + tipoeve + "," + id + ",'" + fecin + "','" + fecfin + "','" + DateTime.Now.ToString("MM-dd-yyyy") + "','" + textBoxObs.Text + "')";
                                        SqlCommand comando = new SqlCommand(query, conexion.con);
                                        comando.ExecuteNonQuery();
                                        conexion.cerrar();
                                        actualizalista();
                                        dateTimePickerFecFin.Value = DateTime.Now;
                                        dateTimePickerFecIn.Value = DateTime.Now;
                                        textBoxObs.Text = "";
                                        MessageBox.Show("Registro completado con éxito");
                                    }
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Seleccione un tipo de evento");
                        }
                    }
                    else
                    {
                        MessageBox.Show("La fecha de termino no puede ser menor que la de inicio.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }
        public void actualizalista()
        {
            try
            {
                conexion.abrir();
                DataTable dtEmpEven = new DataTable();
                string query = "SELECT evenemp.id_even AS 'ID EVENTO',evenemp.badgenumber AS 'ID EMPLEADO',userinfocus.name AS 'NOMBRE',evento.descripcion 'DESCRIPCIÓN EVENTO',evenemp.fecin AS 'FECHA INICIO',evenemp.fecfin AS 'FECHA TERMINO',evenemp.OBSERVACIONES FROM (userinfocus inner join evenemp on userinfocus.badgenumber=evenemp.badgenumber) INNER JOIN evento on evenemp.id_even=evento.id_even where EVENEMP.FECREG>=CONVERT(DATE,GETDATE());";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmpEven);
                dataGridViewLista.DataSource = dtEmpEven;
                conexion.cerrar();
                dataGridViewDatos.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        private void VacOIncapacidad_Load(object sender, EventArgs e)
        {
            cargacomboDep();
            actualizadatos();
            actualizalista();
            cargacombo();
            toolStripComboBox2.SelectedIndex = 0;
            this.Cursor = Cursors.Default;
            //toolStripComboBox1.ComboBox.Cursor = Cursors.Default;

        }

        public void actualizadatos()
        {
            try
            {
                if (toolStripComboBox1.ComboBox.SelectedValue.ToString() == "1")
                {
                    DataTable dtEmp = new DataTable();
                    conexion.abrir();
                    string query = "SELECT USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, USERINFOCUS.PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTID NOT IN (1,32) ORDER BY BADGENUMBER;";
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmp);
                    conexion.cerrar();
                    dataGridViewDatos.DataSource = dtEmp;
                }
                else
                {
                    if (toolStripComboBox2.ComboBox.SelectedIndex == 0)
                    {
                        DataTable dtEmp = new DataTable();
                        conexion.abrir();
                        string query = "SELECT USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, USERINFOCUS.PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID INNER JOIN HOREMPLEADO ON USERINFOCus.Badgenumber=HOREMPLEADO.Badgenumber INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE DEPARTMENTS.DEPTID NOT IN (1,32) and DEPARTMENTS.DEPTID=" + toolStripComboBox1.ComboBox.SelectedValue.ToString() + " AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                        SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                        adaptador.Fill(dtEmp);
                        conexion.cerrar();
                        dataGridViewDatos.DataSource = dtEmp;
                    }
                    else
                    {
                        DataTable dtEmp = new DataTable();
                        conexion.abrir();
                        string query = "SELECT USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, USERINFOCUS.PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID INNER JOIN HOREMPLEADO ON USERINFOCus.Badgenumber=HOREMPLEADO.Badgenumber INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE DEPARTMENTS.DEPTID NOT IN (1,32) and DEPARTMENTS.DEPTID=" + toolStripComboBox1.ComboBox.SelectedValue.ToString() + " AND HORARIOS.Descripcion LIKE '%" + toolStripComboBox2.ComboBox.SelectedIndex.ToString() + "%' AND HORARIOS.TIPOHOR=1 ORDER BY USERINFOCUS.Badgenumber;";
                        SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                        adaptador.Fill(dtEmp);
                        conexion.cerrar();
                        dataGridViewDatos.DataSource = dtEmp;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

        }

        public void cargacombo()
        {
            try
            {
                DataTable dtDesc = new DataTable();
                conexion.abrir();
                string query = "SELECT id_even,descripcion from evento;";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtDesc);
                conexion.cerrar();
                comboBoxTipo.DisplayMember = "Descripcion";
                comboBoxTipo.ValueMember = "id_even";
                comboBoxTipo.DataSource = dtDesc;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }


        public void cargacomboDep()
        {
            try
            {
                DataTable dtDEP = new DataTable();
                conexion.abrir();
                string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS WHERE DEPTID NOT IN(32)";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtDEP);
                conexion.cerrar();
                toolStripComboBox1.ComboBox.DisplayMember = "DEPTNAME";
                toolStripComboBox1.ComboBox.ValueMember = "DEPTID";
                toolStripComboBox1.ComboBox.DataSource = dtDEP;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        public void cargacomboTurno()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        public void busdinamic(string consulta)
        {
            try
            {
                DataTable dtEmp = new DataTable();
                conexion.abrir();
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion.con);
                adaptador.Fill(dtEmp);
                conexion.cerrar();
                dataGridViewDatos.DataSource = dtEmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }
        private void toolStripTextBoxID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;
                if (toolStripTextBoxID.Text == "")
                {
                    actualizadatos();
                }
                else
                {
                    toolStripTextBoxNombre.Text = "";
                    query = "SELECT USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, USERINFOCUS.PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTID NOT IN (1,32) AND USERINFOCUS.BADGENUMBER LIKE '%" + toolStripTextBoxID.Text + "%' COLLATE Latin1_general_CI_AI";
                    busdinamic(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

        }
        private void toolStripTextBoxNombre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;
                if (toolStripTextBoxNombre.Text == "")
                {
                    actualizadatos();
                }
                else
                {
                    toolStripTextBoxID.Text = "";
                    query = "SELECT USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, USERINFOCUS.PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTID NOT IN (1,32) AND USERINFOCUS.NAME LIKE '%" + toolStripTextBoxNombre.Text + "%' COLLATE Latin1_general_CI_AI";
                    busdinamic(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Seguro que desea eliminar este evento?", "Salir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                conexion.abrir();
                string query = "DELETE FROM EVENEMP WHERE id_even=" + dataGridViewLista.CurrentRow.Cells[0].Value.ToString() + " and badgenumber='" + dataGridViewLista.CurrentRow.Cells[1].Value.ToString() + "'  and fecin='" + DateTime.Parse(dataGridViewLista.CurrentRow.Cells[4].Value.ToString()).ToString("yyyy-MM-dd") + "' and fecfin='" + DateTime.Parse(dataGridViewLista.CurrentRow.Cells[5].Value.ToString()).ToString("yyyy-MM-dd") + "'";
                Debug.WriteLine(query);
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                MessageBox.Show("Registro eliminado con éxito.");
                actualizalista();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount =
            dataGridViewDatos.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    Debug.WriteLine(dataGridViewDatos.Rows[Int32.Parse(dataGridViewDatos.SelectedRows[i].Index.ToString())].Cells[0].Value.ToString());
                }
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizadatos();
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizadatos();
        }
    }
}
