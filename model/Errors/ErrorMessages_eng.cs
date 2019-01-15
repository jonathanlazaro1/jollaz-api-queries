namespace JollazApiQueries.Model.Errors
{
    public class ErrorMessages_eng : IErrorMessages
    {
        public string NoOfOperatorsXNoOfFilters => "The number of logical operators doesn't match with the number of filters.";

        public string ParameterCastError => "Could not convert the parameter value to the type of property";

        public string InvalidCriterion => "Criterion not supported for this property";

        public string PropertyNotFound => "Property not found";

        public string NoNestedPropsInCollections => "Filtering on nested properties of class collections is not supported.";

        public string LogicalOperatorOutOfRange => "Could not infer the supplied logical operator.";

        public string PropertyTypeNotSupported => "Property data type is not supported";

        public string OnlySupportedInAdvancedFilter => "This operation is only supported by using an advanced filter.";

        public string SearchParameterIsNull => "Search parameter cannot be null";

        public string UnableToSelect => "Unable to select: some of the provided properties was not found.";
        
        public string UnableToGroup => "Unable to group: the instruction provided is invalid.";
    }
}