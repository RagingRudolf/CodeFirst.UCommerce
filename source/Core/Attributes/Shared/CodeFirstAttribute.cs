using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared
{
	public abstract class CodeFirstAttribute : Attribute
	{
		protected CodeFirstAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; protected set; }
	}
}