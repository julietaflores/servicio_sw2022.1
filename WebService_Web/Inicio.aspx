<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebService_Web.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

  <head>
    <link href="https://fonts.googleapis.com/css?family=Pacifico&display=swap" rel="stylesheet">
    <meta charset="utf-8" />
    <title>El Centollo</title>
 
    <style>
 
      *{
         margin: 0;
         padding: 0;
 
      }
 
      #header{
        background-color: #F9F8FC;
        height: 200px;
      }
 
 
     #nav{
       height: 62px;
       background-color:#17181C;
       text-align: center;
     }
 
     #nav ul{
        list-style: none;
        display: inline-block;
     }
 
     #nav ul li{
         float: left;
         margin-top: 20px;
     }
 
     #nav ul li a {
         color: white;
         font-weight: bold;
         text-decoration: none;
         font-size: 20px;
         padding: 20px;
     }
 
     #nav ul li a:hover{
       background-color: #929fb3;
     }
 
   </style>
 
 </head>
 
 <body>
 
  <div id="header">
     <div id="logo">
    
     </div>
     <div id="nav">
       <ul>
         <li><a href="#">Post</a></li>
         <li><a href="#">Perfil</a></li>
       
       </ul>
     </div>
   </div>
 </body>
</html>

