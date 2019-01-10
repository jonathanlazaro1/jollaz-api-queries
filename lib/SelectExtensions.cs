using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using JollazApiQueries.Library.Models.Requests;

namespace JollazApiQueries.Library.Extensions
{
    public static class SelectExtensions
    {
        ///<summary>
        /// Extension method that allows selecting custom properties in a query, based on a Data Request.
        /// <param name="query">The query where the selecting will be applied.</param>
        /// <param name="dataRequest">The data request, which contains the property names that will be used to select query properties.</param>
        /// <typeparam name="T">The query data type.</typeparam>
        /// <returns>Returns an object of type IQueryable, which is a collection of anonymous objects with the properties selected before.</returns>
        ///</summary>
        public static IQueryable SelectByDataRequest(this IQueryable query, DataRequest dataRequest)
        {
            if (dataRequest.Select.Count() < 1)
            {
               return query;
            }
            string ret = string.Join(", ", dataRequest.Select);
            try
            {
                return query.Select($"new ({ret})");
            }
            catch (ParseException)
            {
                throw new ArgumentException(ResourceManagerUtils.ErrorMessages.UnableToSelect);
            }
        }
    }
}