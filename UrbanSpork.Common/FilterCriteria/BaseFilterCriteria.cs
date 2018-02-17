using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UrbanSpork.Common.FilterCriteria
{
    public class BaseFilterCriteria
    {
        public string SearchTerms = "";

        public string SortDirection = "ASC";

        public string SortField = "";

        public bool EnablePaging = true;
    }
}
