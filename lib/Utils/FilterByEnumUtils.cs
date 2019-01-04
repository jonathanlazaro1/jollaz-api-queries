using System;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Models.Options;
using JollazApiQueries.Models.Requests;

namespace JollazApiQueries.Library.Utils
{
    public static class FilterByEnumUtils
    {
        public static Expression FilterByEnum(Expression exp, PropertyInfo prop, FilterItem filter)
        {
            var type = prop.PropertyType.IsEnum 
                ? prop.PropertyType 
                : Nullable.GetUnderlyingType(prop.PropertyType);

            var parameter = filter.Parameter;
            string enumName = Enum.GetName(type, parameter);
            if (string.IsNullOrWhiteSpace(enumName))
                throw new ArrayTypeMismatchException($"Parameter {parameter} cannot be converted to {prop.PropertyType.Name}");
            var enumValue = Enum.Parse(type, enumName);

            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    exp = Expression.Equal(exp, Expression.Constant(enumValue, prop.PropertyType));
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Criterion {filter.Criterion} is invalid to property {prop.Name}");
            }
            return exp;
        }
    }
}