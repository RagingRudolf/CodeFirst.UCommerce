using System;
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

		public static TAttribute GetAttribute<TAttribute>(this Type type, bool inherit = false)
			where TAttribute : Attribute
		{
			TAttribute attribute = type.GetCustomAttribute(typeof(TAttribute), inherit) as TAttribute;

			return attribute;
		}

		public static TAttribute AssertGetAttribute<TAttribute>(this Type type, bool inherit = false)
			where TAttribute : Attribute
		{
			TAttribute attribute = GetAttribute<TAttribute>(type, inherit);

			if (attribute == null)
				throw new InvalidOperationException();

			return attribute;
		}
	}
}