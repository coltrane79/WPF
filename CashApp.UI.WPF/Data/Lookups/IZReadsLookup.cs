using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashApp.Model.Model;

namespace CashApp.UI.WPF.Data.Lookups
{
    public interface IZReadsLookup
    {
        Task<IEnumerable<ZRead>> GetZReadsAsync(DateTime Date);
    }
}