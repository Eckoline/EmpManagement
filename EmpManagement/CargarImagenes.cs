using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace EmpManagement
{
    public partial class CargarImagenes : Form
    {
        public CargarImagenes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] imagenes = Directory.GetFiles(@folderBrowserDialog1.SelectedPath+@"\");
                byte[] byteArrayImagen = null;
                List<Image> imgList = new List<Image>();
                conexionbd conexion = new conexionbd();
                conexion.abrir();
                foreach (string imagen in imagenes)
                {
                    string badgenumber,extension;
                    imgList.Add(Image.FromFile(imagen));
                    pictureBox1.Load(imagen);
                    badgenumber = Path.GetFileName(imagen);
                    Debug.WriteLine(badgenumber);
                    extension= Path.GetExtension(imagen);
                    var replacement = badgenumber.Replace(extension, "");
                    Debug.WriteLine(replacement);
                    byteArrayImagen = ImageToByteArray(pictureBox1.Image);
                    string query = "UPDATE USERINFOCUS SET foto=@imagen where badgenumber=" + replacement;
                    SqlCommand comando = new SqlCommand(query, conexion.con);
                    comando.Parameters.AddWithValue("@imagen", byteArrayImagen);
                    comando.ExecuteNonQuery();
                }
                conexion.cerrar();
            }
        }
        private void CargarImagenes_Load(object sender, EventArgs e)
        {
            DataTable dtvalid = new DataTable();
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            string query = "select subclasificacion from users where nombre='" + Program.usuario + "' and subclasificacion is not null";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion.con);
            adaptador.Fill(dtvalid);
            if (dtvalid.Rows.Count > 0)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
            conexion.cerrar();
        }

        public byte[] ImageToByteArray(System.Drawing.Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
