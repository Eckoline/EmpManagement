using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Diagnostics;

namespace EmpManagement
{
    public partial class ReporteSemanal : Form
    {
        public ReporteSemanal()
        {
            InitializeComponent();
        }
        DataTable detalledias = new DataTable();
        DataTable detalledatos = new DataTable();

        private void ReporteSemanal_Load(object sender, EventArgs e)
        {
            detalledatos.Columns.Add("ID");
            detalledatos.Columns.Add("Nombre");
            detalledatos.Columns.Add("Asistencias");
            detalledatos.Columns.Add("Inasistencias");
            detalledatos.Columns.Add("Retardos");
            detalledatos.Columns.Add("Sal. Temp.");
            detalledatos.Columns.Add("Inconsistencias");
            detalledatos.Columns.Add("H.T. Laboradas");
            detalledatos.Columns.Add("H.T. Comedor");
            detalledatos.Columns.Add("H. Extras");


            detalledias.Columns.Add("ID");
            detalledias.Columns.Add("Fecha");
            detalledias.Columns.Add("Asistencia");
            detalledias.Columns.Add("Inasistencia");
            detalledias.Columns.Add("Retardo");
            detalledias.Columns.Add("Sal. Temp");
            detalledias.Columns.Add("H. Laboradas");
            detalledias.Columns.Add("H. Comedor");
            detalledias.Columns.Add("Inconsistencia");
            detalledias.Columns.Add("Detalle Incon.");

        }

        private void buttonAcept_Click(object sender, EventArgs e)
        {
         
            //dataGridViewDatos.Rows.Clear();
           // dataGridViewDetalleDias.Rows.Clear();
            TimeSpan hrs_comedor = TimeSpan.Zero;
            TimeSpan hrs_laboradas = TimeSpan.Zero;
            TimeSpan hrsdia_laboradas = TimeSpan.Zero; 
            TimeSpan hrsdia_comedor = TimeSpan.Zero; 
            float hrsextra = 0;
            float hrsem = 0;
            float hrsdia= 0;

            //calcular fechas
            TimeSpan dias;
            dias = dateTimePickerFin.Value.AddDays(1) - dateTimePickerIni.Value;
            Debug.WriteLine(dias.Days);
            Debug.WriteLine(dias);
            String[] fechas = new String[dias.Days + 1];
            for (int i = 0; i < dias.Days+1; i++)
            {
                fechas[i] = dateTimePickerIni.Value.AddDays(i).ToString("yyyy-MM-dd");
                Debug.WriteLine(fechas[i]);
            }
            //Obtención de empleados.
            DataTable dtHor = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query;
            query = "SELECT ID_HOR FROM HORARIOS WHERE IDGROUP=1";
            SqlDataAdapter adaptador0 = new SqlDataAdapter(query, conexion.con);
            adaptador0.Fill(dtHor);
            conexion.cerrar();
            int marcastop = 0;
            int idhor = 0;
            int[] semanalpys = { 1, 2, 4, 5, 7, 8, 25,26};
            int[] semanalt = {3,6,27};
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = dtHor.Rows.Count;
            toolStripProgressBar1.Value = 0;
            while (marcastop < dtHor.Rows.Count)
            {
                int asistencia = 0;
                int inasistencia = 0;
                int retardo = 0;
                int incosistencia = 0;
                int salidatemprano = 0;
                string detalle;
                hrs_comedor = TimeSpan.Zero;
                hrs_laboradas = TimeSpan.Zero;
                DateTime aux;
                hrsdia_laboradas = TimeSpan.Zero;
                hrsdia_comedor = TimeSpan.Zero;
                int conaux = 0;
                idhor = Int32.Parse(dtHor.Rows[marcastop]["ID_HOR"].ToString());
                DataTable dtEmpleado = new DataTable();
                conexion.abrir();
                query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,USERINFOCUS.DEFAULTDEPTID,HORARIOS.HOR_IN,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE HOREMPLEADO.ID_HOR=" + idhor + ";";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmpleado);
                conexion.cerrar();

                if (Array.Exists(semanalpys, x => x == idhor))
                {
                    
                    for (int j = 0; j < dtEmpleado.Rows.Count; j++)
                    {
                        DataTable dtEmpleadoHor = new DataTable();
                        conexion.abrir();
                        query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_IN,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFOCUS.BADGENUMBER= " + dtEmpleado.Rows[j]["BADGENUMBER"].ToString();
                        SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                        adaptador1.Fill(dtEmpleadoHor);
                        conexion.cerrar();
                        TimeSpan tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                        TimeSpan tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                        TimeSpan hrs_dia = tout - tin;
                        hrsem= float.Parse(dtEmpleadoHor.Rows[0]["HRS_SEMANA"].ToString());
                        hrsdia= float.Parse(dtEmpleadoHor.Rows[0]["HRS_DIA"].ToString());


                        for (int h = 0; h < fechas.Length - 1; h++)
                        {
                            //Console.WriteLine(h);
                            conexion.abrir();
                            DataTable dtMarcas = new DataTable();
                            query = "SELECT DISTINCT CHECKTIME,BADGENUMBER FROM CHECKINOUT WHERE BADGENUMBER=" + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND CHECKTIME BETWEEN '" + fechas[h] + "' AND '" + fechas[h + 1] + "' ORDER BY CHECKTIME;";
                            Console.WriteLine(query);
                            SqlDataAdapter adaptadorM = new SqlDataAdapter(query, conexion.con);
                            adaptadorM.Fill(dtMarcas);
                            conexion.cerrar();
                            TimeSpan[] checadasval = new TimeSpan[dtMarcas.Rows.Count+1];
                            TimeSpan basecomparacionmin = new TimeSpan(0, 10, 0);
                            TimeSpan dif = new TimeSpan();
                            //TimeSpan[] checadas = new TimeSpan[dtMarcas.Rows.Count];

                            if (dtMarcas.Rows.Count == 0)
                            {
                                inasistencia = inasistencia + 1;
                                incosistencia = incosistencia + 1;
                                detalle = "No se encontraron marcas";
                                detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 1, 0, 0, 0, 0, 1, detalle);

                            }
                            else
                            {
                                //Debug.WriteLine("Checadas recibidas");
                                for (int i = 0; i < dtMarcas.Rows.Count; i++)
                                {
                                    checadasval[i] = TimeSpan.Parse(DateTime.Parse(dtMarcas.Rows[i]["CHECKTIME"].ToString()).ToString("HH:mm"), System.Globalization.CultureInfo.CurrentCulture);
                                   // Debug.WriteLine(checadasval[i]);
                                
                                }
                               // Debug.WriteLine("Checadas acomodadas y validadas");
                                for (int i = 0; i < dtMarcas.Rows.Count-1; i++)
                                {
                                    dif= checadasval[i + 1] - checadasval[i];
                                    if (dif< basecomparacionmin)
                                    {
                                        //checadas[i] = checadas[i+1];
                                        checadasval[i + 1] = checadasval[i + 2];
                                    }
                                }
                                //Debug.WriteLine(dtEmpleado.Rows[j]["Badgenumber"].ToString());
                                //Debug.WriteLine(fechas[h]);
                                int contador = 0;
                                for (int i = 0; i < dtMarcas.Rows.Count; i++)
                                {
                                    if (checadasval[i] <= basecomparacionmin)
                                    {
                                        contador = contador + 1;
                                    }
                                    //Debug.WriteLine(checadasval[i]);
                                }
                                //Debug.WriteLine("Contador");
                                //Debug.WriteLine(contador);

                                //Debug.WriteLine("Checadas finales");
                                TimeSpan[] checadas = new TimeSpan[checadasval.Length - contador-1];
                                for (int i = 0; i < checadas.Length; i++)
                                {
                                    checadas[i] = checadasval[i];
                                    //Debug.WriteLine(checadas[i]);
                                }
                             
                               
                                switch (checadas.Length)
                                {
                                    case 1:
                                        asistencia = asistencia + 1;
                                        //inasistencia = 0;
                                        incosistencia = incosistencia + 1;
                                        //retardo = 0;
                                        //salidatemprano = 0;
                                        hrsdia_laboradas = TimeSpan.Zero;
                                        hrsdia_comedor = TimeSpan.Zero;
                                        detalle = "Se encontró solo una marca";
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, 0, 0, TimeSpan.Zero, TimeSpan.Zero, 1, detalle);
                                        break;

                                    case 2:
                                        int aux1 = 0, aux2 = 0;
                                        int auxporteros = 0;
                                        asistencia = asistencia + 1;
                                        //inasistencia = 0;
                                        //retardo = 0;
                                        //salidatemprano = 0;
                                        if (dtEmpleado.Rows[0]["DEFAULTDEPTID"].ToString() == "37")
                                        {
                                            auxporteros = 0;
                                            detalle = "Correcto. Porteros";
                                            hrsdia_laboradas = checadas[1] - checadas[0];
                                            hrsdia_comedor = TimeSpan.Zero;
                                            if (hrsdia_laboradas >= hrs_dia)
                                            {
                                                hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                                if (checadas[0] > tin)
                                                {
                                                    retardo = retardo + 1;
                                                    aux1 = 1;
                                                }
                                                if (checadas[1] < tout)
                                                {
                                                    salidatemprano = salidatemprano + 1;
                                                    aux2 = 1;
                                                }
                                            }
                                            else
                                            {
                                                detalle = "Se encontraron dos registros sin coherencia";
                                            }

                                        }
                                        else
                                        {
                                            incosistencia = incosistencia + 1;
                                            auxporteros = 1;
                                            detalle = "Se encontraron solo 2 marcas";
                                            hrsdia_laboradas = checadas[1] - checadas[0];
                                            hrsdia_comedor = TimeSpan.Zero;
                                            if (hrsdia_laboradas >= hrs_dia)
                                            {
                                                hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                                if (checadas[0] > tin)
                                                {
                                                    retardo = retardo + 1;
                                                    aux1 = 1;
                                                }
                                                if (checadas[1] < tout)
                                                {
                                                    salidatemprano = salidatemprano + 1;
                                                    aux2 = 1;
                                                }
                                            }
                                            else
                                            {
                                                detalle = "Se encontraron dos registros sin coherencia";
                                            }
                                        }
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, auxporteros, detalle);
                                        break;
                                    case 3:
                                        aux1 = 0;
                                        aux2 = 0;
                                        detalle = "Se encontraron solo 3 registros";
                                        incosistencia = incosistencia + 1;
                                        asistencia = asistencia + 1;
                                        hrsdia_laboradas = checadas[2] - checadas[0];
                                        hrsdia_comedor = TimeSpan.Zero;
                                        hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                        if (hrsdia_laboradas >= hrs_dia)
                                        {
                                            detalle = "No se registro salida de comedor.";
                                            if (checadas[0] > tin)
                                            {
                                                retardo = retardo + 1;
                                                aux1 = 1;
                                            }
                                            if (checadas[2] < tout)
                                            {
                                                salidatemprano = salidatemprano + 1;
                                                aux2 = 1;
                                            }
                                        }
                                        else
                                        {
                                            detalle = "No se registro salida final.";
                                            if (checadas[0] > tin)
                                            {
                                                retardo = retardo + 1;
                                                aux1 = 1;
                                            }
                                        }
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, 1, detalle);
                                        break;
                                    case 4:
                                        int aux3 = 0;
                                        aux1 = 0;
                                        aux2 = 0;
                                        asistencia = asistencia + 1;
                                        hrsdia_laboradas = checadas[3] - checadas[0];
                                        hrsdia_comedor = checadas[2] - checadas[1];
                                        detalle = "Correcto";
                                        hrs_comedor = hrs_comedor + hrsdia_comedor;
                                        hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                        if (hrsdia_laboradas < hrs_dia)
                                        {
                                            incosistencia = incosistencia + 1;
                                            aux3 = 1;
                                            detalle = "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada";
                                            if (checadas[3] < tout)
                                            {
                                                salidatemprano = salidatemprano + 1;
                                                aux2 = 1;
                                            }
                                        }
                                        if (checadas[0] > tin)
                                        {
                                            retardo = retardo + 1;
                                            aux1 = 1;
                                        }
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, aux3, detalle);
                                        break;
                                }                             
                            }
                        }

                        if ((float.Parse(hrs_laboradas.TotalHours.ToString()) + hrsdia) > hrsem)
                        {
                            hrsextra = float.Parse(hrs_laboradas.TotalHours.ToString()) - (hrsem - hrsdia);//aqui hay que quitar hrsdia
                        }
                        else
                        {
                            hrsextra = 0;
                        }
                        
                        detalledatos.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), dtEmpleado.Rows[j]["Name"].ToString(), asistencia, inasistencia, retardo, salidatemprano, incosistencia, float.Parse(hrs_laboradas.TotalHours.ToString("N3")), float.Parse(hrs_comedor.TotalHours.ToString("N3")),hrsextra);
                        retardo = 0;
                        asistencia = 0;
                        inasistencia = 0;
                        incosistencia = 0;
                        salidatemprano = 0;
                        detalle = "";
                        hrs_laboradas = TimeSpan.Zero;
                        hrs_comedor = TimeSpan.Zero;
                        hrsextra = 0;
                    }
                }
               else
                {
                    if (Array.Exists(semanalt, x => x == idhor))
                    {
                        for (int j = 0; j < dtEmpleado.Rows.Count; j++)
                        {
                            DataTable dtEmpleadoHor = new DataTable();
                            conexion.abrir();
                            query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_IN,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFOCUS.BADGENUMBER= " + dtEmpleado.Rows[j]["BADGENUMBER"].ToString();
                            SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                            adaptador1.Fill(dtEmpleadoHor);
                            conexion.cerrar();
                            TimeSpan tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                            TimeSpan tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                            TimeSpan hrs_dia = tout - tin;
                            hrsem = float.Parse(dtEmpleadoHor.Rows[0]["HRS_SEMANA"].ToString());
                            hrsdia = float.Parse(dtEmpleadoHor.Rows[0]["HRS_DIA"].ToString());
                            for (int h = 0; h <= fechas.Length - 3; h++)
                            {
                                //Console.WriteLine(h);
                                conexion.abrir();
                                DataTable dtMarcas = new DataTable();
                                query = "SELECT DISTINCT CHECKTIME,BADGENUMBER FROM CHECKINOUT WHERE BADGENUMBER=" + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND CHECKTIME BETWEEN '" + fechas[h] + "' AND '" + fechas[h + 2] + "' ORDER BY CHECKTIME;";
                               Debug.WriteLine(query);
                                SqlDataAdapter adaptadorM = new SqlDataAdapter(query, conexion.con);
                                adaptadorM.Fill(dtMarcas);
                                conexion.cerrar();
                                DateTime[] checadas = new DateTime[dtMarcas.Rows.Count];
                                TimeSpan basecomparacion = new TimeSpan(0, 5, 0);
                                TimeSpan[] dif = new TimeSpan[dtMarcas.Rows.Count+1];


                                for (int i = 0; i < dtMarcas.Rows.Count; i++)
                                {
                                    checadas[i]=DateTime.Parse(dtMarcas.Rows[i]["CHECKTIME"].ToString());  
                                }

                                for (int i = 0; i < dtMarcas.Rows.Count-2; i++)
                                {
                                    dif[i] = checadas[i + 1] - checadas[i];
                                    if (dif[i] < basecomparacion)
                                    {
                                        checadas[i] = checadas[i + 1];
                                    }
                                }

                                if (dtMarcas.Rows.Count == 0)
                                {
                                    inasistencia = inasistencia + 1;
                                    incosistencia = incosistencia + 1;
                                    detalle = "No se encontraron marcas";
                                    detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 1, 0, 0, 0, 0, 1, detalle);

                                }
                                else
                                {
                                    //conaux = 0;
                                    for (int i = 0; i < checadas.Length; i++)
                                    {
                                        Debug.WriteLine(checadas[i]);
                                    }

                                }
                            }
                            detalledatos.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), dtEmpleado.Rows[j]["Name"].ToString(), asistencia, inasistencia, retardo, salidatemprano, incosistencia, float.Parse(hrs_laboradas.TotalHours.ToString("N3")), float.Parse(hrs_comedor.TotalHours.ToString("N3")), hrsextra);
                            retardo = 0;
                            asistencia = 0;
                            inasistencia = 0;
                            incosistencia = 0;
                            salidatemprano = 0;
                            detalle = "";
                            hrs_laboradas = TimeSpan.Zero;
                            hrs_comedor = TimeSpan.Zero;
                            hrsextra = 0;
                        }
                    }
                }
                marcastop = marcastop + 1;
                toolStripProgressBar1.Value = marcastop;
            }
            dataGridViewDetalleDias.DataSource = detalledias;
            dataGridViewDatos.DataSource = detalledatos;
            //query = "SELECT USERINFO.USERID,USERINFO.BADGENUMBER,HOREMPLEADO.ID_HOR,USERINFO.DEFAULTDEPTID,HORARIOS.HOR_IN,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA FROM(USERINFO INNER JOIN HOREMPLEADO ON USERINFO.USERID = HOREMPLEADO.USERID)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFO.DEFAULTDEPTID = 4 OR USERINFO.DEFAULTDEPTID=5;"
            //int[] idempleado = new int[dtEmpleado.Rows.Count];
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DetalleMarcaciones frmMarcaciones = new DetalleMarcaciones();
           
            frmMarcaciones.dateTimePickerIni.Value = DateTime.Parse(dataGridViewDetalleDias.CurrentRow.Cells["Fecha"].Value.ToString());
            frmMarcaciones.dateTimePickerFin.Value = DateTime.Parse(dataGridViewDetalleDias.CurrentRow.Cells["Fecha"].Value.ToString());
            frmMarcaciones.textBoxID.Text = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            frmMarcaciones.Show();
        }
        private void ExportarDatos(DataGridView datalistado)
        {
            // try
            //{
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); // Instancia a la libreria de Microsoft Office
            excel.Application.Workbooks.Add(true); //Con esto añadimos una hoja en el Excel para exportar los archivos
            int IndiceColumna = 0;
            foreach (DataGridViewColumn columna in datalistado.Columns) //Aquí empezamos a leer las columnas del listado a exportar
            {
                IndiceColumna++;
                excel.Cells[1, IndiceColumna] = columna.Name;
            }
            int IndiceFila = 0;
            foreach (DataGridViewRow fila in datalistado.Rows) //Aquí leemos las filas de las columnas leídas
            {
                IndiceFila++;
                IndiceColumna = 0;
                foreach (DataGridViewColumn columna in datalistado.Columns)
                {
                    IndiceColumna++;
                    excel.Cells[IndiceFila + 1, IndiceColumna] = fila.Cells[columna.Name].Value;
                }
            }
            excel.Visible = true;
            // }
            /*catch (Exception)
            {
                MessageBox.Show("No hay Registros a Exportar.");
            }
            */
        }

        private void dataGridViewDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string busqueda = "";
            busqueda = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            DataTable df = (from item in detalledias.Rows.Cast<DataRow>()
                            let codigo = Convert.ToString(item[0] == null ? string.Empty : item[0].ToString())
                            where codigo.Equals(busqueda)
                            select item).CopyToDataTable();
            //Mostramos las coincidencias
            dataGridViewDetalleDias.DataSource = df;
            //dataGridViewDetalleDias.Rows.Clear();

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportarDatos(dataGridViewDatos);
        }

        public void acomodo()
        {

        }
    }



}
