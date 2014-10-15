using System;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Shared;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Category
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class CategoryDefinitionAttribute : BaseDefinitionAttribute
	{
		public CategoryDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}