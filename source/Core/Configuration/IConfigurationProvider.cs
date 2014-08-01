using System.Reflection;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
{
	public interface IConfigurationProvider
	{
		Assembly GetAssembly();
	}
}