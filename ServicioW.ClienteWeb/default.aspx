<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ServicioW.ClienteWeb._default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>jQuery Infinite Scroll | YogiHosting Demo</title>
    <link rel="icon" type="images/png" href="http://www.yogihosting.com/wp-content/themes/yogi-yogihosting/Images/favicon.ico" />
    <style>
        body {
            background: #FFF no-repeat;
            background-image: -webkit-gradient(radial, 50% 0, 150, 50% 0, 300, from(#444), to(#111));
        }

        h1, h2 {
            text-align: center;
            color: #FFF;
        }

            h2 a {
                color: #FFF;
                text-decoration: none;
            }

        .container {
            width: 960px;
            margin: auto;
         
            font-size: 25px;
        }

            .container h3 {
                text-decoration: underline;
                text-align: center;
            }

        .floatRight {
            float: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>jQuery Infinite Scroll</h1>
        <h2><a href="http://www.yogihosting.com/jquery-infinite-scroll/">Read the tutorial on YogiHosting »</a></h2>
        <div class="container">
            <h3>Page 1</h3>
            <p>
                Thereoves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain. There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain.
            </p>
        </div>
        <input type="hidden" id="pageHidden" value="1" />
        &nbsp;</form>
    <!—Reference to jQuery-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            var loadingValue="N";

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
                    url: "default.aspx/getdata",
                    contentType: "application/json; charset=utf-8",
                    data: '{"PageNo":"' + $("#pageHidden").val() + '"}',
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
</body>
</html>
