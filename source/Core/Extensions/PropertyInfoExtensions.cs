using System;
using System.Reflection;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Extensions
{
	public static class PropertyInfoExtensions
	{
		public static TAttribute AssertGetCustomAttribute<TAttribute>(this PropertyInfo propertyInfo, bool inherit = false)
			where TAttribute : Attribute
		{
			var attribute = propertyInfo.GetCustomAttribute<TAttribute>();

			if (attribute == null)
				throw new InvalidOperationException();

			return attribute;
		}
	}
}