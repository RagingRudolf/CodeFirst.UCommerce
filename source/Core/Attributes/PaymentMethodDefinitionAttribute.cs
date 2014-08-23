using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
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