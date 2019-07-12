namespace JollazApiQueries.Model.Core
{
    ///<summary>
    /// This class represents the ordering to be applied in a query.
    ///</summary>
    public class OrderingItem
    {
        ///<summary>
        /// The name of the property to be ordered. Supports nested properties. Doesn't support nested properties from class collections.
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// Defines if the ordering will be applied in descending order. If not supplied, the default value is false (ascending).
        ///</summary>
        public bool Descending { get; set; }
    }
}