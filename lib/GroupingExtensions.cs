using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using JollazApiQueries.Model.Core;
using JollazApiQueries.Model.Core.Errors;

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
                return query.GroupBy(dataRequest.Grouping).ToKeyAndValuesResult();
            }
            catch (ParseException)
            {
                throw new ArgumentException(ResourceManagerUtils.ErrorMessages.UnableToGroup);
            }
        }

        private static IQueryable<GroupedData> ToKeyAndValuesResult(this IQueryable data)
        {
            var ret = new List<GroupedData>();
            foreach (var group in data.ToDynamicList())
            {
                var retItem = new GroupedData();
                retItem.Key = group.Key; 
                foreach (var value in group)
                {
                    retItem.Values.Add(value);
                }
                ret.Add(retItem);
            }
            return ret.AsQueryable();
        }
    }

    public class GroupedData
    {
        public dynamic Key { get; set; }

        public ICollection<dynamic> Values { get; set; }

        public GroupedData()
        {
            this.Values = new List<dynamic>();
        }
    }
}