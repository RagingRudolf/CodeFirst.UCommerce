using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;
using RagingRudolf.CodeFirst.UCommerce.Core.Sorting;
using Umbraco.Core;

namespace RagingRudolf.CodeFirst.UCommerce.Cms.Umbraco
{
	public class ApplicationBootstrapper : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			// Load assembly
			// Refactor into using configuration section or take a default named assembly.
			var assemblyModel = Assembly.Load("RagingRudolf.CodeFirst.UCommerce.Models");
			var types = assemblyModel
				.GetUCommerceDefinitions()
				.Select(t => new DependencyField<Type>(t.Name, t.BaseType.AsDependency(), t))
				.ToList();
			IEnumerable<Type> sorted = TopologicalSort.Sort(types);
		}
	}
}