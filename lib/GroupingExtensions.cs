using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using JollazApiQueries.Library.Models.Requests;

namespace JollazApiQueries.Library.Extensions
{
    public static class GroupingExtensions
    {
        ///<summary>
        /// Extension method that allows grouping a query, based on a DataRequest.
        /// <param name="query">The query where the grouping will be applied.</param>
        /// <param name="dataRequest">The data request, which contains the lambdas to group the query.</param>
        /// <returns>Returns an object of type IQueryable, which is a collection of grouped anonymous objects.</returns>
        ///</summary>
        public static IQueryable GroupByDataRequest(this IQueryable query, DataRequest dataRequest)
        {
            if (string.IsNullOrEmpty(dataRequest.Grouping))
            {
                return query;
            }
            try
            {
                return query.GroupBy(dataRequest.Grouping);
            }
            catch (ParseException)
            {
                throw new ArgumentException(ResourceManagerUtils.ErrorMessages.UnableToGroup);
            }
        }
    }
}