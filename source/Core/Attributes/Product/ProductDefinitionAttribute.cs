using System;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Shared;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Product
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