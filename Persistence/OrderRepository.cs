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
    public class OrderRepository:IOrderRepository
    {
        private readonly ApiContext context;
        public OrderRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<Order> GetOrderByIdUser(long id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Orders.FindAsync(id);

            return await context.Orders.SingleOrDefaultAsync();
        }

        public void Add(Order item)
        {
            context.Orders.Add(item);
        }

        public void Remove(Order item)
        {
            context.Remove(item);
        }

        /*public async Task<QueryResult<HospitalDevice>> GetHospitalDevices(HospitalDeviceQuery queryObj)
        {
            var result = new QueryResult<HospitalDevice>();
            var query = context.HospitalDevices.AsQueryable();

            var columnsMap = new Dictionary<string, Expression<Func<HospitalDevice, object>>>()
            {
                ["HospitalID"] = v => v.HospitalID,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }*/
    }
}