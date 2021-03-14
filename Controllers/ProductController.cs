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

namespace Exercice03082021.Controllers
{
    [Route("api/products")]
    public class ProductController:Controller
    {
         private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository repository;
        public ProductController(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = mapper.Map<ProductResource, Product>(productResource);

            repository.Add(item);
            await unitOfWork.CompleteAsync();

            item = await repository.GetProductById(item.ID);

            var result = mapper.Map<Product, ProductResource>(item);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await repository.GetProductById(id);

            if (item == null)
                return NotFound();

            mapper.Map<ProductResource, Product>(productResource, item);

            await unitOfWork.CompleteAsync();

            item = await repository.GetProductById(item.ID);
            var result = mapper.Map<Product, ProductResource>(item);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var item = await repository.GetProductById(id);

            if (item == null)
                return NotFound();

            repository.Remove(item);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var item = await repository.GetProductById(id);

            if (item == null)
                return NotFound();

            var productResource = mapper.Map<Product, ProductResource>(item);

            return Ok(productResource);
        }
        [HttpGet]
        public async Task<QueryResultResource<ProductResource>> GetProducts(ProductQueryResource filterResource)
        {
            var filter = mapper.Map<ProductQueryResource, ProductQuery>(filterResource);
            var queryResult = await repository.GetProducts(filter);

            return mapper.Map<QueryResult<Product>, QueryResultResource<ProductResource>>(queryResult);
        }
    }
}