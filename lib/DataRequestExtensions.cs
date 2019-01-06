using System.Linq;
using JollazApiQueries.Library.Models.Requests;

namespace JollazApiQueries.Library.Extensions
{
    public static class DataRequestExtensions
    {
        public static DataResult<T> Proccess<T>(this DataRequest dataRequest, IQueryable<T> query)
        {

        }
    }
}