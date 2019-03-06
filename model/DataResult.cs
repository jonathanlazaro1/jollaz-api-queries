using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using JollazApiQueries.Model.Core;

namespace JollazApiQueries.Model
{
    ///<summary>
    /// Class that represents the result of a DataRequest that was applied to a query. The data type of the return collection is unknown.
    ///</summary>
    public class DataResult : DataResult<dynamic>
    {
        ///<summary>
        /// The complete DataResult class constructor.
        ///<param name="items">The query which will be filtered. It's assumed that the query is already proccessed and paginated.</param>
        ///<param name="itemsCount">The total number of items on query.</param>
        ///<param name="itemsPerPage">How many items will be returned in this DataResult.</param>
        ///<param name="currentPage">The current results page.</param>
        ///</summary>
        public DataResult(IQueryable items, int itemsCount, int pageCount, int itemsPerPage, int currentPage) : base(itemsCount, pageCount, itemsPerPage, currentPage)
        {
            this.Items = items.ToDynamicList();
        }

        ///<summary>
        /// The items collection that will be returned. The collection type is unknown, which allows the custom selection of the return properties.
        ///</summary>
        public new ICollection<dynamic> Items { get; set; }
    }
}