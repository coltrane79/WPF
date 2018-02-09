using CashApp.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Data.Lookups
{
    public interface IBalanceSheetLookupItem
    {
        Task<IEnumerable<CashBalanceSheetLookupItem>> GetBalanceSheetsAsync();
    }
}
