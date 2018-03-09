using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Filters;

namespace UrbanSpork.Common.FilterCriteria
{
    public class SystemActivityReportFilterCriteria : IFilterCriteria
    {
        public string SearchTerms { get; set; } = "";

        public string SortDirection { get; set; } = "DSC";

        public string SortField { get; set; } = "timestamp";

        public bool EnablePaging { get; set; } = false;

        public DateTime StartDate { get; set; } = new DateTime();

        public DateTime? EndDate { get; set; } = null;
    }
}
