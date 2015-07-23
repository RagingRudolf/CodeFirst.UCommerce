using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.PaymentMethodDefinitions
{
    //[PaymentMethodDefinition("Global Collect", Description = "My Collect")]
	public class GlobalCollect
	{
		[Field("Field Test", "Number", DisplayOnSite = true)]
		public string Test { get; set; }
	}
}