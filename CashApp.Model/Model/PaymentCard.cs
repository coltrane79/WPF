namespace CashApp.Model.Model
{
    public class PaymentCard
    {
        private string _description;
        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string Descritpion
        {
            get { return _description; }
            set { _description = value; }
        }

        public PaymentCard()
        {

        }

    }
}