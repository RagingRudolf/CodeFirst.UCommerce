using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.Examples.Models.CategoryDefinitions
{
	[CategoryDefinition("Default Category 1", Description = "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[CategoryDefinitionField("IsPrimaryCategoryAlt", "Number",
			Multilingual = true,
			DisplayOnSite = true,
			RenderInEditor = true
		)]
		public bool IsPrimaryCategory { get; set; }
	}
}