using RagingRudolf.CodeFirst.UCommerce.Core;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.DataType;

namespace RagingRudolf.Examples.Models.DataTypes
{
	[DataType("BookType", BuiltInDataTypes.Enum)]
	public class MyEnumDataType
	{
		[EnumValue("Ebook")]
		public string Ebook { get; set; }

		[EnumValue("Papirback")]
		public string Papirback { get; set; }

		[EnumValue("Hardback")]
		public string Hardback { get; set; }

		[EnumValue("Voicebook")]
		[LanguageDescription("da-DK", "Lydbog", Description = "Dansk beskrivelse")]
		[LanguageDescription("en-US", "Voice book", Description = "English description")]
		public string Voicebook { get; set; }
	}
}