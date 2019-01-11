using System.Linq;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Models.Results;

namespace JollazApiQueries.Library.Extensions
{
    public static class DataRequestExtensions
    {
        public static DataResult Proccess<T>(this IQueryable<T> query, DataRequest dataRequest)
        {
            return new DataResult(query
                .FilterByDataRequest(dataRequest)
                .OrderByDataRequest(dataRequest),
                dataRequest);
        }
    }
}