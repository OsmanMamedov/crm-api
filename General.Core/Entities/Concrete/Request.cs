
using General.Core.Entities.Concrete;
using System.Collections.Generic;

namespace General.Core.Entities
{
    public class Request : Entity, IRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<FilterObject> Filters { get; set; }
        public List<SortObject> Sorts { get; set; }
        public List<string> Includes { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
        public string Search { get; set; } = "";
        public string Sort { get; set; } = "";
        public bool Desc { get; set; } = false;
        public Login Login { get; set; }
        public long CompanyId { get; set; }
        public bool Status { get; set; }
        public int DayCount { get; set; }
    }

    public class IncludeObject
    {
        public string Include { get; set; }
        public string ThenInclude { get; set; }
        public bool Then { get; set; }
    }

    public class FilterObject
    {
        public string Column { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
    }

    public class SortObject
    {
        public string Column { get; set; }
        public bool Asc { get; set; }
    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
