using System.Threading.Tasks;
using Exercice03082021.Core.Models;
using Exercice03082021.Core.Models.Querys;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace Exercice03082021.Core
{
    public interface IUserRepository
    {
         Task<User> GetUserById(long id);

        void Add(User user);
        void Remove(User user);

        Task<User> GetUserByUser(string user);

        //AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}