using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using JollazApiQueries.Models.Requests;

namespace JollazApiQueries.Library.Extensions
{
    public static class OrderingExtensions
    {
        public static IQueryable<T> OrderByDataRequest<T>(this IQueryable<T> query, DataRequest request)
        {
            foreach (var ordering in request.Ordering.Reverse())
            {
                query = query.OrderByOrderingItem<T>(ordering);
            }
            return query;
        }

        private static IQueryable<T> OrderByOrderingItem<T>(this IQueryable<T> query, OrderingItem ordering)
        {
            try
            {
                return query.OrderBy($"{ordering.Name} {(ordering.Descending ? "desc" : "asc")}");
            }
            catch (ParseException)
            {
                throw new ArgumentException($"{ResourceManagerUtils.ErrorMessages.PropertyNotFound}: {ordering.Name}");
            }
        }
    }
}