<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pp.aspx.cs" Inherits="ServicioW.ClienteWeb.pp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</head>

<body>
  <script
    src="https://www.paypal.com/sdk/js?client-id=AZpP8kr9RP4O1lMJ-cSlZYN5QMoXSgtzI1gtg53m3GGJPFue2VTW4sTJiAVVL6UL4E1gm15Q5cWfNUWj">
  </script>

    
  <div id="paypal-button-container">pagaste</div>

  
 <script>
  paypal.Buttons({
    createOrder: function(data, actions) {
      return actions.order.create({
        purchase_units: [{
          amount: {
            value: '0.01'
          }
        }]
      });
    },
    onApprove: function(data, actions) {
      // Capture the funds from the transaction
      return actions.order.capture().then(function(details) {
        // Show a success message to your buyer
        alert('Transaction completed by ' + details.payer.name.given_name);
      });
    }
  }).render('#paypal-button-container');
</script>
</body>
    
</html>
