using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WareHouse.Models;

namespace WareHouse.DAO
{
    public class ProductDAO : DAO
    {
        internal int saveProduct(Product product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Main_Connectionstring))
            {
                using (SqlCommand command = new SqlCommand("spSaveProduct", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (product.Id != null)
                            command.Parameters.AddWithValue("@Id", product.Id);
                        else
                            command.Parameters.AddWithValue("@Id", DBNull.Value);

                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

        /// <summary>getProducts is a method in the Product class that returns product
        /// </summary>
        /// <param name="id">Product id, NULL to get all products</param>
        internal List<Product> getProducts(int? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Main_Connectionstring))
            {
                using (SqlCommand command = new SqlCommand("spGetProductsById", sqlConnection))
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
                        var productList = new List<Product>();
                        while (rdr.Read())
                        {
                            var product = new Product();
                            product.Id = Convert.ToInt32(rdr["Id"]);
                            product.Name = rdr["Name"].ToString();
                            product.Description = rdr["Description"].ToString();
                            product.Price = Convert.ToDecimal(rdr["Price"]);
                            product.isDeleted = Convert.ToBoolean(rdr["isDeleted"]);
                            product.CreateDate = Convert.ToDateTime(rdr["CreateDate"]);
                            productList.Add(product);
                        }
                        return productList;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>deleteProduct is a method in the Product class that marks product as deleted
        /// </summary>
        /// <param name="id">Product id that need to be deleted</param>
        internal void deleteProduct(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Main_Connectionstring))
            {
                using (SqlCommand command = new SqlCommand("spDeleteProduct", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}