using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using LabWeb.Conexion;
using LabWeb.Models;

namespace LabWeb.Data
{
    public class OrderDAO
    {
        ConexionBD conexion;
        NorthwindEntities db;

        public OrderDAO()
        {
            conexion = new ConexionBD();
            db = new NorthwindEntities();
        }
    
        public List<Orders> consultarPedidos()
        {
            List<Orders> listaPedidos = new List<Orders>();
            Orders pedido;

            if(conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT TOP(50) OrderID, OrderDate, RequiredDate, ShippedDate, Freight ");
                builder.Append("FROM Orders WHERE estado = 0");

                using (SqlCommand command = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            pedido = new Orders();
                            pedido.OrderID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
                            pedido.OrderDate = reader.IsDBNull(1) ? new DateTime() : DateTime.Parse(reader.GetValue(1).ToString());
                            pedido.RequiredDate = reader.IsDBNull(2) ? new DateTime() : DateTime.Parse(reader.GetValue(2).ToString());
                            pedido.ShippedDate = reader.IsDBNull(3) ? new DateTime() : DateTime.Parse(reader.GetValue(3).ToString());
                            pedido.Freight = decimal.Parse(reader.IsDBNull(4) ? "" : reader.GetValue(4).ToString());
                            listaPedidos.Add(pedido);
                        }
                    }
                }

                foreach(Orders order in listaPedidos)
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("SELECT od.OrderID, od.ProductID, p.ProductName, od.UnitPrice, od.Quantity FROM[Order Details] od, Products p ");
                    builder2.Append("WHERE od.OrderID = @id AND od.ProductID = p.ProductID");

                    using (SqlCommand command2 = new SqlCommand(builder2.ToString(), conexion.getConn()))
                    {
                        Order_Details lineas;
                        order.Order_Details = new List<Order_Details>();
                        command2.Parameters.AddWithValue("@id", order.OrderID);
                        using (IDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                lineas = new Order_Details();
                                lineas.OrderID = int.Parse(reader2.IsDBNull(0) ? "" : reader2.GetValue(0).ToString());
                                lineas.ProductID = int.Parse(reader2.IsDBNull(1) ? "" : reader2.GetValue(1).ToString());
                                lineas.Products = new Products();
                                lineas.Products.ProductName = reader2.IsDBNull(2) ? "" : reader2.GetValue(2).ToString();///!!!!!!!!!
                                lineas.UnitPrice = decimal.Parse(reader2.IsDBNull(3) ? "" : reader2.GetValue(3).ToString());
                                lineas.Quantity = short.Parse(reader2.IsDBNull(4) ? "" : reader2.GetValue(4).ToString());
                                order.Order_Details.Add(lineas);
                            }
                        }
                    }
                }
            }
            return listaPedidos;
        }
               
        public List<Orders> obtenerPedidosPorID(int id)
        {
            List<Orders> listaPedidos = new List<Orders>();
            Orders pedido;

            if (conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT TOP(50) OrderID, OrderDate, RequiredDate, ShippedDate, Freight ");
                builder.Append("FROM Orders WHERE estado = 0 AND OrderID LIKE '%"+id+"%' ");

                using (SqlCommand command = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedido = new Orders();
                            pedido.OrderID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
                            pedido.OrderDate = reader.IsDBNull(1) ? new DateTime() : DateTime.Parse(reader.GetValue(1).ToString());
                            pedido.RequiredDate = reader.IsDBNull(2) ? new DateTime() : DateTime.Parse(reader.GetValue(2).ToString());
                            pedido.ShippedDate = reader.IsDBNull(3) ? new DateTime() : DateTime.Parse(reader.GetValue(3).ToString());
                            pedido.Freight = decimal.Parse(reader.IsDBNull(4) ? "" : reader.GetValue(4).ToString());
                            listaPedidos.Add(pedido);
                        }
                    }
                }

                foreach (Orders order in listaPedidos)
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("SELECT od.OrderID, od.ProductID, p.ProductName, od.UnitPrice, od.Quantity FROM[Order Details] od, Products p ");
                    builder2.Append("WHERE od.OrderID = @id AND od.ProductID = p.ProductID");

                    using (SqlCommand command2 = new SqlCommand(builder2.ToString(), conexion.getConn()))
                    {
                        Order_Details lineas;
                        order.Order_Details = new List<Order_Details>();
                        command2.Parameters.AddWithValue("@id", order.OrderID);
                        using (IDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                lineas = new Order_Details();
                                lineas.OrderID = int.Parse(reader2.IsDBNull(0) ? "" : reader2.GetValue(0).ToString());
                                lineas.ProductID = int.Parse(reader2.IsDBNull(1) ? "" : reader2.GetValue(1).ToString());
                                lineas.Products = new Products();
                                lineas.Products.ProductName = reader2.IsDBNull(2) ? "" : reader2.GetValue(2).ToString();///!!!!!!!!!
                                lineas.UnitPrice = decimal.Parse(reader2.IsDBNull(3) ? "" : reader2.GetValue(3).ToString());
                                lineas.Quantity = short.Parse(reader2.IsDBNull(4) ? "" : reader2.GetValue(4).ToString());
                                order.Order_Details.Add(lineas);
                            }
                        }
                    }
                }
            }
            return listaPedidos;
        }

        public int eliminarPedido(int id)
        {
            int cod = 0;
            if(conexion.Conectar()=="True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("UPDATE Orders SET estado =1 ");
                builder.Append("WHERE OrderID = @id");

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

        public int insertarPedido(Orders order)
        {
            //Orders newOrder;
            //Order_Details detalles;
            int cod = 0;
            //if(order!=null)
            //{
            //    newOrder = new Orders();
            //    newOrder.CustomerID = order.CustomerID;
            //    newOrder.OrderDate = order.OrderDate;
            //    newOrder.RequiredDate = order.RequiredDate;
            //    newOrder.ShippedDate = order.ShippedDate;
            //    newOrder.Freight = order.Freight;
            //    newOrder.estado =0;
            //    foreach(Order_Details detail in order.Order_Details)
            //    {
            //        detalles = new Order_Details();
            //        detalles.OrderID = detail.OrderID;
            //        detalles.ProductID = detail.ProductID;
            //        //detalles.Products = new Products();
            //        //detalles.Products.ProductName = detail.Products.ProductName;///!!!!!!!!!!!!!
            //        detalles.UnitPrice = detail.UnitPrice;
            //        detalles.Quantity = detail.Quantity;
            //        newOrder.Order_Details.Add(detalles);
            //    }
            //    db.Orders.Add(newOrder);
            //    db.SaveChanges();
            //    return cod = 1;
            //}
            //else
            //{
            //    return cod;
            //}

            //transacción de una orders con ADO
            using (conexion.getConn())
            {
                SqlTransaction transaction = null;
                try
                {
                    conexion.getConn().Open();
                    transaction = conexion.getConn().BeginTransaction();
                    //cabecera
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conexion.getConn();
                        command.Transaction = transaction;
                        command.CommandText = string.Format("INSERT INTO Orders (CustomerID, Freight, OrderDate) VALUES ({0}, {1})", order.CustomerID,order.Freight);
                        command.ExecuteNonQuery();
                    }

                    foreach (Order_Details detail in order.Order_Details)
                    {
                        //detalle
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = conexion.getConn();
                            command.Transaction = transaction;
                            command.CommandText = string.Format("INSERT INTO [Order Details] (OrderID, ProductID, UnitPrice, Quantity) VALUES ((SELECT MAX(OrderID) FROM Orders),{0},{1},{2})", detail.ProductID, detail.UnitPrice, detail.Quantity);
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return 1;
                    //Console.WriteLine("Transacción exitosa!");
                }
                catch (SqlException e)
                {
                    //Console.WriteLine("Error en la Transacción.");
                    transaction.Rollback();
                    return cod;
                }
            }
        }
         
    }
}