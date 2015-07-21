using System.Configuration;
using System.IO;
using System.Reflection;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Configuration
{
	public class ConfigurationProvider : IConfigurationProvider
	{
		private readonly CodeFirstConfigurationSection _configurationSection;
		
		public ConfigurationProvider()
		{
			_configurationSection = ConfigurationManager.GetSection("RagingRudolf/CodeFirst") as CodeFirstConfigurationSection;
		}

		public Assembly GetAssembly()
		{
			if (_configurationSection == null)
				throw new ConfigurationErrorsException(
					"Misconfiguration! Could not find ConfigurationSection 'RagingRudolf/CodeFirst' " +
					"in web.config or app.config. Make sure it has been added and configured.");
			
			if (_configurationSection.AssemblyName.IsEmpty())
				throw new ConfigurationErrorsException(
					"Misconfiguration! assemblyname attribute for ConfigurationSection 'RagingRudolf/CodeFirst' " +
					"is not allowed to be null or empty. Make sure to define a valid assembly name.");

			Assembly assembly = null;

			try
			{
				assembly = Assembly.Load(_configurationSection.AssemblyName);
			} catch(FileNotFoundException ex) {
				throw new ConfigurationErrorsException(
					string.Format("Misconfiguration! Configured assembly with name '{0}' could not be found.", _configurationSection.AssemblyName), ex);
			}

			return assembly;
		}

		public bool Synchronize
		{
			get { return _configurationSection != null && _configurationSection.Synchronize; }
		}
	}
}