using LabWeb.Conexion;
using LabWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace LabWeb.Data
{
    public class ProductDAO
    {
        ConexionBD conexion;

        public ProductDAO()
        {
            conexion = new ConexionBD();
        }

        public List<Products> getProductos()
        {
            Products product;
            List<Products> listaProducts = new List<Products>();
            if(conexion.Conectar()=="True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT ProductID, ProductName, UnitPrice, QuantityPerUnit FROM Products");
                using (SqlCommand command = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            product = new Products();
                            product.ProductID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
                            product.ProductName = reader.IsDBNull(1) ? "" : reader.GetValue(1).ToString();
                            product.UnitPrice = decimal.Parse(reader.IsDBNull(2) ? "" : reader.GetValue(2).ToString());
                            product.QuantityPerUnit = reader.IsDBNull(3) ? "" : reader.GetValue(3).ToString();
                            listaProducts.Add(product);
                        }
                    }
                }
            }
            return listaProducts;
        }
    }
}