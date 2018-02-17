﻿using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetUserCollectionQuery : IQuery<List<UserDTO>>
    {
        public UserFilterCriteria FilterCriteria; 
    }
}
