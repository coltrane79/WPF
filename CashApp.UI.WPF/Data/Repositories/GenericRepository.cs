using System.Threading.Tasks;
using System.Data.Entity;

namespace CashApp.UI.WPF.Data.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity: class
        where TContext: DbContext
    {
        protected readonly TContext Context;

        public GenericRepository(TContext context)
        {
            this.Context = context;
        }
        public void Add(TEntity Model)
        {
            Context.Set<TEntity>().Add(Model);
        }

        public void DeletebyIdAsync(TEntity Model)
        {
            //if(Context.Set<TEntity>().ContainsAsync(Model))
            Context.Set<TEntity>().Attach(Model); ;
            Context.Set<TEntity>().Remove(Model);
            
        }

        public virtual async Task<TEntity> GetByIdAsync(int? id)
        {
            return await Context.Set<TEntity>()
                .FindAsync(id);
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
