namespace JollazApiQueries.Model.Core
{
    ///<summary>
    /// This class represents an expression, which is defined by a number of filters and its operators. All the expression filters are applied together.
    ///</summary>
    public class FilterExpression
    {
        ///<summary>
        /// Collection of expression's filters.
        ///</summary>
        public FilterItem[] Filters { get; set; }

        ///<summary>
        /// Collection of expression's operators. They are used to merge filters.
        ///</summary>
        public FilterOperator[] Operators { get; set; }

        ///<summary>
        /// FilterExpression's constructor. All collections are initialized, in order to avoid NullReference exceptions.
        ///</summary>
        public FilterExpression()
        {
            this.Filters = new FilterItem[] { };
            this.Operators = new FilterOperator[] { };
        }
    }
}