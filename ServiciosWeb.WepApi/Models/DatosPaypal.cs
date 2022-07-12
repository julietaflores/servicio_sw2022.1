using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class DatosPaypal
    {
         public   string item_name   { get; set; }
      public  string item_currency { get; set; }
      public    string item_price { get; set; }
      public  string item_quantity { get; set; }
      public string  string_sku { get; set; }
        //////////////////////////////////////
        public string   billingAddress_city  { get; set; }
       public string    billingAddress_country_code  { get; set; }
       public  string   billingAddress_line1 { get; set; }
      public    string  billingAddress_postal_code  { get; set; }
      public    string  billingAddress_state { get; set; }
        ////////////////////////////////
        public   string crdtCard_cvv2 { get; set; }
         public int crdtCard_expire_month  { get; set; }
       public int crdtCard_expire_year  { get; set; }
      public   string crdtCard_first_name  { get; set; }
          public  string crdtCard_last_name  { get; set; }
         public   string crdtCard_number  { get; set; }
        public    string crdtCard_type  { get; set; }
        //////////////////////////////////
      public  string details_shipping { get; set; }
      public  string   details_subtotal { get; set; }
      public  string   details_tax { get; set; }

        /////////////////////////////     
        public string amnt_currency  { get; set; }
        public string   amnt_total  { get; set; }
        /////////////////////////////////////////////////////           
            public string tran_description { get; set; }
        public string tran_invoice_number { get; set; }
        ///////////////////////////////////
        public string payr_payment_method { get; set; }
        /// ///////////////////////////////////////////////////////

       public string  pymnt_intent { get; set; }
    }
}