using System.Threading.Tasks;
using Exercice03082021.Core;

namespace Exercice03082021.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext context;

        public UnitOfWork(ApiContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}