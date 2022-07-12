<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorFotos.aspx.cs" Inherits="ServicioW.ClienteWeb.VisorFotos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="js/prototype.js"></script>

<script type="text/javascript" src="js/lightbox.js"></script>
    <link rel="stylesheet" href="css/lightbox.css" type="text/css" media="screen" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
<a href="Images/image-1.jpg" rel="lightbox[G1]"><img src="Images/image-1.jpg" width="200px" height="200px"></a>
<a href="Images/image-2.jpg" rel="lightbox[G2]"><img src="Images/image-2.jpg" width="200px" height="200px"></a>
<a href="Images/image-3.jpg" rel="lightbox[G2]"><img src="Images/image-3.jpg" width="200px" height="200px"></a>
        </div>
    </form>
</body>
</html>
