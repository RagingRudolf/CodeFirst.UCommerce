﻿using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes.DataType
{
	public class EnumValueAttribute : CodeFirstAttribute
	{
		public EnumValueAttribute(string value)
			: base(value)
		{
		}
	}
}