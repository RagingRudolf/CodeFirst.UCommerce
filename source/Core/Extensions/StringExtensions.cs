namespace RagingRudolf.UCommerce.CodeFirst.Core.Extensions
{
	public static class StringExtensions
	{
		public static bool IsEmpty(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

		public static bool IsNotEmpty(this string str)
		{
			return !str.IsEmpty();
		}
	}
}