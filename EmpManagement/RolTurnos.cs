using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace EmpManagement
{
    public partial class RolTurnos : Form
    {
        public RolTurnos()
        {
            InitializeComponent();
        }


        int contador = 0;
        string query;
        conexionbd conexion = new conexionbd();
        string pagina;
        public DataTable dtT1Op = new DataTable();
        public DataTable dtT2Op = new DataTable();
        public DataTable dtT3Op = new DataTable();

        public DataTable dtT1Sup = new DataTable();
        public DataTable dtT2Sup = new DataTable();
        public DataTable dtT3Sup = new DataTable();

        public DataTable dtT1Mol = new DataTable();
        public DataTable dtT2Mol = new DataTable();
        public DataTable dtT3Mol = new DataTable();

        public DataTable dtT1AC = new DataTable();
        public DataTable dtT2AC = new DataTable();
        public DataTable dtT3AC = new DataTable();

        public DataTable dtT1Alm = new DataTable();
        public DataTable dtT2Alm = new DataTable();
        public DataTable dtT3Alm = new DataTable();

        public DataTable dtT1Aux = new DataTable();
        public DataTable dtT2Aux = new DataTable();
        public DataTable dtT3Aux = new DataTable();


        public DataTable dtT1Mold = new DataTable();
        public DataTable dtT2Mold = new DataTable();
        public DataTable dtT3Mold = new DataTable();

       /* public DataTable dt11 = new DataTable();
        public DataTable dt22 = new DataTable();
        public DataTable dt33= new DataTable();
       */

        public int banderaop = 0;
        public int banderaauxsup = 0;
        public int banderamol = 0;
        public int banderaac = 0;
        public int banderaal = 0;
        public int banderaaux = 0;
        public int banderamold = 0;

        private void RolTurnos_Load(object sender, EventArgs e)
        {
            cargacombo();
            creacolumna();

         
            //creacolumnasdatatables();
            banderaop = 0;
            banderaauxsup = 0;
            banderamol = 0;
            banderaac = 0;
            banderaal = 0;
            banderaaux = 0;
            banderamold = 0;
            toolStripComboBox1.SelectedIndex = 0;
            this.WindowState = FormWindowState.Maximized;
        }
       
        public void creacolumna()
        {
            DataGridViewCheckBoxColumn check1 = new DataGridViewCheckBoxColumn
            {
                Name = "Sel"
            };
            DataGridViewCheckBoxColumn check2 = new DataGridViewCheckBoxColumn
            {
                Name = "Sel"
            };
            DataGridViewCheckBoxColumn check3 = new DataGridViewCheckBoxColumn
            {
                Name = "Sel"
            };

            DataGridViewCheckBoxColumn check11 = new DataGridViewCheckBoxColumn
            {
                Name = "Sel"
            };

            DataGridViewTextBoxColumn id11 = new DataGridViewTextBoxColumn
            {
                Name = "ID"
            };

            DataGridViewTextBoxColumn name11 = new DataGridViewTextBoxColumn
            {
                Name = "Nombre"
            };


            DataGridViewCheckBoxColumn check22 = new DataGridViewCheckBoxColumn
            {
                Name = "Sel"
            };
            DataGridViewTextBoxColumn id22 = new DataGridViewTextBoxColumn
            {
                Name = "ID"
            };
            DataGridViewTextBoxColumn name22 = new DataGridViewTextBoxColumn
            {
                Name = "Nombre"
            };

            DataGridViewCheckBoxColumn check33 = new DataGridViewCheckBoxColumn
            {
                Name = "Sel"
            };
            DataGridViewTextBoxColumn id33 = new DataGridViewTextBoxColumn
            {
                Name = "ID"
            };
            DataGridViewTextBoxColumn name33 = new DataGridViewTextBoxColumn
            {
                Name = "Nombre"
            };

            dataGridViewt1.Columns.Insert(0, check1);
            dataGridViewt2.Columns.Insert(0, check2);
            dataGridViewt3.Columns.Insert(0, check3);
            /*
            dt11.Columns.Add("Sel", typeof(Boolean));
            dt11.Columns.Add("ID", typeof(Int32));
            dt11.Columns.Add("Nombre", typeof(String));

            dt22.Columns.Add("Sel", typeof(Boolean));
            dt22.Columns.Add("ID", typeof(Int32));
            dt22.Columns.Add("Nombre", typeof(String));

            dt33.Columns.Add("Sel", typeof(Boolean));
            dt33.Columns.Add("ID", typeof(Int32));
            dt33.Columns.Add("Nombre", typeof(String));
           */

            dataGridViewt11.Columns.Insert(0, check11);
            dataGridViewt11.Columns.Insert(1, id11);
            dataGridViewt11.Columns.Insert(2, name11);


            dataGridViewt22.Columns.Insert(0, check22);
            dataGridViewt22.Columns.Insert(1, id22);
            dataGridViewt22.Columns.Insert(2, name22);

            dataGridViewt33.Columns.Insert(0, check33);
            dataGridViewt33.Columns.Insert(1, id33);
            dataGridViewt33.Columns.Insert(2, name33);
        }

        public void cargacombo()
        {
            toolStripComboBox1.Items.Add("Operadoras");
            toolStripComboBox1.Items.Add("Sups y Aux. Sup");
            toolStripComboBox1.Items.Add("Molineros");
            toolStripComboBox1.Items.Add("Auditores Calidad");
            toolStripComboBox1.Items.Add("Almacén");
            toolStripComboBox1.Items.Add("Aux. Mantto");
            toolStripComboBox1.Items.Add("Moldes");
        }

        public void load(string pagina)
        {
            switch (pagina)
            {
                case "Operadoras":
                    //carga operadoras
                    Loadquery(dtT1Op, 1, 38);
                    Loadquery(dtT2Op, 2, 38);
                    Loadquery(dtT3Op, 3, 38);
                    dataGridViewt1.DataSource = dtT1Op;
                    dataGridViewt2.DataSource = dtT2Op;
                    dataGridViewt3.DataSource = dtT3Op;

                    DataTable dtclonet1=new DataTable();
                    DataTable dtclonet2 = new DataTable();
                    DataTable dtclonet3 = new DataTable();

                    Loadquery(dtclonet1, 1, 38);
                    Loadquery(dtclonet2, 2, 38);
                    Loadquery(dtclonet3, 3, 38);
                    dataGridViewt1viejo.DataSource = dtclonet1;
                    dataGridViewt2viejo.DataSource = dtclonet2;
                    dataGridViewt3viejo.DataSource = dtclonet3;


       

                    dataGridViewt1.Columns[1].ReadOnly =true;
                    dataGridViewt2.Columns[1].ReadOnly = true;
                    dataGridViewt3.Columns[1].ReadOnly = true;

                    dataGridViewt11.Columns[1].ReadOnly = true;
                    dataGridViewt22.Columns[1].ReadOnly = true;
                    dataGridViewt33.Columns[1].ReadOnly = true;

                    dataGridViewt1.Columns[2].ReadOnly = true;
                    dataGridViewt2.Columns[2].ReadOnly = true;
                    dataGridViewt3.Columns[2].ReadOnly = true;

                    dataGridViewt11.Columns[2].ReadOnly = true;
                    dataGridViewt22.Columns[2].ReadOnly = true;
                    dataGridViewt33.Columns[2].ReadOnly = true;

                    dataGridViewt1.Columns[3].ReadOnly = true;
                    dataGridViewt2.Columns[3].ReadOnly = true;
                    dataGridViewt3.Columns[3].ReadOnly = true;

                    

                    banderaop = 1;
                    break;

                case "Sups y Aux. Sup":
                    //carga sups y auxsups
                    Loadquery(dtT1Sup, 4, 8);
                    Loadquery(dtT2Sup, 5, 8);
                    Loadquery(dtT3Sup, 6, 8);
                    dataGridViewt1.DataSource = dtT1Sup;
                    dataGridViewt2.DataSource = dtT2Sup;
                    dataGridViewt3.DataSource = dtT3Sup;

                    dtclonet1 = new DataTable();
                    dtclonet2 = new DataTable();
                    dtclonet3 = new DataTable();
                    Loadquery(dtclonet1, 4, 8);
                    Loadquery(dtclonet2, 5, 8);
                    Loadquery(dtclonet3, 6, 8);

                    dataGridViewt1viejo.DataSource = dtclonet1;
                    dataGridViewt2viejo.DataSource = dtclonet2;
                    dataGridViewt3viejo.DataSource = dtclonet3;
                    banderaauxsup = 1;
                    break;

                case "Molineros":
                    //carga molineros
                    Loadquery(dtT1Mol, 1, 7);
                    Loadquery(dtT2Mol, 2, 7);
                    Loadquery(dtT3Mol, 3, 7);
                    dataGridViewt1.DataSource = dtT1Mol;
                    dataGridViewt2.DataSource = dtT2Mol;
                    dataGridViewt3.DataSource = dtT3Mol;
                    dtclonet1 = new DataTable();
                    dtclonet2 = new DataTable();
                    dtclonet3 = new DataTable();
                    Loadquery(dtclonet1, 4, 8);
                    Loadquery(dtclonet2, 5, 8);
                    Loadquery(dtclonet3, 6, 8);

                    dataGridViewt1viejo.DataSource = dtclonet1;
                    dataGridViewt2viejo.DataSource = dtclonet2;
                    dataGridViewt3viejo.DataSource = dtclonet3;
                    banderamold = 1;
                    break;

                case "Auditores Calidad":
                    //carga auditor calidad
                    Loadquery(dtT1AC, 4, 13);
                    Loadquery(dtT2AC, 5, 13);
                    Loadquery(dtT3AC, 6, 13);
                    dataGridViewt1.DataSource = dtT1AC;
                    dataGridViewt2.DataSource = dtT2AC;
                    dataGridViewt3.DataSource = dtT3AC;

                    dtclonet1 = new DataTable();
                    dtclonet2 = new DataTable();
                    dtclonet3 = new DataTable();

                    Loadquery(dtclonet1, 4, 13);
                    Loadquery(dtclonet2, 5, 13);
                    Loadquery(dtclonet3, 6, 13);
                    dataGridViewt1viejo.DataSource = dtclonet1;
                    dataGridViewt2viejo.DataSource = dtclonet2;
                    dataGridViewt3viejo.DataSource = dtclonet3;

                    banderaac = 1;
                    break;

                case "Almacén":
                    //carga almacen
                    Loadquery(dtT1Alm, 7, 9);
                    Loadquery(dtT2Alm, 8, 9);
                    //Loadquery(dtT3Alm, 3, 9);
                    dataGridViewt1.DataSource = dtT1Alm;
                    dataGridViewt2.DataSource = dtT2Alm;

                    dtclonet1 = new DataTable();
                    dtclonet2 = new DataTable();
                    Loadquery(dtclonet1, 7, 9);
                    Loadquery(dtclonet2, 8, 9);

                    dataGridViewt1viejo.DataSource = dtclonet1;
                    dataGridViewt2viejo.DataSource = dtclonet2;
                    // dataGridViewt3.DataSource = dtT3Alm;


                    banderaal = 1;
                    break;

                case "Aux. Mantto":
                    //carga auxmantto
                    Loadquery(dtT1Aux, 7, 30);
                    Loadquery(dtT2Aux, 8, 30);
                    //Loadquery(dtT3Aux, 3, 30);
                    dataGridViewt1.DataSource = dtT1Aux;
                    dataGridViewt2.DataSource = dtT2Aux;

                    dtclonet1 = new DataTable();
                    dtclonet2 = new DataTable();
                    Loadquery(dtclonet1, 7, 30);
                    Loadquery(dtclonet2, 8, 30);

                    dataGridViewt1viejo.DataSource = dtclonet1;
                    dataGridViewt2viejo.DataSource = dtclonet2;
       
                    //dataGridViewt3.DataSource = dtT3Aux;
                    banderaaux = 1;
                    break;

                case "Moldes":
                    //carga mold
                    Loadquery(dtT1Mold, 7, 10);
                    Loadquery(dtT2Mold, 8, 10);
                    //Loadquery(dtT3Mold, 3, 10);
                    dataGridViewt1.DataSource = dtT1Mold;
                    dataGridViewt2.DataSource = dtT2Mold;

                    dtclonet1 = new DataTable();
                    dtclonet2 = new DataTable();
                    Loadquery(dtclonet1, 7, 10);
                    Loadquery(dtclonet2, 8, 10);
                    dataGridViewt1viejo.DataSource = dtclonet1;
                    dataGridViewt2viejo.DataSource = dtclonet2;

                    banderamold = 1;
                    break;
            }
        }


        void Loadquery(DataTable dt, int idhor, int dept)
        {
            conexion.abrir();
            query = "SELECT USERINFOCus.Badgenumber AS ID,USERINFOCus.Name AS Nombre,HORARIOS.ID_HOR,HORARIOS.Descripcion FROM USERINFOCus inner join HOREMPLEADO ON USERINFOcus.Badgenumber=HOREMPLEADO.Badgenumber inner join horarios on HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR where horarios.ID_HOR=" + idhor + " AND USERINFOCUS.DEFAULTDEPTID=" + dept + "";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dt);
            conexion.cerrar();
        }

        //envios t1
        private void buttont1t1_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt1, dataGridViewt11);
            labelCantidad1.Text = dataGridViewt11.Rows.Count.ToString();
            selecdeselec();
          
        }

        private void buttont2t1_Click(object sender, EventArgs e)
        {

            envio(dataGridViewt1, dataGridViewt22);
            labelCantidad2.Text = dataGridViewt22.Rows.Count.ToString();
            selecdeselec();
        }

        private void buttont3t1_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt1, dataGridViewt33);
            labelCantidad3.Text = dataGridViewt33.Rows.Count.ToString();
            selecdeselec();
        }

        //envios t2
        private void buttont1t2_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt2, dataGridViewt11);
            labelCantidad1.Text = dataGridViewt11.Rows.Count.ToString();
            selecdeselec();
        }


        private void buttont2t2_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt2, dataGridViewt22);
            labelCantidad2.Text = dataGridViewt22.Rows.Count.ToString();
            selecdeselec();
        }

        private void buttont3t2_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt2, dataGridViewt33);
            labelCantidad3.Text = dataGridViewt33.Rows.Count.ToString();
            selecdeselec();
        }

        //envios t3
        private void buttont1t3_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt3, dataGridViewt11);
            labelCantidad1.Text = dataGridViewt11.Rows.Count.ToString();
            selecdeselec();
        }

        private void buttont2t3_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt3, dataGridViewt22);
            labelCantidad2.Text = dataGridViewt22.Rows.Count.ToString();
            selecdeselec();

        }

        private void buttont3t3_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt3, dataGridViewt33);
            labelCantidad3.Text = dataGridViewt33.Rows.Count.ToString();
            selecdeselec();
        }


        public void envio(DataGridView dg1, DataGridView dg2)
        {
            //dg2.Columns.Add()
            //
            // Se define una lista temporal de registro seleccionados
            //
            List<DataGridViewRow> rowSelected = new List<DataGridViewRow>();

            //
            // Se recorre ca registro de la grilla de origen
            //
            foreach (DataGridViewRow row in dg1.Rows)
            {
                //
                // Se recupera el campo que representa el checkbox, y se valida la seleccion
                // agregandola a la lista temporal
                //
                DataGridViewCheckBoxCell cellSelecion = row.Cells["Sel"] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(cellSelecion.Value))
                {
                    rowSelected.Add(row);
                }
            }

            //
            // Se agrega el item seleccionado a la grilla de destino
            // eliminando la fila de la grilla original
            //
            foreach (DataGridViewRow row in rowSelected)
            {
                dg2.Rows.Add(false, row.Cells["ID"].Value, row.Cells["Nombre"].Value);
                dg1.Rows.Remove(row);
            }
          
        }

        //envios t11
        private void buttont2t11_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt11, dataGridViewt22);
            labelCantidad1.Text = dataGridViewt11.Rows.Count.ToString();
            labelCantidad2.Text = dataGridViewt22.Rows.Count.ToString();
            selecdeselec();

        }

        private void buttont3t11_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt11, dataGridViewt33);
            labelCantidad1.Text = dataGridViewt11.Rows.Count.ToString();
            labelCantidad3.Text = dataGridViewt33.Rows.Count.ToString();
            selecdeselec();
        }

        //envios t22
        private void buttont1t22_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt22, dataGridViewt11);
            labelCantidad1.Text = dataGridViewt11.Rows.Count.ToString();
            labelCantidad2.Text = dataGridViewt22.Rows.Count.ToString();
            selecdeselec();

        }

        private void buttont3t22_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt22, dataGridViewt33);
            labelCantidad2.Text = dataGridViewt22.Rows.Count.ToString();
            labelCantidad3.Text = dataGridViewt33.Rows.Count.ToString();
            selecdeselec();

        }

        //envios t33
        private void buttont1t33_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt33, dataGridViewt11);
            labelCantidad3.Text = dataGridViewt33.Rows.Count.ToString();
            labelCantidad1.Text = dataGridViewt11.Rows.Count.ToString();
            selecdeselec();
        }

        private void buttont2t33_Click(object sender, EventArgs e)
        {
            envio(dataGridViewt33, dataGridViewt22);
            labelCantidad3.Text = dataGridViewt33.Rows.Count.ToString();
            labelCantidad2.Text = dataGridViewt22.Rows.Count.ToString();
            selecdeselec();
        }


        //restablecer todo
  

        //Seleccionar todos
        private void checkBoxSel1_CheckedChanged(object sender, EventArgs e)
        {
            sel(dataGridViewt1, checkBoxSel1.Checked, "Sel");
        }

        private void checkBoxSel2_CheckedChanged(object sender, EventArgs e)
        {

            sel(dataGridViewt2, checkBoxSel2.Checked, "Sel");
        }

        private void checkBoxSel3_CheckedChanged(object sender, EventArgs e)
        {
            sel(dataGridViewt3, checkBoxSel3.Checked, "Sel");
        }

        private void checkBoxSel11_CheckedChanged(object sender, EventArgs e)
        {
            sel(dataGridViewt11, checkBoxSel11.Checked, "Sel");

        }

        private void checkBoxSel22_CheckedChanged(object sender, EventArgs e)
        {
            sel(dataGridViewt22, checkBoxSel22.Checked, "Sel");
        }

        private void checkBoxSel33_CheckedChanged(object sender, EventArgs e)
        {
            sel(dataGridViewt33, checkBoxSel33.Checked, "Sel");
        }

        public void sel(DataGridView dg1, Boolean check, String colu)
        {
            if (check == true)
            {
                foreach (DataGridViewRow row in dg1.Rows)
                {

                    row.Cells[colu].Value = true;

                }
            }
            else
            {
                foreach (DataGridViewRow row in dg1.Rows)
                {

                    row.Cells[colu].Value = false;

                }

            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pagina = toolStripComboBox1.Text;
            load(pagina);
            //dataGridViewt11.Rows.Clear();
            //dataGridViewt22.Rows.Clear();
            //dataGridViewt33.Rows.Clear();
            selecdeselec();
            cantidadcero();
        }

        public void selecdeselec()
        {
            checkBoxSel1.Checked = false;
            checkBoxSel2.Checked = false;
            checkBoxSel3.Checked = false;
            checkBoxSel11.Checked = false;
            checkBoxSel22.Checked = false;
            checkBoxSel33.Checked = false;
        }

        private void terminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pagina = toolStripComboBox1.Text;
       
                switch (pagina)
                {
                    case "Operadoras":
                        toolStripComboBox1.SelectedIndex = 1;
                        pagina = toolStripComboBox1.Text;
                        actualizahor(dataGridViewt11, 1);
                        actualizahor(dataGridViewt22, 2);
                        actualizahor(dataGridViewt33, 3);
                        dataGridViewt11.Rows.Clear();
                        dataGridViewt22.Rows.Clear();
                        dataGridViewt33.Rows.Clear();
                        break;

                    case "Sups y Aux. Sup":
                        toolStripComboBox1.SelectedIndex = 2;
                        pagina = toolStripComboBox1.Text;
                        actualizahor(dataGridViewt11, 4);
                        actualizahor(dataGridViewt22, 5);
                        actualizahor(dataGridViewt33, 6);
                        dataGridViewt11.Rows.Clear();
                        dataGridViewt22.Rows.Clear();
                        dataGridViewt33.Rows.Clear();
                        break;

                    case "Molineros":
                        toolStripComboBox1.SelectedIndex = 3;
                        pagina = toolStripComboBox1.Text;
                        actualizahor(dataGridViewt11, 1);
                        actualizahor(dataGridViewt22, 2);
                        actualizahor(dataGridViewt33, 3);
                        dataGridViewt11.Rows.Clear();
                        dataGridViewt22.Rows.Clear();
                        dataGridViewt33.Rows.Clear();
                        break;

                    case "Auditores Calidad":
                        toolStripComboBox1.SelectedIndex = 4;
                        pagina = toolStripComboBox1.Text;
                        actualizahor(dataGridViewt11, 4);
                        actualizahor(dataGridViewt22, 5);
                        actualizahor(dataGridViewt33, 6);
                        dataGridViewt11.Rows.Clear();
                        dataGridViewt22.Rows.Clear();
                        dataGridViewt33.Rows.Clear();
                        break;

                    case "Almacén":
                        toolStripComboBox1.SelectedIndex = 5;
                        pagina = toolStripComboBox1.Text;
                        actualizahor(dataGridViewt11, 7);
                        actualizahor(dataGridViewt22, 8);
                        dataGridViewt11.Rows.Clear();
                        dataGridViewt22.Rows.Clear();
                        //dataGridViewt33.Rows.Clear();

                        break;

                    case "Aux. Mantto":
                        toolStripComboBox1.SelectedIndex = 6;
                        pagina = toolStripComboBox1.Text;
                        actualizahor(dataGridViewt11, 7);
                        actualizahor(dataGridViewt22, 8);
                        dataGridViewt11.Rows.Clear();
                        dataGridViewt22.Rows.Clear();
                       // dataGridViewt33.Rows.Clear();
                        break;

                    case "Moldes":
                        actualizahor(dataGridViewt11, 7);
                        actualizahor(dataGridViewt22, 8);
                        dataGridViewt11.Rows.Clear();
                        dataGridViewt22.Rows.Clear();
                       // dataGridViewt33.Rows.Clear();
                        MessageBox.Show("Termino");
                        break;
                }
            
            contador++;
            if (contador > 6)
            {
                MessageBox.Show("El diseño de Rol ha terminado. El reporte en excel se abrirá en unos momentos.");
                this.Close();   
            }
            else
            {
                toolStripComboBox1.SelectedIndex = contador;
            }
            
        }

        public void actualizahor(DataGridView dg, int idhor)
        {
            foreach (DataGridViewRow row in dg.Rows)
            {
                conexion.abrir();
                query = "UPDATE HOREMPLEADO SET ID_HOR=" + idhor + " WHERE BADGENUMBER=" + row.Cells["ID"].Value.ToString();
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                //MessageBox.Show(query);
                //row["Numero"].ToString();
            }
        }

        private void reestablecerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridViewt1.DataSource = null;
            dataGridViewt2.DataSource = null;
            dataGridViewt3.DataSource = null;
            dataGridViewt11.DataSource = null;
            dataGridViewt11.Rows.Clear();
            dataGridViewt11.Columns.Clear();
            dataGridViewt22.DataSource = null;
            dataGridViewt22.Rows.Clear();
            dataGridViewt22.Columns.Clear();
            dataGridViewt33.DataSource = null;
            dataGridViewt33.Columns.Clear();
            dataGridViewt33.Rows.Clear();
            selecdeselec();
            cantidadcero();
            pagina = toolStripComboBox1.Text;
            load(pagina);
            MessageBox.Show("Se reestableció correctamente el rol de: " + pagina +". No se realizaron cambios en la Base de Datos.");
        }
        public void cantidadcero()
        {
            labelCantidad1.Text = "";
            labelCantidad2.Text = "";
            labelCantidad3.Text = "";
        }
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Users\userf\source\repos\EmpManagement\EmpManagement\Excel\ROL.xlsx"));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
    
                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "First Name";
                oSheet.Cells[1, 2] = "Last Name";
                oSheet.Cells[1, 3] = "Full Name";
                oSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";
                saNames[1, 0] = "Tom";
                saNames[1, 1] = "Brown";
                saNames[2, 0] = "Sue";
                saNames[2, 1] = "Thomas";
                saNames[3, 0] = "Jane";
                saNames[3, 1] = "Jones";
                saNames[4, 0] = "Adam";
                saNames[4, 1] = "Johnson";

                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "B6").Value2 = saNames;

                //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                oRng = oSheet.get_Range("C2", "C6");
                oRng.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                oRng = oSheet.get_Range("D2", "D6");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                //Manipulate a variable number of columns for Quarterly Sales Data.
                DisplayQuarterlySales(oSheet);

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }

        }
        private void DisplayQuarterlySales(Excel._Worksheet oWS)
        {
            Excel._Workbook oWB;
            Excel.Series oSeries;
            Excel.Range oResizeRange;
            Excel._Chart oChart;
            String sMsg;
            int iNumQtrs;

            //Determine how many quarters to display data for.
            for (iNumQtrs = 4; iNumQtrs >= 2; iNumQtrs--)
            {
                sMsg = "Enter sales data for ";
                sMsg = String.Concat(sMsg, iNumQtrs);
                sMsg = String.Concat(sMsg, " quarter(s)?");

                DialogResult iRet = MessageBox.Show(sMsg, "Quarterly Sales?",
                MessageBoxButtons.YesNo);
                if (iRet == DialogResult.Yes)
                    break;
            }

            sMsg = "Displaying data for ";
            sMsg = String.Concat(sMsg, iNumQtrs);
            sMsg = String.Concat(sMsg, " quarter(s).");

            MessageBox.Show(sMsg, "Quarterly Sales");

            //Starting at E1, fill headers for the number of columns selected.
            oResizeRange = oWS.get_Range("E1", "E1").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=\"Q\" & COLUMN()-4 & CHAR(10) & \"Sales\"";

            //Change the Orientation and WrapText properties for the headers.
            oResizeRange.Orientation = 38;
            oResizeRange.WrapText = true;

            //Fill the interior color of the headers.
            oResizeRange.Interior.ColorIndex = 36;

            //Fill the columns with a formula and apply a number format.
            oResizeRange = oWS.get_Range("E2", "E6").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=RAND()*100";
            oResizeRange.NumberFormat = "$0.00";

            //Apply borders to the Sales data and headers.
            oResizeRange = oWS.get_Range("E1", "E6").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            //Add a Totals formula for the sales data and apply a border.
            oResizeRange = oWS.get_Range("E8", "E8").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=SUM(E2:E6)";
            oResizeRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle
            = Excel.XlLineStyle.xlDouble;
            oResizeRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).Weight
            = Excel.XlBorderWeight.xlThick;

            //Add a Chart for the selected data.
            oWB = (Excel._Workbook)oWS.Parent;
            oChart = (Excel._Chart)oWB.Charts.Add(Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);

            //Use the ChartWizard to create a new chart from the selected data.
            oResizeRange = oWS.get_Range("E2:E6", Missing.Value).get_Resize(
            Missing.Value, iNumQtrs);
            oChart.ChartWizard(oResizeRange, Excel.XlChartType.xl3DColumn, Missing.Value,
            Excel.XlRowCol.xlColumns, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            oSeries = (Excel.Series)oChart.SeriesCollection(1);
            oSeries.XValues = oWS.get_Range("A2", "A6");
            for (int iRet = 1; iRet <= iNumQtrs; iRet++)
            {
                oSeries = (Excel.Series)oChart.SeriesCollection(iRet);
                String seriesName;
                seriesName = "=\"Q";
                seriesName = String.Concat(seriesName, iRet);
                seriesName = String.Concat(seriesName, "\"");
                oSeries.Name = seriesName;
            }

            oChart.Location(Excel.XlChartLocation.xlLocationAsObject, oWS.Name);

            //Move the chart so as not to cover your data.
            oResizeRange = (Excel.Range)oWS.Rows.get_Item(10, Missing.Value);
            oWS.Shapes.Item("Chart 1").Top = (float)(double)oResizeRange.Top;
            oResizeRange = (Excel.Range)oWS.Columns.get_Item(2, Missing.Value);
            oWS.Shapes.Item("Chart 1").Left = (float)(double)oResizeRange.Left;
        }

        private void toolStripTextBoxID_TextChanged(object sender, EventArgs e)
        {
            toolStripTextBoxNombre.Text = "";
            if (toolStripTextBoxNombre.Text == "")
            {
                BuscarEnDatagrid(dataGridViewt1, "ID", toolStripTextBoxID.TextBox);
                BuscarEnDatagrid(dataGridViewt2, "ID", toolStripTextBoxID.TextBox);
                BuscarEnDatagrid(dataGridViewt3, "ID", toolStripTextBoxID.TextBox);
                BuscarEnDatagrid(dataGridViewt11, "ID", toolStripTextBoxID.TextBox);
                BuscarEnDatagrid(dataGridViewt22, "ID", toolStripTextBoxID.TextBox);
                BuscarEnDatagrid(dataGridViewt33, "ID", toolStripTextBoxID.TextBox);
            }

        }

        private void toolStripTextBoxNombre_TextChanged(object sender, EventArgs e)
        {
            toolStripTextBoxID.Text = "";
            if (toolStripTextBoxID.Text == ""){
                BuscarEnDatagrid(dataGridViewt1, "Nombre", toolStripTextBoxNombre.TextBox);
                BuscarEnDatagrid(dataGridViewt2, "Nombre", toolStripTextBoxNombre.TextBox);
                BuscarEnDatagrid(dataGridViewt3, "Nombre", toolStripTextBoxNombre.TextBox);
                BuscarEnDatagrid(dataGridViewt11, "Nombre", toolStripTextBoxNombre.TextBox);
                BuscarEnDatagrid(dataGridViewt22, "Nombre", toolStripTextBoxNombre.TextBox);
                BuscarEnDatagrid(dataGridViewt33, "Nombre", toolStripTextBoxNombre.TextBox);
            }
       
        }

        public void BuscarEnDatagrid(DataGridView datagrid, string nombre_columna, TextBox textbox)
        {
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                int fila = row.Index;
                string valor = Convert.ToString(row.Cells[nombre_columna].Value);

                if (valor.IndexOf(textbox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    
                    datagrid.Rows[fila].DefaultCellStyle.BackColor = Color.SeaGreen;
                    //datagrid.Rows[fila].DefaultCellStyle.BackColor = Color.SeaGreen;
                }
                    
                else
                    datagrid.Rows[fila].DefaultCellStyle.BackColor = Color.White;

                //Si esta vacio el campo de busqueda quitar el sombreado
                if (textbox.Text == string.Empty)
                    datagrid.Rows[fila].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void panel109_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}//valor.Contains(textbox.Text)
