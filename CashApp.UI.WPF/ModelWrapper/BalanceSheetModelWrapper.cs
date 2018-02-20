using CashApp.Model.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CashApp.UI.WPF.ModelWrapper
{
    public class BalanceSheetModelWrapper : ModelWrapper<CashBalanceSheet>
    {
        public BalanceSheetModelWrapper(CashBalanceSheet Model)
            : base(Model)
        {

        }
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        [Required]
        public DateTime Date
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                //ValidateProperty(nameof(Date));
            }
        }    
        public SalesPerson SalesPerson
        {
            get { return GetValue<SalesPerson>(); ; }
            set { SetValue(value); }
        }
        public decimal FloatStart
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public CashTotal CashTotals
        {
            get { return GetValue<CashTotal>(); }
            set { SetValue(value); }
        }
        public decimal CashDeposit
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public PaymentCard Debit
        {
            get { return GetValue<PaymentCard>(); }
            set { SetValue(value); }
        }
        public PaymentCard MasterCard
        {
            get { return GetValue<PaymentCard>(); }
            set { SetValue(value); }
        }
        public PaymentCard Visa
        {
            get { return GetValue<PaymentCard>(); }
            set { SetValue(value); }
        }
        public PaymentCard Amex
        {
            get { return GetValue<PaymentCard>(); }
            set { SetValue(value); }
        }
        public PaymentCard Other
        {
            get { return GetValue<PaymentCard>(); }
            set { SetValue(value); }
        }
        public decimal ChequeTotal
        {
            get { return GetValue<decimal>(); }            
            set { SetValue(value); }
        }
        public decimal FloatEnd
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal Disbursement
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal Returns
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public ZRead DailyZRead
        {
            get { return GetValue<ZRead>(); }
            set { SetValue(value); }
        }
        public decimal Variance
        {
            get { return GetValue<decimal>(); }
           set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            if (Date > DateTime.Now)
            {
                yield return "Cannot add future dated transactions";
            }
        }
    }
}
