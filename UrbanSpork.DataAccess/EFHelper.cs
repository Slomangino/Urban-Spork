using System;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.Common
{
    public class EFHelper
    {
        public static void InsertOrUpdate(ApproverActivityProjection projection)
        {
            using (var context = new UrbanDbContext())
            {
                context.Entry(projection).State = projection.Id == Guid.Empty ?
                    EntityState.Added :
                    EntityState.Modified;
            }
        }
    }
}