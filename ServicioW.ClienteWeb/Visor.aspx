<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visor.aspx.cs" Inherits="ServicioW.ClienteWeb.Visor" %>

<!DOCTYPE html>

<html>
<head>
<title>Ejemplo de uso de Lightbox 2</title>
   </head>
 
<body>   
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script>

        jQuery.noConflict();
        var j = jQuery.noConflict();
        var j1 = 15;
        function ram() {
            abc(j1);
            j1++
        }
        j1 = j1;



        $(document).ready(function () {
            var loadingValue = "N";

            $(document).unbind(".firstCall");
            $(document).on("ajaxStart.firstCall", function () {
                $("#loadingImg").show();
                loadingValue = "Y";
            });

            $(document).on("ajaxStop.firstCall", function () {
                $("#loadingImg").hide();
                loadingValue = "N";
            });

            // Each time the user scrolls
            $(window).scroll(function () {
                // End of the document reached?
                if ($(document).height() - $(this).height() - 100 < $(this).scrollTop()) {
                    if (loadingValue == "N") {
                        CallServerFunction();
                    }
                }
            });

            function CallServerFunction() {
                var page = parseInt($('#pageHidden').val());
                page = page + 1;
                $('#pageHidden').val(page);
                $.ajax({
                    type: "POST",
                    url: "Visor.aspx/increasediv",
                    contentType: "application/json; charset=utf-8",
                    data: "{'id':'" + 1 + "'}",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d) {
                            $(".container").append(msg.d);
                        }
                    },
                    error: function (req, status, error) {
                        alert("Error try again");
                    }
                });
            }
        });
    </script>
    	<script type="text/javascript" src="js/prototype.js"></script>   
	    <script type="text/javascript" src="js/scriptaculous.js?load=effects,builder"></script>
    	<script type="text/javascript" src="js/lightbox.js"></script>
  
<link rel="stylesheet" href="css/lightbox.css" type="text/css" media="screen" />
    
<h3>Yobi belleza</h3>

<a href="http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=1.png" rel="lightbox[galeria1]" title="imagen 1"><img width="600" height="600" src="http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=1.png"/> </a>
<a href="imagen2.jpg" rel="lightbox[galeria1]"></a>
<a href="imagen3.jpg" rel="lightbox[galeria1]"></a>

<h3>Yobi belleza</h3>

<a href="http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=1.png" rel="lightbox[galeria1]" title="imagen 1"><img width="600" height="600" src="http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=logo.png"/></a>
<a href="imagen2.jpg" rel="lightbox[galeria1]"></a>
<a href="imagen3.jpg" rel="lightbox[galeria1]"></a>

<h3>Yobi belleza</h3>

<a href="http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=1.png" rel="lightbox[galeria1]" title="imagen 1"><img width="600" height="600" src="http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=ffeb6426-a78f-4fc1-a133-1866cb7289bb.jpg"/></a>
<a href="imagen2.jpg" rel="lightbox[galeria1]"></a>
<a href="imagen3.jpg" rel="lightbox[galeria1]"></a>


</body>
</html>