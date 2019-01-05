using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JollazApiQueries.Library.Models.Options;
using JollazApiQueries.Library.Models.Requests;
using JollazApiQueries.Library.Utils;

namespace JollazApiQueries.Library.Extensions
{
    ///<summary>
    /// This class is responsible for doing the main query filtering work.
    ///</summary>
    public static class QueryFilterExtensions
    {
        ///<summary>
        /// Creates a base expression that will be used in data filter expressions, with support to nested properties.
        /// <para>Must return the found property based on the informed argument.</para>
        /// <para>Can return an expression to check against null values, if applicable.</para>
        /// <typeparam name="T">The type from where the property will be obtained.</typeparam>
        /// <param name="pe">The start expression, based on the entity class name and its type.</param>
        /// <param name="args">The text that defines the property(ies) filter name(s). Supports nesting by using dots (".").</param>
        /// <param name="prop">The out property found from the supplied argument. Will raise an exception if the property isn't found.</param>
        /// <param name="expNotNull">The out expression that will check against null values. Can be null, if the property is from a not nullable type.</param>
        /// <returns>Returns the base expression that will be used to filter data in a query.</returns>
        ///</summary>
        private static Expression CreateBaseExpression<T>(ParameterExpression pe, string args, out PropertyInfo prop, out Expression expNotNull)
        {
            prop = null;
            var props = args.Split('.');
            var type = typeof(T);

            Expression exp = null;
            expNotNull = null;

            foreach (var strProp in props)
            {
                prop = type.GetProperties().SingleOrDefault(p => p.Name.ToLower().Equals(strProp.ToLower()));
                if (prop == null)
                {
                    throw new ArgumentException($"{ResourceManagerUtils.ErrorMessages.PropertyNotFound}: {args}");
                }

                // If prop is a collection
                if (prop.IsCollection())
                {
                    if (prop.Name.ToLower() != props.Last().ToLower())
                    {
                        throw new InvalidOperationException(ResourceManagerUtils.ErrorMessages.NoNestedPropsInCollections);
                    }
                }

                exp = exp == null ? Expression.Property(pe, prop) : Expression.Property(exp, prop);
                type = prop.PropertyType;

                    
                if ((!prop.PropertyType.IsEnum) // This is necessary, since enum falls into IsPrimitive condition
                && (prop.PropertyType == typeof(string)
                    || prop.IsCollection()
                    || !prop.PropertyType.IsPrimitive
                    || Nullable.GetUnderlyingType(prop.PropertyType) != null))
                {
                    var notNull = Expression.NotEqual(exp, Expression.Constant(null, prop.PropertyType));
                    expNotNull = expNotNull == null ? notNull : Expression.AndAlso(expNotNull, notNull);
                }
            }
            return exp;
        }

        ///<summary>
        /// Creates an expression based on a collection of filters and operators.
        ///<param name="filters">Collection of filter items from an expression.</param>
        ///<param name="operators">Collection of operators that define the relation between the expression filters.</param>
        ///<param name="pe">Expression that defines the way how the query argument will be built.</param>
        ///</summary>
        private static Expression CreateExpression<T>(FilterItem[] filters, FilterOperator[] operators, ParameterExpression pe)
        {
            if (operators.Count() < filters.Count() - 1)
                throw new ArgumentException(ResourceManagerUtils.ErrorMessages.NoOfOperatorsXNoOfFilters);

            Expression exp = null;
            for (int i = 0; i < filters.Count(); i++)
            {
                var filter = filters[i];
                if (filter.Parameter == null && filter.Criterion != FilterCriterion.NotNull)
                    throw new ArgumentNullException($"{ResourceManagerUtils.ErrorMessages.SearchParameterIsNull}: {filter.Name}");

                PropertyInfo prop = null;
                Expression notNull = null;
                Expression auxExp = CreateBaseExpression<T>(pe, filter.Name, out prop, out notNull);

                // Check if the property is from a nullable type
                // Otherwise, stays with the property type
                var propType = Nullable.GetUnderlyingType(prop.PropertyType);
                propType = propType ?? prop.PropertyType;

                if (filter.Criterion == FilterCriterion.NotNull)
                {
                    auxExp = notNull;
                    notNull = null;
                }
                else if (prop.IsCollection())
                {
                    auxExp = FilterByCollectionUtils.FilterByCollection(auxExp, prop, filter);
                }
                else if (propType.IsEnum)
                {
                    auxExp = FilterByEnumUtils.FilterByEnum(auxExp, prop, filter);
                }
                else
                {
                    var typeCode = Type.GetTypeCode(propType);
                    switch (typeCode)
                    {
                        case TypeCode.String:
                            auxExp = FilterByStringUtils.FilterByString(auxExp, prop, filter);
                            break;
                        case TypeCode.Char:
                            auxExp = FilterByStringUtils.FilterByString(auxExp, prop, filter);
                            break;
                        case TypeCode.Int32:
                            auxExp = FilterByUtils.FilterBy(auxExp, prop, filter, TypeCode.Int32);
                            break;
                        case TypeCode.Decimal:
                            auxExp = FilterByUtils.FilterBy(auxExp, prop, filter, TypeCode.Decimal);
                            break;
                        case TypeCode.Double:
                            auxExp = FilterByUtils.FilterBy(auxExp, prop, filter, TypeCode.Double);
                            break;
                        case TypeCode.DateTime:
                            auxExp = FilterByUtils.FilterBy(auxExp, prop, filter, TypeCode.DateTime);
                            break;
                        case TypeCode.Boolean:
                            auxExp = FilterByBoolUtils.FilterByBool(auxExp, prop, filter);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException($"{ResourceManagerUtils.ErrorMessages.PropertyTypeNotSupported}: {filter.Name}");
                    }
                }

                auxExp = filter.Not ? Expression.Not(auxExp) : auxExp;
                auxExp = notNull != null ? Expression.AndAlso(notNull, auxExp) : auxExp;

                exp = BindExpressions(exp, auxExp, operators.ElementAtOrDefault(i - 1));
            }
            return exp;
        }

        ///<summary>
        /// Combines two expressions using a FilterOperator argument.
        /// <param name="e1">The left expression.</param>
        /// <param name="e2">The right expression.</param>
        /// <param name="op">The operator that will be used to combine the two expressions.</param>
        ///<returns>Returns the combined expression.</returns>
        ///</summary>
        private static Expression BindExpressions(Expression e1, Expression e2, FilterOperator op)
        {
            if (e1 == null)
            {
                e1 = e2;
            }
            else
            {
                switch (op)
                {
                    case FilterOperator.And:
                        e1 = Expression.And(e1, e2);
                        break;
                    case FilterOperator.Or:
                        e1 = Expression.Or(e1, e2);
                        break;
                    case FilterOperator.Xor:
                        e1 = Expression.ExclusiveOr(e1, e2);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(ResourceManagerUtils.ErrorMessages.LogicalOperatorOutOfRange);
                }
            }
            return e1;
        }

        ///<summary>
        /// Extension method that allows query filtering based on a data request.
        /// <param name="query">The query where the filters will be applied.</param>
        /// <param name="dataRequest">The data request, which contains the filters that will be applied to the query.</param>
        /// <typeparam name="T">The query data type.</typeparam>
        /// <returns>Returns the query with the filter applied and ready to fetch.</returns>
        ///</summary>
        public static IQueryable<T> FilterByDataRequest<T>(this IQueryable<T> query, DataRequest dataRequest)
        {
            Expression exp = null;
            Expression auxExp = null;
            ParameterExpression pe = Expression.Parameter(typeof(T), typeof(T).Name);

            if (dataRequest.Expressions.Any())
            {
                if (dataRequest.Operators.Count() < dataRequest.Expressions.Count() - 1)
                    throw new ArgumentException(ResourceManagerUtils.ErrorMessages.NoOfOperatorsXNoOfFilters);

                for (int i = 0; i < dataRequest.Expressions.Count(); i++)
                {
                    var filterExp = dataRequest.Expressions[i];
                    auxExp = CreateExpression<T>(filterExp.Filters, filterExp.Operators, pe);
                    exp = BindExpressions(exp, auxExp, dataRequest.Operators.ElementAtOrDefault(i - 1));
                }
            }
            else if (dataRequest.Filters.Any())
            {
                exp = CreateExpression<T>(dataRequest.Filters, dataRequest.Operators, pe);
            }
            return exp != null
            ? query.Where(Expression.Lambda<Func<T, Boolean>>(exp, pe))
            : query;
        }
    }
}