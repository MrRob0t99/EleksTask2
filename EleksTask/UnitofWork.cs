using System.Threading.Tasks;

namespace EleksTask
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationContext Context { get; }

        public UnitOfWork(ApplicationContext context)
        {
            Context = context;
        }
        public async Task Commit()
        {
           await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
