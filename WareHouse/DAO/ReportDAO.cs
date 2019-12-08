using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WareHouse.Models;

namespace WareHouse.DAO
{
    public class ReportDAO : DAO
    {
        internal List<StockReportViewModel> getStockReport(int? productId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Main_Connectionstring))
            {
                using (SqlCommand command = new SqlCommand("spGetWareHouseCount", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (productId != null)
                            command.Parameters.AddWithValue("@Id", productId);
                        else
                            command.Parameters.AddWithValue("@Id", DBNull.Value);

                        SqlDataReader rdr = command.ExecuteReader();
                        var reportList = new List<StockReportViewModel>();
                        while (rdr.Read())
                        {
                            var reportView = new StockReportViewModel();
                            reportView.Product = new Product().GetProducts(Convert.ToInt32(rdr["Id"])).First();
                            reportView.Quantity= Convert.ToDecimal(rdr["Sum"]);
                            reportList.Add(reportView);
                        }
                        return reportList;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

    }
}