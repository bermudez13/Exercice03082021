namespace Exercice03082021.Controllers.Resources.QuerysResources
{
    public class OrderQueryResource
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}