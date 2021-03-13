using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Exercice03082021.Core;
using Exercice03082021.Core.Models;
using Exercice03082021.Core.Models.Querys;
using Exercice03082021.Extensions;

namespace Exercice03082021.Persistence
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApiContext context;
        public ProductRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetProductById(long id)
        {
            return await context.Products.FindAsync(id);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
        }

        public void Remove(Product product)
        {
            context.Remove(product);
        }

        /*public async Task<QueryResult<Product>> GetProducts(ProductQuery queryObj)
        {
            var result = new QueryResult<Product>();
            var query = context.Products.AsQueryable();

            var columnsMap = new Dictionary<string, Expression<Func<Product, object>>>()
            {
                ["name"] = v => v.Name
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }*/
    }
}