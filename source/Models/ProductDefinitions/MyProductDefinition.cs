using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.ProductDefinitions
{
    //[ProductDefinition("My Product Definition", Description = "My own product description")]
	public class MyProductDefinition
	{
        //[ProductDefinitionField("Stock", "Number", DisplayOnSite = true, IsVariantProperty = true)]
		[Language("da-DK", "Lager", Description = "Antal enheder på lager.")]
		[Language("en-US", "Stock", Description = "Number of items in stock.")]
		public int Stock { get; set; }
	}
}