using System;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes.DataType
{
	[AttributeUsage(AttributeTargets.Class)]
	public class DataTypeAttribute : CodeFirstAttribute
	{
		public DataTypeAttribute(string name, string definitionName)
			: base(name)
		{
			DefinitionName = definitionName;
		}

		public DataTypeAttribute(string name, BuiltInDataType definitionName)
			: this(name, definitionName.ToString())
		{
		}

		public bool Nullable { get; set; }
		public string DefinitionName { get; private set; }
		public string ValidationExpression { get; set; }
	}
}