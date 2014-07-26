using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Extensions
{
	public static class AssemblyExtensions
	{
		public static IEnumerable<Type> GetUCommerceDefinitions(this Assembly assembly)
		{
			var types = assembly
				.GetTypes()
				.EmptyIfNull();
			var filtered = types
				.Where(x => x.IsDefined(typeof(UCommerceAttribute), true) && x.IsClass && (x.IsNestedPublic || x.IsPublic));

			return filtered;
		}
	}
}