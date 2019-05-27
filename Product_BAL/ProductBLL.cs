using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product_entity;
using Product_Exception;
using System.Data;
using System.Data.SqlClient;

using Product_DAL;
using System.Text.RegularExpressions;

namespace Product_BAL
{
    public class ProductBLL
    {
        StringBuilder sb = new StringBuilder();
        private bool ValidateProduct(Product pro)
        {


            bool IsValidProduct = true;

            if (!Regex.Match(pro.ProductName, @"^[A-Z][a-z]*$").Success) // validation of ProductName
            {
                IsValidProduct = false;
                sb.Append(Environment.NewLine + "Product Name should contain Characters and must begin with a capital letter!");
            }
            if (!(Regex.IsMatch(pro.Price.ToString(), @"[0-9]$"))) // validation for productprice
            {
                IsValidProduct = false;
                sb.Append(Environment.NewLine + "Product Price must contain digits only");
            }
            if (pro.Price.ToString().Equals(string.Empty))
            {
                IsValidProduct = false;
                sb.Append("Product Price cannot be blank " + Environment.NewLine);

            }
            if (!(Regex.IsMatch(pro.SerialNumber.ToString(), @"[]$")))  // validation for serial number
            {
                IsValidProduct = false;
                sb.Append(Environment.NewLine + "Serial number should be in this format 1234-1234-1234-1234");
            }

            return IsValidProduct;
        }
        public DataTable DisplayProductBal()
        {
            try
            {
                ProductDAL sd = new ProductDAL();
                DataTable dtProduct = sd.DisplayProductDal();
                if (dtProduct.Rows.Count <= 0)
                {
                    throw new ProductException("No Product Available");
                }
                return dtProduct;
            }
            catch (ProductException se)
            { throw se; }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception e)
            { throw e; }
        }
        public int AddProductBAL(Product pobj)
        {
            try
            {
                int pid = 0;
                ProductDAL pd = new ProductDAL();
                if (ValidateProduct(pobj))
                {
                    pid = pd.AddProductDal(pobj);
                }
                else
                    throw new ProductException(sb.ToString());

                return pid;
            }
            catch (ProductException)
            {
                throw;
            }
        }
    }
}
