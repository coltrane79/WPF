using CashApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.Data
{
    public class CashAppDbContext : DbContext
    {
        public DbSet<CashBalanceSheet> CashBalanceSheets { get; set; }
        public DbSet<SalesPerson> SalesPeople { get; set; }
        public DbSet<ZRead> ZReads { get; set; }
    }
}
