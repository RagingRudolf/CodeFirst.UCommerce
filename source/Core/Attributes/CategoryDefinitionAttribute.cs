﻿using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
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