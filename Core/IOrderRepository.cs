using System.Threading.Tasks;
using Exercice03082021.Core.Models;
using Exercice03082021.Core.Models.Querys;

namespace Exercice03082021.Core
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdUser(long id, bool includeRelated = true); 
        void Add(Order order);
        void Remove(Order order);
        //Task<QueryResult<Order>> GetOrders(OrderQuery filter);
    }
}