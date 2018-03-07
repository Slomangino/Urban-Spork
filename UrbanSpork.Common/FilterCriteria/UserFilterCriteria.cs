using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Filters;

namespace UrbanSpork.Common.FilterCriteria
{
    public class UserFilterCriteria : IFilterCriteria
    {
        public string SearchTerms { get; set; }

        public string SortDirection { get; set; } = "ASC";

        public string SortField { get; set; } = "FirstName";

        public bool EnablePaging { get; set; } = false;

        public bool IncludeIsAdmin { get; set; } = true;

        public bool IncludeInactive { get; set; } = false;

        public DateTime? MinDate { get; set; } = null;

        public DateTime? MaxDate { get; set; } = null;

        public string FilterField { get; set; } = "";

        public string FilterFieldInput { get; set; } = "";

    }
}
