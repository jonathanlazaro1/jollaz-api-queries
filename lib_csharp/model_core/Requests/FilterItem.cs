namespace JollazApiQueries.Model.Core
{
    ///<summary>
    /// This class represents the filter item to be applied in a query.
    ///</summary>
    public class FilterItem
    {
        ///<summary>
        /// The property name where the filter will be applied.
        ///<para>It's possible to access nested classes properties, using the dot (.) symbol.</para>
        ///<para>It's not possible to access nested properties from class collections.</para>
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// Filtering parameter. Must be from a primitive data type that can be safely casted to the property type.
        ///</summary>
        public object Parameter { get; set; }

        ///<summary>
        /// Defines if the filter must consider or not the difference between upper and lowercase letters.
        /// <para>Only String data type is supported. In all other types, it's ignored.</para>
        /// <para>When not supplied, default value is false.</para>
        ///</summary>
        public bool MatchCase { get; set; }

        ///<summary>
        /// Defines if the filter will be parsed as a special expression, without the validations that library usually performs.
        /// <para>When not supplied, default value is false.</para>
        ///</summary>
        public bool IsAdvanced { get; set; }

        ///<summary>
        /// Defines if the filter will be inverted, or if the result will be the opposite of what was supplied to the filter.
        /// <para>When not supplied, default value is false.</para>
        ///</summary>
        public bool Not { get; set; }

        ///<summary>
        /// Filter criterion by which parameter will be checked against the value of the property.
        ///</summary>
        public FilterCriterion Criterion { get; set; }
    }
}