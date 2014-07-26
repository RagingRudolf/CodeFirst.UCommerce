using System.Collections.Generic;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Extensions
{
	public static class EnumerationExtensions
	{
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
		{
			return enumerable ?? new List<T>();
		} 
	}
}