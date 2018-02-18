using CashApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Data.Repositories
{
    public interface IZReadRepository
    {
        void Remove(ZRead zread);
        void Add(ZRead zread);
        Task SaveChanges();
    }
}
