using System;
using NUnit.Framework;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;

namespace RagingRudolf.UCommerce.CodeFirst.Tests.Core.Extensions
{
    [TestFixture]
    public class TypeExtensionsTester
    {
        [TestCase(typeof(PublicAbstractClass), false)]
        [TestCase(typeof(PublicClass), true)]
        [TestCase(typeof(PublicClass.PublicNestedClass), true)]
        [TestCase(typeof(PublicClass.InternalNestedClass), true)]
        public void IsPublicClass_Type_IsPublic(Type type, bool expectation)
        {
            Assert.That(() => type.IsPublicClass(), Is.EqualTo(expectation));
        }
    }

    #region TestClasses

    public abstract class PublicAbstractClass {  }

    public class PublicClass
    {
        public class PublicNestedClass {  }

        internal class InternalNestedClass {  }
    }

    internal class InternalClass {  }

    #endregion
}