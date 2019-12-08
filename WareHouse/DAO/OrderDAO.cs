using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WareHouse.Models;

namespace WareHouse.DAO
{
    public class OrderDAO : DAO
    {
        internal int saveOrder(OrderViewModel newOrder)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Main_Connectionstring))
            {
                using (SqlCommand command = new SqlCommand("spSaveOrder", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", newOrder.ProductId);
                        command.Parameters.AddWithValue("@Quantity", newOrder.Quantity);
                        command.Parameters.AddWithValue("@Username", newOrder.Username);
                        command.Parameters.AddWithValue("@Sum", newOrder.CalculateSum(newOrder.Quantity, newOrder.ProductId));
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

        internal List<OrderViewModel> getOrders(int? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Main_Connectionstring))
            {
                using (SqlCommand command = new SqlCommand("spGetOrdersById", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (id != null)
                            command.Parameters.AddWithValue("@Id", id);
                        else
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        SqlDataReader rdr = command.ExecuteReader();
                        var orderList = new List<OrderViewModel>();
                        while (rdr.Read())
                        {
                            orderList.Add(GetOrderFromReader(rdr));
                        }
                        return orderList;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        internal List<OrderViewModel> getOrdersForReport(int? productId,DateTime? startDate, DateTime? endDate)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Main_Connectionstring))
            {
                using (SqlCommand command = new SqlCommand("spGetOrdersByDateTime", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);
                        command.Parameters.AddWithValue("@productId", productId);
                        SqlDataReader rdr = command.ExecuteReader();
                        var orderList = new List<OrderViewModel>();
                        while (rdr.Read())
                        {
                            orderList.Add(GetOrderFromReader(rdr));
                        }
                        return orderList;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


        private OrderViewModel GetOrderFromReader(SqlDataReader rdr)
        {
            var order = new OrderViewModel();
            order.Id = Convert.ToInt32(rdr["Id"]);
            order.ProductId = Convert.ToInt32(rdr["ProductId"].ToString());
            order.Quantity = Convert.ToDecimal(rdr["Quantity"]);
            order.Sum = Convert.ToDecimal(rdr["Sum"]);
            order.Username = rdr["Username"].ToString();
            order.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
            return order;
        }
    }
}