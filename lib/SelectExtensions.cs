using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using JollazApiQueries.Models.Requests;

namespace JollazApiQueries.Library.Extensions
{
    public static class SelectExtensions
    {
        public static IQueryable SelectByDataRequest<T>(this IQueryable<T> query, DataRequest request)
        {
            string ret = string.Join(", ", request.Select);
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