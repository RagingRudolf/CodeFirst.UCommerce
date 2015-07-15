using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class DefinitionFieldAttribute : BaseDefinitionFieldAttribute
	{
		public DefinitionFieldAttribute(string name, string dataType) 
			: base(name, dataType)
		{
		}
	
		public string DefaultValue { get; set; }
	}
}