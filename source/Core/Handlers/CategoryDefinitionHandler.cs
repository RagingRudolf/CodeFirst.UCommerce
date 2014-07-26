using System;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class CategoryDefinitionHandler : IHandler
	{
		public bool CanHandle(Type type)
		{
			return type.IsDefined(typeof (CategoryDefinitionAttribute), false);
		}

		public void Handle(Type type)
		{
			Attribute attribute = type.GetAttribute<CategoryDefinitionAttribute>();
		}
	}
}