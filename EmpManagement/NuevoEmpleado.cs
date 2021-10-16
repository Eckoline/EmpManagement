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
    public partial class NuevoEmpleado : Form
    {
        public NuevoEmpleado()
        {
            InitializeComponent();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFecNac.Value < DateTime.Now.AddYears(-15))
            {
                if ((textBoxID.Text == "")||(textBoxNombre.Text==""))
                {
                    MessageBox.Show("Los campos de ID y Nombre son necesarios");
                }
                else
                {
                    conexionbd conexion = new conexionbd();
                    string query;
                    DataTable dtIDBad = new DataTable();
                    conexion.abrir();
                    query = "SELECT  Badgenumber FROM USERINFO where Badgenumber='" + textBoxID.Text + "'";
                    SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                    adaptador1.Fill(dtIDBad);
                    conexion.cerrar();
                    if (dtIDBad.Rows.Count > 0)
                    {
                        MessageBox.Show("ID ya registrado, intente con otro.");
                    }
                    else
                    {
                       try
                        {
                            string id, tel, teleme, cp;
                            string badgenumber, nombre, curp, rfc, nss, genero, ciudad, estado, direccion, vehiculo;
                            string estudios, cedula;
                            DateTime fecnac, fecin;
                            DataTable dtID = new DataTable();
                            conexion.abrir();
                            query = "SELECT TOP 1 USERID FROM USERINFOCUS order by USERID DESC";
                            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                            adaptador.Fill(dtID);
                            conexion.cerrar();
                            id = (Int32.Parse(dtID.Rows[0]["USERID"].ToString()) + 1).ToString();
                            tel = textBoxTel.Text;
                            teleme = textBoxTelEme.Text;
                            nombre = textBoxNombre.Text;
                            curp = textBoxCurp.Text;
                            rfc = textBoxRFC.Text;
                            nss = textBoxNss.Text;
                            genero = comboBoxGenero.Text;
                            ciudad = textBoxCiudad.Text;
                            estado = textBoxEstado.Text;
                            direccion = textBoxDireccion.Text;
                            vehiculo = textBoxVehiculo.Text;
                            badgenumber = textBoxID.Text;
                            estudios = comboBoxNivelE.Text;
                            cedula = textBoxCedula.Text;
                            //fecnac = dateTimePickerFecNac.Value.ToString("");
                            //fecin = dateTimePickerFechaIngreso.Value;
                            cp = textBoxCP.Text;
                            //SqlConnection conexion = new SqlConnection("server=DIEGO-PC ; database=base1 ; integrated security = true");
                            conexion.abrir();
                            query = "INSERT INTO USERINFOCUS(USERID,Badgenumber,SSN,Name,Gender,TITLE,BIRTHDAY,HIREDDAY,street,CITY,STATE,ZIP,OPHONE,DEFAULTDEPTID,mverifypass,CURP,ESTADOCIVIL,RFC,PUESTO,TELEMERGENCIA,VEHICULO) values (" + id + ",'" + badgenumber + "','" + nss + "','" + nombre + "','" + genero + "','" + estudios + "','" + dateTimePickerFecNac.Value.ToString("dd-MM-yyyy") + "','" + dateTimePickerFechaIngreso.Value.ToString("dd-MM-yyyy") + "','" + direccion + "','" + ciudad + "','" + estado + "','" + cp + "','" + tel + "'," + comboBoxDep.SelectedIndex.ToString() + ",'','" + curp + "','" + comboBoxEstCivil.Text + "','" + rfc + "','" + comboBoxPuesto.Text + "','" + teleme + "','" + vehiculo + "')";
                            //MessageBox.Show(query);
                            //Console.WriteLine(query);
                            SqlCommand comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                            MessageBox.Show("Registro realizado con éxito.");
                            this.Close();
                       }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese una Fecha de Nacimiento Valida.");
            }

        }

        private void textBoxTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void textBoxTel_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTelEme_KeyPress(object sender, KeyPressEventArgs e)
        {
       
        }

        private void textBoxCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }

        }

        private void NuevoEmpleado_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            DataTable dtComboHora = new DataTable();
            DataTable dtPuesto = new DataTable();

            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            comboBoxDep.DisplayMember = "DEPTNAME";
            comboBoxDep.ValueMember = "DEPTID";
            comboBoxDep.DataSource = dtComboDepts;
            conexion.cerrar();

            conexion.abrir();
            query = "SELECT ID_HOR,Descripcion FROM Horarios where Descripcion NOT LIKE '%Sabado%';";
            SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
            adaptador1.Fill(dtComboHora);
            comboBoxHorario.DisplayMember = "Descripcion";
            comboBoxHorario.ValueMember = "ID_HOR";
            comboBoxHorario.DataSource = dtComboHora;
            conexion.cerrar();

            conexion.abrir();
            query = "SELECT distinct Puesto FROM UserInfoCus ORDER BY Puesto ASC";
            SqlDataAdapter adaptador2 = new SqlDataAdapter(query, conexion.con);
            adaptador2.Fill(dtPuesto);
            comboBoxPuesto.DisplayMember = "Puesto";
            comboBoxPuesto.ValueMember = "Puesto";
            comboBoxPuesto.DataSource = dtPuesto;
            conexion.cerrar();

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
