using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model;
using JollazApiQueries.Model.Core;

namespace JollazApiQueries.Library.Extensions
{
    public static class DataRequestExtensions
    {
        private static int CalculateItemsPerPage(this int itemsPerPage) => itemsPerPage < 1 ? 1 : itemsPerPage;

        private static int CalculatePageCount(int itemsTotal, int itemsPerPage)
        {
            var pageCount = (int)Math.Ceiling((decimal)itemsTotal / itemsPerPage);
                return pageCount > 0 ? pageCount : 1;
        }

        private static int CalculateCurrentPage(int currentPage, int pageCount)
        {
            // Value has to be at least 1
                currentPage = currentPage < 1 ? 1 : currentPage;
                // Valor can't be greater than PageCount
                currentPage = currentPage > pageCount ? pageCount : currentPage;
                return currentPage;
        }
        
        public static DataResult Proccess<T>(this IQueryable<T> query, DataRequest dataRequest)
        {
            var proccessedQuery = query
                .FilterByDataRequest(dataRequest)
                .OrderByDataRequest(dataRequest)
                .SelectByDataRequest(dataRequest)
                .GroupByDataRequest(dataRequest);
            
            var queryCount = proccessedQuery.Count();
            var itemsPerPage = dataRequest.ItemsPerPage.CalculateItemsPerPage();
            var pageCount = CalculatePageCount(queryCount, itemsPerPage);
            var currentPage = CalculateCurrentPage(dataRequest.CurrentPage, pageCount);

            return new DataResult(proccessedQuery
                    .Skip(itemsPerPage * (currentPage - 1))
                    .Take(itemsPerPage),
                queryCount,
                pageCount,
                itemsPerPage,
                currentPage);
        }
    }
    
}