using System;
using System.ComponentModel.DataAnnotations;

namespace CashApp.Model.Model
{
    public class ZRead
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime ZReadDate { get; set; }
        public decimal SalesTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal GiftCertificate { get; set; }
        public decimal Coupon { get; set; }
        public decimal Void { get; set; }
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal Cash { get; set; }
        public decimal CashInDrawer { get; set; }
        public ZRead()
        {

        }

    }
}