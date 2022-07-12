<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebService_Web.Login" %>

<%@ Register assembly="Infragistics4.Web.v15.1, Version=15.1.20151.1018, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics4.Web.v15.1, Version=15.1.20151.1018, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio</title>
     <link rel="stylesheet" type="text/css" href="styleWeb.css" />
   
    <style type="text/css">
        .auto-style1 {
            width: 100%;
             font-family:'Segoe UI';
            font-size:9px;
        }
        .auto-style2 {
            height: 30px;
            width: 25%;
            color:black;
             font-family:'Segoe UI';
             font-size:9px;
        }
          .auto-style3 {
            text-align: center;
             font-family:'Segoe UI';
            font-size:9px;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <div class="header-Control">
            <div class="control-align"> 
                <div class="img-control img"> 
                        <img src="Images/logo_service_web_02_512x.png"  />
                </div>
                <div class="login"> 
                    <table>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;Correo</td>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;Contraseña</td>
                        </tr>
                          <tr>
                                 <td>  <asp:TextBox ID="correo" runat="server" CssClass="inputtxt" ></asp:TextBox> </td>
                             <td>  <asp:TextBox ID="password" runat="server" CssClass="inputtxt" TextMode="Password" ></asp:TextBox> </td>
                              <td> <asp:Button ID="Button1" runat="server" Text="Ingresar" CssClass="btn" OnClick="Button1_Click" /></td>
                        </tr>
                        <tr>
                             <td>&nbsp;</td>
                             <td>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink" runat="server"  ForeColor="White" NavigateUrl="~/RecuperarContrasena.aspx">Olvidaste tu Contraseña</asp:HyperLink></td>
                         <td>&nbsp;</td>
                            </tr>

                    </table>

                </div>
             
            </div>
        </div>
        <table class="auto-style1">
            <tr>
                <td>
                    <ig:WebScriptManager ID="WebScriptManager1" runat="server">
                    </ig:WebScriptManager>
                    <asp:Label ID="LabelPersonaId" runat="server" Text="Label" Visible="False"></asp:Label>
                    <ig:WebDialogWindow ID="wdwForms" runat="server" Height="400px" InitialLocation="Centered" Visible="False" Width="400px" BorderStyle="Double" Modal="True" Moveable="False" BackColor="White" BorderColor="Black" ForeColor="White">
                    <ContentPane>
                        <Template>
                         
                              <table>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Ingresar Nueva Clave:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxNuevaContrasena" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Repetir la nueva clave"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxRepetecionContrasena" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" colspan="2">
                    <asp:Button ID="Button2" runat="server" Text="Guardar" OnClick="Button2_Click"  />
                </td>
            </tr>
           
        </table>

                         
                        </Template>
                        
                    </ContentPane>
                          <Header CaptionAlignment="Center" CaptionText="Nueva Contraseña" ViewStateMode="Enabled" Visible="true">
                    
                    </Header>
                   
                </ig:WebDialogWindow>
                </td>
            </tr>
            <tr>
                <td>
                    <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" Height="300px" Visible="False" Width="400px">
                    </ig:WebDialogWindow>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
