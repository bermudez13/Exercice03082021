using AutoMapper;
using Exercice03082021.Controllers.Resources;
using Exercice03082021.Controllers.Resources.QuerysResources;
using Exercice03082021.Core.Models;
using Exercice03082021.Core.Models.Querys;
using System.Linq;

namespace Exercice03082021.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Domain to Api Resource

            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            CreateMap<Order, OrderResource>();

            CreateMap<Product, ProductResource>();

            CreateMap<User, UserResource>();
            #endregion

            //Api Resource to Domain
            #region  Resource to Domain
            CreateMap<OrderResource , Order>();

            CreateMap<ProductResource, Product>();

            CreateMap<UserResource, User>();

            //Querys
            CreateMap<OrderQueryResource,OrderQuery>();

            CreateMap<ProductQueryResource , ProductQuery>();

            CreateMap<UserQueryResource, UserQuery>();
            #endregion

        }
    }
}