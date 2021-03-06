using System;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Model.Core;
using JollazApiQueries.Model.Core.Errors;

namespace JollazApiQueries.Library.Utils
{
    public static class FilterByBoolUtils
    {
        public static Expression FilterByBool(Expression exp, PropertyInfo prop, FilterItem filter)
        {
            var parameter = filter.Parameter;
            bool converted = FilterByUtils.TryParse(filter.Parameter, TypeCode.Boolean, out parameter);
            if (!converted)
            {
                throw new InvalidCastException($"{ResourceManagerUtils.ErrorMessages.ParameterCastError}: {filter.Name}");
            }

            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    exp = Expression.Equal(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(filter.Criterion.ToString(), $"{ResourceManagerUtils.ErrorMessages.InvalidCriterion}: {prop.Name}");
            }
            return exp;
        }
    }
}