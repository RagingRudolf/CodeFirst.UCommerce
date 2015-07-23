using RagingRudolf.UCommerce.CodeFirst.Core;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.PaymentMethodDefinitions
{
    [Definition(BuiltInDefinitionType.PaymentMethod, "Global Collect", "My Collect")]
	public class GlobalCollect
	{
		[Field("Field Test", "Number", DisplayOnSite = true)]
		public string Test { get; set; }
	}
}