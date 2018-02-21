using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.FilterCriteria
{
    public class UserFilterCriteria : BaseFilterCriteria
    {
        public bool IncludeIsAdmin { get; set; }

        public bool IncludeInactive { get; set; }
    }
}
