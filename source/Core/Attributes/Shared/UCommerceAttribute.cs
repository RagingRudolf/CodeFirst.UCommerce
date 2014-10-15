using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
{
	public abstract class UCommerceAttribute : Attribute
	{
		protected UCommerceAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; protected set; }
	}
}