using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UrbanSpork.Common.FilterCriteria
{
    public class BaseFilterCriteria
    {
        public string SearchTerms { get; set; }

        public string SortDirection { get; set; }

        public string SortField { get; set; }

        public bool EnablePaging { get; set; }
    }
}
