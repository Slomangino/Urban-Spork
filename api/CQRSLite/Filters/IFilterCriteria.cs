using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.CQRS.Filters
{
    public interface IFilterCriteria
    {
        string SearchTerms { get; set; }

        string SortDirection { get; set; }

        string SortField { get; set; }

        bool EnablePaging { get; set; }
    }
}
