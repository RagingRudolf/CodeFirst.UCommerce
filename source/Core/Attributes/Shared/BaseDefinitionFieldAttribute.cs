namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared
{
	public abstract class BaseDefinitionFieldAttribute : CodeFirstAttribute
	{
		protected BaseDefinitionFieldAttribute(string name, string dataType) 
			: base(name)
		{
			DataType = dataType;
		}

		public string DataType { get; protected set; }
		public bool DisplayOnSite { get; set; }
		public bool Multilingual { get; set; }
		public bool RenderInEditor { get; set; }
		public bool Searchable { get; set; }
		public int SortOrder { get; set; }
	}
}