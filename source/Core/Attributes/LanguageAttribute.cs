﻿using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class LanguageAttribute : Attribute
	{
		public LanguageAttribute(string language, string displayName)
		{
			Language = language;
			DisplayName = displayName;
		}
		
		public string Language { get; protected set; }
		public string DisplayName { get; protected set; }
		public string Description { get; set; }
	}
}