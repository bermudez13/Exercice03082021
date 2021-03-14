using System.Collections.Generic;

namespace Exercice03082021.Controllers.Resources.QuerysResources
{
     public class QueryResultResource<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}