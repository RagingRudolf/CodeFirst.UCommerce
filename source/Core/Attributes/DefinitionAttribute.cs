using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DefinitionAttribute : CodeFirstAttribute
    {
        public DefinitionAttribute(DefinitionType definitionType, string name) 
            : base(name)
        {
            DefinitionType = definitionType;
        }

        public DefinitionAttribute(DefinitionType definitionType, string name, string description)
            : this(definitionType, name)
        {
            Description = description;
        }

        public string Description { get; private set; }
        public DefinitionType DefinitionType { get; private set; }
    }
}