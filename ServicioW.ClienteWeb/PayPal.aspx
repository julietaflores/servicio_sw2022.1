<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPal.aspx.cs" Inherits="ServicioW.ClienteWeb.PayPal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- Add meta tags for mobile and IE -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
  <div id="paypal-button" class="auto-style1">
      <asp:TextBox ID="TextBoxTotal" runat="server" Enabled="False">5</asp:TextBox>
        </div>
<script src="https://www.paypalobjects.com/api/checkout.js"></script>
<script>
    var total1 = (document.getElementById('TextBoxTotal').value)
  paypal.Button.render({
    // Configure environment
        
      env: 'sandbox',
    client: {
   
          sandbox: 'AZpP8kr9RP4O1lMJ-cSlZYN5QMoXSgtzI1gtg53m3GGJPFue2VTW4sTJiAVVL6UL4E1gm15Q5cWfNUWj'
    },
    // Customize button (optional)
    locale: 'en_US',
    style: {
      size: 'small',
      color: 'gold',
        shape: 'pill',
        fundingicons: 'true',
        label:'pay',
    },

    // Enable Pay Now checkout flow (optional)
    commit: true,

    // Set up a payment
    payment: function(data, actions) {
      return actions.payment.create({
        transactions: [{
          amount: {
            total: total1,
            currency: 'USD'
          }
        }]
      });
    },
    // Execute the payment
    onAuthorize: function(data, actions) {
      return actions.payment.execute().then(function() {
        // Show a confirmation message to the buyer
          window.location = "realizado.aspx";
       
      });
    }
  }, '#paypal-button');

</script> 
    </form>
</body>
    
</html>
