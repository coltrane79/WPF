using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashApp.Model.Model;
using CashApp.Data;

namespace CashApp.UI.WPF.Data.Repositories
{
    public class ZReadRespository : IZReadRepository
    {
        private CashAppDbContext _context;
        public ZReadRespository(CashAppDbContext Context)
        {
            _context = Context;
        }

        public void Add(ZRead zread)
        {
            _context.ZReads.Add(zread);
        }

        public async void Remove(ZRead zread)
        {
            _context.ZReads.Attach(zread);
            _context.ZReads.Remove(zread);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
