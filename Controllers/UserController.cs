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
    [Route("api/users")]
    public class UserController:Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository repository;
        public UserController(IUserRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = mapper.Map<UserResource, User>(userResource);

            repository.Add(item);
            await unitOfWork.CompleteAsync();

            item = await repository.GetUserById(item.ID);

            var result = mapper.Map<User, UserResource>(item);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await repository.GetUserById(id);

            if (item == null)
                return NotFound();

            mapper.Map<UserResource, User>(userResource, item);

            await unitOfWork.CompleteAsync();

            item = await repository.GetUserById(item.ID);
            var result = mapper.Map<User, UserResource>(item);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var item = await repository.GetUserById(id);

            if (item == null)
                return NotFound();

            repository.Remove(item);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var item = await repository.GetUserById(id);

            if (item == null)
                return NotFound();

            var userResource = mapper.Map<User, UserResource>(item);

            return Ok(userResource);
        }
        [HttpGet]
        public async Task<QueryResultResource<UserResource>> GetUsers(UserQueryResource filterResource)
        {
            var filter = mapper.Map<UserQueryResource, UserQuery>(filterResource);
            var queryResult = await repository.GetUsers(filter);

            return mapper.Map<QueryResult<User>, QueryResultResource<UserResource>>(queryResult);
        }

        [HttpGet("{user}")]
        public async Task<IActionResult> GetUserByUser(string user)
        {
            var item = await repository.GetUserByUser(user);

            if (item == null)
                return NotFound();

            var userResource = mapper.Map<User, UserResource>(item);

            return Ok(userResource);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = repository.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}