using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Product_Exception;
using Product_entity;
using Product_DAL;
using Product_BAL;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
        SqlConnection conObj = new SqlConnection();
        SqlCommand cmdObj;
        SqlParameter parmObj;
        SqlDataReader rdrStudent = null;
        DataTable dtStudent = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_AddProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product p = new Product
                {
                    ProductName = txt_ProName.Text,
                    Brandname = txt_BrandName.Text,
                    ProductType = cb_ProductType.Text,
                    ProductDescription = txt_ProdDes.Text,
                    Price = decimal.Parse(txt_Prodprice.Text)
                };

                ProductBLL pb = new ProductBLL();
                int pid = pb.AddProductBAL(p);
                MessageBox.Show(string.Format("New Product Added.\nProduct Id: {0}", pid),
                    "Product Management System");
            }
            catch (ProductException ex)
            {
                MessageBox.Show(ex.Message, "Product Management System");
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Product Management System");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conObj.ConnectionString = connStr;
            cmdObj = new SqlCommand("select IDENT_CURRENT('DotnetUser4_PRODUCT')+IDENT_INCR('DotnetUser4_PRODUCT')", conObj);
            try
            {
                conObj.Open();
                object nxId = cmdObj.ExecuteScalar();
                txt_SelNum.Text = nxId.ToString();



            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conObj.Close();
            }
        }

        private void btn_Display_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductBLL st = new ProductBLL();
                DataTable dt = st.DisplayProductBal();
                dt_Display.ItemsSource = dt.DefaultView;


            }
            catch (ProductException ex)
            {
                MessageBox.Show(ex.Message, "employee Management System");
            }
            catch (SqlException se)
            {

                MessageBox.Show(se.Message.ToString());
            }
            finally
            {

            }
        }

    }
}
