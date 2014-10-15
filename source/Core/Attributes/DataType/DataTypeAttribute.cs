using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes.DataType
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class DataTypeAttribute : UCommerceAttribute
	{
		public DataTypeAttribute(string name, string definitionName)
			: base(name)
		{
			DefinitionName = definitionName;
		}

		public DataTypeAttribute(string name, BuiltInDataTypes definitionName)
			: this(name, definitionName.ToString())
		{
		}

		public string DefinitionName { get; private set; }
		public bool Nullable { get; set; }
	}
}