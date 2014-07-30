using System.Configuration;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
{
	public class CodeFirstConfiguration : ConfigurationSection
	{
		[ConfigurationProperty("assemblies")]
		public AssemblyElementCollection Assemblies
		{
			get { return (AssemblyElementCollection) this["assemblies"]; }
			set { this["assemblies"] = value; }
		}
	}
}