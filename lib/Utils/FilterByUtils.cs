using System;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Model.Options;
using JollazApiQueries.Model.Requests;

namespace JollazApiQueries.Library.Utils
{
    ///<summary>
    /// Filter class for data types: numeric (int, decimal, etc) and date/time (DateTime)
    ///</summary>
    public static class FilterByUtils
    {
        ///<summary>
        /// Tries to convert value to an object with the specified TypeCode,
        /// returning if the operation has been succeeded or not.
        /// <param name="value">The value to be converted.</param>
        /// <param name="typeCode">The TypeCode to which the value will be converted to.</param>
        /// <param name="result">The converted object, with the specified TypeCode.</param>
        ///<returns>Returns a boolean value indicating if the conversion was successful or not.</returns>
        ///</summary>
        public static bool TryParse(object value, TypeCode typeCode, out object result)
        {
            try
            {
                result = Convert.ChangeType(value, typeCode);
            }
            catch (Exception e)
            {
                if (e is System.InvalidCastException || e is System.FormatException)
                {
                    result = null;
                    return false;
                }
                throw;
            }
            return true;
        }

        ///<summary>
        /// Constructs the expression that filters to the data type and value specified.
        /// <para>Works with number and date/time data types.</para>
        /// <para>Raises an exception if conversion to the specified data type is unsuccesful.</para>
        /// <param name="exp">An already existent expression, in which the search filter for the supplied value will be added.</param>
        /// <param name="prop">The property in which the filter expression will be based.</param>
        /// <param name="filter">Filter item that will be used to build the expression.</param>
        /// <param name="typeCode">TypeCode that will be used to do the conversion of the parameter type.</param>
        ///</summary>
        public static Expression FilterBy(Expression exp, PropertyInfo prop, FilterItem filter, TypeCode typeCode)
        {
            var parameter = filter.Parameter;
            bool converted = TryParse(filter.Parameter, typeCode, out parameter);
            if (!converted)
            {
                throw new InvalidCastException($"{ResourceManagerUtils.ErrorMessages.ParameterCastError} {prop.Name}.");
            }

            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    exp = Expression.Equal(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                case FilterCriterion.GreaterThanOrEqual:
                    exp = Expression.GreaterThanOrEqual(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                case FilterCriterion.GreaterThan:
                    exp = Expression.GreaterThan(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                case FilterCriterion.LessThanOrEqual:
                    exp = Expression.LessThanOrEqual(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                case FilterCriterion.LessThan:
                    exp = Expression.LessThan(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(filter.Criterion.ToString(), $"{ResourceManagerUtils.ErrorMessages.InvalidCriterion}: {prop.Name}");
            }
            return exp;
        }
    }
}
