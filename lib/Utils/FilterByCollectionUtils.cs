using System;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Models.Options;
using JollazApiQueries.Models.Requests;

namespace JollazApiQueries.Library.Utils
{
    public static class FilterByCollectionUtils
    {
        public static Expression FilterByCollection(Expression exp, PropertyInfo prop, FilterItem filter)
        {
            var intParameter = Int32.Parse(filter.Parameter.ToString());
            exp = Expression.Property(exp, "Count");

            switch (filter.Criterion)
            {
                case FilterCriterion.Equal:
                    
                    exp = Expression.Equal(exp, Expression.Constant(intParameter, typeof(Int32)));
                    break;
                case FilterCriterion.LessThanOrEqual:
                    exp = Expression.LessThanOrEqual(exp, Expression.Constant(intParameter, typeof(Int32)));
                    break;
                case FilterCriterion.LessThan:
                    exp = Expression.LessThan(exp, Expression.Constant(intParameter, typeof(Int32)));
                    break;
                case FilterCriterion.GreaterThanOrEqual:
                    exp = Expression.GreaterThanOrEqual(exp, Expression.Constant(intParameter, typeof(Int32)));
                    break;
                case FilterCriterion.GreaterThan:
                    exp = Expression.GreaterThan(exp, Expression.Constant(intParameter, typeof(Int32)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Criterion {filter.Criterion} is invalid to property {prop.Name}");
            }
            return exp;
        }
    }
}