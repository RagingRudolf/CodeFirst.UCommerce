using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Extensions
{
	public static class TypeExtensions
	{
		public static TAttribute AssertGetAttribute<TAttribute>(this Type definitionType, bool inherit = false)
			where TAttribute : Attribute
		{
			var attribute = definitionType.GetCustomAttribute<TAttribute>(inherit);

			if (attribute == null)
				throw new InvalidOperationException(
				    $"Type '{definitionType.Name}' does not have an attribute of type '{typeof (TAttribute).Name}' attached.");

			return attribute;
		}

		public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(this Type definitionType, bool inherit = false)
		{
			var properties = definitionType
				.GetProperties()
				.EmptyIfNull()
				.Where(x => x.IsDefined(typeof(TAttribute), inherit));

			return properties;
		}

        public static IEnumerable<Type> WithAttribute<TAttribute>(this IEnumerable<Type> types)
            where TAttribute : Attribute
        {
            return types
                .EmptyIfNull()
                .Where(type => type.IsDefined(typeof(TAttribute), inherit: true) && type.IsPublicClass());
        }

	    public static bool IsPublicClass(this Type type)
        {
            return type.IsClass && (type.IsNested || type.IsPublic) && !type.IsAbstract;
        }
	}
}