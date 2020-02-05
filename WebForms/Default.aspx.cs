using System;
using System.Data;
using System.Data.SqlClient;

namespace WebForms
{
    public partial class Default : System.Web.UI.Page
    {
        string CONNECTION_STRING = "data source=e<server>; database=Northwind; integrated security=SSPI";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataGrid1.DataSource = null;
            DataGrid1.DataBind();
            Label1.Text = "";
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
                    string valueToSearc = CompanyName.Text;
                    switch (howToSearch)
                    {
                        case "Equal":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName = " + "'" + valueToSearc + "' " +
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        case "StartWith":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName like " + "'" + valueToSearc + "%' " +
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        case "EndWith":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName like " + "'%" + valueToSearc + "' " +
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        case "Middle":
                            sqlQuery = "select C.CustomerID, C.CompanyName, Count(O.OrderID) " +
                            "from Customers C INNER JOIN Orders O on C.CustomerID = O.CustomerID " +
                            "where C.CompanyName like " + "'%" + valueToSearc + "%' " +
                            "group by C.CustomerID, C.CompanyName";
                            break;
                        default:
                            Label1.Text = "You didnt pick how to search";
                            return;

                    }



                }
                SqlDataAdapter sde = new SqlDataAdapter(sqlQuery, con);
                DataSet ds = new DataSet();
                sde.Fill(ds);
                DataGrid1.DataSource = ds;
                DataGrid1.DataBind();
            }
        }
    }
}
