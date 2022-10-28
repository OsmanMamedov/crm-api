using General.Core.Entities.Abstract;
namespace General.Core.Entities.Concrete
{
    public class Response : IResponse
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }
        public object Results { get; set; }
    }
}
