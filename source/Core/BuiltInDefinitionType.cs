using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core
{
    public enum BuiltInDefinitionType
    {
        Category,
        Product,
        CampaignItem,
        PaymentMethod,
        [Obsolete("Use DataTypeAttribute instead of DefinitionAttribute.")]
        DataType
    }
}