using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CategoryDefinitionAttribute : BaseDefinitionAttribute
	{
		public CategoryDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}