using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	public class DefinitionFieldAttribute : BaseDefinitionFieldAttribute
	{
		public DefinitionFieldAttribute(string name, string dataType) 
			: base(name, dataType)
		{
		}
	
		public string DefaultValue { get; set; }
	}
}