
using System.Collections.Generic;

namespace General.Core.Entities
{
    public interface IRequest
    {
        int Skip { get; set; }
        int Take { get; set; }
        List<FilterObject> Filters { get; set; }
        List<SortObject> Sorts { get; set; }
    }
}