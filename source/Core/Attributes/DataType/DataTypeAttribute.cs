using System;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes.DataType
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataTypeAttribute : DefinitionAttribute
    {
        public DataTypeAttribute(string name, string definitionName) 
            : base(BuiltInDefinitionType.DataType, name, string.Empty)
        {
            DefinitionName = definitionName;
        }

        public string DefinitionName { get; private set; }
        public bool Nullable { get; set; }
        public string ValidationExpression { get; set; }
    }
}