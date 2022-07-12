<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajax.aspx.cs" Inherits="WebService_Web.ajax"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript"   src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript">
        function GetDataPage() {
            var URL = "http://190.104.2.126:80/ServicioWebPrueba/api/PackParameter?lang=es";

            $.getJSON(URL, function (datos) {
                console.log(datos);
            });
        }

</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <asp:Button ID="btnOne" runat="server" Text="Get Data From Page" OnClientClick="GetDataPage(); return false;" />
        </div>
    </form>
</body>
</html>
