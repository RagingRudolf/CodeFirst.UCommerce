using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DefinitionAttribute : CodeFirstAttribute
    {
        public DefinitionAttribute(BuiltInDefinitionType definitionType, string name) 
            : base(name)
        {
            DefinitionType = definitionType;
        }

        public DefinitionAttribute(BuiltInDefinitionType definitionType, string name, string description)
            : this(definitionType, name)
        {
            Description = description;
        }

        public string Description { get; private set; }
        public BuiltInDefinitionType DefinitionType { get; private set; }
    }
}