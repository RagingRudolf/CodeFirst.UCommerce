using System;

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
	}
}