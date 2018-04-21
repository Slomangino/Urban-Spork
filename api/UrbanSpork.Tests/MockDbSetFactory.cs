using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;

namespace UrbanSpork.Tests
{
    public static class MockDbSetFactory
    {
        // to return some test data, put some data in this list that is received
        public static Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());
            dbSetMock.As<IAsyncEnumerable<T>>().Setup(x => x.GetEnumerator())
                .Returns(() => queryableList.AsAsyncEnumerable().GetEnumerator());
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(() => queryableList.Provider);

            return dbSetMock;
        }
    }
}
