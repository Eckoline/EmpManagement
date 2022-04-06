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
    public partial class EstatusEventos : Form
    {
        public EstatusEventos()
        {
            InitializeComponent();
        }
        conexionbd conexion = new conexionbd();
        DataTable dtComboDepts = new DataTable();
        string query = "";
        private void EstatusEventos_Load(object sender, EventArgs e)
        {
            if (Program.usuario == "rhreclu")
            {
                eliminarToolStripMenuItem.Enabled = false;
            }
           // guardarToolStripMenuItem.Enabled = false;
            query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where  evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
            Debug.WriteLine(query);
            llenadatos(query);
            cargafiltro();
        }

        public void llenadatos(string consulta)
        {
            conexion.abrir();
            DataTable dtEmpEven = new DataTable();
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion.con);
            adaptador.Fill(dtEmpEven);
            conexion.cerrar();
            dataGridViewDatos.DataSource = dtEmpEven;
        }

        private void toolStripTextBoxID_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBoxID.Text == "")
            {
                if (toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString()=="1")
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
                }
                else
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where userinfocus.defaultdeptid=" + toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() + " and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
                }     
            }
            else
            {
                toolStripTextBoxNom.Text = "";
                if(toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() == "1")
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where EVENEMP.badgenumber LIKE '%" + toolStripTextBoxID.Text + "%' and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
                }
                else
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where EVENEMP.badgenumber LIKE '%" + toolStripTextBoxID.Text + "%' and userinfocus.defaultdeptid=" + toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() + " and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
                }
                
            }
            Debug.WriteLine(query);
            llenadatos(query);
        }

        private void toolStripTextBoxNom_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBoxNom.Text == "")
            {
                if (toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() == "1")
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
                }
                else
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where userinfocus.defaultdeptid=" + toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() + " and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
                }
            }
            else
            {
                toolStripTextBoxID.Text = "";
                if (toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() == "1")
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where USERINFOCUS.NAME LIKE '%" + toolStripTextBoxNom.Text + "%' COLLATE Modern_Spanish_CI_AI  and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "' ;";
                }
                else
                {
                    query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where USERINFOCUS.NAME '%" + toolStripTextBoxNom.Text + "%'  COLLATE Modern_Spanish_CI_AI and userinfocus.defaultdeptid=" + toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() + " and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
                }

            }
            Debug.WriteLine(query);
            llenadatos(query);

        }

        private void toolStripComboBoxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripTextBoxNom.Text = "";
            toolStripTextBoxID.Text = "";
            if (toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() == "1")
            {
                query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
            }
            else
            {
                query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento', evenemp.Observaciones from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where userinfocus.defaultdeptid=" + toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() + " and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "';";
            }
            //query = "select EVENEMP.id_even as 'ID Evento', EVENEMP.badgenumber as 'ID', USERINFOCUS.NAME AS 'Nombre', EVENEMP.fecin as 'F. Inicio', EVENEMP.fecfin as 'F. Final', EVENEMP.fecreg 'F. Registro', EVENTO.DESCRIPCION as 'Descripción', EVENTO.GRUPO AS 'Tipo Evento' from (EVENEMP inner join USERINFOCus on EVENEMP.badgenumber=USERINFOCus.Badgenumber)inner join evento on EVENEMP.id_even=evento.id_even where userinfocus.defaultdeptid=" + toolStripComboBoxFiltro.ComboBox.SelectedValue.ToString() + " and evenemp.fecin>='" + dateTimePickerIni.Value.ToString("MM-dd-yyyy") + "' and fecfin<='" + dateTimePickerFin.Value.ToString("MM-dd-yyyy") + "' ;";
            Debug.WriteLine(query);
            llenadatos(query);
        }

        private void dateTimePickerFin_ValueChanged(object sender, EventArgs e)
        {
            
          
        }
        public void cargafiltro()
        {
            conexion.abrir();
            string query = "SELECT DEPTID,DEPTNAME FROM DEPARTMENTS WHERE DEPTID<>32";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtComboDepts);
            toolStripComboBoxFiltro.ComboBox.DisplayMember = "DEPTNAME";
            toolStripComboBoxFiltro.ComboBox.ValueMember = "DEPTID";
            toolStripComboBoxFiltro.ComboBox.DataSource = dtComboDepts;
            conexion.cerrar();
        }
        public string setcolor(string evento)
        {
            string col="";
            string query1="";
            conexion.abrir();
            DataTable dtEmpEven = new DataTable();
            query1 = "SELECT COLOR FROM EVENTO WHERE DESCRIPCION='" + evento+"'";
            SqlDataAdapter adaptador = new SqlDataAdapter(query1, conexion.con);
            adaptador.Fill(dtEmpEven);
            conexion.cerrar();
            col = dtEmpEven.Rows[0]["COLOR"].ToString();
            return col;
        }


        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //guardarToolStripMenuItem.Enabled = true;
        }

        private void dataGridViewDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string colorrow="WHITE";
            if (this.dataGridViewDatos.Columns[e.ColumnIndex].Name == "Descripción")
            {
                if (e.Value != null)
                {
                    if (e.Value.GetType() != typeof(System.DBNull))
                    {
                        colorrow = setcolor(e.Value.ToString());
                        if (colorrow == "Black")
                        {
                            dataGridViewDatos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        }
                        dataGridViewDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromName(colorrow);
                    }
                }
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            i = toolStripComboBoxFiltro.SelectedIndex;

            DialogResult resultado = MessageBox.Show("¿Seguro que desea eliminar este evento?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                conexion.abrir();
                query = "DELETE FROM EVENEMP where id_even='"+dataGridViewDatos.CurrentRow.Cells[0].Value.ToString()+"' and badgenumber='"+ dataGridViewDatos.CurrentRow.Cells[1].Value.ToString() + "' and fecin='"+ DateTime.Parse(dataGridViewDatos.CurrentRow.Cells[3].Value.ToString()).ToString("MM-dd-yyyy") + "' and fecfin='"+ DateTime.Parse(dataGridViewDatos.CurrentRow.Cells[4].Value.ToString()).ToString("MM-dd-yyyy") + "' and fecreg='"+ DateTime.Parse(dataGridViewDatos.CurrentRow.Cells[5].Value.ToString()).ToString("MM-dd-yyyy") + "'";
                Debug.WriteLine(query);
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                toolStripComboBoxFiltro.ComboBox.SelectedIndex = 1;
                toolStripComboBoxFiltro.ComboBox.SelectedIndex = i;
            }
            else
            {
                MessageBox.Show("No se realizaron cambios.");
            }
        }

        private void dateTimePickerIni_ValueChanged(object sender, EventArgs e)
        {
            int i = 0;
            toolStripTextBoxNom.Text = "";
            toolStripTextBoxID.Text = "";
            i=toolStripComboBoxFiltro.SelectedIndex;
            toolStripComboBoxFiltro.SelectedIndex = 1;
            toolStripComboBoxFiltro.SelectedIndex = i;

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
