using System;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Model.Core;
using JollazApiQueries.Model.Core.Errors;

namespace JollazApiQueries.Library.Utils
{
    public static class FilterByGuidUtils
    {
        public static Expression FilterByGuid(Expression exp, PropertyInfo prop, FilterItem filter)
        {
            Guid guidParameter = Guid.Empty;
            var converted = Guid.TryParse(filter.Parameter.ToString(), out guidParameter);
            if (!converted)
            {
                throw new InvalidCastException($"{ResourceManagerUtils.ErrorMessages.ParameterCastError}: {filter.Name}");
            }

            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    exp = Expression.Equal(exp, Expression.Constant(guidParameter, prop.PropertyType));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(filter.Criterion.ToString(), $"{ResourceManagerUtils.ErrorMessages.InvalidCriterion}: {prop.Name}");
            }
            return exp;
        }
    }
}