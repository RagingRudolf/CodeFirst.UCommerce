using RagingRudolf.UCommerce.CodeFirst.Core;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.CategoryDefinitions
{
    [Definition(BuiltInDefinitionType.Category, "Default Category 1", "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[Field("IsPrimaryCategoryAlt", "Number", Multilingual = true, DisplayOnSite = true, RenderInEditor = true)]
		[Language("en-US", "Primary category", Description = "This is primary category for a product.")]
		[Language("da-DK", "Primær kategori", Description = "Dette er den primære kategori for et produkt.")]
		public bool IsPrimaryCategory { get; set; }
	}
}