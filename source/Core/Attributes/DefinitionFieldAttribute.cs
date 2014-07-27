using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	public class DefinitionFieldAttribute : UCommerceAttribute
	{
		public DefinitionFieldAttribute(string name, string dataType) 
			: base(name)
		{
			DataType = dataType;
		}

		public string DataType { get; protected set; }

		public bool DisplayOnSite { get; set; }
		public bool	Multilingual { get; set; }
		public bool RenderInEditor { get; set; }
		public bool Searchable { get; set; }
		public int SortOrder { get; set; }
		public string DefaultValue { get; set; }
	}
}