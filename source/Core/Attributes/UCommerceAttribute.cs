using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
{
	public abstract class UCommerceAttribute : Attribute
	{
		public string Name { get; set; }
	}
}