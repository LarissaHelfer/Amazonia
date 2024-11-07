using System;
using System.Linq;
using System.Linq.Expressions;

namespace Application
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, string sortField, string sortOrder)
        {
            var propertyName = char.ToUpper(sortField[0]) + sortField.Substring(1);

            var param = Expression.Parameter(typeof(T), "x");
            var property = typeof(T).GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T)}'");

            var propertyAccess = Expression.MakeMemberAccess(param, property);
            var orderByExpression = Expression.Lambda(propertyAccess, param);

            var orderByMethod = sortOrder.ToLower() == "ascend" || sortOrder.ToLower() == "asc"
                ? "OrderBy"
                : "OrderByDescending";

            var resultExpression = Expression.Call(typeof(Queryable), orderByMethod,
                new Type[] { typeof(T), property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> source, object filter)
        {
            var filterProperties = filter.GetType().GetProperties();
            var entityProperties = typeof(T).GetProperties().ToDictionary(p => p.Name, p => p);

            foreach (var property in filterProperties)
            {
                if (entityProperties.ContainsKey(property.Name))
                {
                    var value = property.GetValue(filter);

                    if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    {
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var member = Expression.Property(parameter, entityProperties[property.Name]);

                        Expression condition;
                        if (member.Type == typeof(string))
                        {
                            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            var constant = Expression.Constant(value, typeof(string));
                            condition = Expression.Call(member, containsMethod, constant);
                        }
                        else
                        {
                            var constant = Expression.Constant(value);
                            condition = Expression.Equal(member, constant);
                        }

                        var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);

                        source = source.Where(lambda);
                    }
                }
            }
            return source;
        }

    }
}
