using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
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
			var provider = ObjectFactory.Instance.Resolve<ISessionProvider>();
			
			IHandler[] handlers =
			{
				new CategoryDefinitionHandler(provider), 
			};

			// Load assembly
			// Refactor into using configuration section or take a default named assembly.
			var assemblyModel = Assembly.Load("RagingRudolf.CodeFirst.UCommerce.Models");
			var types = assemblyModel
				.GetUCommerceDefinitions()
				.Select(t => new DependencyField<Type>(t.Name, t.BaseType.AsDependency(), t))
				.ToList();
			IEnumerable<Type> sorted = TopologicalSort.Sort(types);

			foreach (Type type in sorted)
			{
				var handler = handlers.FirstOrDefault(x => x.CanHandle(type));

				if (handler == null)
					throw new InvalidOperationException();

				handler.Handle(type);
			}
		}
	}
}