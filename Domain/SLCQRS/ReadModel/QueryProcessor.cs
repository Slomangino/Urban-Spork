using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;

namespace UrbanSpork.Domain.SLCQRS.ReadModel
{
    public class QueryProcessor : IQueryProcessor
    {
        //private readonly IContainer _container;

        //public QueryProcessor(IContainer container)
        //{
        //    _container = container;
        //}
        private readonly IComponentContext _context;

        public QueryProcessor(IComponentContext context)
        {
            _context = context;
        }

        public TResult Process<TResult>(IQuery<TResult> query)

        {
            //var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            //var handler = (IQueryHandler<IQuery<TResult>, TResult>)_context.GetService(handlerType);

            //var handler = _container.Resolve<IQueryHandler<IQuery<TResult>, TResult>>();
            var handler = (IQueryHandler<IQuery<TResult>, TResult>)_context.Resolve<IQueryHandler<IQuery<TResult>, TResult>>();

            //change

            return handler.Handle(query);
        }
    }
}
