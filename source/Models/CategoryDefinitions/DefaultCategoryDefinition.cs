using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.Examples.Models.CategoryDefinitions
{
	[CategoryDefinition("Default Category 1", Description = "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[DefinitionField("IsPrimaryCategoryAlt", "Number", Multilingual = true, DisplayOnSite = true, RenderInEditor = true)]
		[LanguageDescription("en-US", "Primary category", Description = "This is primary category for a product.")]
		[LanguageDescription("da-DK", "Primær kategori", Description = "Dette er den primære kategori for et produkt.")]
		public bool IsPrimaryCategory { get; set; }
	}
}