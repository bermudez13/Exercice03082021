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
    public class UserRepository:IUserRepository
    {
        private readonly ApiContext context;
        public UserRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserById(long id)
        {
            return await context.Users.FindAsync(id);
        }

        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public void Remove(User user)
        {
            context.Remove(user);
        }

        public async Task<User> GetUserByUser(string user)
        {
            return await context.Users.FindAsync(user);
        }

        
    }
}