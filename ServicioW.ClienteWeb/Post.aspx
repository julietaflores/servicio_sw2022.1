<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="ServicioW.ClienteWeb.Post" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  <script  type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.4.2/jquery.min.js"></script>
  
    <script  type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
   <script type="text/javascript">
       var j = 15;
       function ram()
       {
           abc(j);
           j++
       }
       j = j;

       function abc(K)
       {
           $.ajax({
               type: 'post',
               contentType: "application/json;charset=utf-8",
               url: 'Post.aspx/increasediv',
               data: "{'id':'" + 1 + "'}",
               async: false,
               success: function (response)
               {


               },
               error: function (response)
               {


               }
            });

       }

       $(window).scroll(function () {
           if ($(window).scrollTop() == $(document).height() - $(window.height())
           {
               ram();
           }


       });
</script>
</head>
<body> 
    <form id="form1" runat="server">
        <div style="height:1000px"; width:100%>
            <div id="para"> </div>
        </div >
    </form>
</body>
</html>
