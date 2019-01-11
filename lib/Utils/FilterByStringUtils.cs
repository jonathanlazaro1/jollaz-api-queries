using System;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Library.Models.Options;
using JollazApiQueries.Library.Models.Requests;

namespace JollazApiQueries.Library.Utils
{
    public static class FilterByStringUtils
    {
        public static Expression FilterByString(Expression exp, PropertyInfo prop, FilterItem filter)
        {
            string parameter = filter.Parameter.ToString();

            // Makes param and exp lowercase, if specified in the filter
            if (!filter.MatchCase)
            {
                MethodInfo methodToLower = typeof(string).GetMethod("ToLower", new Type[0]);
                exp = Expression.Call(exp, methodToLower);
                parameter = parameter.ToLower();
            }

            string methodName = null;
            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    exp = Expression.Equal(exp, Expression.Constant(parameter, prop.PropertyType));
                    break;
                case FilterCriterion.StringContains:
                    methodName = "Contains";
                    break;
                case FilterCriterion.StringStartsWith:
                    methodName = "StartsWith";
                    break;
                case FilterCriterion.StringEndsWith:
                    methodName = "EndsWith";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(filter.Criterion.ToString(), $"{ResourceManagerUtils.ErrorMessages.InvalidCriterion}: {prop.Name}");
            }
            if (!string.IsNullOrEmpty(methodName))
            {
                MethodInfo method = typeof(string).GetMethod(methodName, new Type[] { typeof(string), typeof(StringComparison) });
                exp = Expression.Call(exp, method, Expression.Constant(parameter), Expression.Constant(StringComparison.InvariantCulture));
            }
            return exp;
        }
    }
}