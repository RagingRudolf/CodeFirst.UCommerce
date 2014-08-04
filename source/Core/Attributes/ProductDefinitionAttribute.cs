using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class ProductDefinitionAttribute : BaseDefinitionAttribute
	{
		public ProductDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}