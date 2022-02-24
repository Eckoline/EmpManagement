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
    public partial class NuevoEmpRHNOM : Form
    {
        public NuevoEmpRHNOM()
        {
            InitializeComponent();
        }
        public int opcion = 0;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            string query;
            string id, nombre, puesto, dep, horario,hor2;

            if (opcion == 2)
            {
                if (textBoxNombre.Text != "")
                {
                    DataTable dtIDBad = new DataTable();
                    query = "SELECT  userinfocus.badgenumber,HOREMPLEADO.ID_HOR from userinfocus inner join HOREMPLEADO on userinfocus.badgenumber=horempleado.badgenumber where userinfocus.badgenumber=" + textBoxID.Text + " and HOREMPLEADO.id_hor IN(10,12,14,16,18,20,22,24)";
                    SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                    adaptador1.Fill(dtIDBad);
                    if ((dtIDBad.Rows.Count > 0)&&(checkBox1.Checked==false))
                    {
                        MessageBox.Show("El usuario tiene horario en día sabado. De clic en Activar Horario Sabado y seleccione el correspondiente.");
                    }
                    else
                    {
                        id = textBoxID.Text;
                        nombre = textBoxNombre.Text;
                        puesto = comboBoxPuesto.Text;
                        dep = comboBoxDep.SelectedValue.ToString();
                        horario = comboBoxHor.SelectedValue.ToString();
                        hor2 = comboBoxHorSab.SelectedValue.ToString();

                        conexion.abrir();
                        query = "UPDATE USERINFOCUS SET NAME=@nombre,DEFAULTDEPTID=@dep,PUESTO=@puesto WHERE BADGENUMBER=@id";
                        SqlCommand comando = new SqlCommand(query, conexion.con);
                        comando.Parameters.AddWithValue("@id", id);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@dep", dep);
                        comando.Parameters.AddWithValue("@puesto", puesto);
                        comando.ExecuteNonQuery();
                        Debug.WriteLine(horario);
                        Debug.WriteLine(hor2);

                        if (checkBox1.Checked)
                        {
                            query = "DELETE HOREMPLEADO WHERE BADGENUMBER=@id ";
                            comando = new SqlCommand(query, conexion.con);
                            comando.Parameters.AddWithValue("@id", id);
                            comando.ExecuteNonQuery();

                            query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(@hor,@id) ";
                            comando = new SqlCommand(query, conexion.con);
                            comando.Parameters.AddWithValue("@hor", horario);
                            comando.Parameters.AddWithValue("@id", id);
                            comando.ExecuteNonQuery();

                            query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(@hor,@id) ";
                            comando = new SqlCommand(query, conexion.con);
                            comando.Parameters.AddWithValue("@hor", hor2);
                            comando.Parameters.AddWithValue("@id", id);
                            comando.ExecuteNonQuery();
                        }
                        else
                        {
                            query = "UPDATE HOREMPLEADO SET ID_HOR=@hor WHERE BADGENUMBER=@id";
                            comando = new SqlCommand(query, conexion.con);
                            comando.Parameters.AddWithValue("@hor", horario);
                            comando.Parameters.AddWithValue("@id", id);
                            comando.ExecuteNonQuery();
                        }

                        conexion.cerrar();

                        MessageBox.Show("Actualización realizada con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            else
            {
                if (opcion == 1)
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

                            /*DataTable dtID = new DataTable();
                            conexion.abrir();
                            query = "SELECT TOP 1 BADGENUMBER FROM USERINFOCUS order by BADGENUMBER DESC";
                            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                            adaptador.Fill(dtID);
                            conexion.cerrar();
                            */
                            id = textBoxID.Text;
                            nombre = textBoxNombre.Text;
                            puesto = comboBoxPuesto.Text;
                            dep = comboBoxDep.SelectedValue.ToString();
                            horario = comboBoxHor.SelectedValue.ToString();
                            hor2 = comboBoxHorSab.SelectedValue.ToString();

                            conexion.abrir();
                            query = "INSERT INTO USERINFOCUS(Badgenumber,Name,DEFAULTDEPTID,puesto) values (@id,@nombre,@dep,@puesto)";
                            SqlCommand comando = new SqlCommand(query, conexion.con);
                            comando.Parameters.AddWithValue("@id", id);
                            comando.Parameters.AddWithValue("@nombre", nombre);
                            comando.Parameters.AddWithValue("@dep", dep);
                            comando.Parameters.AddWithValue("@puesto", puesto);
                            comando.ExecuteNonQuery();

                            query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(@hor,@id)";
                            comando = new SqlCommand(query, conexion.con);
                            comando.Parameters.AddWithValue("@id", id);
                            comando.Parameters.AddWithValue("@hor", horario);
                            comando.ExecuteNonQuery();

                            if (checkBox1.Checked)
                            {
                                query = "INSERT INTO HOREMPLEADO(ID_HOR,BADGENUMBER) VALUES(@hor,@id)";
                                comando = new SqlCommand(query, conexion.con);
                                comando.Parameters.AddWithValue("@id", id);
                                comando.Parameters.AddWithValue("@hor", hor2);
                                comando.ExecuteNonQuery();
                            }

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
            }
        }
        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Seguro que desea cancelar", "Cancelar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                this.Close();
            }

        }

        private void NuevoEmpRHNOM_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            DataTable dtComboHora = new DataTable();
            DataTable dtComboHorasab = new DataTable();
            DataTable dtPuesto = new DataTable();

            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS WHERE DEPTID NOT IN(32,1)";
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

            query = "SELECT ID_HOR,Descripcion FROM HORARIOS WHERE ID_HOR IN(10,12,14,16,18,20,22,24)";
            adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboHorasab);
            comboBoxHorSab.DisplayMember = "Descripcion";
            comboBoxHorSab.ValueMember = "ID_HOR";
            comboBoxHorSab.DataSource = dtComboHorasab;


            conexion.cerrar();
            if (opcion == 2)
            {
                textBoxID.Enabled = false;

            }
            else
            {
                textBoxID.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBoxHorSab.Visible = true;
            }
            else
            {
                comboBoxHorSab.Visible = false;
            }
        }
    }
}
