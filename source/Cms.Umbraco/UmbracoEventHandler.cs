using RagingRudolf.UCommerce.CodeFirst.Core;
using Umbraco.Core;

namespace RagingRudolf.UCommerce.CodeFirst.Cms.Umbraco
{
	public class UmbracoEventHandler : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			CodeFirstBootstrapper.Initialize();
		}
	}
}