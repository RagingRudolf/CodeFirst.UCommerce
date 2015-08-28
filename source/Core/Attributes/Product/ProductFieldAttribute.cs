using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Product
{
	public class ProductFieldAttribute : BaseDefinitionFieldAttribute
	{
		public ProductFieldAttribute(string name, string dataType) 
			: base(name, dataType)
		{
		}

		public bool Facet { get; set; }
		public bool IsVariantProperty { get; set; }
	}
}