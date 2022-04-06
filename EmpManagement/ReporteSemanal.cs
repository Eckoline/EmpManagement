using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace EmpManagement
{
    public partial class ReporteSemanal : Form
    {
        public ReporteSemanal()
        {
            InitializeComponent();
        }
        private void ReporteSemanal_Load(object sender, EventArgs e)
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
            detalledatos.Columns.Add("H. E. Apr");
            detalledatos.Columns.Add("Turno");
            detalledatos.Columns.Add("Comentarios");

            conexionbd conexion = new conexionbd();
            DataTable dtdepts = new DataTable();
            conexion.abrir();
            string query = "SELECT DISTINCT DEPARTMENTS.DEPTID,DEPARTMENTS.DEPTNAME FROM DEPARTMENTS inner join USERINFOCus ON DEPARTMENTS.DEPTID=USERINFOCus.DEFAULTDEPTID inner join HOREMPLEADO on USERINFOCus.Badgenumber=HOREMPLEADO.Badgenumber inner join HORARIOS on HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE HORARIOS.idgroup=1 AND HORARIOS.tipohor=1 AND DEPARTMENTS.DEPTID NOT IN(1,32);";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtdepts);
            toolStripComboBox1.ComboBox.DisplayMember = "DEPTNAME";
            toolStripComboBox1.ComboBox.ValueMember = "DEPTID";
            toolStripComboBox1.ComboBox.DataSource = dtdepts;
            conexion.cerrar();
            reporteTiemposToolStripMenuItem.Visible = false;
            toolStripComboBox1.Visible = false;
            groupBoxEdi.Enabled = false;

        }
        DataTable detalledatos = new DataTable();
        private void buttonAcept_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            //dataGridViewDatos.Rows.Clear();
            // dataGridViewDetalleDias.Rows.Clear();
            //TimeSpan hrs_comedor = TimeSpan.Zero;
            // TimeSpan hrs_laboradas = TimeSpan.Zero;
            TimeSpan hrsdia_laboradas = TimeSpan.Zero;
            TimeSpan hrsdia_comedor = TimeSpan.Zero;
            TimeSpan hrs_extra = TimeSpan.Zero;
            TimeSpan hrs_extraJus = TimeSpan.Zero;
            float hrsextra = 0;
            float hrsem = 0;
            float hrsdia = 0;
            DateTime auxintime;
            DateTime auxouttime;
            String turno;
            string datein, datefin;
            datein = dateTimePickerIni.Value.ToString("MM-dd-yyyy");
            datefin = dateTimePickerFin.Value.ToString("MM-dd-yyyy");

            //calcular fechas
            TimeSpan dias;
            dias = dateTimePickerFin.Value.AddDays(1) - dateTimePickerIni.Value;
            if ((dias.Days > 7) || (dateTimePickerIni.Value.DayOfWeek != DayOfWeek.Monday) || (dateTimePickerFin.Value.DayOfWeek == DayOfWeek.Monday))
            {
                MessageBox.Show("No es posible generar el reporte.");
            }
            else
            {
                String[] fechas = new String[dias.Days + 1];
                for (int i = 0; i < dias.Days + 1; i++)
                {
                    fechas[i] = dateTimePickerIni.Value.AddDays(i).ToString("yyyy-MM-dd");
                    //Debug.WriteLine(fechas[i]);
                }
                //Obtención de empleados.
                DataTable dtHor = new DataTable();
                DataTable dthorpys = new DataTable();
                DataTable dthort = new DataTable();
                DataTable dthorsab = new DataTable();
                SqlDataAdapter adaptador = new SqlDataAdapter();
                conexionbd conexion = new conexionbd();
                DataTable dtcambioturno = new DataTable();
                SqlDataAdapter adaptadorcambiotu = new SqlDataAdapter();


                string[] nuevosparam = new string[7];

                conexion.abrir();
                string query;
                query = "SELECT ID_HOR FROM HORARIOS WHERE IDGROUP=1";
                SqlDataAdapter adaptador0 = new SqlDataAdapter(query, conexion.con);
                adaptador0.Fill(dtHor);
                conexion.cerrar();
                int marcastop = 0;
                int idhor = 0;

                conexion.abrir();
                query = "SELECT ID_HOR FROM HORARIOS WHERE idgroup = 1 and tipohor = 1 and Descripcion not LIKE '%3%';";
                adaptador0 = new SqlDataAdapter(query, conexion.con);
                adaptador0.Fill(dthorpys);
                conexion.cerrar();
               
                int[] semanalpys = new int[dthorpys.Rows.Count];
                //int[] semanalpys = { 1, 2, 4, 5, 7, 8, 9, 11, 13, 15, 19, 25, 26, 33, 34 };

                for (int i = 0; i < dthorpys.Rows.Count; i++)
                {
                    semanalpys[i] = Int32.Parse(dthorpys.Rows[i][0].ToString());
              
                }


                conexion.abrir();
                query = "SELECT ID_HOR FROM HORARIOS WHERE idgroup = 1 and tipohor = 1 and Descripcion LIKE '%3%';";
                adaptador0 = new SqlDataAdapter(query, conexion.con);
                adaptador0.Fill(dthort);
                conexion.cerrar();
          
                int[] semanalt = new int[dthort.Rows.Count];

                for (int i = 0; i < dthort.Rows.Count; i++)
                {
                    semanalt[i] = Int32.Parse(dthort.Rows[i][0].ToString());
                
                }



                conexion.abrir();
                query = "SELECT ID_HOR FROM HORARIOS WHERE idgroup = 1 and tipohor = 2 and Descripcion LIKE '%SABADO%';";
                adaptador0 = new SqlDataAdapter(query, conexion.con);
                adaptador0.Fill(dthorsab);
                conexion.cerrar();

                // int[] semanapysab = { 9, 11, 13, 15, 19 };

                int[] semanapysab = new int[dthorsab.Rows.Count];

                for (int i = 0; i < dthorsab.Rows.Count; i++)
                {
                    semanapysab[i] = Int32.Parse(dthorsab.Rows[i]["ID_HOR"].ToString()) - 1;
                 
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
                    adaptador = new SqlDataAdapter(query, conexion.con);
                    adaptador.Fill(dtEmpleado);
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
                            //TimeSpan hrs_dia = tout - tin1;
                            TimeSpan hrs_dia = tout - tin;
                            //Debug.WriteLine(hrs_dia);
                            hrsem = float.Parse(dtEmpleadoHor.Rows[0]["HRS_SEMANA"].ToString());
                            hrsdia = float.Parse(dtEmpleadoHor.Rows[0]["HRS_DIA"].ToString());
                            turno = dtEmpleadoHor.Rows[0]["Descripcion"].ToString();

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
                                            String[,] eventosempleado = new string[10, 4];
                                            detalle = "";
                                            evento = "";
                                            tipoeven = "";
                                            observaciones = "";

                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Inasistencia");
                                            //evento = eventosempleado[0, 0];
                                            //tipoeven = eventosempleado[0, 1];
                                            //observaciones = eventosempleado[0, 2];

                                            if (eventosempleado[0, 0] != null)
                                            {
                                                int faltainjus = 0;
                                                evento = eventosempleado[0, 0];
                                                tipoeven = eventosempleado[0, 1];
                                                observaciones = eventosempleado[0, 2];
                                                faltainjus = Int32.Parse(eventosempleado[0, 3]);
                                                conexion.abrir();
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0," + faltainjus + ",0,0,'00:00','00:00',0,'No se encontrarón marcas.','" + fechas[h] + " " + evento + ". " + observaciones + " ','00:00:00','" + evento + "','00:00:00')";
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
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'" + hrsdia_laboradas + "','00:00:00',0,'" + detalle + "','','00:00:00','','00:00:00','00:00:00')";
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
                                                    String[,] eventosempleado1 = new string[10, 4];
                                                    eventosempleado1 = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                    if (eventosempleado1[0, 0] != null)
                                                    {
                                                        evento = fechas[h] + ". " + eventosempleado1[0, 0];
                                                        tipoeven = eventosempleado1[0, 1];
                                                        observaciones = eventosempleado1[0, 2];
                                                        bandera = Int32.Parse(eventosempleado1[0, 3]);
                                                    }
                                                    conexion.abrir();
                                                    query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0,0,0,'00:00','00:00'," + bandera + ",'" + detalle + "','" + evento + ". " + observaciones + "','00:00:00','" + tipoeven + "','00:00:00')";
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
                                                        //hrs_dia = tout - tin1;
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
                                                    // Debug.WriteLine(hrs_dia);
                                                    if (dtEmpleado.Rows[0]["DEFAULTDEPTID"].ToString() == "37")
                                                    {
                                                        auxporteros = 0;
                                                        detalle = "Correcto. Porteros.";
                                                        hrsdia_laboradas = checadas[1] - checadas[0];
                                                        hrsdia_comedor = TimeSpan.Zero;
                                                        //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                                        if (checadas[0] > tin)
                                                        {
                                                            aux1 = 1;
                                                            auxporteros = 1;
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");
                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                auxporteros = Int32.Parse(eventosempleado[0, 3]);
                                                            }

                                                        }
                                                        if (checadas[1] < tout)
                                                        {
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                            aux2 = 1;
                                                            auxporteros = 1;
                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                auxporteros = Int32.Parse(eventosempleado[0, 3]);
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
                                                    }
                                                    else
                                                    {
                                                        auxporteros = 1;
                                                        detalle = "Se encontraron solo 2 marcas. No se registró comedor.";
                                                        hrsdia_laboradas = checadas[1] - checadas[0];
                                                        hrsdia_comedor = TimeSpan.Zero;
                                                        //   hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                        if (checadas[0] > tin)
                                                        {
                                                            String[,] eventosempleado = new string[10, 4];
                                                            aux1 = 1;
                                                            inc2 = 1;
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                inc2 = Int32.Parse(eventosempleado[0, 3]);
                                                            }
                                                        }
                                                        if (checadas[1] < tout)
                                                        {
                                                            aux2 = 1;
                                                            inc2 = 1;
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                            if (eventosempleado[0, 0] != null)
                                                            {

                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                inc2 = Int32.Parse(eventosempleado[0, 3]);
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
                                                    }

                                                    hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                    if (hrs_extraJus == TimeSpan.MaxValue)
                                                    {
                                                        hrs_extraJus = hrs_extra;
                                                    }

                                                    conexion.abrir();
                                                    query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + auxporteros + ",'" + detalle + ". " + banderacambio + "','" + evento + ". " + observaciones + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extraJus + "')";
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

                                                        /*if (nuevosparam[0] != null && idhor!=33 && idhor!=34)
                                                        {
                                                            tin1 = TimeSpan.Parse(nuevosparam[2]);
                                                            tin = TimeSpan.Parse(nuevosparam[1]);
                                                            tout = TimeSpan.Parse(nuevosparam[3]);
                                                            hrs_dia = tout - tin1;
                                                            banderacambio = "Cambio Turno.";
                                                        }
                                                        else
                                                        {

                                                        }*/

                                                        tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                                                        tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                                        tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                                        //hrs_dia = tout - tin1;
                                                        hrs_dia = tout - tin;

                                                    }
                                                    // Debug.WriteLine(hrs_dia);
                                                    hrsdia_laboradas = checadas[2] - checadas[0];
                                                    hrsdia_comedor = TimeSpan.Zero;
                                                    //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;


                                                    if (checadas[0] > tin)
                                                    {
                                                        aux1 = 1;
                                                        inc = 1;
                                                        String[,] eventosempleado = new string[10, 4];
                                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                        if (eventosempleado[0, 0] != null)
                                                        {

                                                            evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                            tipoeven = eventosempleado[0, 1];
                                                            observaciones = eventosempleado[0, 2];
                                                            aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                            inc = Int32.Parse(eventosempleado[0, 3]);

                                                        }
                                                    }
                                                    if (checadas[2] < tout)
                                                    {
                                                        detalle = "No se registro salida final.";
                                                        String[,] eventosempleado = new string[10, 4];
                                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");

                                                        aux2 = 1;
                                                        inc = 1;
                                                        if (eventosempleado[0, 0] != null)
                                                        {
                                                            evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                            tipoeven = eventosempleado[0, 1];
                                                            observaciones = eventosempleado[0, 2];
                                                            aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                            inc = Int32.Parse(eventosempleado[0, 3]);
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
                                                    hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                    if (hrs_extraJus == TimeSpan.MaxValue)
                                                    {
                                                        hrs_extraJus = hrs_extra;
                                                    }
                                                    conexion.abrir();
                                                    query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + inc + ",'" + detalle + "','" + evento + ". " + observaciones + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extraJus + "')";
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
                                                        TimeSpan[] enviom3 = new TimeSpan[2];
                                                        enviom3[0] = checadas[2];
                                                        enviom3[0] = checadas[3];
                                                        // nuevosparam = CambioTurno(dtEmpleado.Rows[0]["DEFAULTDEPTID"].ToString(), enviom3, idhor.ToString());
                                                        /*  if (nuevosparam[0] != null)
                                                          {
                                                              tin1 = TimeSpan.Parse(nuevosparam[2]);
                                                              tin = TimeSpan.Parse(nuevosparam[1]);
                                                              tout = TimeSpan.Parse(nuevosparam[3]);
                                                              hrs_dia = tout - tin1;
                                                              banderacambio = "Cambio Turno.";
                                                          }
                                                          else
                                                          {
                                                              tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                                                              tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                                              tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                                              hrs_dia = tout - tin1;
                                                          }*/

                                                        tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_INTURNO"].ToString());
                                                        tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                                        tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                                        // hrs_dia = tout - tin1;
                                                        hrs_dia = tout - tin;
                                                    }
                                                    hrsdia_laboradas = checadas[3] - checadas[0];
                                                    hrsdia_comedor = checadas[2] - checadas[1];
                                                    detalle = "Correcto";
                                                    //hrs_comedor = hrs_comedor + hrsdia_comedor;
                                                    //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                    // Debug.WriteLine(hrs_dia);
                                                    if (checadas[0] > tin)
                                                    {
                                                        aux1 = 1;
                                                        aux3 = 1;
                                                        String[,] eventosempleado = new string[10, 4];
                                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                        if (eventosempleado[0, 0] != null)
                                                        {

                                                            evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                            tipoeven = eventosempleado[0, 1];
                                                            observaciones = eventosempleado[0, 2];
                                                            aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                            aux3 = Int32.Parse(eventosempleado[0, 3]);

                                                        }

                                                    }

                                                    if (checadas[3] < tout)
                                                    {
                                                        detalle = "No se registro salida final.";
                                                        String[,] eventosempleado = new string[10, 4];
                                                        eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                        aux2 = 1;
                                                        aux3 = 1;
                                                        if (eventosempleado[0, 0] != null)
                                                        {
                                                            evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                            tipoeven = eventosempleado[0, 1];
                                                            observaciones = eventosempleado[0, 2];
                                                            aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                            aux3 = Int32.Parse(eventosempleado[0, 3]);

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

                                                    hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                    if (hrs_extraJus == TimeSpan.MaxValue)
                                                    {
                                                        hrs_extraJus = hrs_extra;
                                                    }

                                                    conexion.abrir();
                                                    query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + ". " + banderacambio + "','" + evento + ". " + observaciones + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extraJus + "')";
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


                                                        //  hrs_comedor = hrs_comedor + hrsdia_comedor;
                                                        //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                        if (hrsdia_laboradas < hrs_dia)
                                                        {
                                                            aux3 = 1;
                                                            detalle = detalle + "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada. 6 checadas";
                                                        }
                                                        if (checadas[checadas.Length - 2] < tout)
                                                        {
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                            aux2 = 1;
                                                            aux3 = 1;
                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                aux3 = Int32.Parse(eventosempleado[0, 3]);

                                                            }
                                                        }
                                                        if (checadas[0] > tin)
                                                        {
                                                            aux1 = 1;
                                                            aux3 = 1;
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");

                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                aux3 = Int32.Parse(eventosempleado[0, 3]);

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

                                                        hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                        if (hrs_extraJus == TimeSpan.MaxValue)
                                                        {
                                                            hrs_extraJus = hrs_extra;
                                                        }

                                                        ////Debug.WriteLine(hrs_extra);
                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','','" + hrs_extra + "','','" + hrs_extraJus + "')";
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
                            // hrs_laboradas = TimeSpan.Zero;
                            //hrs_comedor = TimeSpan.Zero;
                            hrsextra = 0;
                        }
                    }
                    else
                    {
                        if (Array.Exists(semanalt, x => x == idhor))
                        {
                            for (int j = 0; j < dtEmpleado.Rows.Count; j++)
                            {
                                TimeSpan dai = new TimeSpan(1, 0, 0, 0);
                                DataTable dtEmpleadoHor = new DataTable();
                                conexion.abrir();
                                query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HOREMPLEADO.ID_HOR,HORARIOS.HOR_IN,HORARIOS.HOR_INTURNO,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA, HORARIOS.Descripcion FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE USERINFOCUS.BADGENUMBER= " + dtEmpleado.Rows[j]["BADGENUMBER"].ToString();
                                SqlDataAdapter adaptador1 = new SqlDataAdapter(query, conexion.con);
                                adaptador1.Fill(dtEmpleadoHor);
                                conexion.cerrar();
                                TimeSpan tin1 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString()).Add(dai);
                                TimeSpan tout2 = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString()).Add(dai).Add(dai);

                                TimeSpan tin = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_IN"].ToString());
                                TimeSpan tout = TimeSpan.Parse(dtEmpleadoHor.Rows[0]["HOR_OUT"].ToString());
                                TimeSpan hrs_dia = tout2 - tin1;
                                //TimeSpan hrs_dia = tout - tin;

                                hrsem = float.Parse(dtEmpleadoHor.Rows[0]["HRS_SEMANA"].ToString());
                                hrsdia = float.Parse(dtEmpleadoHor.Rows[0]["HRS_DIA"].ToString());
                                turno = dtEmpleadoHor.Rows[0]["Descripcion"].ToString();
                                for (int h = 0; h <= fechas.Length - 2; h++)
                                {
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
                                        conexion.abrir();
                                        DataTable dtMarcas = new DataTable();
                                        query = "SELECT DISTINCT CHECKTIME,BADGENUMBER FROM CHECKINOUT WHERE BADGENUMBER=" + dtEmpleado.Rows[j]["BADGENUMBER"].ToString() + " AND CHECKTIME BETWEEN '" + fechas[h] + " 17:00:00' AND '" + fechas[h + 1] + " 10:00:00' ORDER BY CHECKTIME;";
                                        //Debug.WriteLine(query);
                                        SqlDataAdapter adaptadorM = new SqlDataAdapter(query, conexion.con);
                                        adaptadorM.Fill(dtMarcas);
                                        //auxintime = DateTime.Parse(dtMarcas.Rows[0]["CHECKTIME"].ToString());
                                        //auxouttime = DateTime.Parse(dtMarcas.Rows[3]["CHECKTIME"].ToString());
                                        conexion.cerrar();
                                        TimeSpan basecomparacion = new TimeSpan(0, 10, 0);
                                        TimeSpan dif = new TimeSpan();
                                        DateTime[] checadasval = new DateTime[dtMarcas.Rows.Count + 1];
                                        int contador = 0;

                                        if (dtMarcas.Rows.Count == 0)
                                        {
                                            if (((DateTime.Parse(fechas[h], System.Globalization.CultureInfo.CurrentCulture).DayOfWeek == DayOfWeek.Saturday)))
                                            {
                                                detalle = "Correcto. Tercer Turno";
                                                conexion.abrir();
                                                query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00',0,'" + detalle + "','','00:00:00','','00:00:00')";
                                                SqlCommand comando = new SqlCommand(query, conexion.con);
                                                comando.ExecuteNonQuery();
                                                conexion.cerrar();

                                            }
                                            else
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
                                                    query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00',0,'" + detalle + "','','00:00:00','','00:00:00')";
                                                    SqlCommand comando = new SqlCommand(query, conexion.con);
                                                    comando.ExecuteNonQuery();
                                                    conexion.cerrar();
                                                    //Debug.WriteLine(query);
                                                    //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 0, 0, 0, 0, 0, 0, detalle);
                                                }
                                                else
                                                {
                                                    String[,] eventosempleado = new string[10, 4];
                                                    detalle = "";
                                                    eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Inasistencia");

                                                    if (eventosempleado[0, 0] != null)
                                                    {
                                                        int faltainjus = 0;
                                                        evento = eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        faltainjus = Int32.Parse(eventosempleado[0, 3]);


                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0," + faltainjus + ",0,0,'00:00','00:00',0,'No se encontrarón marcas.','" + fechas[h] + " " + evento + ". " + observaciones + " ','00:00:00','" + evento + "','00:00:00')";
                                                        SqlCommand comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();

                                                        /*evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                        tipoeven = eventosempleado[0, 1];
                                                        observaciones = eventosempleado[0, 2];
                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,0,0,0,'00:00','00:00',0,'','" + fechas[h] + " " + detalle + "','00:00:00','" + detalle + "')";
                                                        SqlCommand comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();*/

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
                                                            detalle = "No se encontraron marcas.";
                                                            conexion.abrir();
                                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',0,1,0,0,'00:00','00:00',1,'" + detalle + "','','00:00:00','','00:00:00')";
                                                            SqlCommand comando = new SqlCommand(query, conexion.con);
                                                            comando.ExecuteNonQuery();
                                                            conexion.cerrar();
                                                            //Debug.WriteLine(query);
                                                            //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 0, 1, 0, 0, 0, 0, 1, detalle);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DateTime[] checadasval2 = new DateTime[dtMarcas.Rows.Count + 1];
                                            TimeSpan basecomparacionmin = new TimeSpan(0, 10, 0);
                                            //Debug.WriteLine("Checadas recibidas");
                                            for (int i = 0; i < dtMarcas.Rows.Count; i++)
                                            {
                                                checadasval2[i] = DateTime.Parse(dtMarcas.Rows[i]["CHECKTIME"].ToString());
                                                //Debug.WriteLine(checadasval[i]);
                                            }

                                            for (int i = 0; i < dtMarcas.Rows.Count - 1; i++)
                                            {
                                                dif = checadasval2[i + 1] - checadasval2[i];
                                                if (dif < basecomparacionmin)
                                                {
                                                    //checadas[i] = checadas[i+1];
                                                    checadasval2[i + 1] = checadasval2[i];
                                                }
                                            }

                                            List<DateTime> lst = checadasval2.ToList();
                                            List<DateTime> checadasfin = lst.Distinct().ToList();

                                            //Debug.WriteLine(dtEmpleado.Rows[j]["Badgenumber"].ToString());
                                            //Debug.WriteLine(fechas[h]);
                                            //Debug.WriteLine("Checadas finales");
                                            DateTime[] checadas = checadasfin.ToArray();
                                            //Debug.WriteLine(dtEmpleado.Rows[j]["Badgenumber"].ToString());
                                            //Debug.WriteLine(fechas[h]);
                                            //Debug.WriteLine("Checadas finales");
                                            for (int i = 0; i < checadas.Length - 1; i++)
                                            {
                                                //Debug.WriteLine(checadas[i]);
                                            }

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
                                                        hrsdia_laboradas = TimeSpan.Zero;
                                                        hrsdia_comedor = TimeSpan.Zero;
                                                        detalle = "Se encontró solo una marca.";
                                                        int bandera = 1;

                                                        String[,] eventosempleado1 = new string[10, 4];
                                                        eventosempleado1 = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                        if (eventosempleado1[0, 0] != null)
                                                        {
                                                            evento = fechas[h] + ". " + eventosempleado1[0, 0];
                                                            tipoeven = eventosempleado1[0, 1];
                                                            observaciones = eventosempleado1[0, 2];
                                                            bandera = Int32.Parse(eventosempleado1[0, 3]);
                                                        }

                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0,0,0,'00:00','00:00'," + bandera + ",'" + detalle + "','" + evento + ". " + observaciones + "','00:00:00','" + tipoeven + "','00:00:00')";
                                                        SqlCommand comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();
                                                        //Debug.WriteLine(query);
                                                        //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, 0, 0, TimeSpan.Zero, TimeSpan.Zero, 1, detalle);
                                                        break;

                                                    case 2:
                                                        int aux1 = 0, aux2 = 0;
                                                        int auxporteros = 0;
                                                        evento = "";
                                                        tipoeven = "";
                                                        observaciones = "";
                                                        if (dtEmpleado.Rows[0]["DEFAULTDEPTID"].ToString() == "37")
                                                        {
                                                            auxporteros = 0;
                                                            detalle = "Correcto. Porteros";
                                                            hrsdia_laboradas = checadas[1] - checadas[0];
                                                            hrsdia_comedor = TimeSpan.Zero;
                                                            // hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                                            if (checadas[0].TimeOfDay > tin)
                                                            {
                                                                evento = "";
                                                                aux1 = 1;
                                                                auxporteros = 1;
                                                                String[,] eventosempleado = new string[10, 4];
                                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");
                                                                if (eventosempleado[0, 0] != null)
                                                                {
                                                                    evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                    tipoeven = eventosempleado[0, 1];
                                                                    observaciones = eventosempleado[0, 2];
                                                                    aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                    auxporteros = Int32.Parse(eventosempleado[0, 3]);
                                                                }
                                                            }
                                                            if (checadas[1].TimeOfDay < tout)
                                                            {
                                                                aux2 = 1;
                                                                evento = "";
                                                                auxporteros = 1;
                                                                String[,] eventosempleado = new string[10, 4];
                                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");

                                                                if (eventosempleado[0, 0] != null)
                                                                {

                                                                    evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                    tipoeven = eventosempleado[0, 1];
                                                                    observaciones = eventosempleado[0, 2];
                                                                    aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                    auxporteros = Int32.Parse(eventosempleado[0, 3]);


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

                                                        }
                                                        else
                                                        {

                                                            auxporteros = 1;
                                                            detalle = "Se encontraron solo 2 marcas. No se registró comedor.";
                                                            hrsdia_laboradas = checadas[1] - checadas[0];
                                                            hrsdia_comedor = TimeSpan.Zero;
                                                            //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                                            if (checadas[0].TimeOfDay > tin)
                                                            {
                                                                evento = "";
                                                                aux1 = 1;
                                                                String[,] eventosempleado = new string[10, 4];
                                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");
                                                                if (eventosempleado[0, 0] != null)
                                                                {
                                                                    evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                    tipoeven = eventosempleado[0, 1];
                                                                    observaciones = eventosempleado[0, 2];
                                                                    aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                    auxporteros = Int32.Parse(eventosempleado[0, 3]);

                                                                }
                                                            }
                                                            if (checadas[1].TimeOfDay < tout)
                                                            {
                                                                evento = "";
                                                                aux2 = 1;
                                                                auxporteros = 1;
                                                                String[,] eventosempleado = new string[10, 4];
                                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");

                                                                if (eventosempleado[0, 0] != null)
                                                                {
                                                                    evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                    tipoeven = eventosempleado[0, 1];
                                                                    observaciones = eventosempleado[0, 2];
                                                                    aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                    auxporteros = Int32.Parse(eventosempleado[0, 3]);


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
                                                        }

                                                        hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                        if (hrs_extraJus == TimeSpan.MaxValue)
                                                        {
                                                            hrs_extraJus = hrs_extra;
                                                        }

                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + auxporteros + ",'" + detalle + "','" + evento + ". " + observaciones + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extraJus + "')";
                                                        comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();
                                                        //Debug.WriteLine(query);
                                                        //detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, auxporteros, detalle);
                                                        break;
                                                    case 3:
                                                        aux1 = 0;
                                                        aux2 = 0;
                                                        detalle = "Se encontraron solo 3 marcas";
                                                        int incon3 = 0;
                                                        evento = "";
                                                        hrsdia_laboradas = checadas[2] - checadas[0];
                                                        hrsdia_comedor = TimeSpan.Zero;
                                                        tipoeven = "";
                                                        //  hrs_laboradas = hrs_laboradas + hrsdia_laboradas;
                                                        if (checadas[0].TimeOfDay > tin)
                                                        {
                                                            aux1 = 1;
                                                            incon3 = 1;

                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");
                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                incon3 = Int32.Parse(eventosempleado[0, 3]);

                                                            }
                                                        }
                                                        if (checadas[2].TimeOfDay < tout)
                                                        {
                                                            aux2 = 1;
                                                            incon3 = 1;
                                                            evento = "";
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                incon3 = Int32.Parse(eventosempleado[0, 3]);

                                                            }
                                                        }

                                                        if (hrsdia_laboradas > hrs_dia)
                                                        {
                                                            detalle = "No se registro salida de comedor.";
                                                            hrs_extra = hrsdia_laboradas - hrs_dia;
                                                            hrs_extra = calculahorasextra(hrs_extra);
                                                            incon3 = 1;

                                                        }
                                                        else
                                                        {
                                                            hrs_extra = TimeSpan.Zero;
                                                            //detalle = "No se registro salida final.";
                                                            hrsdia_comedor = checadas[2] - checadas[1];
                                                            //incon3 = 1;
                                                        }
                                                        hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                        if (hrs_extraJus == TimeSpan.MaxValue)
                                                        {
                                                            hrs_extraJus = hrs_extra;
                                                        }
                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + incon3 + ",'" + detalle + "','" + evento + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extraJus + "')";
                                                        comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();
                                                        //Debug.WriteLine(query);
                                                        // detalledias.Rows.Add(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h], 1, 0, aux1, aux2, hrsdia_laboradas, hrsdia_comedor, 1, detalle);
                                                        break;
                                                    case 4:
                                                        int aux3 = 0;
                                                        aux1 = 0;
                                                        aux2 = 0;
                                                        int incon4 = 0;
                                                        evento = "";
                                                        hrsdia_laboradas = checadas[3] - checadas[0];
                                                        hrsdia_comedor = checadas[2] - checadas[1];
                                                        detalle = "Correcto";
                                                        tipoeven = "";
                                                        // hrs_comedor = hrs_comedor + hrsdia_comedor;
                                                        //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                        if (checadas[0].TimeOfDay > tin)
                                                        {
                                                            aux1 = 1;
                                                            aux3 = 1;
                                                            evento = "";
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");
                                                            if (eventosempleado[0, 0] != null)
                                                            {
                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                aux3 = Int32.Parse(eventosempleado[0, 3]);

                                                            }
                                                        }
                                                        if (checadas[3].TimeOfDay < tout)
                                                        {
                                                            aux2 = 1;
                                                            aux3 = 1;
                                                            evento = "";
                                                            String[,] eventosempleado = new string[10, 4];
                                                            eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                            detalle = "No se registró salida final.";
                                                            if (eventosempleado[0, 0] != null)
                                                            {

                                                                evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                tipoeven = eventosempleado[0, 1];
                                                                observaciones = eventosempleado[0, 2];
                                                                aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                aux3 = Int32.Parse(eventosempleado[0, 3]);

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
                                                            //aux3 = 1;
                                                            //detalle = "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada.";
                                                        }
                                                        hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                        if (hrs_extraJus == TimeSpan.MaxValue)
                                                        {
                                                            hrs_extraJus = hrs_extra;
                                                        }
                                                        conexion.abrir();
                                                        query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','" + evento + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extraJus + "')";
                                                        comando = new SqlCommand(query, conexion.con);
                                                        comando.ExecuteNonQuery();
                                                        conexion.cerrar();
                                                        break;
                                                    default:
                                                        if ((checadas.Length - 1) > 4)
                                                        {
                                                            aux3 = 0;
                                                            aux1 = 0;
                                                            aux2 = 0;
                                                            incon4 = 0;
                                                            evento = "";
                                                            hrsdia_laboradas = checadas[checadas.Length - 2] - checadas[0];
                                                            hrsdia_comedor = TimeSpan.Zero;
                                                            detalle = "Se encontrarón más de 4 checadas. Posible permiso dentro de horario laboral.";
                                                            tipoeven = "";
                                                            // hrs_comedor = hrs_comedor + hrsdia_comedor;
                                                            //hrs_laboradas = hrs_laboradas + hrsdia_laboradas;

                                                            if (checadas[0].TimeOfDay > tin)
                                                            {
                                                                aux1 = 1;
                                                                aux3 = 1;
                                                                evento = "";
                                                                String[,] eventosempleado = new string[10, 4];
                                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Retardo");
                                                                if (eventosempleado[0, 0] != null)
                                                                {
                                                                    evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                    tipoeven = eventosempleado[0, 1];
                                                                    observaciones = eventosempleado[0, 2];
                                                                    aux1 = Int32.Parse(eventosempleado[0, 3]);
                                                                    aux3 = Int32.Parse(eventosempleado[0, 3]);


                                                                }
                                                            }
                                                            if (checadas[checadas.Length - 2].TimeOfDay < tout)
                                                            {
                                                                aux2 = 1;
                                                                aux3 = 1;
                                                                evento = "";
                                                                String[,] eventosempleado = new string[10, 4];
                                                                eventosempleado = GetEventos(dtEmpleado.Rows[j]["Badgenumber"].ToString(), datein, datefin, fechas[h], "Salida Temprano");
                                                                detalle = "No se registró salida final.";
                                                                if (eventosempleado[0, 0] != null)
                                                                {
                                                                    evento = fechas[h] + ". " + eventosempleado[0, 0];
                                                                    tipoeven = eventosempleado[0, 1];
                                                                    observaciones = eventosempleado[0, 2];
                                                                    aux2 = Int32.Parse(eventosempleado[0, 3]);
                                                                    aux3 = Int32.Parse(eventosempleado[0, 3]);

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
                                                                aux3 = 1;
                                                                detalle = detalle + "Inconsistencia de horas laboradas son menores que las correspondientes a la jornada.";
                                                            }
                                                            hrs_extraJus = horexjus(dtEmpleado.Rows[j]["Badgenumber"].ToString(), fechas[h]);
                                                            if (hrs_extraJus == TimeSpan.MaxValue)
                                                            {
                                                                hrs_extraJus = hrs_extra;
                                                            }
                                                            conexion.abrir();
                                                            query = "INSERT INTO detalledias values(" + dtEmpleado.Rows[j]["Badgenumber"].ToString() + ",'" + fechas[h] + "',1,0," + aux1 + "," + aux2 + ",'" + hrsdia_laboradas + "','" + hrsdia_comedor + "'," + aux3 + ",'" + detalle + "','" + evento + "','" + hrs_extra + "','" + tipoeven + "','" + hrs_extraJus + "')";
                                                            comando = new SqlCommand(query, conexion.con);
                                                            comando.ExecuteNonQuery();
                                                            conexion.cerrar();
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    marcastop = marcastop + 1;
                    toolStripProgressBar1.Value = marcastop;
                }
                sumaincon();
                actualizain();
                pintagrilla();
                reporteTiemposToolStripMenuItem.Visible = true;
                toolStripComboBox1.Visible = true;
                // pintagrid();
            }
            groupBoxEdi.Enabled = true;
        }
        public void sumaincon()
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleado = new DataTable();
            conexion.abrir();
            string query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HORARIOS.Descripcion FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE HORARIOS.IDGROUP=1 AND HORARIOS.tipohor=1 AND USERINFOCUS.DEFAULTDEPTID <>32 ORDER BY HORARIOS.Descripcion ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtempleado);
            conexion.cerrar();
            //detalledatos.Columns.Clear();
            detalledatos.Rows.Clear();
            //detalledatos.Clear();

            string asisbd, inasisbd, retbd, saltempbd, turnobd, incosisbd;
            int hrslabbd, hrscombd, hrsex, hrsexaprob;
            foreach (DataRow row in dtempleado.Rows)
            {
                asisbd = "";
                inasisbd = "";
                retbd = "";
                saltempbd = "";
                turnobd = "";
                incosisbd = "";
                hrscombd = 0;
                hrscombd = 0;
                hrsex = 0;
                DataTable dtempleadoSUM = new DataTable();
                DataTable dtempleadocom = new DataTable();
                conexion.abrir();
                query = "SELECT SUM(asistencia) as ASISTENCIAS,SUM(inasistencia) as INASISTENCIAS, SUM(retardo) as RETARDOS, SUM(saltemp) as 'SAL.TEMP.',SUM(inconsistencia) as INCONSISTENCIAS,SUM(DATEPART(SECOND, [hlab]) + 60 * DATEPART(MINUTE, [hlab]) + 3600 * DATEPART(HOUR, [hlab] )) as 'H.T. LABORADAS',SUM(DATEPART(SECOND, [hcomedor]) + 60 * DATEPART(MINUTE, [hcomedor]) + 3600 * DATEPART(HOUR, [hcomedor] )) as 'H.T. COMEDOR',SUM(DATEPART(SECOND, [hextra]) + 60 * DATEPART(MINUTE, [hextra]) + 3600 * DATEPART(HOUR, [hextra] )) as 'H. EXTRA',SUM(DATEPART(SECOND, [horexapr]) + 60 * DATEPART(MINUTE, [horexapr]) + 3600 * DATEPART(HOUR, [horexapr] )) as 'H. EXTRA APROB' FROM detalledias where badgenumber=" + row["BADGENUMBER"].ToString() + " AND fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "'";
                //Debug.WriteLine(query);
                SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
                adaptadorsum.Fill(dtempleadoSUM);
                conexion.cerrar();
                if (dtempleadoSUM.Rows.Count > 0)
                {
                    asisbd = dtempleadoSUM.Rows[0]["ASISTENCIAS"].ToString();
                    inasisbd = dtempleadoSUM.Rows[0]["INASISTENCIAS"].ToString();
                    retbd = dtempleadoSUM.Rows[0]["RETARDOS"].ToString();
                    saltempbd = dtempleadoSUM.Rows[0]["SAL.TEMP."].ToString();
                    incosisbd = dtempleadoSUM.Rows[0]["INCONSISTENCIAS"].ToString();

                    if (dtempleadoSUM.Rows[0]["H.T. LABORADAS"].ToString() == "")
                        hrslabbd = 0;
                    else
                        hrslabbd = Int32.Parse(dtempleadoSUM.Rows[0]["H.T. LABORADAS"].ToString());

                    if (dtempleadoSUM.Rows[0]["H.T. COMEDOR"].ToString() == "")
                        hrscombd = 0;
                    else
                        hrscombd = Int32.Parse(dtempleadoSUM.Rows[0]["H.T. COMEDOR"].ToString());

                    if (dtempleadoSUM.Rows[0]["H. EXTRA"].ToString() == "")
                        hrsex = 0;
                    else
                        hrsex = Int32.Parse(dtempleadoSUM.Rows[0]["H. EXTRA"].ToString());

                    if (dtempleadoSUM.Rows[0]["H. EXTRA APROB"].ToString() == "")
                        hrsexaprob = 0;
                    else
                        hrsexaprob = Int32.Parse(dtempleadoSUM.Rows[0]["H. EXTRA APROB"].ToString());


                    turnobd = row["Descripcion"].ToString();


                    DataTable dtcom = new DataTable();
                    string cadenacom = "";
                    conexion.abrir();
                    query = "SELECT comentarios from detalledias where badgenumber=" + row["BADGENUMBER"].ToString() + " AND fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "'";
                    adaptadorsum = new SqlDataAdapter(query, conexion.con);
                    adaptadorsum.Fill(dtcom);
                    conexion.cerrar();
                    foreach (DataRow rowcom in dtcom.Rows)
                    {
                        if (rowcom["comentarios"].ToString() != "" && rowcom["comentarios"].ToString() != null && rowcom["comentarios"].ToString() != "." && rowcom["comentarios"].ToString() != ". ")
                        {
                            cadenacom = rowcom["comentarios"].ToString() + ". " + cadenacom;

                        }
                        //Debug.WriteLine(cadenacom);
                    }

                    detalledatos.Rows.Add(row["BADGENUMBER"].ToString(), row["NAME"].ToString(), asisbd, inasisbd, retbd, saltempbd, incosisbd, CalcularTiempo(hrslabbd), CalcularTiempo(hrscombd), CalcularTiempo(hrsex), CalcularTiempo(hrsexaprob), turnobd, cadenacom);
                    // detalledatos.Rows.Add(row["BADGENUMBER"].ToString(), row["NAME"].ToString(), asisbd, inasisbd, retbd, saltempbd, incosisbd, CalcularTiempo(hrslabbd), CalcularTiempo(hrscombd), 0, turnobd);
                }
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
            conexionbd conexion = new conexionbd();
            DataTable detallediasbd = new DataTable();
            conexion.abrir();
            string query = "SELECT * FROM detalledias where badgenumber=" + dataGridViewDatos.CurrentRow.Cells[0].Value.ToString() + " and fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "' ORDER BY Fecha";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(detallediasbd);
            conexion.cerrar();
            dataGridViewDetalleDias.DataSource = detallediasbd;
            pintagrilla();

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatos.Rows.Count > 0)
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
                oWB = (Excel._Workbook)(oXL.Workbooks.Open(@"C:\Excel\SemanalReporte.xlsx"));
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
            else
            {
                MessageBox.Show("No hay datos para exportar. Genere primero el reporte.");
            }
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
                //pintagrid();
            }
        }
        private String CalcularTiempo(Int32 tsegundos)
        {
            Int32 horas = (tsegundos / 3600);
            Int32 minutos = ((tsegundos - horas * 3600) / 60);
            Int32 segundos = tsegundos - (horas * 3600 + minutos * 60);
            return horas.ToString() + ":" + minutos.ToString() + ":" + segundos.ToString();
        }
        public string[,] GetEventos(string badgenumber, string fecin, string fecfin, string fecac, string tipoev)
        {
            conexionbd conexion = new conexionbd();
            DataTable dtempleadoEventos = new DataTable();
            //DateTime fecini, fecfinal;
            conexion.abrir();
            string query = "DECLARE @FEC DATE; SET @FEC = '" + DateTime.Parse(fecac).ToString("yyyy-MM-dd") + "';SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO, EVENEMP.OBSERVACIONES,EVENTO.Valor FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN = EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER = " + badgenumber + " AND @FEC BETWEEN fecin AND fecfin AND EVENTO.GRUPO = '" + tipoev + "'; ";
  
            string[,] eventos = new string[10, 4];
            SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
            adaptadorsum.Fill(dtempleadoEventos);
            conexion.cerrar();
            if (dtempleadoEventos.Rows.Count > 0)
            {
                eventos[0, 0] = dtempleadoEventos.Rows[0]["Descripcion"].ToString();
                eventos[0, 1] = dtempleadoEventos.Rows[0]["Descripcion"].ToString();
                eventos[0, 2] = dtempleadoEventos.Rows[0]["Observaciones"].ToString();
                eventos[0, 3] = dtempleadoEventos.Rows[0]["Valor"].ToString();
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
                query = "DECLARE @FEC DATE; SET @FEC = '" + fecac + "';SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO, EVENEMP.OBSERVACIONES FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN = EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER = " + badgenumber + " AND @FEC BETWEEN fecin AND fecfin AND EVENTO.descripcion NOT LIKE '%Injustificado%' AND EVENTO.descripcion NOT LIKE '%Injustificada%' ";
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

        private void toolStripTextBoxID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxNombre.Text = "";
                detalledatos.DefaultView.RowFilter = $"ID LIKE '{toolStripTextBoxID.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (this.dataGridViewDatos.Columns[e.ColumnIndex].Name == "Inconsistencias")
            {
                if (e.Value != null)
                {
                    if (e.Value.GetType() != typeof(System.DBNull))
                    {
                        if (e.Value.ToString() == "")
                        {
                            e.Value = 0;
                        }
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
                toolStripTextBoxID.Text = "";
                detalledatos.DefaultView.RowFilter = $"Nombre LIKE '{toolStripTextBoxNombre.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                string query = "delete detalledias from detalledias inner join HOREMPLEADO on detalledias.badgenumber=HOREMPLEADO.Badgenumber inner join HORARIOS on HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR where HORARIOS.idgroup=1 and detalledias.fecha>='" + datein + "' and detalledias.fecha<='" + datefin + "'";
                Debug.WriteLine(query);
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
                dataGridViewDatos.DataSource = null;
                dataGridViewDetalleDias.DataSource = null;
                toolStripProgressBar1.Value = 0;
                reporteTiemposToolStripMenuItem.Visible = false;
                toolStripComboBox1.Visible = false;
                MessageBox.Show("Eliminación de reporte correcto.");
                reporteTiemposToolStripMenuItem.Visible = false;
            }
        }

        private void buttonEditarH_Click(object sender, EventArgs e)
        {

            /*
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query = "INSERT INTO movusuarios values('"+Program.usuario+"','"++"','"++"')";
            SqlCommand comando = new SqlCommand(query, conexion.con);
            comando.ExecuteNonQuery();
            conexion.cerrar();*/
        }

        public string[] CambioTurno(string deptid, TimeSpan[] checadas, string idhoractual)
        {
            conexionbd conexion = new conexionbd();
            DataTable dthors = new DataTable();
            DataTable dtcompara = new DataTable();

            conexion.abrir();

            string query;
            query = "SELECT distinct HOREMPLEADO.ID_HOR FROM USERINFOCus INNER JOIN HOREMPLEADO ON USERINFOCus.Badgenumber=HOREMPLEADO.Badgenumber INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCus.DEFAULTDEPTID=" + deptid + " AND Horarios.idgroup=1 and Horarios.tipohor=1; ";
            //query = "DECLARE @FEC DATE; SET @FEC = '" + fecac + "';SELECT EVENEMP.BADGENUMBER,EVENEMP.ID_EVEN,EVENEMP.FECIN,EVENEMP.FECFIN,EVENTO.DESCRIPCION,EVENTO.GRUPO, EVENEMP.OBSERVACIONES FROM EVENTO INNER JOIN EVENEMP ON EVENTO.ID_EVEN = EVENEMP.ID_EVEN  WHERE EVENEMP.BADGENUMBER = " + badgenumber + " AND @FEC BETWEEN fecin AND fecfin AND EVENTO.GRUPO = '" + tipoev + "'; ";
            string[] newparametros = new string[7];
            SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
            adaptadorsum.Fill(dthors);
            //string nuevohorario;

            foreach (DataRow row in dthors.Rows)
            {
                for (int i = 0; i < checadas.Length; i++)
                {
                    query = "DECLARE @CHECADA sql_variant; SET @CHECADA  = '" + checadas[i] + "';SELECT DISTINCT HORARIOS.ID_HOR,HORARIOS.HOR_IN,HORARIOS.HOR_INTURNO,HORARIOS.HOR_OUT,HORARIOS.HRS_DIA,HORARIOS.HRS_SEMANA,HORARIOS.Descripcion FROM HORARIOS INNER JOIN HOREMPLEADO ON HORARIOS.ID_HOR=HOREMPLEADO.ID_HOR INNER JOIN USERINFOCus ON HOREMPLEADO.Badgenumber=USERINFOCus.Badgenumber WHERE (@CHECADA BETWEEN HOR_IN AND HOR_OUT) AND HORARIOS.idgroup=1 AND HORARIOS.tipohor=1 AND USERINFOCus.DEFAULTDEPTID=" + deptid + " AND HORARIOS.ID_HOR<>" + idhoractual + ";";
                    adaptadorsum = new SqlDataAdapter(query, conexion.con);
                    adaptadorsum.Fill(dtcompara);
                    if (dtcompara.Rows.Count > 0)
                    {
                        newparametros[0] = dtcompara.Rows[0]["ID_HOR"].ToString();
                        newparametros[1] = dtcompara.Rows[0]["HOR_IN"].ToString();
                        newparametros[2] = dtcompara.Rows[0]["HOR_INTURNO"].ToString();
                        newparametros[3] = dtcompara.Rows[0]["HOR_OUT"].ToString();
                        newparametros[4] = dtcompara.Rows[0]["HRS_DIA"].ToString();
                        newparametros[5] = dtcompara.Rows[0]["HRS_SEMANA"].ToString();
                        newparametros[6] = dtcompara.Rows[0]["Descripcion"].ToString();
                        break;
                    }
                }
            }
            conexion.cerrar();
            return newparametros;
        }

        private void reporteTiemposToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //ExportarDatos(dataGridViewDatos);
            // C:\Users\userf\source\repos\EmpManagement\EmpManagement\documentos\gafete.xlsx
            conexionbd conexion = new conexionbd();

            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
            Excel.Range oRngComent;
            int inicio = 24;
            int inicioid = 7;
            int inicioturno = 6;
            int iniciofechas = 9;
            int iniciochecadas = 9;
            int contadorcolumnas = 0;
            int iniciodetalle = 14;
            int aumentofilas = 0;
            int auxfilas = 0;
            int iniciovacaciones = 0;
            int iniciopermisos = 0;
            int k = 0;//variable para aumento de filas
            int h = 0;//variable para registro de comentarios
            int iniciocomentarios = 8;


            //string[,] saNames = new string[dataGridViewDatos.Rows.Count, 12];
            //Debug.WriteLine(dataGridViewDatos.Rows.Count);


            //Start Excel and get Application object.
            oXL = new Excel.Application();
            // oXL.Visible = true;

            string oldName = @"C:\Excel\ReporteTiempos.xlsx";
            string newName = @"C:\Excel\ReporteTiempos" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xlsx";
            System.IO.File.Copy(oldName, newName);

            //Get a new workbook.
            oWB = (Excel._Workbook)(oXL.Workbooks.Open(newName));
            // oWB = (Excel._Workbook)(oXL.Workbooks.Add());
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;
            char[] array1 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };

            DataTable dtuserinfo = new DataTable();
            conexion.abrir();
            //string query = "select detalledias.*,DEPARTMENTS.DEPTNAME from detalledias inner join USERINFOCus on detalledias.badgenumber=USERINFOCus.Badgenumber inner join DEPARTMENTS on USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID where detalledias.fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "' and userinfocus.defaultdeptid=" + toolStripComboBox1.ComboBox.SelectedValue.ToString() + " ORDER BY detalledias.fecha";
            string query = "SELECT USERINFOCUS.Badgenumber,USERINFOCUS.Name,HORARIOS.Descripcion from USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER=HOREMPLEADO.BADGENUMBER INNER JOIN HORARIOS ON HOREMPLEADO.ID_HOR=HORARIOS.ID_HOR WHERE USERINFOCUS.DEFAULTDEPTID=" + toolStripComboBox1.ComboBox.SelectedValue.ToString() + " AND HORARIOS.IDGROUP=1 AND HORARIOS.TIPOHOR=1 ORDER BY HORARIOS.ID_HOR ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtuserinfo);
   
            oSheet.get_Range("B3").Value2 = toolStripComboBox1.ComboBox.Text.ToString();
            // oSheet.Cells[3,8]= dateTimePickerIni.Value.ToShortDateString();

            oSheet.get_Range("H3").Value2 = dateTimePickerIni.Value.ToString("MM-dd-yyyy");
            oSheet.get_Range("H4").Value2 = dateTimePickerFin.Value.ToShortDateString();

            for (int i = 0; i < dtuserinfo.Rows.Count - 1; i++)
            {
                //|Debug.WriteLine(inicio+contadorcolumnas);
                Excel.Range source = oSheet.Range["A6:I22"];
                Excel.Range dest = oSheet.Range["A" + (inicio + contadorcolumnas).ToString()];
                source.Copy(dest);
                contadorcolumnas = contadorcolumnas + 18;
            }
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = dtuserinfo.Rows.Count;
            toolStripProgressBar1.Value = 0;
            contadorcolumnas = 0;
            int cuentausuarios = 0;
            foreach (DataRow row in dtuserinfo.Rows)
            {
                DataTable detalledias = new DataTable();
                oSheet.get_Range("B" + (inicioid + contadorcolumnas).ToString()).Value2 = row["Badgenumber"].ToString();
                Match m = Regex.Match(row["Descripcion"].ToString(), "(\\d+)");
                string num = string.Empty;
                num = m.Value;
                oSheet.get_Range("I" + (inicioturno + contadorcolumnas).ToString()).Value2 = num;
                oSheet.get_Range("D" + (inicioid + contadorcolumnas).ToString()).Value2 = row["Name"].ToString();
                query = "select detalledias.* from detalledias inner join USERINFOCus on detalledias.badgenumber=USERINFOCus.Badgenumber inner join DEPARTMENTS on USERINFOCus.DEFAULTDEPTID=DEPARTMENTS.DEPTID where detalledias.fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "' and userinfocus.defaultdeptid=" + toolStripComboBox1.ComboBox.SelectedValue.ToString() + " and detalledias.badgenumber=" + row["Badgenumber"].ToString() + " ORDER BY detalledias.fecha";
                adaptador = new SqlDataAdapter(query, conexion.con);
                adaptador.Fill(detalledias);
                int cuentacolumnas = 0;
                List<int> marianas = new List<int>();

                foreach (DataRow row1 in detalledias.Rows)
                {

                    if (row1["comentarios"].ToString() != "" && row1["comentarios"].ToString() != ". ")
                    {
                        oSheet.get_Range("J" + (iniciocomentarios + contadorcolumnas + h).ToString()).Value2 = row1["comentarios"].ToString();
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

                    if ((checadas.Length - 1) > 4)
                    {
                        aumentofilas = (checadas.Length - 1) - 4;
                        auxfilas = aumentofilas;
                    }
                    else
                    {
                        aumentofilas = 0;
                    }
                    //contadorcolumnas = contadorcolumnas + auxfilas;

                    if (aumentofilas > k)
                    {
                        for (int i = 0; i <= (aumentofilas - 1 - k); i++)
                        {
                            oSheet.Range["A" + (iniciochecadas + 4 + contadorcolumnas + k).ToString()].EntireRow.Insert();
                        }

                    }
                    //oSheet.get_Range(array1[cuentacolumnas]+(iniciochecadas+contadorcolumnas).ToString(), array1[cuentacolumnas]+ (iniciochecadas + contadorcolumnas + checadas.Length).ToString()).Value2 = checadas;
                    for (int i = 0; i < checadas.Length - 1; i++)
                    {
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciochecadas + i).ToString()).Value2 = checadas[i].ToString();
                    }
                    //oSheet.get_Range("A10").Value2 = checadas[0].ToString();

                    //Registra detalledias
                    for (int i = 0; i < 6; i++)
                    {
                        if (auxfilas > k)
                        {
                            oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + (auxfilas - k) + i).ToString()).Value2 = row1[i + 2].ToString();
                        }
                        else
                        {
                            oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + auxfilas + i).ToString()).Value2 = row1[i + 2].ToString();
                        }

                    }
                    if (auxfilas > k)
                    {
             
                       // oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + (auxfilas - k) + 6).ToString()).Value2 = row1[11].ToString();
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + (auxfilas - k) + 6).ToString()).Value2 = row1[13].ToString();
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + (auxfilas - k) + 7).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "Vacaciones");
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + (auxfilas - k) + 8).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "");

                    }
                    else
                    {
                        //oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + auxfilas + 6).ToString()).Value2 = row1[11].ToString();
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + auxfilas + 6).ToString()).Value2 = row1[13].ToString();
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + auxfilas + 7).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "Vacaciones"); ;
                        oSheet.get_Range(array1[cuentacolumnas] + (contadorcolumnas + iniciodetalle + auxfilas + 8).ToString()).Value2 = GetEventosReporte(row["Badgenumber"].ToString(), DateTime.Parse(row1["fecha"].ToString()).ToString("MM-dd-yyyy"), "");
                    }
                    marianas.Add(k);
                    cuentacolumnas = cuentacolumnas + 1;
                    k = auxfilas;
                }
                auxfilas = 0;
                k = 0;
                aumentofilas = 0;
                cuentacolumnas = 0;
                oSheet.get_Range("H" + (iniciocomentarios + contadorcolumnas).ToString(), "I" + (iniciocomentarios + contadorcolumnas + marianas.Max() + 4).ToString()).Merge();
                contadorcolumnas = contadorcolumnas + marianas.Max() + 18;
                h = 0;
                cuentausuarios++;
                toolStripProgressBar1.Value = cuentausuarios;
            }

            MessageBox.Show("Reporte Terminado.");

            oXL.Visible = true;
            oXL.UserControl = true;
            conexion.cerrar();


            /* catch (Exception theException)
             {
                 String errorMessage;
                 errorMessage = "Error: ";
                 errorMessage = String.Concat(errorMessage, theException.Message);
                 errorMessage = String.Concat(errorMessage, " Line: ");
                 errorMessage = String.Concat(errorMessage, theException.Source);

                 MessageBox.Show(errorMessage, "Error");
             }*/


        }

        private float CalcularTiempoFloat(Int32 tsegundos)
        {
            float rest = 0;
            Int32 horas = (tsegundos / 3600);
            Int32 minutos = ((tsegundos - horas * 3600) / 60);
            // Int32 segundos = tsegundos - (horas * 3600 + minutos * 60);
            if (minutos >= 30)
            {
                rest = 0.5f;
            }
            else
            {
                rest = 0f;
            }
            return horas + rest;
        }

        public TimeSpan horexjus(string badgenumber, string fecha)
        {
            SqlDataAdapter adaptadorhorex = new SqlDataAdapter();
            DataTable dthorex = new DataTable();
            TimeSpan horex;
            string queryhor = "";
            conexionbd conexion = new conexionbd();
            int iden = 0;
            conexion.abrir();
            queryhor = "SELECT horex FROM HOREXJUST where badgenumber=" + badgenumber + " and fechadetalle='" + fecha + "';";
            adaptadorhorex = new SqlDataAdapter(queryhor, conexion.con);
            adaptadorhorex.Fill(dthorex);
            conexion.cerrar();
            if (dthorex.Rows.Count > 0)
            {
                horex = TimeSpan.Parse(dthorex.Rows[0]["horex"].ToString());
            }
            else
            {
                horex = TimeSpan.MaxValue;
            }
            return horex;
        }
        private void reporteFormatoNominasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
            conexionbd conexion = new conexionbd();
            DataTable dtempleado = new DataTable();
            int contador = 0;
            conexion.abrir();
            string query = "SELECT USERINFOCUS.BADGENUMBER,USERINFOCUS.NAME,HORARIOS.Descripcion FROM(USERINFOCUS INNER JOIN HOREMPLEADO ON USERINFOCUS.BADGENUMBER = HOREMPLEADO.BADGENUMBER)INNER JOIN HORARIOS ON HORARIOS.ID_HOR = HOREMPLEADO.ID_HOR WHERE HORARIOS.IDGROUP=1 AND HORARIOS.tipohor=1 AND USERINFOCUS.DEFAULTDEPTID <>32 ORDER BY HORARIOS.Descripcion ";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtempleado);
            conexion.cerrar();
            //detalledatos.Columns.Clear();
            //detalledatos.Rows.Clear();
            //detalledatos.Clear();

            oXL = new Excel.Application();
            oXL.Visible = true;
            //Get a new workbook.
            oWB = (Excel._Workbook)(oXL.Workbooks.Add());
            // oWB = (Excel._Workbook)(oXL.Workbooks.Add());
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;
            string[,] saNames = new string[dtempleado.Rows.Count, 10];

            string asisbd, inasisbd, retbd, saltempbd, turnobd, incosisbd;
            float hrslabbd, hrscombd, hrsex;
            foreach (DataRow row in dtempleado.Rows)
            {
                asisbd = "";
                inasisbd = "";
                retbd = "";
                saltempbd = "";
                turnobd = string.Empty; ;
                incosisbd = "";
                hrscombd = 0;
                hrscombd = 0;
                hrsex = 0;
                DataTable dtempleadoSUM = new DataTable();
                DataTable dtempleadocom = new DataTable();
                conexion.abrir();
                query = "SELECT SUM(asistencia) as ASISTENCIAS,SUM(inasistencia) as INASISTENCIAS, SUM(retardo) as RETARDOS, SUM(saltemp) as 'SAL.TEMP.',SUM(DATEPART(SECOND, [hlab]) + 60 * DATEPART(MINUTE, [hlab]) + 3600 * DATEPART(HOUR, [hlab] )) as 'H.T. LABORADAS',SUM(DATEPART(SECOND, [hcomedor]) + 60 * DATEPART(MINUTE, [hcomedor]) + 3600 * DATEPART(HOUR, [hcomedor] )) as 'H.T. COMEDOR',SUM(DATEPART(SECOND, [hextra]) + 60 * DATEPART(MINUTE, [hextra]) + 3600 * DATEPART(HOUR, [hextra] )) as 'H. EXTRA',(sum(asistencia)-sum(retardo)) AS PUNTUALIDAD,SUM(inconsistencia) as INCONSISTENCIAS FROM detalledias where badgenumber=" + row["BADGENUMBER"].ToString() + " AND fecha BETWEEN '" + dateTimePickerIni.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePickerFin.Value.ToString("yyyy-MM-dd") + "'";
                //Debug.WriteLine(query);
                SqlDataAdapter adaptadorsum = new SqlDataAdapter(query, conexion.con);
                adaptadorsum.Fill(dtempleadoSUM);
                conexion.cerrar();

                //Debug.WriteLine(dataGridViewDatos.Rows.Count);

                asisbd = dtempleadoSUM.Rows[0]["ASISTENCIAS"].ToString();
                inasisbd = dtempleadoSUM.Rows[0]["INASISTENCIAS"].ToString();
                retbd = dtempleadoSUM.Rows[0]["RETARDOS"].ToString();
                saltempbd = dtempleadoSUM.Rows[0]["SAL.TEMP."].ToString();
                incosisbd = dtempleadoSUM.Rows[0]["INCONSISTENCIAS"].ToString();
                Match m = Regex.Match(row["Descripcion"].ToString(), "(\\d+)");
                turnobd = m.Value;
                if (dtempleadoSUM.Rows[0]["H.T. LABORADAS"].ToString() == "")
                    hrslabbd = 0;
                else
                    hrslabbd = CalcularTiempoFloat(Int32.Parse(dtempleadoSUM.Rows[0]["H.T. LABORADAS"].ToString()));

                if (dtempleadoSUM.Rows[0]["H.T. COMEDOR"].ToString() == "")
                    hrscombd = 0;
                else
                    hrscombd = CalcularTiempoFloat(Int32.Parse(dtempleadoSUM.Rows[0]["H.T. COMEDOR"].ToString()));

                if (dtempleadoSUM.Rows[0]["H. EXTRA"].ToString() == "")
                    hrsex = 0;
                else
                    hrsex = CalcularTiempoFloat(Int32.Parse(dtempleadoSUM.Rows[0]["H. EXTRA"].ToString()));

                saNames[contador, 0] = row["BADGENUMBER"].ToString();  //row.Cells["Asistencias"].Value.ToString();
                saNames[contador, 1] = asisbd; //row.Cells["Asistencias"].Value.ToString();
                saNames[contador, 2] = inasisbd; //row.Cells["Inasistencias"].Value.ToString();
                saNames[contador, 3] = retbd; //row.Cells["Retardos"].Value.ToString();
                saNames[contador, 4] = saltempbd; //row.Cells["Sal. Temp."].Value.ToString();
                saNames[contador, 5] = incosisbd; //row.Cells["Inconsistencias"].Value.ToString();
                saNames[contador, 6] = hrslabbd.ToString(); //row.Cells["H.T. Laboradas"].Value.ToString();
                saNames[contador, 7] = hrscombd.ToString(); //row.Cells["H.T. Comedor"].Value.ToString();
                saNames[contador, 8] = hrsex.ToString();//row.Cells["H. Extras"].Value.ToString();
                saNames[contador, 9] = turnobd; //row.Cells["Turno"].Value.ToString();
                //saNames[contador, 9] = row.Cells["Comentarios"].Value.ToString();

                contador = contador + 1;
            }
            contador = 0;
            oSheet.get_Range("A1", "I" + dtempleado.Rows.Count.ToString()).Value2 = saNames;
            oSheet.get_Range("A1", "I" + dtempleado.Rows.Count.ToString()).NumberFormat = "#0.00";
            //oSheet.get_Range("A").Value2 = dataGridViewDatos.CurrentRow.Cells["PUESTO"].Value.ToString();
            oXL.Visible = true;
            oXL.UserControl = true;
        }

        private void buttonEdHE_Click(object sender, EventArgs e)
        {

            HE frmjust = new HE();
            frmjust.FormClosed += update_formCloed;
            frmjust.labelfecin.Text = dateTimePickerIni.Value.ToString("yyyy-MM-dd");
            frmjust.labelfecfin.Text = dateTimePickerFin.Value.ToString("yyyy-MM-dd");
            frmjust.labelid.Text = dataGridViewDetalleDias.CurrentRow.Cells["Badgenumber"].Value.ToString();
            frmjust.labelnom.Text = dataGridViewDatos.CurrentRow.Cells["Nombre"].Value.ToString();
            frmjust.Show();
            /* frmjust.labelNombre.Text = dataGridViewDatos.CurrentRow.Cells["Nombre"].Value.ToString();
             int incon = Int32.Parse(dataGridViewDetalleDias.CurrentCell.ColumnIndex.ToString());
             int num = Int32.Parse(dataGridViewDatos.CurrentCell.Value.ToString());
             string inconsistenciacom = "";*/

        }
    }
}
