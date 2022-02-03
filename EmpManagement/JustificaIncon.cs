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
    public partial class JustificaIncon : Form
    {
        public JustificaIncon()
        {
            InitializeComponent();
        }

        private void buttonAcept_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            string comentarios;
            int asis, inasis, ret, saltemp,incon;
            conexion.abrir();
            DataTable dtDiasF = new DataTable();
            string query = "SELECT asistencia,inasistencia,retardo,saltemp,inconsistencia,comentarios FROM detalledias WHERE badgenumber="+labelIDEmp.Text+" AND fecha='"+labelFec.Text+"'";
            SqlDataAdapter adaptadorDias = new SqlDataAdapter(query, conexion.con);
            adaptadorDias.Fill(dtDiasF);
            conexion.cerrar();
            comentarios = dtDiasF.Rows[0]["comentarios"].ToString();
            asis= Int32.Parse(dtDiasF.Rows[0]["asistencia"].ToString());
            inasis = Int32.Parse(dtDiasF.Rows[0]["inasistencia"].ToString());
            ret = Int32.Parse(dtDiasF.Rows[0]["retardo"].ToString());
            saltemp = Int32.Parse(dtDiasF.Rows[0]["saltemp"].ToString());
            incon= Int32.Parse(dtDiasF.Rows[0]["inconsistencia"].ToString());
            
            conexion.abrir();
            DataTable dtTip = new DataTable();
            query = "SELECT id_even,descripcion from evento";
            SqlDataAdapter adaptadoreven = new SqlDataAdapter(query, conexion.con);
            adaptadoreven.Fill(dtTip);
            conexion.cerrar();
            string[] tiposeven = new string[dtTip.Rows.Count];
            int i = 0;
            foreach(DataRow row in dtTip.Rows)
            {
                tiposeven[i] = row["descripcion"].ToString();
                i++;
            }
            switch (comboBoxTipo.SelectedValue)
            {
                case 0:
                  
                    break;
                case 1:
                    if (checkBoxcorr.Checked)
                    {
                        inasis = inasis + 1;
                        incon = incon + 1;
                    }
                    else
                    {
                        inasis = inasis - 1;
                        incon = incon - 1;
                    }
               
                    break;
                case 2:

                    break;
                case 3:
                    if (checkBoxcorr.Checked)
                    {
                        inasis = inasis +1;
                        incon = incon + 1;
                    }
                    else
                    {
                        inasis = inasis - 1;
                        incon = incon - 1;
                    }
                  
                    break;
                case 4:
                    if (checkBoxcorr.Checked)
                    {
                        inasis = inasis +1;
                        incon = incon + 1;
                    }
                    else
                    {
                        inasis = inasis - 1;
                        incon = incon - 1;
                    }
             
                    break;
                case 5:
                    if (checkBoxcorr.Checked)
                    {
                        inasis = inasis + 1;
                        incon = incon + 1;
                    }
                    else
                    {
                        inasis = inasis - 1;
                        incon = incon - 1;
                    }
         
                    break;
                case 6:
                    break;
                case 7:
                    if (checkBoxcorr.Checked)
                    {
                        inasis = inasis + 1;
                        incon = incon + 1;
                    }
                    else
                    {
                        inasis = inasis - 1;
                        incon = incon - 1;
                    }
            
                    break;
                case 8:
                    break;
                case 9:
                    //
                    if (checkBoxcorr.Checked)
                    {
                        ret = ret +1;
                        incon = incon + 1;
                    }
                    else
                    {
                        ret = ret - 1;
                        incon = incon - 1;
                    }
                 
                    break;
                case 10:
                    //
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    //
                    break;
                case 14:
                    //
                    if(checkBoxcorr.Checked)
                    {
                        saltemp = saltemp +1;
                        incon = incon + 1;
                    }
                    else
                    {
                        saltemp = saltemp - 1;
                        incon = incon - 1;
                    }
         
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    //
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    if (checkBoxcorr.Checked)
                    {
                        ret = ret +1;
                        incon = incon + 1;
                    }
                    else
                    {
                        ret = ret - 1;
                        incon = incon - 1;
                    }
                
                    break;

            }

            if ((textBoxComentInc.Text.Length > 10)&&(textBoxComentInc.Text!=""))
            {
                conexion.abrir();
                if (comentarios.Equals(""))
                {
                    query = "UPDATE detalledias SET tipoeven='"+comboBoxTipo.Text+"', comentarios='" + comboBoxTipo.Text+ ", " + textBoxComentInc.Text + "', asistencia=" + asis + ",inasistencia=" + inasis + ",retardo=" + ret + ",saltemp=" + saltemp + ", inconsistencia=" + incon + " WHERE badgenumber=" + labelIDEmp.Text + " AND fecha='" + labelFec.Text + "'";
                }
                else
                {
                    query = "UPDATE detalledias SET tipoeven='"+comboBoxTipo.Text+"',comentarios='" + comentarios + ". " + comboBoxTipo.Text + ", " + textBoxComentInc.Text + "', asistencia=" + asis + ",inasistencia=" + inasis + ",retardo=" + ret + ",saltemp=" + saltemp + ", inconsistencia=" + incon + " WHERE badgenumber=" + labelIDEmp.Text + " AND fecha='" + labelFec.Text + "'";
                }
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                Debug.WriteLine(query);

                conexion.abrir();
                query = "INSERT INTO EVENEMP VALUES(" + comboBoxTipo.SelectedValue.ToString() + "," + labelIDEmp.Text + ",'" + labelFec.Text + "','" + labelFec.Text + "','" + DateTime.Now.ToString("MM-dd-yyyy") + "')";
                comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Introduzca una justificación valida.");
            }
 
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se realizó ningún cambio.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void cargacombo()
        {
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            DataTable dtcomboTip = new DataTable();
            string query = "SELECT id_even,descripcion from evento where grupo='"+labelTipo.Text+"'";
            SqlDataAdapter adaptadorDias = new SqlDataAdapter(query, conexion.con);
            adaptadorDias.Fill(dtcomboTip);
            conexion.cerrar();
            comboBoxTipo.DisplayMember = "descripcion";
            comboBoxTipo.ValueMember = "id_even";
            comboBoxTipo.DataSource = dtcomboTip;
        }

        private void JustificaIncon_Load(object sender, EventArgs e)
        {
            cargacombo();
        }
    }
}
