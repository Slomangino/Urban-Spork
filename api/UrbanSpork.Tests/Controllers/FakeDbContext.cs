using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UrbanSpork.Tests.Controllers
{
    public class FakeDbContext<T> : DbSet<T>, IQueryable, IEnumerable<T> where T : class
    {
        List<T> _data;

        public FakeDbContext()
        {
            _data = new List<T>();
        }

        public override T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public override EntityEntry<T> Add(T item)
        {
            _data.Add(item);
            return null;
        }

        public override EntityEntry<T> Remove(T item)
        {
            _data.Remove(item);
            return null;
        }

        public override EntityEntry<T> Attach(T item)
        {
            return null;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public List<T> Local
        {
            get { return _data; }
        }

        public override void AddRange(IEnumerable<T> entities)
        {
            _data.AddRange(entities);
        }

        public override void RemoveRange(IEnumerable<T> entities)
        {
            for (int i = entities.Count() - 1; i >= 0; i--)
            {
                T entity = entities.ElementAt(i);
                if (_data.Contains(entity))
                {
                    Remove(entity);
                }
            }
        }

        Type IQueryable.ElementType
        {
            get { return _data.AsQueryable().ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _data.AsQueryable().Provider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
