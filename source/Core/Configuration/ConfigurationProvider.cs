using System.Configuration;
using System.Reflection;

using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
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
			string assemblyName = _configuration != null && _configuration.AssemblyName.IsNotEmpty()
				? _configuration.AssemblyName
				: "RagingRudolf.CodeFirst.UCommerce.Models";
			var assembly = Assembly.Load(assemblyName);

			return assembly;
		}

		public bool Synchronize
		{
			get { return _configuration != null && _configuration.Synchronize; }
		}
	}
}