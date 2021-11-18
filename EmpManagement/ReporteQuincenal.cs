using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace EmpManagement
{
    public partial class ReporteQuincenal : Form
    {
        public ReporteQuincenal()
        {
            InitializeComponent();
        }
        DataTable detalledias = new DataTable();
        DataTable detalledatos = new DataTable();
        private void ReporteQuincenal_Load(object sender, EventArgs e)
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
            detalledias.Rows.Clear();
            TimeSpan hrs_comedor = TimeSpan.Zero;
            TimeSpan hrs_laboradas = TimeSpan.Zero;
            TimeSpan hrsdia_laboradas = TimeSpan.Zero;
            TimeSpan hrsdia_comedor = TimeSpan.Zero; ;

            //calcular fechas
            TimeSpan dias;
            dias = dateTimePickerFin.Value.AddDays(1) - dateTimePickerIni.Value;
            String[] fechas = new String[dias.Days + 1];
            for (int i = 0; i < dias.Days + 1; i++)
            {
                fechas[i] = dateTimePickerIni.Value.AddDays(i).ToString("yyyy-MM-dd");
                //MessageBox.Show(fechas[i]);
            }


            //Obtención de empleados.
            DataTable dtHor = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query;
            query = "SELECT ID_HOR FROM HORARIOS WHERE IDGROUP=2"; //Obtengo horarios del dep administrativos y calidad
            SqlDataAdapter adaptador0 = new SqlDataAdapter(query, conexion.con);
            adaptador0.Fill(dtHor);
            conexion.cerrar();
            int marcastop = 0;
            int idhor = 0;
            int[] semanalpys = { 17,23}; //id de los horarios de lunes a viernes de admin y calidad, se filtran solo estos para que el sistema lo haga solo una vez y no tambien cuando sea sabado
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = dtHor.Rows.Count;
            toolStripProgressBar1.Value = 0;

            while (marcastop < dtHor.Rows.Count) //Hara todo el proceso hasta que haya revisado todos los horarios
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
                query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_IN,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,USERINFOCUS.DEFAULTDEPTID FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE HOREMPLEADO.ID_HOR=" + idhor + ";";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(dtEmpleado);
                conexion.cerrar();
                if (Array.Exists(semanalpys, x => x == idhor)) //pregunta si los id del arreglo semanalpys existe dentro de idhor (semanal deberia cambiarse por quincenal)
                { 
                    for (int j = 0; j < dtEmpleado.Rows.Count; j++) //recorre todos los empleados con ese id de horario
                    {   //trae los horarios del empleado en turno
                        DataTable dtEmpleadoHor = new DataTable();
                        conexion.abrir();
                        query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_INTURNO,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFOCUS.BADGENUMBER= " + dtEmpleado.Rows[j]["BADGENUMBER"].ToString();
                        SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                        adaptador1.Fill(dtEmpleadoHor);
                        conexion.cerrar();
                        TimeSpan tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                        TimeSpan tinsab = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_INTURNO"].ToString());
                        TimeSpan tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                        TimeSpan toutsab = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_OUT"].ToString());
                        TimeSpan hrs_dia = tout - tin;
                        TimeSpan hrs_diasab = toutsab - tinsab;

                        //recorre las fechas descritas en los datetimepicker
                        for (int h = 0; h <= fechas.Length - 2; h++)
                        {
                            //obtiene marcas
                            conexion.abrir();
                            DataTable dtMarcas = new DataTable();
                            query = "SELECT DISTINCT CHECKTIME,BADGENUMBER FROM CHECKINOUT WHERE BADGENUMBER=" + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND CHECKTIME BETWEEN '" + fechas[h] + "' AND '" + fechas[h + 1] + "' ORDER BY CHECKTIME;";
                            Console.WriteLine(query);
                            SqlDataAdapter adaptadorM = new SqlDataAdapter(query, conexion.con);
                            adaptadorM.Fill(dtMarcas);
                            conexion.cerrar();
                            TimeSpan[] checadasval = new TimeSpan[dtMarcas.Rows.Count + 1];
                            TimeSpan basecomparacionmin = new TimeSpan(0, 10, 0);
                            TimeSpan dif = new TimeSpan();
                            //TimeSpan[] checadas = new TimeSpan[dtMarcas.Rows.Count];

                            //si las marcas son igual a 0 quiere decir que no registro ni una sola entrada o que es dia sabado
                            if (dtMarcas.Rows.Count == 0)
                            {
                                if (DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek != DayOfWeek.Sunday)
                                {
                                    inasistencia = inasistencia + 1;
                                    incosistencia = incosistencia + 1;
                                    detalle = "No se encontraron marcas";
                                    detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 1, 0, 0, 0, 0, 1, detalle);

                                }
                            }
                            else //si si encuentra, hará el proceso
                            {//proceso de registro de checadas en el arreglo.
                             //Debug.WriteLine("Checadas recibidas");
                                for (int i = 0; i < dtMarcas.Rows.Count; i++)
                                {
                                    checadasval[i] = TimeSpan.Parse(DateTime.Parse(dtMarcas.Rows[i]["CHECKTIME"].ToString()).ToString("HH:mm"), System.Globalization.CultureInfo.CurrentCulture);
                                    // Debug.WriteLine(checadasval[i]);

                                }
                                // Debug.WriteLine("Checadas acomodadas y validadas");
                                for (int i = 0; i < dtMarcas.Rows.Count - 1; i++)
                                {
                                    dif = checadasval[i + 1] - checadasval[i];
                                    if (dif < basecomparacionmin)
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
                                TimeSpan[] checadas = new TimeSpan[checadasval.Length - contador - 1];
                                for (int i = 0; i < checadas.Length; i++)
                                {
                                    checadas[i] = checadasval[i];
                                    //Debug.WriteLine(checadas[i]);
                                }
                                switch (checadas.Length)
                                {
                                    case 1://si solo encuentra una checada
                                        asistencia = asistencia + 1;
                                        incosistencia = incosistencia + 1;
                                        hrsdia_laboradas = TimeSpan.Zero;
                                        hrsdia_comedor = TimeSpan.Zero;
                                        detalle = "Se encontró solo una marca";
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, 0, 0, TimeSpan.Zero, TimeSpan.Zero, 1, detalle);
                                        break;

                                    case 2://si solo encuentra dos checadas
                                        int aux1 = 0, aux2 = 0;
                                        int auxcomedor = 0;
                                        TimeSpan auxtimetout = TimeSpan.Zero;
                                        TimeSpan auxtimetin = TimeSpan.Zero;
                                        TimeSpan auxtimehrsdia = TimeSpan.Zero;

                                        if (DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            auxtimetin = tinsab;
                                            auxtimetout = toutsab;
                                            auxtimehrsdia = hrs_diasab;
                                            detalle = "Día Sabado. No se encontraron marcas de comedor";
                                            auxcomedor = 0;

                                        }
                                        else
                                        {
                                            auxtimetin = tin;
                                            auxtimetout = tout;
                                            auxtimehrsdia = hrs_dia;
                                            detalle = "Se encontraron solo 2 marcas";
                                            incosistencia = incosistencia + 1;
                                            auxcomedor = 1;
                                        }
                                        asistencia = asistencia + 1;
                                        hrsdia_laboradas = checadas[1] - checadas[checadas.Length-1];
                                        hrsdia_comedor = TimeSpan.Zero;
                                        if (hrsdia_laboradas >= auxtimehrsdia)
                                        {
                                            hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                            if (checadas[0] > auxtimetin)
                                            {
                                                retardo = retardo + 1;
                                                aux1 = 1;
                                            }
                                            if (checadas[checadas.Length-1] < auxtimetout)
                                            {
                                                salidatemprano = salidatemprano + 1;
                                                aux2 = 1;
                                            }
                                        }
                                        else
                                        {
                                            detalle = "Se encontraron dos registros sin coherencia";
                                        }
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, auxcomedor, detalle);
                                        break;
                                    case 3://si solo encuentra 3 checadas
                                        aux1 = 0;
                                        aux2 = 0;
                                        auxcomedor = 0;
                                        auxtimetout = TimeSpan.Zero;
                                        auxtimetin = TimeSpan.Zero;
                                        auxtimehrsdia = TimeSpan.Zero;

                                        if (DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            auxtimetin = tinsab;
                                            auxtimetout = toutsab;
                                            auxtimehrsdia = hrs_diasab;
                                            detalle = "Día sabado. Se encontraron solo 3 marcas";

                                        }
                                        else
                                        {
                                            auxtimetin = tin;
                                            auxtimetout = tout;
                                            auxtimehrsdia = hrs_dia;
                                            detalle = "Se encontraron solo 3 registros. No se registro entrada o Salida Comedor";
                                        }

                                        asistencia = asistencia + 1;
                                        //inasistencia = 0;
                                        //retardo = 0;
                                        //salidatemprano = 0;
                                        hrsdia_laboradas = checadas[2] - checadas[0];
                                        hrsdia_comedor = TimeSpan.Zero;
                                        incosistencia = incosistencia + 1; 

                                        hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                        if (hrsdia_laboradas >= auxtimehrsdia)
                                        {
                                            if (checadas[0] > auxtimetin)
                                            {
                                                retardo = retardo + 1;
                                                aux1 = 1;
                                            }
                                            if (checadas[checadas.Length-1] < auxtimetout)
                                            {
                                                salidatemprano = salidatemprano + 1;
                                                aux2 = 1;
                                            }
                                        }
                                        else
                                        {

                                            /* pendiente de desarrollo, la idea es observar si la 3er checada es
                                             salida final o salida de comedor, si es salida final se puede deducir 
                                            que no se hizo la checada de salida de comedor, pero se
                                            puede obtener retardo, salida temprano*/

                                        }
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, 1, detalle);
                                        break;
                                    case 4://si encuentra las 4 checadas
                                        int aux3 = 0;
                                        auxtimetout = TimeSpan.Zero;
                                        auxtimetin = TimeSpan.Zero;
                                        auxtimehrsdia = TimeSpan.Zero;

                                        if (DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            auxtimetin = tinsab;
                                            auxtimetout = toutsab;
                                            auxtimehrsdia = hrs_diasab;

                                        }
                                        else
                                        {
                                            auxtimetin = tin;
                                            auxtimetout = tout;
                                            auxtimehrsdia = hrs_dia;
                                        }

                                        aux1 = 0;
                                        aux2 = 0;
                                        asistencia = asistencia + 1;
                                        //inasistencia = 0;
                                        //retardo = 0;
                                        //salidatemprano = 0;
                                        
                                        hrsdia_laboradas = checadas[3] - checadas[0];
                                        hrsdia_comedor = checadas[2] - checadas[1];
                                        //incosistencia = 0;
                                        detalle = "Correcto";
                                        hrs_comedor = hrs_comedor + hrsdia_comedor;
                                        hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                        if (hrsdia_laboradas < auxtimehrsdia)
                                        {
                                            incosistencia = incosistencia + 1;
                                            aux3 = 1;
                                            detalle = "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada";
                                            if (checadas[3] < auxtimetout)
                                            {
                                                salidatemprano = salidatemprano + 1;
                                                aux2 = 1;
                                            }
                                        }
                                        if (checadas[0] > auxtimetin)
                                        {
                                            retardo = retardo + 1;
                                            aux1 = 1;
                                        }
                                        detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, aux3, detalle);
                                        break;
                                                                   }
                            }
                        }
                        detalledatos.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), dtEmpleado.Rows[j]["Name"].ToString(), asistencia, inasistencia, retardo, salidatemprano, incosistencia, float.Parse(hrs_laboradas.TotalHours.ToString("N3")), float.Parse(hrs_comedor.TotalHours.ToString("N3")));
                        //dataGridViewDatos.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), asistencia, inasistencia, retardo, salidatemprano, incosistencia, hrs_laboradas, hrs_comedor);
                        //Console.WriteLine("Empleado: " + dtEmpleado.Rows[j]["Badgenumber"].ToString());
                        //Console.WriteLine("Asistencias: " + asistencia);
                        //Console.WriteLine("Inasistencias: " + inasistencia);
                        //Console.WriteLine("Retardos: " + retardo);
                        //Console.WriteLine("Salidas Temprano: " + salidatemprano);
                        //Console.WriteLine("Inconsistencias: " + incosistencia);
                        //Console.WriteLine("Horas totales Laboradas: " + hrs_laboradas);
                        //Console.WriteLine("Horas totales Comedor: " + hrs_comedor);
                        retardo = 0;
                        asistencia = 0;
                        inasistencia = 0;
                        incosistencia = 0;
                        salidatemprano = 0;
                        detalle = "";
                        hrs_laboradas = TimeSpan.Zero;
                        hrs_comedor = TimeSpan.Zero;
                        
                    }

                }
                
                marcastop = marcastop + 1;
                toolStripProgressBar1.Value = marcastop;

            }
            dataGridViewDetalleDias.DataSource = detalledias;
            dataGridViewDatos.DataSource = detalledatos;
        }

        private void buttonMarcaciones_Click(object sender, EventArgs e)
        {
            DetalleMarcaciones frmMarcaciones = new DetalleMarcaciones();
            frmMarcaciones.dateTimePickerIni.Value = dateTimePickerIni.Value;
            frmMarcaciones.dateTimePickerFin.Value = dateTimePickerFin.Value.AddDays(1);
            frmMarcaciones.textBoxID.Text = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            frmMarcaciones.Show();
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

        private void ExportarDatos(DataGridView datalistado)
        {
             try
            {
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
            }
            catch (Exception)
            {
                MessageBox.Show("No hay Registros a Exportar.");
            }
            
        }
    }
}
