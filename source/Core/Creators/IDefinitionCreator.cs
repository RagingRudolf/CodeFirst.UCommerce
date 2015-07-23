using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Creators
{
    public interface IDefinitionCreator
    {
        void CreateOrUpdate(Type type);
    }
}