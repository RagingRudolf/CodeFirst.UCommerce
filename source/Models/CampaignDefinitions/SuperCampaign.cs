﻿using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Campaign;

namespace RagingRudolf.Examples.Models.CampaignDefinitions
{
	[CampaignDefinition("Super Campaign", Description = "This is a super campaign for awesome on sales item!")]
	public class SuperCampaign
	{
		[DefinitionField("DebitorGroup", "Number", DefaultValue = "1", RenderInEditor = true)]
		[LanguageDescription("da-DK", "Debitor gruppe",  Description = "Gruppe id'et, som kan modtage denne rabat.")]
		public string DebitorGroup { get; set; }
	}
}