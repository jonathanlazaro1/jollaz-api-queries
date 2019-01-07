using System.Linq;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Results;

namespace JollazApiQueries.Library.Extensions
{
    public static class DataRequestExtensions
    {
        private static IQueryable<T> ProccessQuery<T>(IQueryable<T> query, DataRequest dataRequest, bool filtering, bool sorting)
        {
            if (filtering)
            {
                query = query.FilterByDataRequest(dataRequest);
            }
            if (sorting)
            {
                query = query.OrderByDataRequest(dataRequest);
            }
            return query;
        }

        public static DataResult<T> Proccess<T>(this DataRequest dataRequest, IQueryable<T> query, bool filtering = true, bool sorting = true)
        {
            query = ProccessQuery(query, dataRequest, filtering, sorting);
            return new DataResult<T>(query, dataRequest.ItemsPerPage, dataRequest.CurrentPage);
        }

        public static DataResult ProccessAndSelect<T>(this DataRequest dataRequest, IQueryable<T> query, bool filtering = true, bool sorting = true)
        {
            query = ProccessQuery(query, dataRequest, filtering, sorting);
            return new DataResult(query.SelectByDataRequest(dataRequest), dataRequest.ItemsPerPage, dataRequest.CurrentPage);
        }
    }
}