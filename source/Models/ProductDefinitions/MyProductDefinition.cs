using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.Examples.Models.ProductDefinitions
{
	[ProductDefinition("My Product Definition", Description = "My own product description")]
	public class MyProductDefinition
	{
		[ProductDefinitionField("Stock", "Number", DisplayOnSite = true, IsVariantProperty = true)]
		[LanguageDescription("da-DK", "Lager", Description = "Antal enheder på lager.")]
		[LanguageDescription("en-US", "Stock", Description = "Number of items in stock.")]
		public int Stock { get; set; }
	}
}