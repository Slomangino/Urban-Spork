using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Filters;

namespace UrbanSpork.Common.FilterCriteria
{
    public class UserFilterCriteria : IFilterCriteria
    {
        public string SearchTerms { get; set; }

        public string SortDirection { get; set; }

        public string SortField { get; set; }

        public bool EnablePaging { get; set; }

        public bool IncludeIsAdmin { get; set; }

        public bool IncludeInactive { get; set; }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }
    }
}
