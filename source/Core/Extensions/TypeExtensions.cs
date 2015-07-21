using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Extensions
{
	public static class TypeExtensions
	{
		public static TAttribute AssertGetCustomAttribute<TAttribute>(this Type definitionType, bool inherit = false)
			where TAttribute : Attribute
		{
			var attribute = definitionType.GetCustomAttribute<TAttribute>(inherit);

			if (attribute == null)
				throw new InvalidOperationException(
					string.Format("Type '{0}' does not have an attribute of type '{1}' attached.", 
						definitionType.Name, 
						typeof(TAttribute).Name));

			return attribute;
		}

		public static IEnumerable<PropertyInfo> GetAttributedProperties<TAttribute>(this Type definitionType, bool inherit = false)
		{
			var properties = definitionType
				.GetProperties()
				.EmptyIfNull()
				.Where(x => x.IsDefined(typeof(TAttribute), inherit));

			return properties;
		}
	}
}