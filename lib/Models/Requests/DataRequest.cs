using System.Collections.Generic;
using JollazApiQueries.Library.Models.Options;

namespace JollazApiQueries.Library.Models.Requests
{
    ///<summary>
    /// Class that represents the filters, selections and orderings being applied to a query.
    ///</summary>
    public class DataRequest
    {
        ///<summary>
        /// Represents how many items will be returned per results page.
        ///</summary>
        public int ItemsPerPage { get; set; }

        ///<summary>
        /// Represents which page of results will come from the request.
        ///</summary>
        public int CurrentPage { get; set; }

        ///<summary>
        /// The collection of filters that will be applied to a query.
        /// From two filters onwards, each new filter must have a corresponding operator.
        /// <para>If Expressions property is supplied, Filters property will be ignored.</para>
        ///</summary>
        public FilterItem[] Filters { get; set; } = new FilterItem[] { };

        ///<summary>
        /// The collection of expressions that will be applied to a query.
        /// From two expressions onwards, each new expression must have a corresponding operator.        
        ///</summary>
        public FilterExpression[] Expressions { get; set; } = new FilterExpression[] { };

        ///<summary>
        /// The collection of logical operators that will be used to merge expressions or filters.
        ///<para> When Expressions property is supplied, Operators values will be used to merge them. Expressions have their own internal operators.</para>
        ///<para> If there aren't Expressions, Operators will be used to merge Filters.</para>
        ///</summary>
        public FilterOperator[] Operators { get; set; } = new FilterOperator[] { };

        ///<summary>
        /// Collection of orderings that will be applied to a query. The orderings are applied according with their order in the request.
        ///</summary>
        public OrderingItem[] Ordering { get; set; } = new OrderingItem[] { };

        ///<summary>
        /// Collection of property names that will be brought back in the response.
        ///<para>Supports nested properties. Doesn't support nested properties from class collections.</para>
        ///</summary>
        public string[] Select { get; set; } = new string[] { };

        ///<summary>
        /// String that contains instructions to grouping the result data.
        ///</summary>
        public string Grouping { get; set; }
    }
}