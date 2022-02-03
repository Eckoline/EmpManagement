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

        }

        private void buttonAcept_Click(object sender, EventArgs e)
        {
            detalledias.Rows.Clear();
            detalledatos.Clear();
            TimeSpan hrs_comedor = TimeSpan.Zero;
            TimeSpan hrs_laboradas = TimeSpan.Zero;
            TimeSpan hrsdia_laboradas = TimeSpan.Zero;
            TimeSpan hrsdia_comedor = TimeSpan.Zero; ;
            TimeSpan hrs_extra = TimeSpan.Zero;

            //calcular fechas
            TimeSpan dias;
            string datein, datefin;
            datein = dateTimePickerIni.Value.ToString("MM-dd-yyyy");
            datefin = dateTimePickerFin.Value.ToString("MM-dd-yyyy");
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
            int[] semanalpys = { 17, 23 }; //id de los horarios de lunes a viernes de admin y calidad, se filtran solo estos para que el sistema lo haga solo una vez y no tambien cuando sea sabado
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = dtHor.Rows.Count;
            toolStripProgressBar1.Value = 0;

            while (marcastop < dtHor.Rows.Count) //Hara todo el proceso hasta que haya revisado todos los horarios
            {
               // int asistencia = 0;
                //int inasistencia = 0;
                //int retardo = 0;
                //int incosistencia = 0;
                //int salidatemprano = 0;
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
                        query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_INTURNO,HORARIOS.HOR_IN,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFOCUS.BADGENUMBER= " + dtEmpleado.Rows[j]["BADGENUMBER"].ToString();
                        SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                        adaptador1.Fill(dtEmpleadoHor);
                        conexion.cerrar();
                        TimeSpan tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                        TimeSpan tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                        TimeSpan tinsab = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_INTURNO"].ToString());
                        TimeSpan tinsab1 = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_IN"].ToString());
                        TimeSpan tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                        TimeSpan toutsab = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_OUT"].ToString());
                        TimeSpan hrs_dia = tout - tin1;
                        TimeSpan hrs_diasab = toutsab - tinsab1;

                        //recorre las fechas descritas en los datetimepicker
                        for (int h = 0; h <= fechas.Length - 2; h++)
                        {
                            DataTable dtvalidacion = new DataTable();
                            conexion.abrir();
                            query = "SELECT * FROM detalledias where BADGENUMBER=" + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND fecha='" + fechas[h] + "'";
                            SqlDataAdapter adaptadorvalidacion = new SqlDataAdapter(query, conexion.con);
                            adaptadorvalidacion.Fill(dtvalidacion);
                            conexion.cerrar();
                            if (dtvalidacion.Rows.Count ==0)
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
                                    int dia, mes, registros;
                                    dia = DateTime.Parse(fechas[h]).Day;
                                    mes = DateTime.Parse(fechas[h]).Month;
                                    conexion.abrir();
                                    DataTable dtDiasF = new DataTable();
                                    query = "SELECT * FROM DIASFESTIVOS WHERE diafestivo=" + dia + " AND mesfestivo=" + mes;
                                    SqlDataAdapter adaptadorDias = new SqlDataAdapter(query, conexion.con);
                                    adaptadorDias.Fill(dtDiasF);
                                    conexion.cerrar();
                                    registros = dtDiasF.Rows.Count;
                                    if (registros > 0)
                                    {
                                        detalle = dtDiasF.Rows[0]["descripcion"].ToString();
                                        conexion.abrir();
                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00:00','00:00:00',0,'" + detalle + "','','00:00:00','')";
                                        insertadetalledias(query);
                                        //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 0, 0, 0, 0, 0, 0, detalle);
                                    }
                                    else
                                    {
                                        String[,] eventosempleado = new string[10, 2];
                                        detalle = "";
                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);
                                        if (eventosempleado[0, 0] != null)
                                        {
                                            for (int i = 0; i < 10; i++)
                                            {
                                                if (eventosempleado[i, 1] == "Inasistencia")
                                                {
                                                    detalle = detalle + " " + eventosempleado[i, 0];
                                                }
                                            }
                                        
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00:00','00:00:00',0,'No se encontraron marcas.','" + fechas[h] + " " + detalle + "','00:00:00','"+detalle+"')";
                                            insertadetalledias(query);
                                        }
                                        else
                                        {
                                            if (DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek != DayOfWeek.Sunday)
                                            {
                                                detalle = "No se encontraron marcas";
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,1,0,0,'00:00','00:00',1,'" + detalle + "','','00:00:00','')";
                                                insertadetalledias(query);
                                                //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 1, 0, 0, 0, 0, 1, detalle);
                                            }
                                        }
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
                                            checadasval[i] = checadasval[i + 1];
                                        }
                                    }
                                    List<TimeSpan> lst = checadasval.ToList();
                                    List<TimeSpan> checadasfin = lst.Distinct().ToList();

                                    //Debug.WriteLine(dtEmpleado.Rows[j]["Badgenumber"].ToString());
                                    Debug.WriteLine(fechas[h]);
                                    Debug.WriteLine("Checadas finales");
                                    TimeSpan[] checadas = checadasfin.ToArray();
                                    for (int i = 0; i < checadas.Length - 1; i++)
                                    {
                                        Debug.WriteLine(checadas[i]);
                                    }       
                                    switch (checadas.Length-1)
                                    {
                                        case 1://si solo encuentra una checada
                                      
                                            hrsdia_laboradas = TimeSpan.Zero;
                                            hrsdia_comedor = TimeSpan.Zero;
                                            detalle = "Se encontró solo una marca";
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0,0,0,'00:00','00:00',1,'" + detalle + "','','00:00:00','')";
                                            insertadetalledias(query);
                                            //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, 0, 0, TimeSpan.Zero, TimeSpan.Zero, 1, detalle);
                                            break;

                                        case 2://si solo encuentra dos checadas
                                            int aux1 = 0, aux2 = 0;
                                            int auxcomedor = 0;
                                            int inc2 = 0;
                                            string evento = "";
                                            TimeSpan auxtimetout = TimeSpan.Zero;
                                            TimeSpan auxtimetin = TimeSpan.Zero;
                                            TimeSpan auxtimehrsdia = TimeSpan.Zero;

                                            if (DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)
                                            {
                                                auxtimetin = tinsab;
                                                auxtimetout = toutsab;
                                                auxtimehrsdia = hrs_diasab;
                                                detalle = "Día Sabado. No se encontraron marcas de comedor.";
                                                inc2 = 0;

                                            }
                                            else
                                            {
                                                auxtimetin = tin;
                                                auxtimetout = tout;
                                                auxtimehrsdia = hrs_dia;
                                                detalle = "Se encontraron solo 2 marcas.";
                                                inc2 = 1;
                                            }
                                   
                                            hrsdia_laboradas = checadas[1] - checadas[0];
                                            hrsdia_comedor = TimeSpan.Zero;
                                            if (hrsdia_laboradas >= auxtimehrsdia)
                                            {
                                                hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                                if (checadas[0] > auxtimetin)
                                                {
                                                    inc2 = 1;
                                                    aux1 = 1;
                                                    String[,] eventosempleado = new string[10, 2];
                                               
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);

                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        for (int i = 0; i < 10; i++)
                                                        {
                                                            if (eventosempleado[i, 1] == "Retardo")
                                                            {
                                                                evento = evento + " " + eventosempleado[i, 0];
                                                                aux1 = 0;
                                                                inc2 = 0;
                                                        
                                                            }
                                                        }
                                                    }
                                                }
                                                if (checadas[1] < auxtimetout)
                                                {
                                                    aux2 = 1;
                                                    inc2 = 1;
                                                    
                                                 
                                                    String[,] eventosempleado = new string[10, 2];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);
                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        for (int i = 0; i < 10; i++)
                                                        {
                                                            if (eventosempleado[i, 1] == "Salida Temprano")
                                                            {
                                                                evento = evento + " " + eventosempleado[i, 0];
                                                                aux2 = 0;
                                                                inc2 = 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                hrs_extra = TimeSpan.Zero;
                                                detalle = "No se registro comedor.";
                                            }
                                            else
                                            {
                                                hrs_extra = TimeSpan.Zero;
                                                detalle = "Se encontraron dos registros sin coherencia.";
                                            }
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + inc2 + ",'" + detalle + "','" + fechas[h] + " "+evento +"','" + hrs_extra + "','"+ evento+"')";
                                            insertadetalledias(query);
                                            //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, auxcomedor, detalle);
                                            break;
                                        case 3://si solo encuentra 3 checadas
                                            aux1 = 0;
                                            aux2 = 0;
                                            auxcomedor = 0;
                                            int inc3 = 0;
                                            auxtimetout = TimeSpan.Zero;
                                            auxtimetin = TimeSpan.Zero;
                                            auxtimehrsdia = TimeSpan.Zero;
                                            evento = "";
                                       

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

                                         
                                            //inasistencia = 0;
                                            //retardo = 0;
                                            //salidatemprano = 0;
                                            hrsdia_laboradas = checadas[2] - checadas[0];
                                            hrsdia_comedor = TimeSpan.Zero;
                                     

                                            hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                            if (hrsdia_laboradas >= auxtimehrsdia)
                                            {
                                                if (checadas[0] > auxtimetin)
                                                {
                                                    aux1 = 1;
                                                    inc3 = 1;
                                                    String[,] eventosempleado = new string[10, 2];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);
                                                    evento = "";
                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        for (int i = 0; i < 10; i++)
                                                        {
                                                            if (eventosempleado[i, 1] == "Retardo")
                                                            {
                                                                evento = evento + " " + eventosempleado[i, 0];
                                                                aux1 = 0;
                                                                inc3 = 0;
                                                            }
                                                        }

                                                    }
                                                }
                                                if (checadas[2] < auxtimetout)
                                                {
                                                    aux2 = 1;
                                                    inc3 = 1;
                                                    String[,] eventosempleado = new string[10, 2];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);
                                                    evento = "";
                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        for (int i = 0; i < 10; i++)
                                                        {
                                                            if (eventosempleado[i, 1] == "Retardo")
                                                            {
                                                                evento = evento + " " + eventosempleado[i, 0];
                                                                aux1 = 0;
                                                                inc3 = 0;
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {

                                                /* pendiente de desarrollo, la idea es observar si la 3er checada es
                                                 salida final o salida de comedor, si es salida final se puede deducir 
                                                que no se hizo la checada de salida de comedor, pero se
                                                puede obtener retardo, salida temprano*/

                                            }
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "',"+inc3+",'" + detalle + "','" + fechas[h] + " " + evento +"','" + hrs_extra + "','" + evento + "')";
                                            insertadetalledias(query);
                                            //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, 1, detalle);

                                            break;
                                        case 4://si encuentra las 4 checadas
                                            int aux3 = 0;
                                            auxtimetout = TimeSpan.Zero;
                                            auxtimetin = TimeSpan.Zero;
                                            auxtimehrsdia = TimeSpan.Zero;
                                            evento = "";

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
                                                aux3 = 1;
                                                detalle = "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada";

                                            }
                                            if (checadas[3] < auxtimetout)
                                            {
                                        
                                                aux2 = 1;
                                                String[,] eventosempleado = new string[10, 2];
                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);
                                                aux3 = 1;
                                                if (eventosempleado[0, 0] != null)
                                                {
                                                    for (int i = 0; i < 10; i++)
                                                    {
                                                        if (eventosempleado[i, 1] == "Salida Temprano")
                                                        {
                                                            evento = evento + " " + eventosempleado[i, 0];
                                                            aux2 = 0;
                                                            aux3 = 0;
                                                        }
                                                    }

                                                }

                                            }
                                            if (checadas[0] > auxtimetin)
                                            {
                                                aux1 = 1;
                                                aux3 = 1;
                                
                                                String[,] eventosempleado = new string[10, 2];
                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);

                                                if (eventosempleado[0, 0] != null)
                                                {
                                                    for (int i = 0; i < 10; i++)
                                                    {
                                                        if (eventosempleado[i, 1] == "Retardo")
                                                        {
                                                            evento = evento + " " + eventosempleado[i, 0];
                                                            aux1 = 0;
                                                            aux3 = 0;
                                                        }
                                                    }
                                                }
                                            }
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','" + fechas[h] + " " + evento + "','" + hrs_extra + "','" + evento + "')";
                                            insertadetalledias(query);
                                            break;

                                        default:
                                            evento = "";
                                            if ((checadas.Length-1) > 4)
                                            {
                                                aux3 = 0;
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
                                     
                                                //inasistencia = 0;
                                                //retardo = 0;
                                                //salidatemprano = 0;

                                                hrsdia_laboradas = checadas[checadas.Length - 2] - checadas[0];
                                                hrsdia_comedor = checadas[checadas.Length - 3] - checadas[1];
                                                //incosistencia = 0;
                                                detalle = "Se encontrarón más de 4 marcaciones. Presione el botón Marcaciones para conocer que sucede.";
                                                hrs_comedor = hrs_comedor + hrsdia_comedor;
                                                hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                if (checadas[checadas.Length - 2] < auxtimetout)
                                                {
                                                    aux3 = 1;
                                                    aux2 = 1;

                                                    String[,] eventosempleado = new string[10, 2];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);

                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        for (int i = 0; i < 10; i++)
                                                        {
                                                            if (eventosempleado[i, 1] == "Salida Temprano")
                                                            {
                                                                evento = evento + " " + eventosempleado[i, 0];
                                                                aux2 = 0;
                                                                aux3 = 0;
                                                            }
                                                        }
                                                    }

                                                }
                                                if (checadas[0] > auxtimetin)
                                                {
                                                    aux3 = 1;
                                                    aux1 = 1;

                                                    String[,] eventosempleado = new string[10, 2];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h]);

                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        for (int i = 0; i < 10; i++)
                                                        {
                                                            if (eventosempleado[i, 1] == "Retardo")
                                                            {
                                                                evento = evento + " " + eventosempleado[i, 0];
                                                                aux1 = 0;
                                                                aux3 = 0;
                                                            }
                                                        }
                                                    }

                                                }
                                                //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, aux3, detalle);
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','" + fechas[h] + " " + evento +"','" + hrs_extra + "','" + evento + "')";
                                                insertadetalledias(query);
                                            }

                                            break;
                                    }
                                }
                            }
                        }
                        detalle = "";
                        hrs_laboradas = TimeSpan.Zero;
                        hrs_comedor = TimeSpan.Zero;

                    }

                }

                marcastop = marcastop + 1;
                toolStripProgressBar1.Value = marcastop;

            }
         
            sumaincon();
            actualizain();
     
        }

        public void pintagrid()
        {
            foreach (DataGridViewRow rowp in dataGridViewDatos.Rows)
            {
                if (Int32.Parse(rowp.Cells["Inconsistencias"].Value.ToString()) > 0)
                {
                    rowp.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        public void sumaincon()
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleado = new DataTable();
            conexion.abrir();
            string query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HORARIOS.Descripcion FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE HORARIOS.IDGROUP=2 AND HORARIOS.ID_HOR NOT IN(18,24) ORDER BY HORARIOS.Descripcion";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtempleado);
            conexion.cerrar();

            DataTable detalledatos = new DataTable();
            detalledatos.Columns.Clear();
            detalledatos.Rows.Clear();
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
            detalledatos.Columns.Add("Turno");
            detalledatos.Columns.Add("Comentarios");
            string asisbd, inasisbd, retbd, saltempbd, turnobd, incosisbd;
            int hrslabbd, hrscombd, hrsextbd = 0;
            foreach (DataRow row in dtempleado.Rows)
            {
                DataTable dtempleadoSUM = new DataTable();
                conexion.abrir();
                query = "SELECT SUM(asistencia) as ASISTENCIAS,SUM(inasistencia) as INASISTENCIAS, SUM(retardo) as RETARDOS, SUM(saltemp) as 'SAL.TEMP.',SUM(inconsistencia) as INCONSISTENCIAS,SUM(DATEPART(SECOND, [hlab]) + 60 * DATEPART(MINUTE, [hlab]) + 3600 * DATEPART(HOUR, [hlab] )) as 'H.T. LABORADAS',SUM(DATEPART(SECOND, [hcomedor]) + 60 * DATEPART(MINUTE, [hcomedor]) + 3600 * DATEPART(HOUR, [hcomedor] )) as 'H.T. COMEDOR' FROM detalledias where badgenumber=" + row["BADGENUMBER"].ToString() + " AND fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "'";
                SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
                adaptadorsum.Fill(dtempleadoSUM);
                conexion.cerrar();
                asisbd = dtempleadoSUM.Rows[0]["ASISTENCIAS"].ToString();
                inasisbd = dtempleadoSUM.Rows[0]["INASISTENCIAS"].ToString();
                retbd = dtempleadoSUM.Rows[0]["RETARDOS"].ToString();
                saltempbd = dtempleadoSUM.Rows[0]["SAL.TEMP."].ToString();
                //incosisbd = dtempleadoSUM.Rows[0]["INCONSISTENCIAS"].ToString();
                if (dtempleadoSUM.Rows[0]["H.T. LABORADAS"].ToString() == "")
                    hrslabbd = 0;
                else
                    hrslabbd = Int32.Parse(dtempleadoSUM.Rows[0]["H.T. LABORADAS"].ToString());

                if (dtempleadoSUM.Rows[0]["H.T. COMEDOR"].ToString() == "")
                    hrscombd = 0;
                else
                    hrscombd = Int32.Parse(dtempleadoSUM.Rows[0]["H.T. COMEDOR"].ToString());

                if (dtempleadoSUM.Rows[0]["INCONSISTENCIAS"].ToString() == "")
                    incosisbd = "0";
                else
                    incosisbd = dtempleadoSUM.Rows[0]["INCONSISTENCIAS"].ToString();

                turnobd = row["Descripcion"].ToString();
                //Debug.WriteLine(row["BADGENUMBER"].ToString());
                //Debug.WriteLine(asisbd);
                //Debug.WriteLine(hrslabbd);
                DataTable dtcom = new DataTable();
                string cadenacom = "";
                
                conexion.abrir();
                query = "SELECT comentarios from detalledias where badgenumber=" + row["BADGENUMBER"].ToString() + " AND fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "'";
                adaptadorsum = new SqlDataAdapter(query, conexion.con);
                adaptadorsum.Fill(dtcom);
                conexion.cerrar();
                foreach (DataRow rowcom in dtcom.Rows)
                {
                    if (rowcom["comentarios"].ToString() != "")
                    {
                        cadenacom = rowcom["comentarios"].ToString()+ ". "+cadenacom;

                    }    
                    Debug.WriteLine(cadenacom);
                }
                detalledatos.Rows.Add(row["BADGENUMBER"].ToString(), row["NAME"].ToString(), asisbd, inasisbd, retbd, saltempbd, incosisbd, CalcularTiempo(hrslabbd), CalcularTiempo(hrscombd), hrsextbd, turnobd,cadenacom);
            }
            dataGridViewDatos.DataSource = detalledatos;
        }
        public void actualizain()
        {
            conexionbd conexion = new conexionbd();
            DataTable detallediasbd = new DataTable();
            conexion.abrir();
            string query = "SELECT * FROM detalledias WHERE fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "'";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(detallediasbd);
            conexion.cerrar();
            dataGridViewDetalleDias.DataSource = detallediasbd;
        }
        private void buttonMarcaciones_Click(object sender, EventArgs e)
        {
            DetalleMarcaciones frmMarcaciones = new DetalleMarcaciones();
            frmMarcaciones.dateTimePickerIni.Value = DateTime.Parse(dataGridViewDetalleDias.CurrentRow.Cells["Fecha"].Value.ToString());
            frmMarcaciones.dateTimePickerFin.Value = DateTime.Parse(dataGridViewDetalleDias.CurrentRow.Cells["Fecha"].Value.ToString());
            frmMarcaciones.textBoxID.Text = dataGridViewDatos.CurrentRow.Cells[0].Value.ToString();
            frmMarcaciones.Show();
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

        public void insertadetalledias(string consulta)
        {
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            SqlCommand comando = new SqlCommand(consulta, conexion.con);
            comando.ExecuteNonQuery();
            conexion.cerrar();
        }

        private void buttonJustificar_Click(object sender, EventArgs e)
        {
            JustificaIncon frmjust = new JustificaIncon();
            frmjust.FormClosed += update_formCloed;
            frmjust.labelFec.Text = DateTime.Parse(dataGridViewDetalleDias.CurrentRow.Cells["Fecha"].Value.ToString()).ToString("yyyy-MM-dd"); ;
            frmjust.labelIDEmp.Text = dataGridViewDetalleDias.CurrentRow.Cells["Badgenumber"].Value.ToString();
            frmjust.labelNombre.Text = dataGridViewDatos.CurrentRow.Cells["Nombre"].Value.ToString();
            int incon = Int32.Parse(dataGridViewDetalleDias.CurrentCell.ColumnIndex.ToString());
            int num = Int32.Parse(dataGridViewDatos.CurrentCell.Value.ToString());
            string inconsistenciacom = "";

            if ((incon >= 3) && (incon <= 5))
            {
                if (num > 0)
                {
                    switch (incon)
                    {
                        case 3:
                            inconsistenciacom = "Inasistencia";
                            break;
                        case 4:
                            inconsistenciacom = "Retardo";
                            break;
                        case 5:
                            inconsistenciacom = "Salida Temprano";
                            break;
                    }
                    frmjust.labelTipo.Text = inconsistenciacom;
                    frmjust.Show();
                }
                else
                {
                    MessageBox.Show("No hay inconsistencia a justificar.");
                }
            }
            else
            {
                MessageBox.Show("De clic en una celda de inconsistencia.");
            }

        }
        void update_formCloed(object sender, FormClosedEventArgs e)
        {
            Form frm = sender as Form;
            if (frm.DialogResult == DialogResult.OK)
            {
                sumaincon();
                actualizain();
                // pintagrid();
            }
        }

        private String CalcularTiempo(Int32 tsegundos)
        {
            Int32 horas = (tsegundos / 3600);
            Int32 minutos = ((tsegundos - horas * 3600) / 60);
            Int32 segundos = tsegundos - (horas * 3600 + minutos * 60);
            return horas.ToString() + ":" + minutos.ToString() + ":" + segundos.ToString();
        }

        private void dataGridViewDatos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            conexionbd conexion = new conexionbd();
            DataTable detallediasbd = new DataTable();
            conexion.abrir();
            string query = "SELECT * FROM detalledias where badgenumber=" + dataGridViewDatos.CurrentRow.Cells[0].Value.ToString() + " AND fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "' ORDER BY Fecha";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(detallediasbd);
            conexion.cerrar();
            dataGridViewDetalleDias.DataSource = detallediasbd;
        }

        public string[,] GetEventos(string badgenumber, string fecin, string fecfin, string fecac)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleadoEventos = new DataTable();
            DateTime fecini, fecfinal;
            conexion.abrir();
            string query = "SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN=EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER=" + badgenumber + " AND fecin>='" + fecin + "' and fecfin<='" + fecfin + "'";
            //Debug.WriteLine(query);
            string[,] eventos = new string[10, 2];
            SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
            adaptadorsum.Fill(dtempleadoEventos);
            conexion.cerrar();
            int j = 0;

            foreach (DataRow row in dtempleadoEventos.Rows)
            {
                fecini = DateTime.Parse(row["fecin"].ToString());
                fecfinal = DateTime.Parse(row["fecfin"].ToString());
                //Debug.WriteLine(fecini);
                //Debug.WriteLine(fecfinal);
                TimeSpan dias;
                dias = fecfinal - fecini;

                //Debug.WriteLine("Días:" + dias.Days);
                String[] fechas = new String[dias.Days + 1];
                //Debug.WriteLine("Longitud array:" + fechas.Length);
                for (int i = 0; i < fechas.Length; i++)
                {
                    fechas[i] = fecini.AddDays(i).ToString("yyyy-MM-dd");
                    //Debug.WriteLine("Fecha:" + fechas[i]);
                    if (fechas[i] == fecac)
                    {
                        eventos[j, 0] = dtempleadoEventos.Rows[j]["Descripcion"].ToString();
                        eventos[j, 1] = dtempleadoEventos.Rows[j]["Grupo"].ToString();

                        //Debug.WriteLine(eventos[j]);
                    }
                }
                j++;
            }

            return eventos;
        }

        private void dataGridViewDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridViewDatos.Columns[e.ColumnIndex].Name == "Inconsistencias")
            {
                if (e.Value != null)
                {
                    if (e.Value.GetType() != typeof(System.DBNull))
                    {
                        //Stock menor a 20
                        if (Convert.ToInt32(e.Value) > 0)
                        {
                            e.CellStyle.BackColor = Color.Red;
                        }

                    }
                }
            }
        }
    }
}
