using System.Collections.Generic;
using System.Threading.Tasks;
using CashApp.Model.Model;

namespace CashApp.UI.WPF.Data.Lookups
{
    public interface ISalesPersonLookupItem
    {
        Task<IEnumerable<SalesPersonLookupItem>> GetSalesPersonAsync();
    }
}