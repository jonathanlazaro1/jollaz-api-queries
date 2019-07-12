using System.Collections.Generic;
using System.Linq;

namespace JollazApiQueries.Model.Core
{
    ///<summary>
    /// Class that represents the result of a DataRequest that was applied to a query. The data type of the return collection is unknown.
    ///</summary>
    public class DataResult<T>
    {
        ///<summary>
        /// Number of query total items, before pagination.
        ///</summary>
        public int ItemsTotal { get; set; }

        ///<summary>
        /// How many items per page are in results.
        ///</summary>
        public int ItemsPerPage { get; set; }

        ///<summary>
        /// The page where the results are coming from.
        ///</summary>
        public int CurrentPage { get; set; }

        ///<summary>
        /// Total pages count.
        ///</summary>
        public int PageCount { get; set; }

        ///<summary>
        /// The items collection that will be returned. The collection type is unknown, which allows the custom selection of the return properties.
        ///</summary>
        public ICollection<T> Items { get; set; }
        
        ///<summary>
        /// The default DataResult class constructor.
        ///</summary>
        public DataResult()
        {
        }

        ///<summary>
        /// The DataResult class constructor without the items step. The Items attribution is in charge of the under class.
        ///<param name="itemsCount">The total number of items on query.</param>
        ///<param name="pageCount">The total number of pages on query.</param>
        ///<param name="itemsPerPage">How many items will be returned in this DataResult.</param>
        ///<param name="currentPage">The current results page.</param>
        ///</summary>

        public DataResult(int itemsCount, int pageCount, int itemsPerPage, int currentPage)
        {
            this.ItemsTotal = itemsCount;
            this.ItemsPerPage = itemsPerPage;
            this.PageCount = pageCount;
            this.CurrentPage = currentPage;
        }

        ///<summary>
        /// The complete DataResult class constructor.
        ///<param name="items">The query which will be filtered. It's assumed that the query is already proccessed and paginated.</param>
        ///<param name="pageCount">The total number of pages on query.</param>
        ///<param name="itemsCount">The total number of items on query.</param>
        ///<param name="itemsPerPage">How many items will be returned in this DataResult.</param>
        ///<param name="currentPage">The current results page.</param>
        ///</summary>
        public DataResult(IQueryable<T> items, int itemsCount, int pageCount, int itemsPerPage, int currentPage) : this(itemsCount, pageCount, itemsPerPage, currentPage)
        {
            this.Items = items.ToList();
        }
    }
}