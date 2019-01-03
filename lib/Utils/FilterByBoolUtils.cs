using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Models.Options;
using JollazApiQueries.Models.Requests;

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
                throw new InvalidCastException($"Could not convert the parameter value to the type of property: {filter.Name}");
            }

            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    exp = Expression.Equal(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Criterion {filter.Criterion} is invalid to property {prop.Name}");
            }
            return exp;
        }
    }
}