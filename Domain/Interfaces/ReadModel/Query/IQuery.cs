﻿using System;
namespace UrbanSpork.CQRS.Interfaces.ReadModel
{
    //<TResult> is the query's return type, generic so we can handle many types of queries
    public interface IQuery<TResult>
    {
    }
}
