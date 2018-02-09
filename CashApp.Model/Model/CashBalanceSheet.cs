using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.Model.Model
{
    public class CashBalanceSheet
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public decimal FloatStart { get; set; }
        public CashTotal CashTotals { get; set; }
        public decimal CashDeposit { get; set; }
        public PaymentCard Debit { get; set; }
        public PaymentCard MasterCard { get; set; }
        public PaymentCard Visa { get; set; }
        public PaymentCard Amex { get; set; }
        public PaymentCard Other { get; set; }
        public decimal ChequeTotal { get; set; }
        public decimal FloatEnd { get; set; }
        public decimal Disbursement { get; set; }
        public decimal Returns { get; set; }
        public ZRead DailyZread { get; set; }
        public decimal Variance { get; set; }
        public CashBalanceSheet()
        {

        }

    }
}
