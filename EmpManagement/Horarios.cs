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
    public partial class Horarios : Form
    {
        public Horarios()
        {
            InitializeComponent();
        }
        public int bandera = 0;

        private void Horarios_Load(object sender, EventArgs e)
        {
            actualizahorarios();
            

        }
        public void actualizahorarios()
        {
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            conexion.abrir();
            string query = "SELECT ID_HOR,DESCRIPCION,HOR_IN AS 'HORA ENTRADA', HOR_INTURNO AS 'HORA ENTRADA TURNO',HOR_OUT AS 'HORA SALIDA', HRS_DIA 'HORAS DÍA', HRS_SEMANA AS 'HORAS SEMANA',idgroup as 'TIPO' FROM HORARIOS ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            conexion.cerrar();
            dataGridViewDatos.DataSource = dtComboDepts;
            dataGridViewDatos.Columns[0].Visible = false;
            dataGridViewDatos.Columns[1].Width = 200;
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        { 
            dataGridViewDatos.ReadOnly = false;
            // Referenciamos el objeto DataTable asociado
            // al control DataGridView
            //
            DataTable dt = (DataTable)this.dataGridViewDatos.DataSource;

            // Creamos un nuevo objeto DataRow
            //
            DataRow dr = dt.NewRow();

            // Insertamos la fila después de la tercera fila,
            // que es la que tiene el ¡ndice 4.
            //
            dt.Rows.InsertAt(dr, 0);
            // dataGridViewDatos.AllowUserToAddRows = true;
            toolStripButtonNew.Enabled = false;
            toolStripButtonDele.Enabled = false;
            toolStripButtonUp.Enabled = false;
            bandera = 1;
        }

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            dataGridViewDatos.ReadOnly = false;
            toolStripButtonNew.Enabled = false;
            toolStripButtonDele.Enabled = false;
            toolStripButtonUp.Enabled = false;
            bandera = 2;
        }

        private void toolStripButtonDele_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DialogResult resultado = MessageBox.Show("¿Seguro que desea Eliminar este Registro?", "Eliminación de registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                conexion.abrir();
                string query = "DELETE FROM HORARIOS WHERE ID_HOR=" + dataGridViewDatos.CurrentRow.Cells["ID_HOR"].Value.ToString();
                Debug.WriteLine(query);
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                actualizahorarios();

            }
            else
            {
                MessageBox.Show("No se realizó ningún cambio.");
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            try
            {
                switch (bandera)
                {
                    case 1:
                        string ID_HOR,HOR_IN,HOR_INTURNO,HOR_OUT,HRS_DIA,HRS_SEMANA,DESCRIPCION,TIPO;
                        int newrow;
                        newrow = 0;
                        DataTable dtComboDepts = new DataTable();
                        conexion.abrir();
                        string query = "SELECT TOP 1 ID_HOR FROM HORARIOS ORDER BY ID_HOR DESC;";
                        SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                        adaptador.Fill(dtComboDepts);
                        conexion.cerrar();
                        ID_HOR =(Int32.Parse( dtComboDepts.Rows[0]["ID_HOR"].ToString())+1).ToString();
                        HOR_IN = dataGridViewDatos.Rows[newrow].Cells[2].Value.ToString();
                        HOR_INTURNO = dataGridViewDatos.Rows[newrow].Cells[3].Value.ToString();
                        HOR_OUT= dataGridViewDatos.Rows[newrow].Cells[4].Value.ToString();
                        HRS_DIA = dataGridViewDatos.Rows[newrow].Cells[5].Value.ToString();
                        HRS_SEMANA = dataGridViewDatos.Rows[newrow].Cells[6].Value.ToString();
                        DESCRIPCION = dataGridViewDatos.Rows[newrow].Cells[1].Value.ToString();
                        TIPO= dataGridViewDatos.Rows[newrow].Cells[7].Value.ToString();
                        if ((HOR_IN != "") || (HOR_INTURNO != "") || (HOR_OUT != "")|| (HRS_DIA != "") || (HRS_SEMANA != "")||(DESCRIPCION != "")||(TIPO!=""))
                        {

                            conexion.abrir();
                            query = "INSERT INTO HORARIOS(ID_HOR,HOR_IN,HOR_INTURNO,HOR_OUT,HRS_DIA,HRS_SEMANA,DESCRIPCION,IDGROUP) VALUES(" + ID_HOR + ",'" + HOR_IN + "','" + HOR_INTURNO + "','" + HOR_OUT + "','" + HRS_DIA + "'," + HRS_SEMANA + ",'" + DESCRIPCION + "','"+TIPO+"')";
                            Debug.WriteLine(query);
                            SqlCommand comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();

                            toolStripButtonNew.Enabled = true;
                            toolStripButtonDele.Enabled = true;
                            toolStripButtonUp.Enabled = true;
                            dataGridViewDatos.AllowUserToAddRows = false;
                            dataGridViewDatos.ReadOnly = true;
                            actualizahorarios();
                        }
                        else
                        {
                            MessageBox.Show("Capture todos los campos.");
                        }

                        // MessageBox.Show(dataGridViewDatos.Rows.Count.ToString());
                        break;
                    case 2:
                        DialogResult resultado = MessageBox.Show("¿Seguro que desea actualizar los registros?", "Actualización de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (resultado == DialogResult.OK)
                        {
                            foreach (DataGridViewRow row in dataGridViewDatos.Rows)
                            {
                                conexion.abrir();
                                query = "UPDATE HORARIOS SET HOR_IN='"+ row.Cells["HORA ENTRADA"].Value.ToString() + "',HOR_INTURNO='"+ row.Cells["HORA ENTRADA TURNO"].Value.ToString() + "', HOR_OUT='"+ row.Cells["HORA SALIDA"].Value.ToString() + "',HRS_DIA='"+ row.Cells["HORAS DÍA"].Value.ToString() + "',HRS_SEMANA="+ row.Cells["HORAS SEMANA"].Value.ToString() + ", DESCRIPCION='"+ row.Cells["DESCRIPCION"].Value.ToString() + "',idgroup='"+ row.Cells["TIPO"].Value.ToString() + "'  WHERE ID_HOR=" + row.Cells["ID_HOR"].Value.ToString();
                                Debug.WriteLine(query);
                                SqlCommand comando = new SqlCommand(query, conexion.con);
                                comando.ExecuteNonQuery();
                                conexion.cerrar();
                                toolStripButtonNew.Enabled = true;
                                toolStripButtonDele.Enabled = true;
                                toolStripButtonUp.Enabled = true;
                                dataGridViewDatos.AllowUserToAddRows = false;
                                dataGridViewDatos.ReadOnly = true;
                            }
                            actualizahorarios();
                        }
                        else
                        {
                            MessageBox.Show("No se realizó ningun cambio");
                        }

                        break;
                    default:
                        MessageBox.Show("No hay algún cambio a guardar.");
                        break;
                }



            }
            catch (Exception ex) //bloque catch para captura de error
            {
                string error = ex.Message; //acción para manejar el error
                MessageBox.Show("Introduzca los datos correctos. " + error);
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Seguro que desea cancelar?", "Cancelar Acción", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                toolStripButtonNew.Enabled = true;
                toolStripButtonDele.Enabled = true;
                toolStripButtonUp.Enabled = true;
                dataGridViewDatos.AllowUserToAddRows = false;
                dataGridViewDatos.ReadOnly = true;
                if (bandera == 1)
                {
                    dataGridViewDatos.Rows.RemoveAt(0);
                }
                bandera = 0;
            }
        }

        private void dataGridViewDatos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (dataGridViewDatos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != String.Empty)
                {
                    SendKeys.Send("{Right}");
                }
            }
            catch
            {
                MessageBox.Show("Se ha producido un error");
            }
        }
    }
}
