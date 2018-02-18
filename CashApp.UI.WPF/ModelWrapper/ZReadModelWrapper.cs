using CashApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.ModelWrapper
{
    public class ZReadModelWrapper: ModelWrapper<ZRead>
    {
        public ZReadModelWrapper(ZRead model)
            : base(model)
        {

        }
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public DateTime ZReadDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
        public decimal SalesTotal
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal TaxTotal
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal GiftCertificate
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal Coupon
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal Void
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal Net
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal Gross
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal Cash
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public decimal CashInDrawer
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            if (ZReadDate > DateTime.Now)
            {
                yield return "Cannot add future dated transactions";
            }
        }
    }
}
