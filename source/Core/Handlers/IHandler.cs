using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Handlers
{
	public interface IHandler
	{
		bool CanHandle(Type type);
		void Handle(Type type);
	}
}