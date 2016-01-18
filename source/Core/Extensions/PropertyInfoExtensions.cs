using System;
using System.Reflection;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Extensions
{
	public static class PropertyInfoExtensions
	{
		public static TAttribute AssertGetAttribute<TAttribute>(this PropertyInfo propertyInfo, bool inherit = false)
			where TAttribute : Attribute
		{
			var attribute = propertyInfo.GetCustomAttribute<TAttribute>();

			if (attribute == null)
				throw new InvalidOperationException(
				    $"Property '{propertyInfo.Name}' doesn't have attribute of type '{typeof (TAttribute).Name}' attached.");

			return attribute;
		}
	}
}