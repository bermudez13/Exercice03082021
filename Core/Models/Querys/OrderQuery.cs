using Exercice03082021.Extensions;

namespace Exercice03082021.Core.Models.Querys
{
    public class OrderQuery:IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}