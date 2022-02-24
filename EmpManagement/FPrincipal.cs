using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace EmpManagement
{
    public partial class FPrincipal : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        public static void SetTreeViewTheme(IntPtr treeHandle)
        {
            SetWindowTheme(treeHandle, "explorer", null);
        }

      

        public FPrincipal()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            this.BringToFront();
        }


        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
            this.PanelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Seguro que desea salir?", "Salir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.OK)
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.GetType() == typeof(DetalleMarcaciones))
                    {
                        frm.Close();
                        break;
                    }
                }
                this.Close();

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Capturar posicion y tamaño antes de maximizar para restaurar
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FPrincipal_Load(object sender, EventArgs e)
        {
           
            switch (Int32.Parse(labelClase.Text))
            {
                case 1:

                    break;
                case 2:
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    break;
                case 3:
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    break;
                case 4:
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    break;
                case 5:
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    break;
                case 6:
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    break;
                case 7:
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    break;
                case 8:
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    break;
                case 9:
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[0].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    treeView1.Nodes[0].Nodes[1].Remove();
                    break;
            }
            SetTreeViewTheme(treeView1.Handle);
            /*try
            {
                conexionbd conexion = new conexionbd();
                conexion.abrir();
                string query = @"BACKUP DATABASE [datosrh] TO  DISK = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\Backup\datosrh.bak' WITH NOFORMAT, INIT,  NAME = N'datosrh-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                SqlCommand comando = new SqlCommand(query, conexion.con);
                comando.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión " + ex.Message);
            }*/
         
        }


        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pictureBox4.Visible = false;
            pictureBox5.Visible = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            pictureBox5.Visible = false;
            pictureBox4.Visible = true;
        }

        //METODO PARA ABRIR FORMULARIOS DENTRO DEL PANEL
        public void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario =panelFormularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
            //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                //formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelFormularios.Controls.Add(formulario);
                panelFormularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                //formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //MessageBox.Show(e.Node.Tag.ToString());
            //MessageBox.Show(e.Node.Index.ToString());
            switch (e.Node.Text)
            {
                case "Directorio Empleados":
                    AbrirFormulario<Directorio>();
                    break;
                case "Marcaciones":
                    AbrirFormulario<Marcaciones>();
                    break;
                case "Semanal":
                    AbrirFormulario<ReporteSemanal>();
                    break;
                case "Quincenal":
                    AbrirFormulario<ReporteQuincenal>();
                    break;
                case "Gafete":
                    AbrirFormulario<Gafete>();
                    break;
                case "Cumpleañeros":
                    AbrirFormulario<Cumple>();
                    break;
                case "Diseño Rol de Turnos":
                    AbrirFormulario<RolTurnos>();
                    break;
                case "Rol de Turnos Actual":
                    AbrirFormulario<prueba>();
                    break;
                case "Conexión Reloj":
                    AbrirFormulario<AdminReloj>();
                    break;
                case "Solicitar Capacitación":
                    AbrirFormulario<Capacitacion>();
                        break ;
                case "Estatus":
                    AbrirFormulario<SolicitudesCap>();

                    break;
                case "Días Festivos":
                    AbrirFormulario<DiaFestivo>();
                    break;
                case "Horarios":
                    AbrirFormulario<Horarios>();
                    break;

                case "Asignación de Eventos":
                    AbrirFormulario<VacOIncapacidad>();
                    break;

                case "Estatus Eventos":
                    AbrirFormulario<EstatusEventos>();
                    break;

                case "Edición Eventos":
                    AbrirFormulario<EdicionEven>();
                    break;
                case "Empleados":
                    AbrirFormulario<RegistrarEmpleado>();
                    break;
                case "Carga Imagen":
                    AbrirFormulario<CargarImagenes>();
                    break;

                /*  case "Admin Reloj":
                      AbrirFormulario<AdminReloj>();
                      break;
                */
                case "Mis Capacitaciones":
                    AbrirFormulario<MisCap>();
                    break;
                case "root":
                    AbrirFormulario<Bajas>();
                    break;

            }

        }
    }
}
