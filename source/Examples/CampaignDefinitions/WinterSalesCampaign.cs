using RagingRudolf.UCommerce.CodeFirst.Core;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.CampaignDefinitions
{
    [Definition(BuiltInDefinitionType.CampaignItem, "Winter sales", "Winter is coming up with some cool prices.")]
    public class WinterSalesCampaign
    {
        [Field("PremiumMembersOnly", Constants.BuiltInDataTypes.Boolean, RenderInEditor = true)]
        public bool PremiumMembersOnly { get; set; }
    }
}