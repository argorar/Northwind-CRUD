using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabWeb.Conexion
{
    public class ConexionBD
    {
        SqlConnection conexion;

        public ConexionBD()
        {
            this.conexion = new SqlConnection("Data Source = LOCALHOST; Initial Catalog = Northwind; user id=admin;password=admin");
        }
        public string Conectar()
        {
            try
            {
                conexion.Open();
                return "True";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }

        }

        public SqlConnection getConn()
        {
            return this.conexion;
        }
    }
}