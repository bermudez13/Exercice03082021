using System;
using System.Threading.Tasks;

namespace Exercice03082021.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}