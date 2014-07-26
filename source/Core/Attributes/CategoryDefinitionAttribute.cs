using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class CategoryDefinitionAttribute : UCommerceAttribute
	{
		public string Description { get; set; }
	}
}