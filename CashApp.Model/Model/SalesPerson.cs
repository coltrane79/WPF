using System.ComponentModel.DataAnnotations;

namespace CashApp.Model.Model
{
    public class SalesPerson
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public override string ToString()
        {
            return firstName + " " + lastName;
        }

        public SalesPerson()
        {

        }
    }
}