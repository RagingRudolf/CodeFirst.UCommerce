using System;
using System.Collections.Generic;
using System.Linq;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;
using RagingRudolf.UCommerce.CodeFirst.Core.Configuration;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using RagingRudolf.UCommerce.CodeFirst.Core.Factories;

namespace RagingRudolf.UCommerce.CodeFirst.Core
{
	public static class CodeFirstBootstrapper
	{
		public static void Initialize()
		{
			IConfigurationProvider configurationProvider = new ConfigurationProvider();

			if (!configurationProvider.Synchronize)
				return;

			var assembly = configurationProvider.GetAssembly();
			IEnumerable<Type> types = assembly.GetTypes()
                .EmptyIfNull()
                .WithAttribute<CodeFirstAttribute>()
                .ToList();

            using (var factory = new DefinitionCreatorFactory())
		    {
		        foreach(var type in types)
                    factory.Create(type);
		    }
		}
	}
}