using System.Collections.Generic;

namespace Exercice03082021.Core.Models.Querys
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}