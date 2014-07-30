using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
{
	public class ConfigurationProvider : IConfigurationProvider
	{
		private readonly CodeFirstConfiguration _configuration;
		
		public ConfigurationProvider()
		{
			_configuration = ConfigurationManager.GetSection("RagingRudolf/CodeFirst") as CodeFirstConfiguration;
		}

		public IEnumerable<Assembly> GetAssemblies()
		{
			IList<Assembly> loadedAssemblies = new List<Assembly>();
			
			if (_configuration == null)
			{
				var assembly = Assembly.Load("RagingRudolf.CodeFirst.UCommerce.Models");
				loadedAssemblies.Add(assembly);
			}
			else
			{
				foreach (AssemblyElement item in _configuration.Assemblies)
				{
					var assembly = Assembly.Load(item.AssemblyString);
					loadedAssemblies.Add(assembly);
				}
			}

			return loadedAssemblies;
		}
	}
}