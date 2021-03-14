using System;
using Microsoft.AspNetCore.Mvc;
using Exercice03082021.Core.Models;
using Exercice03082021.Core.Models.Querys;
using Exercice03082021.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Exercice03082021.Controllers.Resources;
using Exercice03082021.Controllers.Resources.QuerysResources;
using Exercice03082021.Persistence;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Linq;
using Exercice03082021.Helpers;

namespace Exercice03082021.Controllers
{
    [Route("api/orders")]
    public class OrderController:Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderRepository repository;
        public OrderController(IOrderRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        //[Authorize(Roles = UserRole.Client)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderResource orderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = mapper.Map<OrderResource, Order>(orderResource);

            repository.Add(item);
            await unitOfWork.CompleteAsync();

            item = await repository.GetOrderByIdUser(item.UserID);

            var result = mapper.Map<Order, OrderResource>(item);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderResource orderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await repository.GetOrderByIdUser(id);

            if (item == null)
                return NotFound();

            mapper.Map<OrderResource, Order>(orderResource, item);

            await unitOfWork.CompleteAsync();

            item = await repository.GetOrderByIdUser(item.UserID);
            var result = mapper.Map<Order, OrderResource>(item);

            return Ok(result);
        }

        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderResource orderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var item = mapper.Map<OrderResource, Order>(orderResource);    

            repository.UpdateOrderStatus(item);
            await unitOfWork.CompleteAsync();

            if (item == null)
                return NotFound();

            mapper.Map<OrderResource, Order>(orderResource, item);

            await unitOfWork.CompleteAsync();

            item = await repository.GetOrderByIdUser(item.UserID);
            var result = mapper.Map<Order, OrderResource>(item);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var item = await repository.GetOrderByIdUser(id);

            if (item == null)
                return NotFound();
            
            if(item.Status==OrderStatus.confirmed)
                return BadRequest(new { message = "Can't delete confirmed order" });
             
             repository.Remove(item);
             await unitOfWork.CompleteAsync();

            return Ok(id);    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var item = await repository.GetOrderByIdUser(id);

            if (item == null)
                return NotFound();

            var orderResource = mapper.Map<Order, OrderResource>(item);

            return Ok(orderResource);
        }
        [HttpGet]
        public async Task<QueryResultResource<OrderResource>> GetOrders(OrderQueryResource filterResource)
        {
            var filter = mapper.Map<OrderQueryResource, OrderQuery>(filterResource);
            var queryResult = await repository.GetOrders(filter);

            return mapper.Map<QueryResult<Order>, QueryResultResource<OrderResource>>(queryResult);
        }
    }
}