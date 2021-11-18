using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EmpManagement
{
    public partial class prueba : Form
    {
        public prueba()
        {
            InitializeComponent();
        }

        private void prueba_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fec = "2021-09-04 13:39:35.000";
            MessageBox.Show(DateTime.Parse(fec).ToString("MM-dd-yyyy HH:mm"));
        }
    }
}
