namespace JollazApiQueries.Library.Models.Errors
{
    public interface IErrorMessages
    {
        string NoOfOperatorsXNoOfFilters { get; }

        string ParameterCastError { get; }

        string InvalidCriterion { get; }

        string PropertyNotFound { get; }

        string NoNestedPropsInCollections { get; }

        string LogicalOperatorOutOfRange { get; }

        string PropertyTypeNotSupported { get; }

        string OnlySupportedInAdvancedFilter { get; }

        string SearchParameterIsNull { get; }

        string UnableToSelect { get; }

        string NoPropertiesToSelect { get; }
    }
}