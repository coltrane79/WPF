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
    public class BalanceSheetRepository : IBalanceSheetRespository
    {
        private CashAppDbContext _context;

        public BalanceSheetRepository(CashAppDbContext Context)
        {
            _context = Context;
        }

        public void Add(CashBalanceSheet balanceSheet)
        {
            _context.CashBalanceSheets.Add(balanceSheet);
        }

        public async Task<CashBalanceSheet> GetByIdAsync(int id)
        {
            return await _context.CashBalanceSheets
                .Include("SalesPerson")
                .SingleAsync<CashBalanceSheet>(c => c.Id == id);

        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {            
            await _context.SaveChangesAsync();
        }
    }
}
