using System;
using System.Data;
using System.Data.SqlClient;

namespace WebForms
{
    public partial class Default : System.Web.UI.Page
    {


        //TODO: put the connection in Web.config
        string CONNECTION_STRING = "data source=<server>; database=Northwind; integrated security=SSPI";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //delete data from the current table before new search
            DataGrid1.DataSource = null;
            DataGrid1.DataBind();


            string valueToSearch = CompanyName.Text;

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                string sqlQuery = "";
                if (String.IsNullOrEmpty(CompanyName.Text))
                {
                    sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "group by C.CustomerID, C.CompanyName";
                }
                else
                {
                    string howToSearch = SearchType.SelectedValue;
                    
                    switch (howToSearch)
                    {
                        case "Equal":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName = @valueToSearch " +
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        case "StartWith":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName like @valueToSearch + '%' "+
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        case "EndWith":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName like '%' + @valueToSearch " +
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        case "Middle":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName like  '%' + @valueToSearch +  '%' " +
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        default:
                            Label1.Text = "You didnt pick how to search";
                            return;

                    }



                }
                // Command ctor with the sql query and connection object
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                //adding parameter into command collection a way against SQL Injection
                cmd.Parameters.AddWithValue("@valueToSearch", valueToSearch);
                
                // bridge between a DataSet and SQL Server by mapping Fill, which changes the data inthe DataSet to match in the Data Source.
                SqlDataAdapter sde = new SqlDataAdapter(cmd);
               

                DataSet ds = new DataSet();
                sde.Fill(ds);
                DataGrid1.DataSource = ds;
                DataGrid1.DataBind();

                // return the focus to the input text for fast new search
                CompanyName.Focus();
            }
        }
    }
}
