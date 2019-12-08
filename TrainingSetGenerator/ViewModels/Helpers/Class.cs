using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace TrainingSetGenerator.ViewModels.Helpers
{
    public static class ClassHelper
    {
        const string RefersToMethod = "Expression '{0}' refers to a method, not a property.";
        const string RefersToField = "Expression '{0}' refers to a field, not a property.";

        public static string GetPropertyName<TP>(Expression<Func<TP>> getProperty)
        {
            var member = getProperty.Body as MemberExpression;
            Debug.Assert(member != null, string.Format(RefersToMethod, getProperty));

            var info = member.Member as PropertyInfo;
            Debug.Assert(info != null, string.Format(RefersToField, getProperty));

            return info.Name;
        }

        public static string GetPropertyName<T, TP>(T obj, Expression<Func<T, TP>> getProperty)
        {
            var member = getProperty.Body as MemberExpression;
            Debug.Assert(member != null, string.Format(RefersToMethod, getProperty));

            var info = member.Member as PropertyInfo;
            Debug.Assert(info != null, string.Format(RefersToField, getProperty));

            Debug.Assert(typeof(T) == info.DeclaringType,
                $"Expresion '{getProperty}' refers to a property that is not from type.");

            return info.Name;
        }
    }
}
