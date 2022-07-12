<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="ServicioW.ClienteWeb.Servicios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            valores<br />
            <asp:Button ID="Button1" runat="server" Height="50px" OnClick="Button1_Click" Text="Aceptar detalle" Width="124px" />
        </div>
    </form>
</body>
</html>
