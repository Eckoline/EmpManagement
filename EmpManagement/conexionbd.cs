﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace EmpManagement
{
    class conexionbd
    {
        string cadena = "Data Source=192.168.0.11,1433;Initial Catalog=att;User ID=conexionrh;Password=3530c9b32D";
        public SqlConnection con = new SqlConnection();
        public conexionbd()
        {
            con.ConnectionString = cadena;
        }
        public void abrir()
        {
            try
            {
                con.Open();
                Console.WriteLine("Conexion Exitosa");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error en la conexión "+ex.Message);
            }
        }

        public void cerrar()
        {
                con.Close();
        }
    }
}
