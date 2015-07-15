using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PaymentMethodDefinitionAttribute : BaseDefinitionAttribute
	{
		public PaymentMethodDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}