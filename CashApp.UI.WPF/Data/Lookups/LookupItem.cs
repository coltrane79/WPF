using CashApp.Data;
using CashApp.Model.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Data.Lookups
{
    public class LookupItem : IBalanceSheetLookupItem
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
    }
}
