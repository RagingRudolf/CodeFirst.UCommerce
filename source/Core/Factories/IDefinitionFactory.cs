using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Factories
{
    public interface IDefinitionFactory
    {
        void Create(Type type);
    }
}