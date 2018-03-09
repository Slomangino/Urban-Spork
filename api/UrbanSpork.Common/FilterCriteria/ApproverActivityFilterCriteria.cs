using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Filters;

namespace UrbanSpork.Common.FilterCriteria
{
    public class ApproverActivityFilterCriteria : IFilterCriteria
    {
        public string SearchTerms { get; set; }
        public string SortDirection { get; set; } = "ASC";
        public string SortField { get; set; }
        public bool EnablePaging { get; set; }
        public DateTime? MinDate { get; set; } = null;
        public DateTime? MaxDate { get; set; } = null;
    }
}
