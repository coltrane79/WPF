using CashApp.Data;
using CashApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Data.Lookups
{
    public class LookupItem : IBalanceSheetLookupItem, ISalesPersonLookupItem, IZReadsLookup
    {
        public async Task<IEnumerable<CashBalanceSheetLookupItem>> GetBalanceSheetsAsync()
        {
            using (var ctx = new CashAppDbContext())
            {
                return await ctx.CashBalanceSheets.AsNoTracking()
                    .Select(b =>
                        new CashBalanceSheetLookupItem
                        {
                            Id = b.Id,
                            DisplayMember = b.Date
                        })
                        .ToListAsync();
            }
        }

        public async Task<IEnumerable<SalesPersonLookupItem>> GetSalesPersonAsync()
        {
            using (var ctx = new CashAppDbContext())
            {
                return await ctx.SalesPeople.AsNoTracking()
                    .Select(b =>
                        new SalesPersonLookupItem
                        {
                            Id = b.Id,
                            DisplayMember = b.firstName + " " + b.lastName
                        })
                        .ToListAsync();
            }
        }

        public async Task<IEnumerable<ZRead>> GetZReadsAsync(DateTime Date)
        {
            using (var ctx = new CashAppDbContext())
            {
                return await ctx.ZReads.AsNoTracking()
                    .Where(z => z.ZReadDate == Date)
                    .ToListAsync();
                        
            }
        }
    }
}
