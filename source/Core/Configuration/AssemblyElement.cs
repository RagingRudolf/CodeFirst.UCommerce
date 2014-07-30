using System.Configuration;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
{
	public class AssemblyElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string)base["name"]; }
			set { base["name"] = value; }
		}

		[ConfigurationProperty("assemblystring", IsRequired = true)]
		public string AssemblyString
		{
			get { return (string) base["assemblystring"]; }
			set { base["assemblystring"] = value; }
		}
	}
}