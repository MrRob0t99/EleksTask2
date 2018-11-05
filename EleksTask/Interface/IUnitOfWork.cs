using System;
using System.Threading.Tasks;

namespace EleksTask
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationContext Context { get; }
        Task Commit();
    }

}
