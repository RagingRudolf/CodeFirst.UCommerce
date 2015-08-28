using RagingRudolf.UCommerce.CodeFirst.Core;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.DataType;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;

namespace RagingRudolf.UCommerce.CodeFirst.Examples.DataTypes
{
    [DataType("BookType", Constants.BuiltInDataTypes.Enum)]
	public class MyEnumDataType
	{
        [EnumValue("Ebook")]
		public string Ebook { get; set; }

        [EnumValue("Papirback")]
		public string Papirback { get; set; }

        [EnumValue("Hardback")]
		public string Hardback { get; set; }

        [EnumValue("Voicebook")]
		[Language("da-DK", "Lydbog", Description = "Dansk beskrivelse")]
		[Language("en-US", "Voice book", Description = "English description")]
		public string Voicebook { get; set; }
	}
}