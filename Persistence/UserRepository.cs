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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Exercice03082021.Helpers;

namespace Exercice03082021.Persistence
{
    public class UserRepository:IUserRepository
    {
        private readonly ApiContext context;
        private readonly AppSettings appSettings;
        public UserRepository(ApiContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
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

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = this.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<QueryResult<User>> GetUsers(UserQuery queryObj)
        {
            var result = new QueryResult<User>();
            var query = context.Users.AsQueryable();

            var columnsMap = new Dictionary<string, Expression<Func<User, object>>>()
            {
                ["FirstName"] = v => v.FirstName,
                ["LastName"] = v => v.LastName,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}