using CashApp.Model.Model;
using System.Data.Entity;

namespace CashApp.Data
{
    public class CashAppDbContext : DbContext
    {
        public DbSet<CashBalanceSheet> CashBalanceSheets { get; set; }
        public DbSet<SalesPerson> SalesPeople { get; set; }
        public DbSet<ZRead> ZReads { get; set; }
    }
}
