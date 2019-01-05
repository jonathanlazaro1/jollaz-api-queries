using System.Globalization;

namespace JollazApiQueries.Library.Errors
{
    public interface IErrorMessages
    {
        CultureInfo Culture { get; }

        string NoOfOperatorsXNoOfFilters { get; }

        string ParameterCastError { get; }

        string InvalidCriterion { get; }

        string PropertyNotFound { get; }

        string NoNestedPropsInCollections { get; }

        string LogicalOperatorOutOfRange { get; }

        string PropertyTypeNotSupported { get; }

        string SearchParameterIsNull { get; }

        string UnableToSelect { get; }
    }
}