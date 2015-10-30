using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Factories
{
    public interface IDefinitionCreatorFactory : IDisposable
    {
        void Create(Type type);
    }
}