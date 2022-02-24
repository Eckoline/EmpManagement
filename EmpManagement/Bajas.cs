using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;

namespace EmpManagement
{
    public partial class Bajas : Form
    {
        public Bajas()
        {
            InitializeComponent();
        }
        public SDKHelper SDK = new SDKHelper();
        private void button1_Click(object sender, EventArgs e)
        {
            int rel,get;
            int newbad;
            Boolean readall;
            List<int> badgenumbers = new List<int>();
            rel = SDK.sta_ConnectTCP(listBox1, "192.168.1.201", "4370", "0");
            if (rel == 1)
            {
                MessageBox.Show("Conexión Realizada con Éxito");
                readall = SDK.axCZKEM1.ReadAllUserID(0);
                if (readall)
                {
                    get = SDK.sta_GetAllUserID(true,comboBox1,comboBox2,comboBox3,comboBox4,comboBox5,textBox1,comboBox6);  
                    
                    for(int i=0; i < comboBox1.Items.Count; i++)
                    {
                        Debug.WriteLine(comboBox1.Items[i]);
                        badgenumbers.Insert(i,Int32.Parse(comboBox1.Items[i].ToString()));
                    }

                    badgenumbers.Sort();
                    Debug.WriteLine("Acomodados");
                    badgenumbers.ForEach((v) => Debug.WriteLine("Element = {0}", v));
                    newbad = badgenumbers.Last()+1;
                    Debug.WriteLine(newbad);
                    textBox2.Text = newbad.ToString();
                }
            }
            else
            {
                MessageBox.Show("No se pudo realizar la conexión, compruebe su conexión a internet");
     
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SDK.sta_SetUserInfo(listBox1, textBox2, textBox3, comboBox7,textBox4,textBox5);
        }
    }
}
