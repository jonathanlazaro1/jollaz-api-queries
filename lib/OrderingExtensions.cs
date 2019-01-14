using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using JollazApiQueries.Library.Models.Requests;

namespace JollazApiQueries.Library.Extensions
{
    public static class OrderingExtensions
    {
        ///<summary>
        /// Extension method that allows query ordering based on a data request.
        /// <param name="query">The query where the ordering will be applied.</param>
        /// <param name="dataRequest">The data request, which contains the property names that will be used to sort the query.</param>
        /// <returns>Returns the query with the ordering applied and ready to fetch.</returns>
        ///</summary>
        public static IQueryable OrderByDataRequest(this IQueryable query, DataRequest dataRequest)
        {
            foreach (var ordering in dataRequest.Ordering.Reverse())
            {
                query = query.OrderByOrderingItem(ordering);
            }
            return query;
        }

        ///<summary>
        /// Extension method that allows query ordering based on an ordering item.
        /// <param name="query">The query where the ordering will be applied.</param>
        /// <param name="ordering">The ordering item that will be parsed and applied to the query.</param>
        /// <returns>Returns the query with the ordering applied and ready to fetch.</returns>
        ///</summary>
        private static IQueryable OrderByOrderingItem(this IQueryable query, OrderingItem ordering)
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