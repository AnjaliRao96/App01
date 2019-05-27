using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Product_entity;
using Product_Exception;
using System.Data;
using System.Data.SqlClient;


namespace Product_DAL
{
    public class ProductDAL
    {
        static string conStr = string.Empty;
        SqlConnection con = null;
        SqlCommand cmd = null;
        static ProductDAL()
        {
            conStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        }
        public ProductDAL()
        {
            con = new SqlConnection(conStr);
        }
        public DataTable DisplayProductDal()
        {
            DataTable dt = null;


            try
            {


                cmd = new SqlCommand();
                cmd.CommandText = "usp_DotnetUser4_DisplayProduct";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dt = new DataTable();
                    dt.Load(dr);
                }
            }

            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dt;
        }
        public int AddProductDal(Product pboj)  // Adding new product
        {
            int pid = 0;
            try
            {

                cmd = new SqlCommand();
                cmd.CommandText = "usp_DotnetUser4_AddProduct"; // creating stored procedure and giving its name here
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@SerialNumber", SqlDbType.Int);
                cmd.Parameters["@SerialNumber"].Direction = ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@ProductName", pboj.ProductName);
                cmd.Parameters.AddWithValue("@Brandname", pboj.Brandname);
                cmd.Parameters.AddWithValue("@ProductType", pboj.ProductType);
                cmd.Parameters.AddWithValue("@ProductDescription", pboj.ProductDescription);
                cmd.Parameters.AddWithValue("@Price", pboj.Price);


                con.Open();
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                pid = int.Parse(cmd.Parameters["@SerialNumber"].Value.ToString());
            }
            catch (ProductException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (SystemException)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pid;
        }
    }
}
