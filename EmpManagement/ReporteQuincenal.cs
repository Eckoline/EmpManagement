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
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

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
            dataGridViewDatos.DataSource = null;
            dataGridViewDetalleDias.DataSource = null;
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
            reporteExcelToolStripMenuItem.Visible = false;
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
            DataTable dthorpys = new DataTable();
            DataTable dthorsab = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query;
            query = "SELECT ID_HOR FROM HORARIOS WHERE IDGROUP=2"; //Obtengo horarios del dep administrativos y calidad
            SqlDataAdapter adaptador0 = new SqlDataAdapter(query, conexion.con);
            adaptador0.Fill(dtHor);
            conexion.cerrar();
            int marcastop = 0;
            int idhor = 0;
            //int[] semanalpys = { 17, 23, 28, 29, 30,31,32 }; //id de los horarios de lunes a viernes de admin y calidad, se filtran solo estos para que el sistema lo haga solo una vez y no tambien cuando sea sabado

            conexion.abrir();
            query = "SELECT ID_HOR FROM HORARIOS WHERE idgroup = 2 and tipohor = 1;";
            adaptador0 = new SqlDataAdapter(query, conexion.con);
            adaptador0.Fill(dthorpys);
            conexion.cerrar();
            Debug.WriteLine("PYS");
            int[] semanalpys = new int[dthorpys.Rows.Count];

            for (int i = 0; i < dthorpys.Rows.Count; i++)
            {
                semanalpys[i] = Int32.Parse(dthorpys.Rows[i][0].ToString());
                Debug.WriteLine(semanalpys[i]);
            }

            conexion.abrir();
            query = "SELECT ID_HOR FROM HORARIOS WHERE idgroup = 2 and tipohor = 2 and Descripcion LIKE '%SABADO%';";
            adaptador0 = new SqlDataAdapter(query, conexion.con);
            adaptador0.Fill(dthorsab);
            conexion.cerrar();

            // int[] semanapysab = { 9, 11, 13, 15, 19 };

            int[] semanapysab = new int[dthorsab.Rows.Count];

            for (int i = 0; i < dthorsab.Rows.Count; i++)
            {
                semanapysab[i] = Int32.Parse(dthorsab.Rows[i]["ID_HOR"].ToString()) - 1;
                Debug.WriteLine(semanapysab[i]);
            }

            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = dtHor.Rows.Count;
            toolStripProgressBar1.Value = 0;
            while (marcastop < dtHor.Rows.Count)
            {
                //int asistencia = 0;
                //int inasistencia = 0;
                //int retardo = 0;
                //int incosistencia = 0;
                //int salidatemprano = 0;
                string detalle = "";
                string evento = "";
                string observaciones = "";
                string tipoeven = "";
                string banderacambio = "";
                //hrs_comedor = TimeSpan.Zero;
                //hrs_laboradas = TimeSpan.Zero;
                DateTime aux;
                hrsdia_laboradas = TimeSpan.Zero;
                hrsdia_comedor = TimeSpan.Zero;
                int conaux = 0;
                idhor = Int32.Parse(dtHor.Rows[marcastop]["ID_HOR"].ToString());
                DataTable dtEmpleado = new DataTable();
                conexion.abrir();
                query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,USERINFOCUS.DEFAULTDEPTID,HORARIOS.HOR_IN,HORARIOS.HOR_INTURNO,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE HOREMPLEADO.ID_HOR=" + idhor + " AND USERINFOCUS.DEFAULTDEPTID NOT IN (32) AND USERINFOCUS.DEFAULTDEPTID IS NOT NULL;";
                //Debug.WriteLine(query);
                adaptador0 = new SqlDataAdapter(query, conexion.con);
                adaptador0.Fill(dtEmpleado);
                conexion.cerrar();

                if (Array.Exists(semanalpys, x => x == idhor))
                {

                    for (int j = 0; j < dtEmpleado.Rows.Count; j++)
                    {
                        DataTable dtEmpleadoHor = new DataTable();
                        conexion.abrir();
                        query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_IN,HORARIOS.HOR_INTURNO,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA,HORARIOS.Descripcion FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFOCUS.BADGENUMBER= " + dtEmpleado.Rows[j]["BADGENUMBER"].ToString();
                        // Debug.WriteLine(query);
                        SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                        adaptador1.Fill(dtEmpleadoHor);
                        conexion.cerrar();
                        TimeSpan tin1, tin, tout, tin1sab, tinsab, toutsab;
                        tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                        tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                        tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                        Debug.WriteLine(tin1);
                        Debug.WriteLine(tin);
                        Debug.WriteLine(tout);
                        //TimeSpan hrs_dia = tout - tin1;
                        TimeSpan hrs_dia = tout - tin;
                        if (idhor == 30)
                        {
                            hrs_dia = new TimeSpan(9, 0, 0);
                        }
                        //Debug.WriteLine(hrs_dia);
                        for (int h = 0; h < fechas.Length - 1; h++)
                        {
                            // Debug.WriteLine(dtEmpleado.Rows[j]["BADGENUMBER"].ToString());
                            //Debug.WriteLine(fechas[h]);
                            DataTable dtvalidacion = new DataTable();
                            conexion.abrir();
                            query = "SELECT * FROM detalledias where BADGENUMBER=" + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND fecha='" + fechas[h] + "'";
                            SqlDataAdapter adaptadorvalidacion = new SqlDataAdapter(query, conexion.con);
                            adaptadorvalidacion.Fill(dtvalidacion);
                            conexion.cerrar();
                            if (dtvalidacion.Rows.Count > 0)
                            {

                            }
                            else
                            {
                                //Console.WriteLine(h);
                                conexion.abrir();
                                DataTable dtMarcas = new DataTable();
                                query = "SELECT DISTINCT CHECKTIME,BADGENUMBER FROM CHECKINOUT WHERE BADGENUMBER=" + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND CHECKTIME BETWEEN '" + fechas[h] + "' AND '" + fechas[h + 1] + "' ORDER BY CHECKTIME;";
                                SqlDataAdapter adaptadorM = new SqlDataAdapter(query, conexion.con);
                                adaptadorM.Fill(dtMarcas);
                                conexion.cerrar();
                                TimeSpan[] checadasval = new TimeSpan[dtMarcas.Rows.Count + 1];
                                TimeSpan basecomparacionmin = new TimeSpan(0, 10, 0);
                                TimeSpan dif = new TimeSpan();
                                //TimeSpan[] checadas = new TimeSpan[dtMarcas.Rows.Count];

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
                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00',0,'','" + detalle + "','00:00:00','','00:00:00')";
                                        SqlCommand comando = new SqlCommand(query, conexion.con);
                                        comando.ExecuteNonQuery();
                                        conexion.cerrar();
                                        //Debug.WriteLine(query);
                                        //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 0, 0, 0, 0, 0, 0, detalle);
                                    }
                                    else
                                    {
                                        String[,] eventosempleado = new string[10, 3];
                                        detalle = "";
                                        evento = "";
                                        tipoeven = "";
                                        observaciones = "";

                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Inasistencia");
                                        evento = eventosempleado[0, 0];
                                        tipoeven = eventosempleado[0, 1];
                                        observaciones = eventosempleado[0, 2];


                                        if (eventosempleado[0, 0] != null)
                                        {
                                            evento = eventosempleado[0, 0];
                                            tipoeven = eventosempleado[0, 1];
                                            observaciones = eventosempleado[0, 2];
                                            conexion.abrir();
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00',0,'No se encontrarón marcas.','" + fechas[h] + " " + evento + ". " + observaciones + " ','00:00:00','" + evento + "','00:00:00')";
                                            SqlCommand comando = new SqlCommand(query, conexion.con);
                                            comando.ExecuteNonQuery();
                                            conexion.cerrar();
                                        }
                                        else
                                        {
                                            if ((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Sunday))
                                            {
                                                detalle = "Día Domingo.";
                                                conexion.abrir();
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00',0,'" + detalle + "','','00:00:00','','00:00:00')";
                                                SqlCommand comando = new SqlCommand(query, conexion.con);
                                                comando.ExecuteNonQuery();
                                                conexion.cerrar();
                                            }
                                            else
                                            {
                                                if((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday))
                                                {
                                                    string idtemp = dtEmpleado.Rows[j]["Badgenumber"].ToString();
                                                    if (idtemp=="50" || idtemp=="172" || idtemp =="745" || idtemp=="388")
                                                    {
                                                        detalle = "Día Sabado. Correcto";
                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00',0,'" + detalle + "','','00:00:00','','00:00:00')";
                                                        SqlCommand comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();
                                                    }
                                                    else
                                                    {
                                                        detalle = "No se encontraron marcas.";
                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,1,0,0,'00:00','00:00',1,'" + detalle + "','','00:00:00','','00:00:00')";
                                                        SqlCommand comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();
                                                    }
                                                }
                                                else
                                                {
                                                    detalle = "No se encontraron marcas.";
                                                    conexion.abrir();
                                                    query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,1,0,0,'00:00','00:00',1,'" + detalle + "','','00:00:00','','00:00:00')";
                                                    SqlCommand comando = new SqlCommand(query, conexion.con);
                                                    comando.ExecuteNonQuery();
                                                    conexion.cerrar();
                                                }
                                     
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    for (int i = 0; i < dtMarcas.Rows.Count; i++)
                                    {
                                        checadasval[i] = TimeSpan.Parse(DateTime.Parse(dtMarcas.Rows[i]["CHECKTIME"].ToString()).ToString("HH:mm"), System.Globalization.CultureInfo.CurrentCulture);
                                        // Debug.WriteLine(checadasval[i]);

                                    }

                                    for (int i = 0; i < dtMarcas.Rows.Count - 1; i++)
                                    {
                                        dif = checadasval[i + 1] - checadasval[i];
                                        if (dif < basecomparacionmin)
                                        {
                                            //checadas[i] = checadas[i+1];
                                            checadasval[i + 1] = checadasval[i];
                                        }
                                    }

                                    List<TimeSpan> lst = checadasval.ToList();
                                    List<TimeSpan> checadasfin = lst.Distinct().ToList();
                                    TimeSpan[] checadas = checadasfin.ToArray();

                                    if ((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Sunday))
                                    {

                                        detalle = "";
                                        evento = "";
                                        detalle = "";
                                        tipoeven = "";
                                        observaciones = "";
                                        if ((checadas.Length - 1) > 1)
                                        {
                                            detalle = "Asistencia Día Domingo.";
                                            hrsdia_laboradas = checadas[checadas.Length - 2] - checadas[0];
                                            conexion.abrir();
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'" + hrsdia_laboradas + "','00:00:00',0,'" + detalle + "','','00:00:00','','00:00:00')";
                                            //Debug.WriteLine(query);
                                            SqlCommand comando = new SqlCommand(query, conexion.con);
                                            comando.ExecuteNonQuery();
                                            conexion.cerrar();
                                            //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, aux3, detalle);
                                        }
                                        else
                                        {
                                            detalle = "Asistencia Día Domingo. Solo se encontró una Marca.";
                                            //hrsdia_laboradas = checadas[checadas.Length - 2] - checadas[0];
                                            conexion.abrir();
                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00:00','00:00:00',0,'" + detalle + "','','00:00:00','','00:00:00')";
                                            //Debug.WriteLine(query);
                                            SqlCommand comando = new SqlCommand(query, conexion.con);
                                            comando.ExecuteNonQuery();
                                            conexion.cerrar();
                                        }
                                    }
                                    else
                                    {
                                        switch (checadas.Length - 1)
                                        {
                                            case 1:
                                                evento = "";
                                                detalle = "";
                                                tipoeven = "";
                                                observaciones = "";
                                                hrsdia_laboradas = TimeSpan.Zero;
                                                hrsdia_comedor = TimeSpan.Zero;
                                                int bandera = 1;
                                                detalle = "Se encontró solo una marca.";
                                                String[,] eventosempleado1 = new string[10, 3];
                                                eventosempleado1 = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                if (eventosempleado1[0, 0] != null)
                                                {
                                                    evento = fechas[h] + ". " + eventosempleado1[0, 0];
                                                    tipoeven = eventosempleado1[0, 1];
                                                    observaciones = eventosempleado1[0, 2];
                                                    bandera = 0;
                                                }
                                                conexion.abrir();
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00'," + bandera + ",'" + detalle + "','" + evento + ". " + observaciones + "','00:00:00','" + tipoeven + "','00:00:00')";
                                                SqlCommand comando = new SqlCommand(query, conexion.con);
                                                comando.ExecuteNonQuery();
                                                conexion.cerrar();
                                                //Debug.WriteLine(query);
                                                //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, 0, 0, TimeSpan.Zero, TimeSpan.Zero, 1, detalle);
                                                break;

                                            case 2:
                                                evento = "";
                                                detalle = "";
                                                tipoeven = "";
                                                observaciones = "";
                                                int aux1 = 0, aux2 = 0;
                                                int auxporteros = 0;
                                                int inc2 = 0;

                                                if ((Array.Exists(semanapysab, x => x == idhor)) && ((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)))
                                                {
                                                    tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_INTURNO"].ToString());
                                                    tin = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_IN"].ToString());
                                                    tout = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_OUT"].ToString());
                                                    //hrs_dia = tout - tin1
                                                    hrs_dia = tout - tin;
                                                }
                                                else
                                                {
                                                    tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                                                    tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                                    tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                                    // hrs_dia = tout - tin1;
                                                    hrs_dia = tout - tin;
                                                }
                                                if (idhor == 30)
                                                {
                                                    hrs_dia = new TimeSpan(9, 0, 0);
                                                }
                                                // Debug.WriteLine(hrs_dia);

                                                auxporteros = 1;
                                                detalle = "Se encontraron solo 2 marcas. No se registró comedor.";
                                                hrsdia_laboradas = checadas[1] - checadas[0];
                                                hrsdia_comedor = TimeSpan.Zero;
                                                //   hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                if (checadas[0] > tin1)
                                                {
                                                    String[,] eventosempleado = new string[10, 3];
                                                    aux1 = 1;
                                                    inc2 = 1;
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        aux1 = 0;
                                                        inc2 = 0;
                                                    }
                                                }
                                                if (checadas[1] < tout)
                                                {
                                                    aux2 = 1;
                                                    inc2 = 1;
                                                    String[,] eventosempleado = new string[10, 3];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                    if (eventosempleado[0, 0] != null)
                                                    {

                                                        evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        aux2 = 0;
                                                        inc2 = 0;
                                                    }
                                                }
                                                if (hrsdia_laboradas > hrs_dia)
                                                {
                                                    hrs_extra = hrsdia_laboradas - hrs_dia;
                                                    hrs_extra = calculahorasextra(hrs_extra);
                                                }
                                                else
                                                {
                                                    hrs_extra = TimeSpan.Zero;
                                                    detalle = "Se encontraron dos marcas sin coherencia.";
                                                }

                                                conexion.abrir();
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + auxporteros + ",'" + detalle + ". " + banderacambio + "','" + evento + ". " + observaciones + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extra + "')";
                                                comando = new SqlCommand(query, conexion.con);
                                                comando.ExecuteNonQuery();
                                                conexion.cerrar();
                                                //Debug.WriteLine(query);
                                                //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, auxporteros, detalle);
                                                break;
                                            case 3:
                                                evento = "";
                                                detalle = "";
                                                tipoeven = "";
                                                observaciones = "";
                                                aux1 = 0;
                                                aux2 = 0;
                                                int inc = 0;
                                                detalle = "Se encontraron solo 3 marcas.";
                                                evento = "";
                                                tipoeven = "";
                                                banderacambio = "";

                                                if ((Array.Exists(semanapysab, x => x == idhor)) && ((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)))
                                                {
                                                    tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_INTURNO"].ToString());
                                                    tin = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_IN"].ToString());
                                                    tout = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_OUT"].ToString());
                                                    // hrs_dia = tout - tin1;
                                                    hrs_dia = tout - tin;
                                                }
                                                else
                                                {
                                         
                                                    tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                                                    tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                                    tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                                    //hrs_dia = tout - tin1;
                                                    hrs_dia = tout - tin;

                                                }
                                                if (idhor == 30)
                                                {
                                                    hrs_dia = new TimeSpan(9, 0, 0);
                                                }
                                                // Debug.WriteLine(hrs_dia);
                                                hrsdia_laboradas = checadas[2] - checadas[0];
                                                hrsdia_comedor = TimeSpan.Zero;
                                                //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;


                                                if (checadas[0] > tin1)
                                                {
                                                    aux1 = 1;
                                                    inc = 1;
                                                    String[,] eventosempleado = new string[10, 3];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                    if (eventosempleado[0, 0] != null)
                                                    {

                                                        evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        aux1 = 0;
                                                        inc = 0;

                                                    }
                                                }
                                                if (checadas[2] < tout)
                                                {
                                                    detalle = "No se registro salida final.";
                                                    String[,] eventosempleado = new string[10, 3];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");

                                                    aux2 = 1;
                                                    inc = 1;
                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        aux2 = 0;
                                                        inc = 0;
                                                    }
                                                }
                                                if (hrsdia_laboradas > hrs_dia)
                                                {
                                                    detalle = "No se registro salida de comedor.";
                                                    hrs_extra = hrsdia_laboradas - hrs_dia;
                                                    hrs_extra = calculahorasextra(hrs_extra);
                                                }
                                                else
                                                {
                                                    hrs_extra = TimeSpan.Zero;
                                                    hrsdia_comedor = checadas[2] - checadas[1];

                                                }
                                                //hrs_extra = TimeSpan.Zero;

                                                conexion.abrir();
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + inc + ",'" + detalle + "','" + evento + ". " + observaciones + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extra + "')";
                                                comando = new SqlCommand(query, conexion.con);
                                                comando.ExecuteNonQuery();
                                                conexion.cerrar();
                                                //Debug.WriteLine(query);
                                                //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, 1, detalle);
                                                break;
                                            case 4:
                                                evento = "";
                                                detalle = "";
                                                tipoeven = "";
                                                observaciones = "";
                                                int aux3 = 0;
                                                aux1 = 0;
                                                aux2 = 0;
                                                evento = "";
                                                tipoeven = "";
                                                banderacambio = "";
                                                if ((Array.Exists(semanapysab, x => x == idhor)) && ((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)))
                                                {
                                                    tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_INTURNO"].ToString());
                                                    tin = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_IN"].ToString());
                                                    tout = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_OUT"].ToString());
                                                    //hrs_dia = tout - tin1;
                                                    hrs_dia = tout - tin;
                                                }
                                                else
                                                {
                                                    tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                                                    tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                                    tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                                    hrs_dia = tout - tin;
                                                   
                                                }
                                       
                                                if (idhor == 30)
                                                {
                                                    hrs_dia = new TimeSpan(9, 0, 0);
                                                }
                                                hrsdia_laboradas = checadas[3] - checadas[0];
                                                hrsdia_comedor = checadas[2] - checadas[1];
                                                detalle = "Correcto";
                                                //hrs_comedor = hrs_comedor + hrsdia_comedor;
                                                //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                // Debug.WriteLine(hrs_dia);
                                                if (checadas[0] > tin1)
                                                {
                                                    aux1 = 1;
                                                    aux3 = 1;
                                                    String[,] eventosempleado = new string[10, 3];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        aux1 = 0;
                                                        aux3 = 0;
                                                    }

                                                }

                                                if (checadas[3] < tout)
                                                {
                                                    detalle = "No se registro salida final.";
                                                    String[,] eventosempleado = new string[10, 3];
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                    aux2 = 1;
                                                    aux3 = 1;
                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        aux2 = 0;
                                                        aux3 = 0;

                                                    }
                                                }

                                                if (hrsdia_laboradas > hrs_dia)
                                                {
                                                    hrs_extra = hrsdia_laboradas - hrs_dia;
                                                    hrs_extra = calculahorasextra(hrs_extra);
                                                }
                                                else
                                                {
                                                    hrs_extra = TimeSpan.Zero;
                                                    detalle = "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada.";
                                                }

                                                conexion.abrir();
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','" + evento + ". " + observaciones + "','" + hrs_extra + "','" + tipoeven + "','"+ hrs_extra + "')";
                                                //Debug.WriteLine(query);
                                                comando = new SqlCommand(query, conexion.con);
                                                comando.ExecuteNonQuery();
                                                conexion.cerrar();
                                                //Debug.WriteLine(query);
                                                //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, aux3, detalle);
                                                break;
                                            default:
                                                detalle = "";
                                                evento = "";
                                                detalle = "";
                                                tipoeven = "";
                                                observaciones = "";
                                                if ((checadas.Length - 1) > 4)
                                                {

                                                    aux3 = 0;
                                                    aux1 = 0;
                                                    aux2 = 0;
                                                    hrs_extra = TimeSpan.Zero;
                                                    TimeSpan hrscom_extra = TimeSpan.Zero;
                                                    if ((Array.Exists(semanapysab, x => x == idhor)) && ((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)))
                                                    {
                                                        tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_INTURNO"].ToString());
                                                        tin = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_IN"].ToString());
                                                        tout = TimeSpan.Parse(dtEmpleadoHor.Rows[1]["HOR_OUT"].ToString());
                                                        //hrs_dia = tout - tin1;
                                                        hrs_dia = tout - tin;
                                                    }
                                                    else
                                                    {
                                                        tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                                                        tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                                        tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                                        //hrs_dia = tout - tin1;
                                                        hrs_dia = tout - tin;
                                                    }
                                                    hrsdia_laboradas = checadas[checadas.Length - 2] - checadas[0];

                                                    if (idhor == 30)
                                                    {
                                                        hrs_dia = new TimeSpan(9, 0, 0);
                                                    }
                                                    //  hrs_comedor = hrs_comedor + hrsdia_comedor;
                                                    //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                    if (hrsdia_laboradas < hrs_dia)
                                                    {
                                                        aux3 = 1;
                                                        detalle = detalle + "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada. 6 checadas";
                                                    }
                                                    if (checadas[checadas.Length - 2] < tout)
                                                    {
                                                        String[,] eventosempleado = new string[10, 3];
                                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                        aux2 = 1;
                                                        aux3 = 1;
                                                        if (eventosempleado[0, 0] != null)
                                                        {
                                                            evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                            tipoeven = eventosempleado[0, 1];
                                                            observaciones = eventosempleado[0, 2];
                                                            aux2 = 0;
                                                            aux3 = 0;

                                                        }
                                                    }
                                                    if (checadas[0] > tin1)
                                                    {
                                                        aux1 = 1;
                                                        aux3 = 1;
                                                        String[,] eventosempleado = new string[10, 3];
                                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                        if (eventosempleado[0, 0] != null)
                                                        {
                                                            evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                            tipoeven = eventosempleado[0, 1];
                                                            observaciones = eventosempleado[0, 2];
                                                            aux1 = 0;
                                                            aux3 = 0;

                                                        }
                                                    }

                                                    if (hrsdia_laboradas > hrs_dia)
                                                    {
                                                        hrsdia_laboradas = checadas[checadas.Length - 2] - checadas[0];
                                                        hrsdia_comedor = TimeSpan.Zero;
                                                        hrs_extra = hrsdia_laboradas - hrs_dia;
                                                        hrs_extra = calculahorasextra(hrs_extra);
                                                        detalle = "Correcto. Horas extra despues de jornada laboral.";

                                                    }
                                                    else
                                                    {
                                                        hrs_extra = TimeSpan.Zero;
                                                        hrsdia_laboradas = checadas[checadas.Length - 2] - checadas[0];
                                                        hrsdia_comedor = TimeSpan.Zero;
                                                        detalle = "Se encontraron más de cuatro checadas. Posible permiso en horario laboral.";
                                                    }

                                                    ////Debug.WriteLine(hrs_extra);
                                                    conexion.abrir();
                                                    //query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','','" + hrs_extra + "','','" + hrs_extra + "')";
                                                    query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','"+evento+". "+observaciones+"','" + hrs_extra + "','"+tipoeven+"','" + hrs_extra + "')";
                                                    //Debug.WriteLine(query);
                                                    comando = new SqlCommand(query, conexion.con);
                                                    comando.ExecuteNonQuery();
                                                    conexion.cerrar();
                                                    //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, aux3, detalle);

                                                }
                                                break;
                                                /*  

                                                  break;*/
                                        }
                                    }
                                }
                            }
                        }
                        //detalledatos.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), dtEmpleado.Rows[j]["Name"].ToString(), asistencia, inasistencia, retardo, salidatemprano, incosistencia, float.Parse(hrs_laboradas.TotalHours.ToString("N3")), float.Parse(hrs_comedor.TotalHours.ToString("N3")), hrsextra,turno);
                        detalle = "";

                    }
                }
                marcastop = marcastop + 1;
                toolStripProgressBar1.Value = marcastop;
            }
            sumaincon();
            actualizain();
            pintagrilla();
            reporteExcelToolStripMenuItem.Visible = true;
        }

        public void pintagrilla()
        {
            string color;
            foreach (DataGridViewRow rowp in dataGridViewDetalleDias.Rows)
            {
                if (rowp.Cells["tipoeven"].Value.ToString() != null)
                {
                    color = setcolor(rowp.Cells["tipoeven"].Value.ToString());
                    if (color == "Black")
                    {
                        rowp.DefaultCellStyle.ForeColor = Color.White;
                    }
                    rowp.DefaultCellStyle.BackColor = Color.FromName(color);
                }
                else
                {
                    rowp.DefaultCellStyle.BackColor = Color.White;
                }

            }
        }


        public string setcolor(string evento)
        {
            string col = "";
            string query1 = "";
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            DataTable dtEmpEven = new DataTable();
            query1 = "SELECT COLOR FROM EVENTO WHERE descripcion='" + evento + "'";
            SqlDataAdapter adaptador = new SqlDataAdapter(query1, conexion.con);
            adaptador.Fill(dtEmpEven);
            conexion.cerrar();
            if (dtEmpEven.Rows.Count > 0)
            {
                col = dtEmpEven.Rows[0]["COLOR"].ToString();
            }
            else
            {
                col = "White";
            }
            return col;
        }

        public void sumaincon()
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleado = new DataTable();
            conexion.abrir();
            string query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HORARIOS.Descripcion FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE HORARIOS.IDGROUP=2 AND HORARIOS.ID_HOR NOT IN(18,24,32) and USERINFOCUS.DEFAULTDEPTID<>32 ORDER BY HORARIOS.Descripcion ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtempleado);
            conexion.cerrar();

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
                        cadenacom = rowcom["comentarios"].ToString() + ". " + cadenacom;

                    }
                    Debug.WriteLine(cadenacom);
                }
                detalledatos.Rows.Add(row["BADGENUMBER"].ToString(), row["NAME"].ToString(), asisbd, inasisbd, retbd, saltempbd, incosisbd, CalcularTiempo(hrslabbd), CalcularTiempo(hrscombd), hrsextbd, turnobd, cadenacom);
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
            pintagrilla();
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
            //ExportarDatos(dataGridViewDatos);
            // C:\Users\userf\source\repos\EmpManagement\EmpManagement\documentos\gafete.xlsx
            conexionbd conexion = new conexionbd();
            DataTable dtem = new DataTable();
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            int contador = 0;


            string[,] saNames = new string[dataGridViewDatos.Rows.Count, 12];
            //Debug.WriteLine(dataGridViewDatos.Rows.Count);

            foreach (DataGridViewRow row in dataGridViewDatos.Rows)
            {
                saNames[contador, 0] = row.Cells["ID"].Value.ToString();
                saNames[contador, 1] = row.Cells["Nombre"].Value.ToString();
                saNames[contador, 2] = row.Cells["Asistencias"].Value.ToString();
                saNames[contador, 3] = row.Cells["Inasistencias"].Value.ToString();
                saNames[contador, 4] = row.Cells["Retardos"].Value.ToString();
                saNames[contador, 5] = row.Cells["Sal. Temp."].Value.ToString();
                saNames[contador, 6] = row.Cells["Inconsistencias"].Value.ToString();
                saNames[contador, 7] = row.Cells["H.T. Laboradas"].Value.ToString();
                saNames[contador, 8] = row.Cells["H.T. Comedor"].Value.ToString();
                saNames[contador, 9] = row.Cells["H. Extras"].Value.ToString();
                saNames[contador, 10] = row.Cells["Turno"].Value.ToString();
                saNames[contador, 11] = row.Cells["Comentarios"].Value.ToString();
                contador = contador + 1;
            }
            contador = 0;
            // try
            //{
            //Start Excel and get Application object.
            oXL = new Excel.Application();
            oXL.Visible = true;

            //Get a new workbook.
            oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Excel\QuincenalReporte.xlsx"));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            //Add table headers going cell by cell.
            /* oSheet.Cells[1, 1] = "First Name";
             oSheet.Cells[1, 2] = "Last Name";
             oSheet.Cells[1, 3] = "Full Name";
             oSheet.Cells[1, 4] = "Salary";
            */
            //Format A1:D1 as bold, vertical alignment = center.
            /*
            oSheet.get_Range("A1", "D1").Font.Bold = true;
            oSheet.get_Range("A1", "D1").VerticalAlignment =
            Excel.XlVAlign.xlVAlignCenter;
            */
            // Create an array to multiple values at once.
            //Fill A2:B6 with an array of values (First and Last Names).

            oSheet.get_Range("A2", "L" + dataGridViewDatos.Rows.Count.ToString()).Value2 = saNames;
            //oSheet.get_Range("A").Value2 = dataGridViewDatos.CurrentRow.Cells["PUESTO"].Value.ToString();
            oXL.Visible = true;
            oXL.UserControl = true;
            // }
            /*
             catch (Exception theException)
             {
                 String errorMessage;
                 errorMessage = "Error: ";
                 errorMessage = String.Concat(errorMessage, theException.Message);
                 errorMessage = String.Concat(errorMessage, " Line: ");
                 errorMessage = String.Concat(errorMessage, theException.Source);

                 MessageBox.Show(errorMessage, "Error");
             }
            */
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
            frmjust.labelFec.Text = DateTime.Parse(dataGridViewDetalleDias.CurrentRow.Cells["Fecha"].Value.ToString()).ToString("MM-dd-yyyy"); ;
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
                pintagrilla();
                // pintagrid();
            }
        }

        public TimeSpan calculahorasextra(TimeSpan hextra)
        {
            TimeSpan calculohor;
            TimeSpan medhoradd = new TimeSpan(0, 30, 0);
            TimeSpan horadd = new TimeSpan(1, 0, 0);

            if ((hextra.Minutes >= 24) && (hextra.Minutes < 54))
            {
                TimeSpan min = new TimeSpan(0, hextra.Minutes, 0);
                hextra = hextra.Subtract(min);
                hextra = hextra + medhoradd;
            }
            else
            {
                if (hextra.Minutes >= 54)
                {
                    TimeSpan min = new TimeSpan(0, hextra.Minutes, 0);
                    hextra = hextra.Subtract(min);
                    hextra = hextra + horadd;
                }
                else
                {
                    TimeSpan min = new TimeSpan(0, hextra.Minutes, 0);
                    hextra = hextra.Subtract(min);
                }
            }
            calculohor = hextra;
            return calculohor;
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
            pintagrilla();
        }

        public string[,] GetEventos(string badgenumber, string fecin, string fecfin, string fecac, string tipoev)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleadoEventos = new DataTable();
            //DateTime fecini, fecfinal;
            conexion.abrir();
            string query = "DECLARE @FEC DATE; SET @FEC = '" + fecac + "';SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO, EVENEMP.OBSERVACIONES FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN = EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER = " + badgenumber + " AND @FEC BETWEEN fecin AND fecfin AND EVENTO.GRUPO = '" + tipoev + "'; ";
            Debug.WriteLine(query);
            string[,] eventos = new string[10, 3];
            SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
            adaptadorsum.Fill(dtempleadoEventos);
            conexion.cerrar();
            if (dtempleadoEventos.Rows.Count > 0)
            {
                eventos[0, 0] = dtempleadoEventos.Rows[0]["Descripcion"].ToString();
                eventos[0, 1] = dtempleadoEventos.Rows[0]["Descripcion"].ToString();
                eventos[0, 2] = dtempleadoEventos.Rows[0]["Observaciones"].ToString();
            }
            return eventos;
        }

        public int GetEventosReporte(string badgenumber, string fecac, string tipoev)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleadoEventos = new DataTable();
            //DateTime fecini, fecfinal;
            conexion.abrir();
            string query = "";
            if (tipoev == "Vacaciones")
            {
                query = "DECLARE @FEC DATE; SET @FEC = '" + fecac + "';SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO, EVENEMP.OBSERVACIONES FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN = EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER = " + badgenumber + " AND @FEC BETWEEN fecin AND fecfin AND EVENTO.descripcion = '" + tipoev + "'; ";
            }
            else
            {
                query = "DECLARE @FEC DATE; SET @FEC = '" + fecac + "';SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO, EVENEMP.OBSERVACIONES FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN = EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER = " + badgenumber + " AND @FEC BETWEEN fecin AND fecfin";
            }

            //string[] eventos = new string[3];
            int eventos = 0;
            SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
            adaptadorsum.Fill(dtempleadoEventos);
            conexion.cerrar();
            if (dtempleadoEventos.Rows.Count > 0)
            {
                eventos = 1;
            }
            else
            {
                eventos = 0;
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

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBox2.Text = "";
                detalledatos.DefaultView.RowFilter = $"ID LIKE '{toolStripTextBox1.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBox1.Text = "";
                detalledatos.DefaultView.RowFilter = $"Nombre LIKE '{toolStripTextBox2.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void limpiarFechasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string datein, datefin;
            datein = dateTimePickerIni.Value.ToString("MM-dd-yyyy");
            datefin = dateTimePickerFin.Value.ToString("MM-dd-yyyy");

            DialogResult resultado = MessageBox.Show("¿Seguro que desea eliminar?", "Salir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                conexionbd conexion = new conexionbd();
                conexion.abrir();
                string query = "delete detalledias from detalledias inner join HOREMPLEADO on detalledias.badgenumber=HOREMPLEADO.Badgenumber inner join HORARIOS on HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR where HORARIOS.idgroup=2 and detalledias.fecha>='" + datein + "' and detalledias.fecha<='" + datefin + "'";
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                dataGridViewDatos.DataSource = null;
                dataGridViewDetalleDias.DataSource = null;
                toolStripProgressBar1.Value = 0;
                MessageBox.Show("Eliminación de reporte correcto.");
                reporteExcelToolStripMenuItem.Visible = false;
            }
        }

        private void dataGridViewDetalleDias_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string colorrow = "White";
            if (this.dataGridViewDetalleDias.Columns[e.ColumnIndex].Name == "tipoeven")
            {
                if (e.Value != null)
                {
                    if (e.Value.GetType() != typeof(System.DBNull))
                    {
                        colorrow = setcolor(e.Value.ToString());
                        if (colorrow == "Black")
                        {
                            e.CellStyle.ForeColor = Color.White;
                        }
                        e.CellStyle.BackColor = Color.FromName(colorrow);

                    }
                }
            }
        }

        private void reporteExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ExportarDatos(dataGridViewDatos);
            // C:\Users\userf\source\repos\EmpManagement\EmpManagement\documentos\gafete.xlsx
            conexionbd conexion = new conexionbd();

            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
            int inicio = 42;
            int inicioid = 7;
            int inicioturno = 6;
            int iniciofechas = 9;
            int iniciochecadas = 9;
            int contadorcolumnas = 0;
            int iniciodetalle = 23;
            int aumentofilas = 0;
            int auxfilas = 0;
            int iniciovacaciones = 0;
            int iniciopermisos = 0;
            int k = 0;//variable para aumento de filas
            int h = 0;//variable para registro de comentarios
            int iniciocomentarios = 8;
            int iniciosem2 = 16;
            int banderacambio = 0;

            int[] semn1 = { 0, 2, 4, 6, 8, 10 };
            int[] semn2 = { 1, 3, 5, 7, 9, 11 };


            //string[,] saNames = new string[dataGridViewDatos.Rows.Count, 12];
            //Debug.WriteLine(dataGridViewDatos.Rows.Count);

            // try
            //{
            //Start Excel and get Application object.
            oXL = new Excel.Application();
            //oXL.Visible = true;

            string oldName = @"C:\Excel\ReporteTiemposQuincenal.xlsx";
            string newName = @"C:\Excel\ReporteTiempos" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xlsx";
            System.IO.File.Copy(oldName, newName);

            //Get a new workbook.
            oWB = (Excel._Workbook)(oXL.Workbooks.Open(newName));
            // oWB = (Excel._Workbook)(oXL.Workbooks.Add());
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;
            char[] array1 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G','H' };

            DataTable dtuserinfo = new DataTable();
            conexion.abrir();
            //string query = "select detalledias.*,DEPARTMENTS.DEPTNAME from detalledias inner join USERINFOCus on detalledias.badgenumber=USERINFOCus.Badgenumber inner join DEPARTMENTS on USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID where detalledias.fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "' and userinfocus.defaultdeptid=" + toolStripComboBox1.ComboBox.SelectedValue.ToString() + " ORDER BY detalledias.fecha";
            string query = "SELECT USERINFOCUS.Badgenumber,USERINFOCUS.Name,HORARIOS.Descripcion from USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE HORARIOS.IDGROUP=2 AND HORARIOS.TIPOHOR=1 ORDER BY HORARIOS.ID_HOR ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtuserinfo);
            Debug.WriteLine(dtuserinfo.Rows.Count);
            oSheet.get_Range("B3").Value2 = "ADMINISTRATIVOS";
            oSheet.get_Range("I3").Value2 = dateTimePickerIni.Value.ToString("dd/MM/yyyy");
            oSheet.get_Range("I4").Value2 = dateTimePickerFin.Value.ToString("dd/MM/yyyy");

            for (int i = 0; i < dtuserinfo.Rows.Count - 1; i++)
            {
                //|Debug.WriteLine(inicio+contadorcolumnas);
                Excel.Range source = oSheet.Range["A6:J40"];
                Excel.Range dest = oSheet.Range["A" + (inicio + contadorcolumnas).ToString()];
                source.Copy(dest);
                contadorcolumnas = contadorcolumnas + 36;
            }
            contadorcolumnas = 0;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = dtuserinfo.Rows.Count;
            toolStripProgressBar1.Value = 0;
            int cuentausuarios = 0;

            foreach (DataRow row in dtuserinfo.Rows)
            {
                DataTable detalledias = new DataTable();
                oSheet.get_Range("B" + (inicioid + contadorcolumnas).ToString()).Value2 = row["Badgenumber"].ToString();
                oSheet.get_Range("D" + (inicioid + contadorcolumnas).ToString()).Value2 = row["Name"].ToString();
                query = "select detalledias.* from detalledias inner join USERINFOCus on detalledias.badgenumber=USERINFOCus.Badgenumber where detalledias.fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "' and detalledias.badgenumber=" + row["Badgenumber"].ToString() + " ORDER BY detalledias.fecha";
                adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(detalledias);
                int cuentacolumnas = 0;
                foreach (DataRow row1 in detalledias.Rows)
                {
                    if (row1["comentarios"].ToString() != "" && row1["comentarios"].ToString() != ". ")
                    {
                        oSheet.get_Range("K" + (iniciocomentarios + contadorcolumnas + h).ToString()).Value2 = row1["comentarios"].ToString();
                        h++;
                    }
                    DataTable detallemarcaciones = new DataTable();
                    //  oSheet.get_Range(array1[cuentacolumnas] + (iniciofechas + contadorcolumnas).ToString()).Value2 = row["Badgenumber"].ToString();
                    query = "Select * from checkinout where badgenumber=" + row["Badgenumber"].ToString() + " and checktime BETWEEN '" + DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy") + "' AND '" + DateTime.Parse(row1["fecha"].ToString()).AddDays(1).ToString("MM-dd-yyyy") + "' ORDER BY CHECKTIME";
                    // Debug.WriteLine(query);
                    adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(detallemarcaciones);

                    TimeSpan[] checadasval = new TimeSpan[detallemarcaciones.Rows.Count + 1];
                    TimeSpan basecomparacionmin = new TimeSpan(0, 10, 0);
                    TimeSpan dif = new TimeSpan();
                    //SELECT DISTINCT CHECKTIME,BADGENUMBER FROM CHECKINOUT WHERE BADGENUMBER = " + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND CHECKTIME BETWEEN '" + fechas[h] + "' AND '" + fechas[h + 1] + "' ORDER BY CHECKTIME; "

                    for (int i = 0; i < detallemarcaciones.Rows.Count; i++)
                    {
                        checadasval[i] = TimeSpan.Parse(DateTime.Parse(detallemarcaciones.Rows[i]["CHECKTIME"].ToString()).ToString("HH:mm"), System.Globalization.CultureInfo.CurrentCulture);

                    }

                    for (int i = 0; i < detallemarcaciones.Rows.Count - 1; i++)
                    {
                        dif = checadasval[i + 1] - checadasval[i];

                        if (dif < basecomparacionmin)
                        {
                            checadasval[i + 1] = checadasval[i];
                        }
                    }

                    List<TimeSpan> lst = checadasval.ToList();
                    List<TimeSpan> checadasfin = lst.Distinct().ToList();
                    TimeSpan[] checadas = checadasfin.ToArray();

                   
                    //oSheet.get_Range(array1[cuentacolumnas]+(iniciochecadas+contadorcolumnas).ToString(), array1[cuentacolumnas]+ (iniciochecadas + contadorcolumnas + checadas.Length).ToString()).Value2 = checadas;
                    for (int i = 0; i < checadas.Length - 1; i++)
                    {
                        if (banderacambio >7)
                        {
                            oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas +iniciosem2+ i).ToString()).Value2 = checadas[i].ToString();
                        }
                        else
                        {
                            oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciochecadas + i).ToString()).Value2 = checadas[i].ToString();
                        }
                         
                    }
                    //oSheet.get_Range("A10").Value2 = checadas[0].ToString();

                    //Registra detalledias
                    for (int i = 0; i < 6; i++)
                    {
                        if (banderacambio > 7)
                        {
                            oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle+ semn2[i]).ToString()).Value2 = row1[i + 2].ToString();
                        }
                        else
                        {
                            oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + semn1[i]).ToString()).Value2 = row1[i + 2].ToString();
                        }
                    
                    }
                    if (banderacambio > 7)
                    {
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle+1 + 12).ToString()).Value2 = row1[11].ToString();
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle+1 + 14).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "Vacaciones"); ;
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle+1 + 16).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "");
                    }
                    else
                    {
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + 12).ToString()).Value2 = row1[11].ToString();
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + 14).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "Vacaciones"); ;
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + 16).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "");
                    }
                   
                    if (cuentacolumnas == 7)
                    {
                        cuentacolumnas = 0;
                    }
                    else
                    {
                        cuentacolumnas = cuentacolumnas + 1;
                    }

                    banderacambio++;
                }
                cuentacolumnas = 0;
                banderacambio = 0;
                contadorcolumnas = contadorcolumnas + 36;
                cuentausuarios++;
                h = 0;
                toolStripProgressBar1.Value = cuentausuarios;
               
            }

           
            //}
            /*
             catch (Exception theException)
             {
                 String errorMessage;
                 errorMessage = "Error: ";
                 errorMessage = String.Concat(errorMessage, theException.Message);
                 errorMessage = String.Concat(errorMessage, " Line: ");
                 errorMessage = String.Concat(errorMessage, theException.Source);

                 MessageBox.Show(errorMessage, "Error");
             }
            */
            conexion.cerrar();
            MessageBox.Show("Reporte Terminado.");
            oXL.Visible = true;
            oXL.UserControl = true;
            reporteExcelToolStripMenuItem.Enabled = true;
        }
    }
}
