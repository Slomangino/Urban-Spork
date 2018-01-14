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

        //public TResult Process<TResult>(IQuery<TResult> query)

        //{
        //    //var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        //    //var handler = (IQueryHandler<IQuery<TResult>, TResult>)_context.GetService(handlerType);

        //    //var handler = _container.Resolve<IQueryHandler<IQuery<TResult>, TResult>>();
        //    var handler = (IQueryHandler<IQuery<TResult>, TResult>)_context.Resolve<IQueryHandler<IQuery<TResult>, TResult>>();

        //    //change

        //    return handler.Handle(query);
        //}

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var tQueryType = query.GetType();
            var tResultType = typeof(TResult);

            // create process internal method dynamically
            // TQuery = tQueryType
            // TResult = tResultType
            TResult myResult = (TResult)Activator.CreateInstance(tResultType);
            IQuery<TResult> myQuery = (IQuery<TResult>) Activator.CreateInstance(tQueryType);
            //object myResult = Activator.CreateInstance(tResultType);


            // invoke method dynamically
            object result = ProcessInternal<IQuery<TResult>, TResult>(myQuery);
            return (TResult)result;
        }

        private TResult ProcessInternal<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler = _context.Resolve<IQueryHandler<TQuery, TResult>>();
            return handler.Handle(query);
        }
    }
}
