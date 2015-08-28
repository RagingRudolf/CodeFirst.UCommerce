using System.Reflection;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Configuration
{
	public interface IConfigurationProvider
	{
		Assembly GetAssembly();
		bool Synchronize { get; }
	}
}