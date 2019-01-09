namespace JollazApiQueries.Library.Models.Options
{
    ///<summary>
    /// Criterion to be used when applying a filter.
    ///<para>Some criteria can be used only on certain data types. Trying to use them in an unsupported data type will result in error.</para>
    ///</summary>
    public enum FilterCriterion
    {
        ///<summary>
        /// Property value is equal to parameter value. Can be used in all supported data types.
        ///</summary>
        Equal = 0,

        ///<summary>
        /// Property value is equal or lesser than parameter value. Only number (int, decimal, etc), date/time (DateTime) data types and collection counts are supported.
        ///</summary>
        LessThanOrEqual = 1,

        ///<summary>
        /// Property value is lesser than parameter value. Only number (int, decimal, etc), date/time (DateTime) data types and collection counts are supported.
        ///</summary>
        LessThan = 2,

        ///<summary>
        /// Property value is equal or greater than parameter value. Only number (int, decimal, etc), date/time (DateTime) data types and collection counts are supported.
        ///</summary>
        GreaterThanOrEqual = 3,

        ///<summary>
        /// Property value is greater than parameter value. Only number (int, decimal, etc), date/time (DateTime) data types and collection counts are supported.        
        ///</summary>
        GreaterThan = 4,

        ///<summary>
        /// Property value contains the parameter value, in any part of the text. Only String data type is supported.
        ///</summary>
        StringContains = 5,

        ///<summary>
        /// Property value contains the parameter value, specifically at the beginning of text. Only String data type is supported.
        ///</summary>
        StringStartsWith = 6,

        ///<summary>
        /// Property value contains the parameter value, specifically at the end of text. Only String data type is supported.
        ///</summary>
        StringEndsWith = 7,

        ///<summary>
        /// Property value is not null.
        ///</summary>
        NotNull = 8
    }

    public class FilterCriteriaUtils
    {
        public static string GetCriterionSignByCriterion(FilterCriterion criterion)
        {
            switch (criterion)
            {
                case FilterCriterion.Equal:
                    return "==";
                case FilterCriterion.GreaterThanOrEqual:
                    return ">=";
                case FilterCriterion.GreaterThan:
                    return ">";
                case FilterCriterion.LessThanOrEqual:
                    return "<=";
                case FilterCriterion.LessThan:
                    return "<";
                default:
                    return string.Empty;
            }
        }
    }
}