using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveCSharp
{
    public static class QueryableExtensions
    {

        //Sample from http://jnye.co/Posts/6/c%23-generic-search-extension-method-for-iqueryable
        public static IQueryable<T> StartsWith<T>(this IQueryable<T> source, Expression<Func<T, string>> stringProperty, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return source;
            }
            var searchTermExpression = Expression.Constant(searchTerm);
            //Note Below instead of GetMethod("Contains") have used GetMethod(nameof(string.Contains))
            var startWithExpression = Expression.Call(stringProperty.Body, typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) }), searchTermExpression);
            var methodCallExpression = Expression.Call(typeof(Queryable), "Where", new Type[] { source.ElementType },
                source.Expression, Expression.Lambda<Func<T, bool>>(startWithExpression, stringProperty.Parameters));
            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

    }
}
