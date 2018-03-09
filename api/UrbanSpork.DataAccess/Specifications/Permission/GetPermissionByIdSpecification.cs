using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.DataAccess.Specifications.Permission
{
    public class GetPermissionByIdSpecification : CompositeSpecification<PermissionDetailProjection>
    {
        private readonly Guid Id;

        public GetPermissionByIdSpecification(Guid id)
        {
            Id = id;
        }

        public override bool IsSatisfiedBy(PermissionDetailProjection o)
        {
            return o.PermissionId == Id;
        }
    }
}
