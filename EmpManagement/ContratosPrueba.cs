using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ip = iTextSharp.text.pdf;
using it = iTextSharp.text;

namespace EmpManagement
{
    public partial class ContratosPrueba : Form
    {
        public ContratosPrueba()
        {
            InitializeComponent();
        }

        private void ContratosPrueba_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            conexion.abrir();
            conexion.cerrar();
            PanAcuerdConfi.Visible = false;
            PanAvPriv.Visible = false;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            String pdfTemplate = @"C:\Users\userf\Documents\ASC\AVISODEPRIVACIDAD.pdf";//Ruta de inicio (de donde jala el archivo y el nombre del archivo)
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            AcroFields af = pdfReader.AcroFields;
            List<string> campos = new List<string>();
            foreach (KeyValuePair<string, AcroFields.Item> kvp in af.Fields)
            {
                string fieldName = kvp.Key.ToString();
                string fieldValue = af.GetField(kvp.Key.ToString());
                Console.WriteLine(fieldName + "" + fieldValue);
                campos.Add(fieldName + "" + fieldValue);
            }
            File.WriteAllLines("campos.txt", campos);

            string newFile = @"C:\Users\userf\Documents\ASC\ContratEx\newcontrat.pdf";//Ruta final o de guardado
            pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            pdfFormFields.SetField("NomCordinador", "Ing. Selma Ramirez");
            pdfFormFields.SetField("FecFirm", "25-03-2020");
            pdfFormFields.SetField("NomCord", "Ing. Selma Ramirez");
            pdfFormFields.SetField("NomTrab", "Angel Soto Trejo");

            pdfStamper.FormFlattening = false;
            pdfStamper.Close();
        }

        private void TipoCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoCon.SelectedIndex != -1)
            {
                MessageBox.Show(TipoCon.SelectedIndex.ToString());
            }
            if (TipoCon.SelectedIndex == 0)
            {
                /*
                 NomCordinador
                 FecFirm
                 NomCord
                 NomTrab
                 */

               PanAcuerdConfi.Visible = false;
                PanAvPriv.Visible = true;

            }
            if (TipoCon.SelectedIndex == 1)
            {
                /*
                    Nacionalidad
                    Edad
                    EstCivil
                    NSS
                    Dom
                    Trabajador
                    RFC
                    CURP
                    Domicilio
                    NoFolio
                    Servicio
                    Puesto
                    Fecin
                    Fecfin
                    FecFirm
                    NombreNew
                 
                 */
                PanAcuerdConfi.Visible = true;
                PanAvPriv.Visible = false;

            }
            if (TipoCon.SelectedIndex == 2)
            {
                /*
                    NombreEmp
                    Nacionalidad
                    Edad
                    EstCivil
                    NSS
                    Dom
                    Fecini
                    Fecfin
                    Puesto
                    Horarioantescomida
                    Horariodespuescomida
                    DiasentreSem
                    HorarioSabado
                    Sueldo
                    Sueldoletra
                    Fechoy
                    Cargo
                    Cargo2
                    Diahoy
                    Añoact
                    MesAct
                    NombreEmple
                    NombreEmpleador
                    NombreTest1
                    NombreTest2
                 */
                PanAcuerdConfi.Visible = false;
                PanAvPriv.Visible = false;

            }

        }

       
    }
}
