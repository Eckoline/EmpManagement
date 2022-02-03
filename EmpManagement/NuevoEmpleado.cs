using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace EmpManagement
{
    public partial class NuevoEmpleado : Form
    {
        public int opcion;
        public NuevoEmpleado()
        {
            InitializeComponent();
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
            try
            {
                conexionbd conexion = new conexionbd();
                DataTable dtComboDepts = new DataTable();
                DataTable dtComboHora = new DataTable();
                DataTable dtPuesto = new DataTable();

                conexion.abrir();
                string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS WHERE DEPTID<>32";
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
                comboBoxPuesto.Text = labelpuesto.Text;

                conexion.abrir();
                query = "SELECT ID_HOR,Descripcion FROM HORARIOS WHERE ID_HOR NOT IN(10,12,14,16,18,20,22,24)";
                SqlDataAdapter adaptador3 = new SqlDataAdapter(query, conexion.con);
                adaptador3.Fill(dtComboHora);
                comboBoxHor.DisplayMember = "Descripcion";
                comboBoxHor.ValueMember = "ID_HOR";
                comboBoxHor.DataSource = dtComboHora;
                conexion.cerrar();
                if (opcion == 1)
                {
                    Debug.WriteLine("Entre");
                    DataTable dtfoto = new DataTable();
                    conexion.abrir();
                    query = "SELECT foto from userinfocus where badgenumber="+textBoxID.Text;
                Debug.WriteLine(query);
                    adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtfoto);
                    //byte[] bytes = Encoding.ASCII.GetBytes(dtfoto.Rows[0]["foto"].ToString());
                    //MemoryStream ms = new MemoryStream((byte[])dtfoto.Rows[0]["foto"].ToString()); 
                    //System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    if (dtfoto.Rows.Count > 0)
                    {
                        Byte[] byteBLOBData = new Byte[0];
                        byteBLOBData = (Byte[])(dtfoto.Rows[0]["foto"]);
                        MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                        pictureBoxFoto.Image = Image.FromStream(stmBLOBData);
                        pictureBoxFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                    conexion.cerrar();
                }
           }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        private void comboBoxDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                conexionbd conexion = new conexionbd();
                string query;
                DataTable dtComboHora = new DataTable();
                DataTable dtPuesto = new DataTable();
                conexion.abrir();
                query = "SELECT distinct Puesto FROM UserInfoCus where DEFAULTDEPTID=" + comboBoxDep.SelectedValue.ToString() + " ORDER BY Puesto DESC";
                SqlDataAdapter adaptador2 = new SqlDataAdapter(query, conexion.con);
                adaptador2.Fill(dtPuesto);
                comboBoxPuesto.DisplayMember = "Puesto";
                comboBoxPuesto.ValueMember = "Puesto";
                comboBoxPuesto.DataSource = dtPuesto;
                conexion.cerrar();
                if (opcion == 1)
                {
                    comboBoxPuesto.Text = labelpuesto.Text;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
                                byte[] byteArrayImagen = ImageToByteArray(pictureBoxFoto.Image);
                                conexion.abrir();
                                query = "UPDATE USERINFOCUS SET SSN='" + nss + "',Name='" + nombre + "',Gender='" + genero + "',Title='" + estudios + "',BIRTHDAY='" + dateTimePickerFecNac.Value.ToString("MM-dd-yyyy") + "',HIREDDAY='" + dateTimePickerFechaIngreso.Value.ToString("MM-dd-yyyy") + "',street='" + direccion + "',CITY='" + ciudad + "',STATE='" + estado + "',ZIP='" + cp + "',OPHONE='" + tel + "',DEFAULTDEPTID=" + comboBoxDep.SelectedValue.ToString() + ",CURP='" + curp + "',ESTADOCIVIL='" + comboBoxEstCivil.Text + "',RFC='" + rfc + "',PUESTO='" + comboBoxPuesto.Text + "',TELEMERGENCIA='" + teleme + "',VEHICULO='" + vehiculo + "', foto=@imagen WHERE Badgenumber=" + textBoxID.Text + "";
                                SqlCommand comando = new SqlCommand(query, conexion.con);
                                comando.Parameters.AddWithValue("@imagen", byteArrayImagen);
                                comando.ExecuteNonQuery();

                                DataTable dthor = new DataTable();
                                query = "SELECT * FROM HOREMPLEADO WHERE Badgenumber=" + textBoxID.Text;
                                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                                adaptador.Fill(dthor);

                                if (dthor.Rows.Count > 0)
                                {
                                    query = "UPDATE HOREMPLEADO SET ID_HOR=" + comboBoxHor.SelectedValue.ToString() + " WHERE BADGENUMBER=" + textBoxID.Text;
                                }
                                else
                                {
                                    query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(" + comboBoxHor.SelectedValue.ToString() + "," + textBoxID.Text + ")";
                                }
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
                                    byte[] byteArrayImagen = ImageToByteArray(pictureBoxFoto.Image);
                                    //SqlConnection conexion = new SqlConnection("server=DIEGO-PC ; database=base1 ; integrated security = true");
                                    conexion.abrir();
                                    query = "INSERT INTO USERINFOCUS(Badgenumber,SSN,Name,Gender,TITLE,BIRTHDAY,HIREDDAY,street,CITY,STATE,ZIP,OPHONE,DEFAULTDEPTID,mverifypass,CURP,ESTADOCIVIL,RFC,PUESTO,TELEMERGENCIA,VEHICULO,ACTIVO,foto) values (" + badgenumber + ",'" + nss + "','" + nombre + "','" + genero + "','" + estudios + "','" + dateTimePickerFecNac.Value.ToString("MM-dd-yyyy") + "','" + dateTimePickerFechaIngreso.Value.ToString("MM-dd-yyyy") + "','" + direccion + "','" + ciudad + "','" + estado + "','" + cp + "','" + tel + "'," + comboBoxDep.SelectedValue.ToString() + ",'','" + curp + "','" + comboBoxEstCivil.Text + "','" + rfc + "','" + comboBoxPuesto.Text + "','" + teleme + "','" + vehiculo + "',1,@imagen)";
                                    //MessageBox.Show(query);
                                    Debug.WriteLine(query);
                                    SqlCommand comando = new SqlCommand(query, conexion.con);
                                    comando.Parameters.AddWithValue("@imagen", byteArrayImagen);
                                    comando.ExecuteNonQuery();
                                    Debug.Write(query);
                                    query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(" + comboBoxHor.SelectedValue.ToString() + "," + textBoxID.Text + ")";
                                    Debug.WriteLine(query);
                                    //MessageBox.Show(query);
                                    //Console.WriteLine(query);
                                    comando = new SqlCommand(query, conexion.con);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
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

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void asignarHorarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            /*if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            */
        }

        private void pictureBoxFoto_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxFoto_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog getImage = new OpenFileDialog();
            getImage.InitialDirectory = "C:\\";
            getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";
            if (getImage.ShowDialog() == DialogResult.OK)
            {
                pictureBoxFoto.ImageLocation = getImage.FileName;
                pictureBoxFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
