using Autofac;
using System.Reflection;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.CQRS.Queries.QueryHandler;

namespace UrbanSpork.ReadModel
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IComponentContext _context;

        public QueryProcessor(IComponentContext context)
        {
            _context = context;
        }

        public Task<TResult> Process<TResult>(IQuery<TResult> query)
        {
            var tQueryType = query.GetType();
            var tResultType = typeof(TResult);

            // create process internal method dynamically
            MethodInfo method = typeof(QueryProcessor).GetMethod("ProcessInternal");
            MethodInfo generic = method.MakeGenericMethod(tQueryType, tResultType);
            
            // invoke method dynamically
            object result = generic.Invoke(this, new[] { query });
            return (Task<TResult>)result;
        }

        public Task<TResult> ProcessInternal<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler = _context.Resolve<IQueryHandler<TQuery, TResult>>();
            return handler.Handle(query);
        }
    }
}
