using CashApp.Model.Model;
using CashApp.UI.WPF.ModelWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Data.Repositories
{ 
    public interface IBalanceSheetRespository
    {
        Task<CashBalanceSheet> GetByIdAsync(int id);
        Task SaveAsync();
        bool HasChanges();
        void Add(CashBalanceSheet balanceSheet);
        void DeletebyIdAsync(CashBalanceSheet balanceSheet);
    }
}
