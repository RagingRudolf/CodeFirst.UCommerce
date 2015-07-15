namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	public class ProductDefinitionFieldAttribute : BaseDefinitionFieldAttribute
	{
		public ProductDefinitionFieldAttribute(string name, string dataType) 
			: base(name, dataType)
		{
		}

		public bool Facet { get; set; }
		public bool IsVariantProperty { get; set; }
	}
}