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
    public partial class EdicionEven : Form
    {
        public EdicionEven()
        {
            InitializeComponent();
        }
        public int bandera = 0;
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
            dt.Rows.InsertAt(dr,0);
            // dataGridViewDatos.AllowUserToAddRows = true;
           
            toolStripButtonNew.Enabled = false;
            toolStripButtonDele.Enabled = false;
            toolStripButtonUp.Enabled = false;
            bandera = 1;
        }

        public void actualizaeven()
        {
            conexionbd conexion = new conexionbd();
            DataTable dtdiaseven = new DataTable();
            conexion.abrir();
            string query = "SELECT  ID_EVEN AS ID, DESCRIPCION AS 'Descripción del Evento', GRUPO AS 'Tipo Evento', Color, Valor FROM EVENTO ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtdiaseven);
            conexion.cerrar();
            dataGridViewDatos.DataSource = dtdiaseven;
            dataGridViewDatos.Columns[0].Visible = false;
        }

        private void EdicionEven_Load(object sender, EventArgs e)
        {
            actualizaeven();
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
                string query = "DELETE FROM EVENTO WHERE ID_EVEN=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                Debug.WriteLine(query);
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                actualizaeven();

            }
            else
            {
                MessageBox.Show("No se realizó ningún cambio.");
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
          /*  try
            {*/
                switch (bandera)
                {
                    case 1:
                        string descripcion, grupo, color,valor;
                        int newrow;
                        newrow = 0;

                        descripcion = dataGridViewDatos.Rows[newrow].Cells[1].Value.ToString();
                        grupo = dataGridViewDatos.Rows[newrow].Cells[2].Value.ToString();
                        color = dataGridViewDatos.Rows[newrow].Cells[3].Value.ToString();
                        valor = dataGridViewDatos.Rows[newrow].Cells[4].Value.ToString();

                    if ((descripcion != "") && (grupo != "") && (color != "")&&(valor!=""))
                        {
                            conexion.abrir();
                            string query = "INSERT INTO EVENTO VALUES('" + descripcion + "','" + grupo + "','" + color + "',"+valor+")";
                            Debug.WriteLine(query);
                            SqlCommand comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                            toolStripButtonNew.Enabled = true;
                            toolStripButtonDele.Enabled = true;
                            toolStripButtonUp.Enabled = true;
                            dataGridViewDatos.AllowUserToAddRows = false;
                            dataGridViewDatos.ReadOnly = true;
                            actualizaeven();
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
                                string query = "UPDATE EVENTO SET DESCRIPCION='"+ row.Cells["Descripción del Evento"].Value.ToString() + "', GRUPO='"+ row.Cells["Tipo Evento"].Value.ToString() + "', COLOR='"+ row.Cells["Color"].Value.ToString() + "', valor="+ row.Cells["Valor"].Value.ToString() + " WHERE ID_EVEN=" + row.Cells["ID"].Value.ToString();
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
                            actualizaeven();
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
            //}
           /* catch (Exception ex) //bloque catch para captura de error
            {
                string error = ex.Message; //acción para manejar el error
                MessageBox.Show("Introduzca los datos correctos. " + error);
            }*/
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
