using System;
using System.Reflection;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Extensions
{
	public static class PropertyInfoExtensions
	{
		public static TAttribute AssertGetCustomAttribute<TAttribute>(this PropertyInfo propertyInfo, bool inherit = false)
			where TAttribute : Attribute
		{
			var attribute = propertyInfo.GetCustomAttribute<TAttribute>();

			if (attribute == null)
				throw new InvalidOperationException(
					string.Format("Property '{0}' doesn't have attribute of type '{1}' attached.", 
						propertyInfo.Name, 
						typeof(TAttribute).Name));

			return attribute;
		}
	}
}