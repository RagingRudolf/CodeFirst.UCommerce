using System.Configuration;
using System.IO;
using System.Reflection;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Configuration
{
	public class ConfigurationProvider : IConfigurationProvider
	{
		private readonly CodeFirstConfiguration _configuration;
		
		public ConfigurationProvider()
		{
			_configuration = ConfigurationManager.GetSection("RagingRudolf/CodeFirst") as CodeFirstConfiguration;
		}

		public Assembly GetAssembly()
		{
			if (_configuration == null)
				throw new ConfigurationErrorsException(
					"Misconfiguration! Could not find ConfigurationSection 'RagingRudolf/CodeFirst' " +
					"in web.config or app.config. Make sure it has been added and configured.");
			
			if (_configuration.AssemblyName.IsEmpty())
				throw new ConfigurationErrorsException(
					"Misconfiguration! assemblyname attribute for ConfigurationSection 'RagingRudolf/CodeFirst' " +
					"is not allowed to be null or empty. Make sure to define a valid assembly name.");

			Assembly assembly = null;

			try
			{
				assembly = Assembly.Load(_configuration.AssemblyName);
			} catch(FileNotFoundException ex) {
				throw new ConfigurationErrorsException(
					string.Format("Misconfiguration! Configured assembly with name '{0}' could not be found.", _configuration.AssemblyName), ex);
			}

			return assembly;
		}

		public bool Synchronize
		{
			get { return _configuration != null && _configuration.Synchronize; }
		}
	}
}