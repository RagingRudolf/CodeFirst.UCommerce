using System;
using RagingRudolf.CodeFirst.UCommerce.Core;

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

		public DataTypeAttribute(string name, BuiltInDataTypes definitionName)
			: this(name, definitionName.ToString())
		{
		}

		public bool Nullable { get; set; }
		public string DefinitionName { get; private set; }
		public string ValidationExpression { get; set; }
	}
}