using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashApp.Model.Model;
using CashApp.Data;

namespace CashApp.UI.WPF.Data.Repositories
{
    public class ZReadRespository : GenericRepository<ZRead,CashAppDbContext>, IZReadRepository
    {
        public ZReadRespository(CashAppDbContext Context) : base(Context)
        {
           
        }
    }
}
