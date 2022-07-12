<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebService_Web.Default_aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio Sesión ServiceWeb</title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
    
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="top">

            <div class="container">  
                  <div class="logo"> 
                        <img src="Images/logoletra.png"  width="90"/>
            </div>
              <div class="form" runat="server"> 
                  <div class="text-field">
                      <asp:Label ID="correo" runat="server" Text="correo electronico" ForeColor="White"></asp:Label>
                       <asp:TextBox ID="txt" runat="server" BorderColor="White"></asp:TextBox> 

                  </div>
                  
                  <div class="text-field"> 
                       <asp:Label ID="contraseña" runat="server" Text="Contraseña" ForeColor="White"></asp:Label>
                       <asp:TextBox ID="password" runat="server" BorderColor="White"></asp:TextBox>
                      <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White">Olvido su contraseña</asp:HyperLink>

                  </div>
                 
                 
                  <asp:Button ID="submit" runat="server" Text="Ingresar" OnClick="submit_Click"  />  

              </div>
                
            </div>
          

        </div>

        <div class="rightbody"> 


        </div>
        <div class="leftbody"> 


        </div>

    </div>
         </form>
</body>
</html>
