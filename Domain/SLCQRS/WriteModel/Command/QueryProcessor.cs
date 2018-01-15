using System;
using System.Collections.Generic;
using System.Reflection;
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
            //TResult myResult = (TResult)Activator.CreateInstance(tResultType);

            //var myQuery = (IQuery<TResult>)Activator.CreateInstance(tQueryType);
            //var myResult = (TResult)Activator.CreateInstance(tResultType);

            //object myResult = Activator.CreateInstance(tResultType);


            // invoke method dynamically
            MethodInfo method = typeof(QueryProcessor).GetMethod("ProcessInternal");
            MethodInfo generic = method.MakeGenericMethod(tQueryType, tResultType);
            object result = generic.Invoke(this, new[] { query });
            

            //object result = ProcessInternal<tQueryType, TResult>(myQuery);
            return (TResult)result;
        }

        public TResult ProcessInternal<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler = _context.Resolve<IQueryHandler<TQuery, TResult>>();
            return handler.Handle(query);
        }
    }
}
