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
        public int opcion;
        public NuevoEmpleado()
        {
            InitializeComponent();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            
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
        public void leeTextbox()
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
                query = "SELECT distinct Puesto FROM UserInfoCus ORDER BY Puesto DESC";
                SqlDataAdapter adaptador2 = new SqlDataAdapter(query, conexion.con);
                adaptador2.Fill(dtPuesto);
                comboBoxPuesto.DisplayMember = "Puesto";
                comboBoxPuesto.ValueMember = "Puesto";
                comboBoxPuesto.DataSource = dtPuesto;
                conexion.cerrar();
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {

          
         
        }

        private void comboBoxDep_SelectedIndexChanged(object sender, EventArgs e)
        {

            conexionbd conexion = new conexionbd();
            string query;
            DataTable dtComboHora = new DataTable();
            DataTable dtPuesto = new DataTable();

            conexion.abrir();
            query = "SELECT distinct Puesto FROM UserInfoCus where DEFAULTDEPTID="+comboBoxDep.SelectedValue.ToString()+" ORDER BY Puesto DESC";
            SqlDataAdapter adaptador2 = new SqlDataAdapter(query, conexion.con);
            adaptador2.Fill(dtPuesto);
            comboBoxPuesto.DisplayMember = "Puesto";
            comboBoxPuesto.ValueMember = "Puesto";
            comboBoxPuesto.DataSource = dtPuesto;
            conexion.cerrar();
            
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            string query;

            if (opcion == 1)
            {
                if (textBoxNombre.Text != "")
                {
                    if (dateTimePickerFecNac.Value < DateTime.Now.AddYears(-15))
                    {
                        try
                        {
                            string id, tel, teleme, cp;
                            string badgenumber, nombre, curp, rfc, nss, genero, ciudad, estado, direccion, vehiculo;
                            string estudios, cedula;


                            id = textBoxID.Text;
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
                            cp = textBoxCP.Text;

                            conexion.abrir();
                            query = "UPDATE USERINFOCUS SET SSN='" + nss + "',Name='" + nombre + "',Gender='" + genero + "',Title='" + estudios + "',BIRTHDAY='" + dateTimePickerFecNac.Value.ToString("dd-MM-yyyy") + "',HIREDDAY='" + dateTimePickerFechaIngreso.Value.ToString("dd-MM-yyyy") + "',street='" + direccion + "',CITY='" + ciudad + "',STATE='" + estado + "',ZIP='" + cp + "',OPHONE='" + tel + "',DEFAULTDEPTID=" + comboBoxDep.SelectedIndex.ToString() + ",CURP='" + curp + "',ESTADOCIVIL='" + comboBoxEstCivil.Text + "',RFC='" + rfc + "',PUESTO='" + comboBoxPuesto.Text + "',TELEMERGENCIA='" + teleme + "',VEHICULO='" + vehiculo + "' WHERE Badgenumber=" + textBoxID.Text + "";
                            SqlCommand comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                            MessageBox.Show("Actualización realizada con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese una Fecha de Nacimiento Valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El campo Nombre es necesarios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                if ((textBoxID.Text != "") && (textBoxNombre.Text != ""))
                {
                    if (dateTimePickerFecNac.Value < DateTime.Now.AddYears(-15))
                    {

                        DataTable dtIDBad = new DataTable();
                        conexion.abrir();
                        query = "SELECT  Badgenumber FROM USERINFOCus where Badgenumber=" + textBoxID.Text + ";";
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

                                /*DataTable dtID = new DataTable();
                                conexion.abrir();
                                query = "SELECT TOP 1 BADGENUMBER FROM USERINFOCUS order by BADGENUMBER DESC";
                                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                                adaptador.Fill(dtID);
                                conexion.cerrar();
                                */
                                id = textBoxID.Text;
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
                                query = "INSERT INTO USERINFOCUS(Badgenumber,SSN,Name,Gender,TITLE,BIRTHDAY,HIREDDAY,street,CITY,STATE,ZIP,OPHONE,DEFAULTDEPTID,mverifypass,CURP,ESTADOCIVIL,RFC,PUESTO,TELEMERGENCIA,VEHICULO,ACTIVO) values (" + badgenumber + ",'" + nss + "','" + nombre + "','" + genero + "','" + estudios + "','" + dateTimePickerFecNac.Value.ToString("dd-MM-yyyy") + "','" + dateTimePickerFechaIngreso.Value.ToString("dd-MM-yyyy") + "','" + direccion + "','" + ciudad + "','" + estado + "','" + cp + "','" + tel + "'," + comboBoxDep.SelectedIndex.ToString() + ",'','" + curp + "','" + comboBoxEstCivil.Text + "','" + rfc + "','" + comboBoxPuesto.Text + "','" + teleme + "','" + vehiculo + "',1)";
                                //MessageBox.Show(query);
                                //Console.WriteLine(query);
                                SqlCommand comando = new SqlCommand(query, conexion.con);
                                comando.ExecuteNonQuery();
                                conexion.cerrar();
                                MessageBox.Show("Registro realizado con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese una Fecha de Nacimiento Valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Los campos de ID y Nombre son necesarios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Este formulario se cerrará", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No:
                    MessageBox.Show("Operación Cancelada");
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("Operación Cancelada");
                    break;
            }
        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Todos los campos se borrarán", "Limpiar Formulario", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Yes:
                    if (opcion == 1)
                    {
                        textBoxNombre.Clear();
                        textBoxCurp.Clear();
                        textBoxNss.Clear();
                        textBoxRFC.Clear();
                        textBoxDireccion.Clear();
                        textBoxCP.Clear();
                        textBoxTel.Clear();
                        textBoxTelEme.Clear();
                        textBoxCedula.Clear();
                        textBoxVehiculo.Clear();
                    }
                    else
                    {
                        textBoxID.Clear();
                        textBoxNombre.Clear();
                        textBoxCurp.Clear();
                        textBoxNss.Clear();
                        textBoxRFC.Clear();
                        textBoxDireccion.Clear();
                        textBoxCP.Clear();
                        textBoxTel.Clear();
                        textBoxTelEme.Clear();
                        textBoxCedula.Clear();
                        textBoxVehiculo.Clear();
                    }

                    break;
                case DialogResult.No:
                    MessageBox.Show("Operación Cancelada");
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("Operación Cancelada");
                    break;
            }

        }
    }
}
