using RagingRudolf.UCommerce.CodeFirst.Core;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Product;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.ProductDefinitions
{
    [Definition(BuiltInDefinitionType.Product, "My Product Definition", "My own product description")]
	public class MyProductDefinition
	{
        [ProductField("Stock", "Number", DisplayOnSite = true, IsVariantProperty = true)]
		[Language("da-DK", "Lager", Description = "Antal enheder på lager.")]
		[Language("en-US", "Stock", Description = "Number of items in stock.")]
		public int Stock { get; set; }
	}
}