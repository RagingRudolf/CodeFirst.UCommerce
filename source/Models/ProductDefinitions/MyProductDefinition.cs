using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.Examples.Models.ProductDefinitions
{
	[ProductDefinition("My Product Definition", Description = "My own product description")]
	public class MyProductDefinition
	{
		[ProductDefinitionField("Stock", "Number",
			DisplayOnSite = true,
			IsVariantProperty = true)]
		public int Stock { get; set; }
	}
}