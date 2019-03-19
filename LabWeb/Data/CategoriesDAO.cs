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
    public class CategoriesDAO
    {
        ConexionBD conexion;
        NorthwindEntities db;

        public CategoriesDAO()
        {
            conexion = new ConexionBD();
            db = new NorthwindEntities();
        }

        public List<Categories> consultarCategorias()
        {
            List<Categories> listaCartegorias = new List<Categories>();
            Categories categoria;

            if (conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT CategoryID, CategoryName, Description ");
                builder.Append("FROM Categories WHERE b_logiv = 0");

                using (SqlCommand command = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoria = new Categories();
                            categoria.CategoryID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
                            categoria.CategoryName = reader.IsDBNull(1) ? "" :reader.GetValue(1).ToString();
                            categoria.Description = reader.IsDBNull(2) ? "" : reader.GetValue(2).ToString();
                            listaCartegorias.Add(categoria);
                        }
                    }
                }

                foreach (Categories categories in listaCartegorias)
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("SELECT c.CategoryID, p.ProductID, p.ProductName, p.UnitPrice FROM Categories c, Products p ");
                    builder2.Append("WHERE c.CategoryID = @id AND c.CategoryID = p.CategoryID");

                    using (SqlCommand command2 = new SqlCommand(builder2.ToString(), conexion.getConn()))
                    {
                        Products productos;
                        categories.Products = new List<Products>();
                        command2.Parameters.AddWithValue("@id", categories.CategoryID);
                        using (IDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                productos = new Products();
                                productos.CategoryID = int.Parse(reader2.IsDBNull(0) ? "" : reader2.GetValue(0).ToString());
                                productos.ProductID = int.Parse(reader2.IsDBNull(1) ? "" : reader2.GetValue(1).ToString());
                                productos.ProductName = reader2.IsDBNull(2) ? "" : reader2.GetValue(2).ToString();///!!!!!!!!!
                                productos.UnitPrice = decimal.Parse(reader2.IsDBNull(3) ? "" : reader2.GetValue(3).ToString());
                                categories.Products.Add(productos);
                            }
                        }
                    }
                }
            }
            return listaCartegorias;
        }

        public List<Categories> obtenerCategoriasPorID(int id)
        {
            List<Categories> listaCartegorias = new List<Categories>();
            Categories categoria;

            if (conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT TOP(50) CategoryID, CategoryName, Description ");
                builder.Append("FROM Categories WHERE b_logiv = 0 AND CategoryID LIKE '%" + id + "%'");

                using (SqlCommand command = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoria = new Categories();
                            categoria.CategoryID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
                            categoria.CategoryName = reader.IsDBNull(1) ? "" : reader.GetValue(1).ToString();
                            categoria.Description = reader.IsDBNull(2) ? "" : reader.GetValue(2).ToString();
                            listaCartegorias.Add(categoria);
                        }
                    }
                }

                foreach (Categories categories in listaCartegorias)
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("SELECT c.CategoryID, p.ProductID, p.ProductName, p.UnitPrice FROM Categories c, Products p ");
                    builder2.Append("WHERE c.CategoryID = @id AND c.CategoryID = p.CategoryID");

                    using (SqlCommand command2 = new SqlCommand(builder2.ToString(), conexion.getConn()))
                    {
                        Products productos;
                        categories.Products = new List<Products>();
                        command2.Parameters.AddWithValue("@id", categories.CategoryID);
                        using (IDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                productos = new Products();
                                productos.CategoryID = int.Parse(reader2.IsDBNull(0) ? "" : reader2.GetValue(0).ToString());
                                productos.ProductID = int.Parse(reader2.IsDBNull(1) ? "" : reader2.GetValue(1).ToString());
                                productos.ProductName = reader2.IsDBNull(2) ? "" : reader2.GetValue(2).ToString();///!!!!!!!!!
                                productos.UnitPrice = decimal.Parse(reader2.IsDBNull(3) ? "" : reader2.GetValue(3).ToString());
                                categories.Products.Add(productos);
                            }
                        }
                    }
                }
            }
            return listaCartegorias;
        }

        public int eliminarCategoria(int id)
        {
            int cod = 0;
            if (conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("UPDATE Categories SET b_logiv = 1 ");
                builder.Append("WHERE CategoryID = @id");

                using (SqlCommand command = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return cod = command.ExecuteNonQuery();
                }
            }
            else
            {
                return cod;
            }
        }

        public int insertarCategoria(Categories categoria)
        {
            Categories newCategoria;
            Products productos;
            int cod = 0;
            if (categoria != null)
            {
                newCategoria = new Categories();
                newCategoria.CategoryID = categoria.CategoryID;
                newCategoria.CategoryName = categoria.CategoryName;
                newCategoria.Description = categoria.Description;
                newCategoria.b_logiv = 0;
                foreach (Products producto in categoria.Products)
                {
                    productos = new Products();
                    productos.CategoryID = producto.CategoryID;
                    productos.ProductID = producto.ProductID;
                    productos.UnitPrice = producto.UnitPrice;
                    productos.ProductName = producto.ProductName;
                    newCategoria.Products.Add(productos);
                }
                db.Categories.Add(newCategoria);
                db.SaveChanges();
                return cod = 1;
            }
            else
            {
                return cod;
            }
        }
    }
}