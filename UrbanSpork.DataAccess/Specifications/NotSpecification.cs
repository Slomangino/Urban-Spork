using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.DataAccess.Specifications
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> Specification;

        public NotSpecification(ISpecification<T> spec)
        {
            this.Specification = spec;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return !this.Specification.IsSatisfiedBy(o);
        }
    }
}
