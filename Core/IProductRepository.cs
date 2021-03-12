using System.Threading.Tasks;
using Exercice03082021.Core.Models;
using Exercice03082021.Core.Models.Querys;

namespace Exercice03082021.Core
{
    public interface IProductRepository
    {
         Task<Product> GetProductById(long id); 
        void Add(Product product);
        void Remove(Product product);
        //Task<QueryResult<Product>> GetProducts(ProductQuery filter);
    }
}