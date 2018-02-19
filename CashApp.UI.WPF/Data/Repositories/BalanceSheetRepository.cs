using CashApp.Data;
using CashApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CashApp.UI.WPF.ModelWrapper;

namespace CashApp.UI.WPF.Data.Repositories
{
    public class BalanceSheetRepository : GenericRepository<CashBalanceSheet, CashAppDbContext>, IBalanceSheetRespository
    {
        public BalanceSheetRepository(CashAppDbContext context)
            : base(context)
        {

        }
        public override async Task<CashBalanceSheet> GetByIdAsync(int? id)
        {
            return await Context.CashBalanceSheets
                .Include("SalesPerson")
                .SingleAsync<CashBalanceSheet>(c => c.Id == id);

        }
        
    }
}
