<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MostrarNombre.aspx.cs" Inherits="WebService_Web.MostrarNombre" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
       <style type="text/css">
 .staticPos{position:static !important;}
           .auto-style1 {
               width: 100%;
           }
           .auto-style2 {
               height: 26px;
           }
           .auto-style3 {
               height: 20px;
               text-align: center;
           }
 </style>
    <title></title>

    <script>
fetch()


    </script>
</head>
<body>
    <form id="form1" runat="server">
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Ingresar Nueva Clave:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Repetir la nueva clave"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="Guardar" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        
        </form>
</body>
</html>
