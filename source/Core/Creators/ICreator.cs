using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Creators
{
    public interface ICreator
    {
        void CreateOrUpdate(Type type);
    }
}