using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.FilterCriteria
{
    public class SystemReportFilterCriteria
    {
        public Guid PermissionId { get; set; } = Guid.Empty;

        public string SearchTerms { get; set; } = "";

        public string SortDirection { get; set; } = "ASC";

        public string SortField { get; set; } = "ForFullName";

        public bool EnablePaging { get; set; } = false;

        public DateTime? EndDate { get; set; } = DateTime.UtcNow;
    }
}
