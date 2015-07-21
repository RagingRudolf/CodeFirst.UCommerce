using System;
using RagingRudolf.UCommerce.CodeFirst.Core;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.CategoryDefinitions
{
    [Definition(BuiltInDefinitionType.Category, "Campaign", "Categories for campaigns.")]
    public class CampaignCategoryDefinition
    {
        [Field("CampaignName", Constants.BuiltInDataTypes.ShortText, DisplayOnSite = true, RenderInEditor = true, Multilingual = true)]
        [Language("da-DK", "Kampagnenavn", Description = "Vil blive brugt som overskrift på kampagnesiden.")]
        [Language("en-GB", "Campaign name", Description = "Is used as header on the campaign page.")]
        public string CampaignName { get; set; }

        [Field("ShortDescription", Constants.BuiltInDataTypes.ShortText, DisplayOnSite = true, RenderInEditor = true, Multilingual = true)]
        [Language("da-DK", "Kort beskrivelse", Description = "Vil blive brugt som appetitvækker på sociale medier.")]
        [Language("en-GB", "Short description", Description = "Will be used for teasers on social media sites.")]
        public string ShortDescription { get; set; }

        [Field("Description", Constants.BuiltInDataTypes.LongText, DisplayOnSite = true, RenderInEditor = true, Multilingual = true)]
        [Language("da-DK", "Kampagne beskrivelse", Description = "Kampagne beskrivelsen, der bliver vist på kampagne siden.")]
        [Language("en-GB", "Campaign description", Description = "Campaign description which will shown on campaign page below the header.")]
        public string Description { get; set; }

        [Field("StartDateTime", Constants.BuiltInDataTypes.DateTime, RenderInEditor = true)]
        public DateTime StartDateTime { get; set; }

        [Field("EndDateTime", Constants.BuiltInDataTypes.DateTime, RenderInEditor = true)]
        public DateTime EndDateTime { get; set; }
    }
}