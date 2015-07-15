using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ProductDefinitionAttribute : BaseDefinitionAttribute
	{
		public ProductDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}