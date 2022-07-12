<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="ServicioW.ClienteWeb.InicioSesion" %>

<!DOCTYPE html>
<html lang="en" dir="ltr">
  <head>
    <meta charset="utf-8">
    <title></title>
    <link rel="stylesheet" href="style.css">
      </head>
  <body>
      <form id="form1" runat="server">
<div class="login-box">
  <h1 >
         <asp:Image ID="Image1" runat="server" Height="49px" ImageUrl="~/Images/Logo_Service_Web-01.png" Width="83px" />
 

  </h1>
      
  <div class="textbox">
    <i class="fas fa-user"></i>
    <input type="text" placeholder="Username">
  </div>

  <div class="textbox">
    <i class="fas fa-lock"></i>
    <input type="password" placeholder="Password">
  </div>

    <asp:Button ID="Button1" runat="server" Height="43px" OnClick="Button1_Click" Text="Button" Width="261px" />
    <br />
    <asp:Button ID="Button2" runat="server" Height="27px" Text="InicioConFecebook" Width="188px" OnClick="Button2_Click" />
    <br />
</div>
      </form>
  </body>
</html>
