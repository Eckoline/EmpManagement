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

            string id, nombre, tipoeve, observaciones;
            string fecin, fecfin;
            int cuenta;
            id = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            nombre = dataGridViewDatos.CurrentRow.Cells[1].Value.ToString();
            fecin = dateTimePickerFecIn.Value.ToString("MM-dd-yyyy");
            fecfin = dateTimePickerFecFin.Value.ToString("MM-dd-yyyy");
            tipoeve = comboBoxTipo.SelectedValue.ToString();

            if (dateTimePickerFecIn.Value < dateTimePickerFecFin.Value)
            {
                if (comboBoxTipo.SelectedIndex > 0)
                {
                    if (textBoxObs.Text != "")
                    {
                        DataTable dtEmpValid = new DataTable();
                        string query = "SELECT * FROM EVENEMP where id_even=" + tipoeve + " and badgenumber='" + id + "' and fecin='" + fecin + "' and fecfin='" + fecfin + "' and fecreg='" + DateTime.Now.ToString("MM-dd-yyyy") + "'";
                        SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                        adaptador.Fill(dtEmpValid);
                        cuenta = dtEmpValid.Rows.Count;
                        if (cuenta > 0)
                        {
                            MessageBox.Show("Registro repetido.");
                        }
                        else
                        {
                            conexion.abrir();
                            query = "INSERT INTO EVENEMP(ID_EVEN,BADGENUMBER,FECIN,FECFIN,FECREG) VALUES(" + tipoeve + "," + id + ",'" + fecin + "','" + fecfin + "','" + DateTime.Now.ToString("MM-dd-yyyy") + "')";
                            SqlCommand comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                            actualizalista();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Falta escribir observaciones del evento.");
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

          
            /*
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            */

        }
        public void actualizalista()
        {
            try
            {
                conexion.abrir();
                DataTable dtEmpEven = new DataTable();
                string query = "SELECT evenemp.id_even AS 'ID EVENTO',evenemp.badgenumber AS 'ID EMPLEADO',userinfocus.name AS 'NOMBRE',evento.descripcion 'DESCRIPCIÓN EVENTO',evenemp.fecin AS 'FECHA INICIO',evenemp.fecfin AS 'FECHA TERMINO' FROM (userinfocus inner join evenemp on userinfocus.badgenumber=evenemp.badgenumber) INNER JOIN evento on evenemp.id_even=evento.id_even where EVENEMP.FECREG>=CONVERT(DATE,GETDATE());";
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
            actualizadatos();
            actualizalista();
            cargacombo();
        }

        public void actualizadatos()
        {
            try
            {
                DataTable dtEmp = new DataTable();
                conexion.abrir();
                string query = "SELECT USERINFOCUS.BADGENUMBER AS ID,USERINFOCUS.NAME AS NOMBRE, USERINFOCUS.PUESTO, DEPARTMENTS.DEPTNAME AS DEPARTAMENTO FROM USERINFOCUS INNER JOIN DEPARTMENTS ON USERINFOCUS.DEFAULTDEPTID=DEPARTMENTS.DEPTID WHERE DEPARTMENTS.DEPTID NOT IN (1,32) ORDER BY BADGENUMBER;";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmp);
                conexion.cerrar();
                dataGridViewDatos.DataSource = dtEmp;
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
    }
}
