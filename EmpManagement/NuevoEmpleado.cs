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
using System.Linq;

namespace EmpManagement
{
    public partial class NuevoEmpleado : Form
    {
        public int opcion;
        public NuevoEmpleado()
        {
            InitializeComponent();
        }

        public SDKHelper SDK = new SDKHelper();
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
                DataTable dtestadociv = new DataTable();

                conexion.abrir();
                string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS WHERE DEPTID NOT IN (1,32)";
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
                query = "SELECT distinct ESTADOCIVIL FROM UserInfoCus ORDER BY ESTADOCIVIL DESC";
                adaptador2 = new SqlDataAdapter(query, conexion.con);
                adaptador2.Fill(dtestadociv);
                comboBoxEstCivil.DisplayMember = "ESTADOCIVIL";
                comboBoxEstCivil.ValueMember = "ESTADOCIVIL";
                comboBoxEstCivil.DataSource = dtestadociv;
                conexion.cerrar();

                /*
                conexion.abrir();
                query = "SELECT ID_HOR,Descripcion FROM HORARIOS WHERE ID_HOR NOT IN(10,12,14,16,18,20,22,24)";
                SqlDataAdapter adaptador3 = new SqlDataAdapter(query, conexion.con);
                adaptador3.Fill(dtComboHora);
                comboBoxHor.DisplayMember = "Descripcion";
                comboBoxHor.ValueMember = "ID_HOR";
                comboBoxHor.DataSource = dtComboHora;
                conexion.cerrar();*/
                if (opcion == 1)
                {
                    //Debug.WriteLine("Entre");
                    DataTable dtfoto = new DataTable();
                    conexion.abrir();
                    query = "SELECT foto from userinfocus where badgenumber=" + textBoxID.Text;
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
                if (opcion == 2)
                {
                    comboBox7.SelectedIndex = 0;
                    int rel, get;
                    int newbad;
                    Boolean readall;
                    List<int> badgenumbers = new List<int>();
                    rel = SDK.sta_ConnectTCP(listBox1, "192.168.1.201", "4370", "0");
                    if (rel == 1)
                    {
                        //MessageBox.Show("Conexión Realizada con Éxito");
                        readall = SDK.axCZKEM1.ReadAllUserID(0);
                        if (readall)
                        {
                            get = SDK.sta_GetAllUserID(true, comboBox1, comboBox2, comboBox3, comboBox4, comboBox5, textBox6, comboBox6);

                            for (int i = 0; i < comboBox1.Items.Count; i++)
                            {
                                //Debug.WriteLine(comboBox1.Items[i]);
                                badgenumbers.Insert(i, Int32.Parse(comboBox1.Items[i].ToString()));
                            }

                            badgenumbers.Sort();
                            //Debug.WriteLine("Acomodados");
                            //badgenumbers.ForEach((v) => Debug.WriteLine("Element = {0}", v));
                            newbad = badgenumbers.Last() + 1;
                            //Debug.WriteLine(newbad);
                            textBoxID.Text = newbad.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo realizar la conexión, compruebe su conexión a internet");
                        this.Close();
                    }
                    SDK.sta_DisConnect();
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
            SqlCommand comando = new SqlCommand();


            conexionbd conexion = new conexionbd();
            string query;
            if (opcion == 1)
            {
                if (textBoxNombre.Text != "")
                {
                    if (dateTimePickerFecNac.Value < DateTime.Now.AddYears(-15))
                    {

                        string id, tel, teleme, cp;
                        string badgenumber, nombre, curp, rfc, nss, genero, ciudad, estado, direccion, vehiculo;
                        string estudios, cedula;
                        int validacionrfc, validacionnss, validacioncurp;

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
                        validacionrfc = rfc.Length - 13;
                        validacionnss = nss.Length - 11;
                        validacioncurp = curp.Length - 18;
                        byte[] byteArrayImagen = null;


                        conexion.abrir();
                        if (validacionrfc == 0)
                        {
                            if (validacionnss == 0)
                            {
                                if (validacioncurp == 0)
                                {

                                    if (pictureBoxFoto.Image == null)
                                    {
                                        query = "UPDATE USERINFOCUS SET SSN='" + nss + "',Name='" + nombre + "',Gender='" + genero + "',Title='" + estudios + "',BIRTHDAY='" + dateTimePickerFecNac.Value.ToString("MM-dd-yyyy") + "',HIREDDAY='" + dateTimePickerFechaIngreso.Value.ToString("MM-dd-yyyy") + "',street='" + direccion + "',CITY='" + ciudad + "',STATE='" + estado + "',ZIP='" + cp + "',OPHONE='" + tel + "',DEFAULTDEPTID=" + comboBoxDep.SelectedValue.ToString() + ",CURP='" + curp + "',ESTADOCIVIL='" + comboBoxEstCivil.Text + "',RFC='" + rfc + "',PUESTO='" + comboBoxPuesto.Text + "',TELEMERGENCIA='" + teleme + "',VEHICULO='" + vehiculo + "', contactoeme='" + textBoxDescCon.Text + "' WHERE Badgenumber=" + textBoxID.Text + "";
                                        comando = new SqlCommand(query, conexion.con);
                                        comando.ExecuteNonQuery();
                                        MessageBox.Show("Actualización realizada con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();

                                    }
                                    else
                                    {
                                        byteArrayImagen = ImageToByteArray(pictureBoxFoto.Image);
                                        query = "UPDATE USERINFOCUS SET SSN='" + nss + "',Name='" + nombre + "',Gender='" + genero + "',Title='" + estudios + "',BIRTHDAY='" + dateTimePickerFecNac.Value.ToString("MM-dd-yyyy") + "',HIREDDAY='" + dateTimePickerFechaIngreso.Value.ToString("MM-dd-yyyy") + "',street='" + direccion + "',CITY='" + ciudad + "',STATE='" + estado + "',ZIP='" + cp + "',OPHONE='" + tel + "',DEFAULTDEPTID=" + comboBoxDep.SelectedValue.ToString() + ",CURP='" + curp + "',ESTADOCIVIL='" + comboBoxEstCivil.Text + "',RFC='" + rfc + "',PUESTO='" + comboBoxPuesto.Text + "',TELEMERGENCIA='" + teleme + "',VEHICULO='" + vehiculo + "', foto=@imagen,CORREO='" + textBoxCorreo.Text + "',contactoeme='" + textBoxDescCon.Text + "' WHERE Badgenumber=" + textBoxID.Text + "";
                                        comando = new SqlCommand(query, conexion.con);
                                        comando.Parameters.AddWithValue("@imagen", byteArrayImagen);
                                        comando.ExecuteNonQuery();
                                        MessageBox.Show("Actualización realizada con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }

                                    query = "INSERT INTO movimientos values(" + Program.id + ",'ACTUALIZACIÓN DE DATOS EMPLEADO: " + textBoxID.Text + "','" + DateTime.Now + "','" + this.Text + "');";
                                    comando = new SqlCommand(query, conexion.con);
                                    comando.ExecuteNonQuery();


                                }
                                else
                                {
                                    if (validacioncurp > 0)
                                    {
                                        MessageBox.Show("Se excedio en CURP por " + validacioncurp + " caracteres");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Faltan " + validacioncurp + " caracteres en CURP");
                                    }
                                }
                            }
                            else
                            {
                                if (validacionnss > 0)
                                {
                                    MessageBox.Show("Se excedio en NSS por " + validacionnss + " caracteres");
                                }
                                else
                                {
                                    MessageBox.Show("Faltan " + validacionnss + " caracteres en NSS");
                                }

                            }
                        }
                        else
                        {
                            if (validacionrfc > 0)
                            {
                                MessageBox.Show("Se excedio en RFC por " + validacionrfc + " caracteres");
                            }
                            else
                            {
                                MessageBox.Show("Faltan " + validacionrfc + " caracteres en RFC");
                            }
                        }



                        /*
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
                        */
                        conexion.cerrar();


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

                            string id, tel, teleme, cp;
                            string badgenumber, nombre, curp, rfc, nss, genero, ciudad, estado, direccion, vehiculo;
                            string estudios, cedula;
                            int validacionrfc, validacionnss, validacioncurp;
                            string nombrereloj;

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
                            byte[] byteArrayImagen = null;
                            //fecnac = dateTimePickerFecNac.Value.ToString("");
                            //fecin = dateTimePickerFechaIngreso.Value;
                            validacionrfc = rfc.Length - 13;
                            validacionnss = nss.Length - 11;
                            validacioncurp = curp.Length - 18;
                            cp = textBoxCP.Text;




                            conexion.abrir();

                            if (validacionrfc == 0)
                            {
                                if (validacionnss == 0)
                                {
                                    if (validacioncurp == 0)
                                    {
                                        int validacionreloj;
                                        string phrase = nombre;
                                        string[] words = phrase.Split(' ');
                                        nombrereloj = words[0] + words[1];
                                        textBox1.Text = nombrereloj;
                                        int rel = SDK.sta_ConnectTCP(listBox1, "192.168.1.201", "4370", "0");
                                        if (rel == 1)
                                        {
                                            validacionreloj = SDK.sta_SetUserInfo(listBox1, textBoxID, textBox1, comboBox7, textBox4, textBox5);
                                            if (validacionreloj == 1)
                                            {
                                                comando = new SqlCommand();
                                                if (pictureBoxFoto.Image == null)
                                                {
                                                    query = "INSERT INTO USERINFOCUS(Badgenumber,SSN,Name,Gender,TITLE,BIRTHDAY,HIREDDAY,street,CITY,STATE,ZIP,OPHONE,DEFAULTDEPTID,mverifypass,CURP,ESTADOCIVIL,RFC,PUESTO,TELEMERGENCIA,VEHICULO,ACTIVO,correo,contactoeme) values (" + badgenumber + ",'" + nss + "','" + nombre + "','" + genero + "','" + estudios + "','" + dateTimePickerFecNac.Value.ToString("MM-dd-yyyy") + "','" + dateTimePickerFechaIngreso.Value.ToString("MM-dd-yyyy") + "','" + direccion + "','" + ciudad + "','" + estado + "','" + cp + "','" + tel + "'," + comboBoxDep.SelectedValue.ToString() + ",'','" + curp + "','" + comboBoxEstCivil.Text + "','" + rfc + "','" + comboBoxPuesto.Text + "','" + teleme + "','" + vehiculo + "',1,@correo,'" + textBoxDescCon.Text + "')";
                                                    comando = new SqlCommand(query, conexion.con);
                                                    comando.Parameters.AddWithValue("@correo", textBoxCorreo.Text);
                                                    comando.ExecuteNonQuery();
                                                    MessageBox.Show("Registro realizado con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    this.Close();
                                                }
                                                else
                                                {
                                                    byteArrayImagen = ImageToByteArray(pictureBoxFoto.Image);
                                                    query = "INSERT INTO USERINFOCUS(Badgenumber,SSN,Name,Gender,TITLE,BIRTHDAY,HIREDDAY,street,CITY,STATE,ZIP,OPHONE,DEFAULTDEPTID,mverifypass,CURP,ESTADOCIVIL,RFC,PUESTO,TELEMERGENCIA,VEHICULO,ACTIVO,foto,correo,contactoeme) values (" + badgenumber + ",'" + nss + "','" + nombre + "','" + genero + "','" + estudios + "','" + dateTimePickerFecNac.Value.ToString("MM-dd-yyyy") + "','" + dateTimePickerFechaIngreso.Value.ToString("MM-dd-yyyy") + "','" + direccion + "','" + ciudad + "','" + estado + "','" + cp + "','" + tel + "'," + comboBoxDep.SelectedValue.ToString() + ",'','" + curp + "','" + comboBoxEstCivil.Text + "','" + rfc + "','" + comboBoxPuesto.Text + "','" + teleme + "','" + vehiculo + "',1,@imagen,@correo,'" + textBoxDescCon.Text + "')";
                                                    comando = new SqlCommand(query, conexion.con);
                                                    comando.Parameters.AddWithValue("@imagen", byteArrayImagen);
                                                    comando.Parameters.AddWithValue("@correo", textBoxCorreo.Text);
                                                    comando.ExecuteNonQuery();
                                                    MessageBox.Show("Registro realizado con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    this.Close();
                                                }
                                                DataTable dthor = new DataTable();
                                                DataTable dthorsab = new DataTable();
                                                int idhor, idhorsab;
                                                conexion.abrir();
                                                query = "SELECT distinct HORARIOS.ID_HOR FROM USERINFOCus INNER JOIN HOREMPLEADO ON USERINFOCus.Badgenumber=HOREMPLEADO.Badgenumber INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCus.DEFAULTDEPTID=" + comboBoxDep.SelectedValue.ToString() + " ORDER BY HORARIOS.ID_HOR; ";
                                                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                                                adaptador.Fill(dthor);
                                                idhor = Int32.Parse(dthor.Rows[0]["ID_HOR"].ToString());

                                                query = "SELECT HORARIOS.ID_HOR FROM USERINFOCus INNER JOIN HOREMPLEADO ON USERINFOCus.Badgenumber=HOREMPLEADO.Badgenumber INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE HORARIOS.ID_HOR=" + (idhor + 1).ToString() + " AND HORARIOS.Descripcion LIKE '%SABADO%'";
                                                adaptador = new SqlDataAdapter(query, conexion.con);
                                                adaptador.Fill(dthorsab);

                                                if (dthorsab.Rows.Count > 0)
                                                {
                                                    idhorsab = Int32.Parse(dthor.Rows[0]["ID_HOR"].ToString()) + 1;

                                                    query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(@hor,@id)";
                                                    comando = new SqlCommand(query, conexion.con);
                                                    comando.Parameters.AddWithValue("@id", badgenumber);
                                                    comando.Parameters.AddWithValue("@hor", idhor);
                                                    comando.ExecuteNonQuery();

                                                    query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(@hor,@id)";
                                                    comando = new SqlCommand(query, conexion.con);
                                                    comando.Parameters.AddWithValue("@id", badgenumber);
                                                    comando.Parameters.AddWithValue("@hor", idhorsab);
                                                    comando.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(@hor,@id)";
                                                    comando = new SqlCommand(query, conexion.con);
                                                    comando.Parameters.AddWithValue("@id", badgenumber);
                                                    comando.Parameters.AddWithValue("@hor", idhor);
                                                    comando.ExecuteNonQuery();
                                                }
                                                query = "INSERT INTO movimientos values(" + Program.id + ",'REGISTRO DE EMPLEADO: " + textBoxID.Text + "','" + DateTime.Now + "','" + this.Text + "');";
                                                comando = new SqlCommand(query, conexion.con);
                                                comando.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Error al registrar los datos. Consulte al administrador del sistema.");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Error de Comunicación");
                                        }
                                       
                                    }
                                    else
                                    {
                                        if (validacioncurp > 0)
                                        {
                                            MessageBox.Show("Se excedio en CURP por " + validacioncurp + " caracteres");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Faltan " + validacioncurp + " caracteres en CURP");
                                        }
                                    }
                                }
                                else
                                {
                                    if (validacionnss > 0)
                                    {
                                        MessageBox.Show("Se excedio en NSS por " + validacionnss + " caracteres");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Faltan " + validacionnss + " caracteres en NSS");
                                    }

                                }
                            }
                            else
                            {
                                if (validacionrfc > 0)
                                {
                                    MessageBox.Show("Se excedio en RFC por " + validacionrfc + " caracteres");
                                }
                                else
                                {
                                    MessageBox.Show("Faltan " + validacionrfc + " caracteres en RFC");
                                }
                            }
                            /*
                                 query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(" + comboBoxHor.SelectedValue.ToString() + "," + textBoxID.Text + ")";
                                 comando = new SqlCommand(query, conexion.con);
                                 comando.ExecuteNonQuery();
                            */
                            conexion.cerrar();
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
            SDK.sta_DisConnect();
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
            DialogResult result = MessageBox.Show("Todos los campos se limpiaran", "Limpiar Formulario", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                        textBoxCorreo.Clear();
                        textBoxDescCon.Clear();
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
                        textBoxCorreo.Clear();
                        textBoxDescCon.Clear();
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
                pictureBoxFoto.Image = null;
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBoxTelEme_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBoxCP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBoxDescCon_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBoxTel_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click_1(object sender, EventArgs e)
        {

        }
    }
}
