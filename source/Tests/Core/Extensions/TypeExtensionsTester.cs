using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;

namespace RagingRudolf.UCommerce.CodeFirst.Tests.Core.Extensions
{
    [TestFixture]
    public class TypeExtensionsTester
    {
        [Test]
        public void WithAttribute_EnumerableIsNull_DoesNotThrowException()
        {
            List<Type> types = null;
            Assert.That(() => types.WithAttribute<CustomTestAttribute>(), Throws.Nothing);
        }

        [Test]
        public void WithAttribute_EmptyList_ReturnsEmptyList()
        {
            var list = new List<Type>().WithAttribute<CustomTestAttribute>();
            Assert.That(() => list.ToList(), Has.Count.EqualTo(0));
        }

        [Test]
        public void WithAttribute_TypesWithoutAttribute_ReturnsEmptyList()
        {
            var list = new List<Type>
            {
                typeof(PublicClass),
                typeof(PublicAbstractClass),
                typeof(PublicClass.PublicNestedClass),
                typeof(PublicClass.InternalNestedClass),
                typeof(InternalClass)
            };
            var subject = list.WithAttribute<CustomTestAttribute>();

            Assert.That(() => subject.ToList(), Has.Count.EqualTo(0));
        }

        [Test]
        public void WithAttribute_TypesWithAttribute_ReturnsPublicTypes()
        {
            var list = new List<Type>
            {
                typeof(PublicClass),
                typeof(PublicAbstractClass),
                typeof(PublicClass.PublicNestedClass),
                typeof(PublicClass.InternalNestedClass),
                typeof(InternalClass),
                typeof(DefinedInternalClass),
                typeof(DefinedPublicAbstractClass),
                typeof(DefinedPublicClass),
                typeof(DefinedPublicClass.DefinedInternalNestedClass),
                typeof(DefinedPublicClass.DefinedPublicNestedClass)
            };
            var subject = list.WithAttribute<CustomTestAttribute>();

            Assert.That(() => subject.ToList(), Has.Count.EqualTo(3));
        }

        [TestCase(typeof(PublicAbstractClass), false)]
        [TestCase(typeof(PublicClass), true)]
        [TestCase(typeof(PublicClass.PublicNestedClass), true)]
        [TestCase(typeof(PublicClass.InternalNestedClass), true)]
        public void IsPublicClass_Type_IsPublic(Type type, bool expectation)
        {
            Assert.That(() => type.IsPublicClass(), Is.EqualTo(expectation));
        }
    }

    #region Helpers

    public class CustomTestAttribute : Attribute { }

    public abstract class PublicAbstractClass {  }

    public class PublicClass
    {
        public class PublicNestedClass {  }

        internal class InternalNestedClass {  }
    }

    internal class InternalClass {  }

    [CustomTest]
    public abstract class DefinedPublicAbstractClass {  }

    [CustomTest]
    public class DefinedPublicClass
    {
        [CustomTest]
        public class DefinedPublicNestedClass {  }

        [CustomTest]
        internal class DefinedInternalNestedClass {  }
    }

    [CustomTest]
    internal class DefinedInternalClass {  }

    #endregion
}