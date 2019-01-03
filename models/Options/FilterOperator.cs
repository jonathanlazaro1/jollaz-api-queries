namespace JollazApiQueries.Models.Options
{
    ///<summary>
    /// Operators to be used to merge filters and/or expressions.
    ///</summary>
    public enum FilterOperator
    {
        ///<summary>
        /// Logical operator "AND".
        ///</summary>
        And = 0,

        ///<summary>
        /// Logical operator "OR".
        ///</summary>
        Or = 1,

        ///<summary>
        /// Logical operator "XOR" (OR exclusive).
        ///</summary>
        Xor = 2
    }
}