<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPalButton.aspx.cs" Inherits="ServicioW.ClienteWeb.PayPalButton" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<script src="https://www.paypal.com/sdk/js?client-id=sb"></script>
<script>paypal.Buttons().render('body');</script>
    <option value="small">small - $10.00</option>



</body>
</html>
