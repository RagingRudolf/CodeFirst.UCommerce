using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.CodeFirst.UCommerce.Models.CategoryDefinitions
{
	[CategoryDefinition("Default Category 1", Description = "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[DefinitionField("IsPrimaryCategoryAlt", "Number",
			Multilingual = true,
			DisplayOnSite = true,
			RenderInEditor = true
		)]
		public bool IsPrimaryCategory { get; set; }
	}
}