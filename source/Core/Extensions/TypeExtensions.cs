using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Extensions
{
	public static class TypeExtensions
	{
		public static string[] AsDependency(this Type type)
		{
			return type != null && type != typeof(Object)
				? new[] { type.Name }
				: null;
		}

		public static TAttribute AssertGetCustomAttribute<TAttribute>(this Type type, bool inherit = false)
			where TAttribute : Attribute
		{
			var attribute = type.GetCustomAttribute<TAttribute>(inherit);

			if (attribute == null)
				throw new InvalidOperationException();

			return attribute;
		}

		public static IEnumerable<PropertyInfo> GetAttributedProperties<TAttribute>(this Type type, bool inherit = false)
		{
			var properties = type
				.GetProperties()
				.EmptyIfNull()
				.Where(x => x.IsDefined(typeof (TAttribute), inherit));

			return properties;
		}
	}
}