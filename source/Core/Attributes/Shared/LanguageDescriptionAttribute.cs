using System;
using UCommerce.EntitiesV2;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
	public class LanguageDescriptionAttribute : Attribute
	{
		public LanguageDescriptionAttribute(string language, string displayName)
		{
			Language = language;
			DisplayName = displayName;
		}
		
		public string Language { get; protected set; }
		public string DisplayName { get; protected set; }

		public string Description { get; set; }

		public DataTypeEnumDescription AsEnumDescription(DataTypeEnum dataTypeEnum)
		{
			return new DataTypeEnumDescription
			{
				CultureCode = Language,
				DataTypeEnum = dataTypeEnum,
				DisplayName = DisplayName,
				Description = Description,
			};
		}
	}
}