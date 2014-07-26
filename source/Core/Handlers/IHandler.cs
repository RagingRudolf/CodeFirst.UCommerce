using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public interface IHandler
	{
		bool CanHandle(Type type);
		void Handle(Type type);
	}
}