using System;
using System.Collections.Generic;
using System.Linq;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;
using RagingRudolf.UCommerce.CodeFirst.Core.Configuration;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using RagingRudolf.UCommerce.CodeFirst.Core.Factories;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Bootstrapping
{
    public class AssemblyByConfigurationBootstrap : ICanBootstrap
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly ISessionProvider _sessionProvider;

        public AssemblyByConfigurationBootstrap(IConfigurationProvider configurationProvider, ISessionProvider sessionProvider)
        {
            if (configurationProvider == null) throw new ArgumentNullException(nameof(configurationProvider));
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _configurationProvider = configurationProvider;
            _sessionProvider = sessionProvider;
        }

        public AssemblyByConfigurationBootstrap()
            : this(new ConfigurationProvider(), ObjectFactory.Instance.Resolve<ISessionProvider>())
        {
        }

        public void Initialize()
        {
            if (!_configurationProvider.Synchronize)
                return;

            var assembly = _configurationProvider.GetAssembly();
            IEnumerable<Type> codeFirstTypes = assembly.GetTypes()
                .EmptyIfNull()
                .WithAttribute<CodeFirstAttribute>()
                .ToList();

            using (var factory = new DefinitionCreatorFactory(_sessionProvider))
            {
                foreach (var type in codeFirstTypes)
                    factory.Create(type);
            }
        }
    }
}