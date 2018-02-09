using CashApp.Model.Model;
using System;

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
           set
           {
                SetValue(value);
                OnPropertyChanged();
           }
        }
        public DateTime Date
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
                ValidateProperty(nameof(Date));
            }
        }

        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            if (Date > DateTime.Now)
            {
                AddErrors(propertyName, "Cannot add future dated transactions");
            }
        }
        public SalesPerson SalesPerson
        {
            get { return GetValue<SalesPerson>(); ; }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public decimal FloatStart
        {
            get { return GetValue<decimal>();  }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public CashTotal CashTotals
        {
            get { return GetValue<CashTotal>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public decimal CashDeposit
        {
            get { return GetValue<decimal>();  }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public PaymentCard Debit
        {
            get { return GetValue<PaymentCard>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public PaymentCard MasterCard
        {
            get { return GetValue<PaymentCard>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public PaymentCard Visa
        {
            get { return GetValue<PaymentCard>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public PaymentCard Amex
        {
            get { return GetValue<PaymentCard>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public PaymentCard Other
        {
            get { return GetValue<PaymentCard>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public decimal ChequeTotal
        {
            get { return GetValue<decimal>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public decimal FloatEnd
        {
            get { return GetValue<decimal>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public decimal Disbursement
        {
            get { return GetValue<decimal>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public decimal Returns
        {
            get { return GetValue<decimal>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public ZRead DailyZRead
        {
            get { return GetValue<ZRead>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
        public decimal Variance
        {
            get { return GetValue<decimal>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged();
            }
        }
    }
}
