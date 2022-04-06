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
    public partial class DiaFestivo : Form
    {
        public DiaFestivo()
        {
            InitializeComponent();
        }
        public int bandera = 0;
        private void DiaFestivo_Load(object sender, EventArgs e)
        {
            actualizadias();
        }

        public void actualizadias()
        {
            conexionbd conexion = new conexionbd();
            DataTable dtComboDepts = new DataTable();
            conexion.abrir();
            string query = "SELECT  ID_DIA AS ID, DESCRIPCION AS 'DESCRIPCIÓN DEL DÍA', mesfestivo AS MES, diafestivo AS 'DÍA' FROM DIASFESTIVOS ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            conexion.cerrar();
            dataGridViewDatos.DataSource = dtComboDepts;
            dataGridViewDatos.Columns[0].Visible = false;
        }

        private void toolStripNew_Click(object sender, EventArgs e)
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
                string query = "DELETE FROM DIASFESTIVOS WHERE ID_DIA=" + dataGridViewDatos.CurrentRow.Cells["ID"].Value.ToString();
                Debug.WriteLine(query);
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                actualizadias();

            }
            else
            {
                MessageBox.Show("No se realizó ningún cambio.");
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            string query;
            SqlCommand comando = new SqlCommand();
            try
            {
                switch (bandera)
                {
                    case 1:
                        string descripcion, dia, mes;
                        int newrow;
                        newrow = 0;

                        descripcion = dataGridViewDatos.Rows[newrow].Cells[1].Value.ToString();
                        mes = dataGridViewDatos.Rows[newrow].Cells[2].Value.ToString();
                        dia = dataGridViewDatos.Rows[newrow].Cells[3].Value.ToString();

                        if ((descripcion != "") && (mes != "") && (dia != ""))
                        {
                            conexion.abrir();
                            query = "INSERT INTO DIASFESTIVOS(descripcion,mesfestivo,diafestivo) VALUES('" + descripcion + "'," + mes + "," + dia + ")";
                            Debug.WriteLine(query);
                            comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                            toolStripButtonNew.Enabled = true;
                            toolStripButtonDele.Enabled = true;
                            toolStripButtonUp.Enabled = true;
                            dataGridViewDatos.AllowUserToAddRows = false;
                            dataGridViewDatos.ReadOnly = true;
                            actualizadias();

                            conexion.abrir();
                            query = "INSERT INTO movimientos values(" + Program.id + ",'INGRESO DE DÍA FESTIVO "+descripcion+"','" + DateTime.Now + "','" + this.Text + "');";
                            comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
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
                                query = "UPDATE DIASFESTIVOS SET descripcion='"+ row.Cells["DESCRIPCIÓN DEL DÍA"].Value.ToString() +"', mesfestivo="+ row.Cells["MES"].Value.ToString() +", diafestivo="+ row.Cells["DÍA"].Value.ToString()+" WHERE ID_DIA="+ row.Cells["ID"].Value.ToString();
                                Debug.WriteLine(query);
                                comando = new SqlCommand(query, conexion.con);
                                comando.ExecuteNonQuery();
                                conexion.cerrar();
                                toolStripButtonNew.Enabled = true;
                                toolStripButtonDele.Enabled = true;
                                toolStripButtonUp.Enabled = true;
                                dataGridViewDatos.AllowUserToAddRows = false;
                                dataGridViewDatos.ReadOnly = true;
                            }
                            conexion.abrir();
                            query = "INSERT INTO movimientos values(" + Program.id + ",'ACTUALIZACIÓN DE DATOS EN DÍA FESTIVOS','" + DateTime.Now + "','" + this.Text + "');";
                            comando = new SqlCommand(query, conexion.con);
                            comando.ExecuteNonQuery();
                            conexion.cerrar();
                            actualizadias();
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
            }
            bandera = 0;
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
