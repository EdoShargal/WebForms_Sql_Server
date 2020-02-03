<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms.Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>  
    <form id="form1" runat="server">  
        <div>  
            <h3>Search form </h3>
             <div>  
                <table class="auto-style1">  
                    <tr>  
                        <td>Company name:</td>
                        <td><asp:TextBox ID="CompanyName" runat="server" /></td>
                    </tr>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                    <tr>  
                        <td>Search:</td>
                        <td>  
                        <asp:RadioButtonList ID="SearchType" runat="server">  
                            <asp:ListItem>Equal</asp:ListItem>  
                            <asp:ListItem>StartWith</asp:ListItem>  
                            <asp:ListItem>EndWith</asp:ListItem>  
                            <asp:ListItem>Middle</asp:ListItem>  
                        </asp:RadioButtonList>  
                    </td>  
                    </tr>
                </table>
            </div>
            <asp:Button ID="Button1" Text="Submit" runat="server" OnClick="Button1_Click"/>
        </div>  
        <asp:DataGrid ID="DataGrid1" runat="server">  
        </asp:DataGrid> 
        
    </form>  
     
        
</body>  
                      

</html>
