using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.Examples.Models.PaymentMethodDefinitions
{
	[PaymentMethodDefinition("Global Collect", Description = "My Collect")]
	public class GlobalCollect
	{
		[DefinitionField("Field Test", "Number", DisplayOnSite = true)]
		public string Test { get; set; }
	}
}