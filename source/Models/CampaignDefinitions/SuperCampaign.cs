using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.CampaignDefinitions
{
	[CampaignDefinition("Super Campaign", Description = "This is a super campaign for awesome on sales item!")]
	public class SuperCampaign
	{
		[Field("DebitorGroup", "Number", DefaultValue = "1", RenderInEditor = true)]
		[Language("da-DK", "Debitor gruppe",  Description = "Gruppe id'et, som kan modtage denne rabat.")]
		public string DebitorGroup { get; set; }
	}
}