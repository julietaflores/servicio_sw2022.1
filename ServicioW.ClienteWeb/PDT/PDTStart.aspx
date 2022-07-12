<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDTStart.aspx.cs" Inherits="ServicioW.ClienteWeb.PDTStart" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form action="<%= ConfigurationManager.AppSettings["PayPalSubmitUrl"] %>" method="post">
    <input type="hidden" name="cmd" value="_xclick" />
    <input type="hidden" name="business" value="<%= ConfigurationManager.AppSettings["PayPalUsername"] %>" />
    <input type="hidden" name="item_name" value="My painting" />
    <input type="hidden" name="amount" value="1.00" />
    <input type="hidden" name="return" value="http://localhost:56963/PDT/PDTSuccess.aspx" />
    <input type="hidden" name="custom" value="Registration started: <%= DateTime.Now.ToString() %>" />
    <input type="submit" value="Start PDT process!" />
    </form>
    <div>
        <br />
        <b>Use following credentials to pay:</b><br />
        <b>User: </b>mika_1254623432_per@mailinator.com<br />
        <b>Password:</b> 123456789
    </div>
</body>
</html>
