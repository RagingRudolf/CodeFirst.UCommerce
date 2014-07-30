using System.Configuration;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
{
	public class AssemblyElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyElement) element).Name;
		}
	}
}