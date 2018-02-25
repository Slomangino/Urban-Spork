using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.DataAccess.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> leftSpecification;
        ISpecification<T> rightSpecification;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.leftSpecification = left;
            this.rightSpecification = right;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return this.leftSpecification.IsSatisfiedBy(o)
                   || this.rightSpecification.IsSatisfiedBy(o);
        }
    }
}
