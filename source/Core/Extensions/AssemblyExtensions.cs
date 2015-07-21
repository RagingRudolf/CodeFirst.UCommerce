using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Extensions
{
	public static class AssemblyExtensions
	{
		public static IEnumerable<Type> GetUCommerceDefinitions(this Assembly assembly)
		{
			var types = assembly
				.GetTypes()
				.EmptyIfNull();
			var filtered = types
				.Where(x => x.IsDefined(typeof(CodeFirstAttribute), true) && x.IsClass && (x.IsNestedPublic || x.IsPublic));

			return filtered;
		}
	}
}