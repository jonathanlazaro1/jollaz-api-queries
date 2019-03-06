using System;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Model.Core;
using JollazApiQueries.Model.Core.Errors;

namespace JollazApiQueries.Library.Utils
{
    public static class FilterByEnumUtils
    {
        public static Expression FilterByEnum(Expression exp, PropertyInfo prop, FilterItem filter)
        {
            var type = prop.PropertyType.IsEnum 
                ? prop.PropertyType 
                : Nullable.GetUnderlyingType(prop.PropertyType);

            int parameter = -1;
            if (!Int32.TryParse(filter.Parameter.ToString(), out parameter))
            {
                throw new InvalidCastException($"{ResourceManagerUtils.ErrorMessages.ParameterCastError}: {filter.Name}");
            }

            string enumName = Enum.GetName(type, parameter);
            if (string.IsNullOrWhiteSpace(enumName))
                throw new ArrayTypeMismatchException($"{ResourceManagerUtils.ErrorMessages.ParameterCastError} {prop.PropertyType.Name}.");
            var enumValue = Enum.Parse(type, enumName);

            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    exp = Expression.Equal(exp, Expression.Constant(enumValue, prop.PropertyType));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(filter.Criterion.ToString(), $"{ResourceManagerUtils.ErrorMessages.InvalidCriterion}: {prop.Name}");
            }
            return exp;
        }
    }
}