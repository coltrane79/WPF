using System;
using System.ComponentModel.DataAnnotations;

namespace CashApp.Model.Model
{
    public class CashBalanceSheet
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        public SalesPerson SalesPerson { get; set; }
        public decimal FloatStart { get; set; } = 0.00M;
        public CashTotal CashTotals { get; set; }
        public decimal CashDeposit { get; set; } = 0.00M;
        public PaymentCard Debit { get; set; }
        public PaymentCard MasterCard { get; set; }
        public PaymentCard Visa { get; set; }
        public PaymentCard Amex { get; set; }
        public PaymentCard Other { get; set; }
        public decimal ChequeTotal { get; set; } = 0.00M;
        public decimal FloatEnd { get; set; } = 0.00M;
        public decimal Disbursement { get; set; } = 0.00M;
        public decimal Returns { get; set; } = 0.00M;
        public ZRead DailyZread { get; set; }
        public decimal Variance { get; set; } = 0.00M;
        public CashBalanceSheet()
        {
            Date = DateTime.Today;
            DailyZread = new ZRead();
            CashTotals = new CashTotal
            {
                hundred = 0,
                fifty = 0,
                twenty = 0,
                ten = 0,
                five = 0,
                two = 0,
                one = 0,
                quarter = 0,
                dime = 0,
                nickel = 0,
                penny = 0
            };

            Debit = new PaymentCard
            {
                Descritpion="Debit",
                Amount = 0.00M
            };

            MasterCard = new PaymentCard
            {
                Descritpion = "MasterCard",
                Amount = 0.00M
            };

            Visa = new PaymentCard
            {
                Descritpion = "Visa",
                Amount = 0.00M
            };

            Amex = new PaymentCard
            {
                Descritpion = "Amex",
                Amount = 0.00M
            };

            Other = new PaymentCard
            {
                Descritpion = "Other",
                Amount = 0.00M
            };
        }

    }
}
