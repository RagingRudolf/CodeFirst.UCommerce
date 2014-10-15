using System;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Shared;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Payment
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class PaymentMethodDefinitionAttribute : BaseDefinitionAttribute
	{
		public PaymentMethodDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}