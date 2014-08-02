using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using RagingRudolf.CodeFirst.UCommerce.Core.Configuration;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;
using RagingRudolf.CodeFirst.UCommerce.Core.Handlers;
using RagingRudolf.CodeFirst.UCommerce.Core.Sorting;

using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

using Umbraco.Core;

namespace RagingRudolf.CodeFirst.UCommerce.Cms.Umbraco
{
	public class ApplicationBootstrapper : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			IConfigurationProvider configurationProvider = new ConfigurationProvider();

			if (!configurationProvider.Synchronize)
				return;

			Assembly assembly = configurationProvider.GetAssembly();
			IList<DependencyField<Type>> types = assembly
				.GetUCommerceDefinitions()
				.Select(t => new DependencyField<Type>(t.Name, t.BaseType.AsDependency(), t))
				.ToList();
			IEnumerable<Type> sorted = TopologicalSort.Sort(types);

			var sessionProvider = ObjectFactory.Instance.Resolve<ISessionProvider>();

			IHandler[] handlers =
			{
				new CategoryDefinitionHandler(sessionProvider), 
			};

			foreach (Type type in sorted)
			{
				IHandler handler = handlers.FirstOrDefault(x => x.CanHandle(type));

				if (handler == null)
					throw new InvalidOperationException(
						string.Format("Can not find a handler for type '{0}'", type));

				handler.Handle(type);
			}
		}
	}
}